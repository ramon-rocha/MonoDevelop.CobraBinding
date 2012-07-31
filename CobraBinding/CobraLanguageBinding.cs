using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.CodeDom.Compiler;

using MonoDevelop.Core;
using MonoDevelop.Core.Assemblies;
using MonoDevelop.Projects;
using MonoDevelop.Projects.CodeGeneration;
using MonoDevelop.Projects.Dom.Parser;

namespace MonoDevelop.Cobra
{
	public class CobraLanguageBinding : IDotNetLanguageBinding
	{
		private bool _isWindows;
		
		public CobraLanguageBinding() {
			_isWindows = Environment.OSVersion.ToString().StartsWith("Microsoft Windows");
		}
		
		public static ClrVersion[] SupportedClrVersions = { ClrVersion.Net_2_0, ClrVersion.Net_4_0 };
		public static string LanguageName = "Cobra";
		public static string SourceFileExtension = ".cobra";

		public ClrVersion[] GetSupportedClrVersions() {
			return CobraLanguageBinding.SupportedClrVersions;
		}

		public string ProjectStockIcon {
			get { return "md-project"; }
		}

		public string BlockCommentStartTag {
			get { return "/#"; }
		}

		public string BlockCommentEndTag {
			get { return "#/"; }
		}

		public FilePath GetFileName(FilePath fileNameWithoutExtension) {
			return fileNameWithoutExtension.ChangeExtension(CobraLanguageBinding.SourceFileExtension);
		}
		
		public string GetFileName(string fileNameWithoutExtension) {
			return this.GetFileName(new FilePath(fileNameWithoutExtension));
		}

		public bool IsSourceCodeFile(FilePath fileName) {
			return fileName.Extension == CobraLanguageBinding.SourceFileExtension;
		}
		
		public bool IsSourceCodeFile(string fileName) {
			return this.IsSourceCodeFile(new FilePath(fileName));
		}

		public string Language {
			get { return CobraLanguageBinding.LanguageName; }
		}

		public string SingleLineCommentTag {
			get { return "#"; }
		}

		public BuildResult Compile(ProjectItemCollection items,
		                           DotNetProjectConfiguration configuration,
		                           ConfigurationSelector configSelector,
		                           IProgressMonitor monitor)
		{
			StringBuilder cmdArgsBuilder = new StringBuilder("-compile");
			
			//TODO: make this conditional
			cmdArgsBuilder.Append(" -debug:full");


			//references (add each one only once)
			var refs = new HashSet<string>();
			var pkgs = new HashSet<string>();

			foreach (ProjectReference projRef in items.GetAll<ProjectReference>()) {

				switch (projRef.ReferenceType) {
				case ReferenceType.Package:
					if (pkgs.Add(projRef.Package.Name)) {
						cmdArgsBuilder.Append(" -pkg:\"");
						cmdArgsBuilder.Append(projRef.Package.Name);
						cmdArgsBuilder.Append("\"");
					}
					break;

				case ReferenceType.Assembly:
					foreach (string fileName in projRef.GetReferencedFileNames(configSelector)) {

						if (refs.Add(fileName)) {
							cmdArgsBuilder.Append(" -reference:\"");
							cmdArgsBuilder.Append(fileName);
							cmdArgsBuilder.Append("\"");
						}
					}
					break;

				//TODO: Project Reference

				default:
					monitor.ReportError("Unhandled Reference Type: " + projRef.ReferenceType.ToString(), new NotImplementedException());
					break;
				}
			}


			//what's the target assembly?
			switch (configuration.CompileTarget)
			{
				case CompileTarget.Exe:
					cmdArgsBuilder.Append(" -target:exe");
					break;

				case CompileTarget.Library:
					cmdArgsBuilder.Append(" -target:lib");
					break;

				case CompileTarget.WinExe:
					cmdArgsBuilder.Append(" -target:winexe");
					break;

				case CompileTarget.Module:
					cmdArgsBuilder.Append(" -target:module");
					break;
			}


			//what will we call the assembly?
			FilePath asmName = configuration.CompiledOutputName;
			cmdArgsBuilder.Append(" -out:\"");
			cmdArgsBuilder.Append(asmName.FullPath);
			cmdArgsBuilder.Append("\"");


			//which files should we compile?
			foreach (ProjectFile projFile in items.GetAll<ProjectFile>())
			{
				switch (projFile.BuildAction) {
					case BuildAction.Compile:
						cmdArgsBuilder.Append(" \"");
						cmdArgsBuilder.Append(projFile.Name);
						cmdArgsBuilder.Append("\"");
						break;
				}
			}


			//create and setup the process which will execute the cobra compiler
			System.Diagnostics.Process proc = new System.Diagnostics.Process();

			//if the user has specified the location of the compiler, use that
			string cobraExe = Environment.GetEnvironmentVariable("COBRA_EXE");

			if (cobraExe == null || cobraExe.Length == 0) {
				//the user has not specified the location of the compiler

				if (_isWindows) {
					/*
					 * On Windows, we cannot rely on the cobra batch file being in the
					 * path because we are not setting UseShellExecute to true.
					 * This is because we need to redirect the standard output which
					 * we couldn't do if we were executing this via the shell.
					 *
					 * See here for more info: http://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.useshellexecute.aspx
					 */
					cobraExe = "c:\\cobra\\bin\\cobra.bat";
				}
				else {
					/*
					 * On a non-windows based system, we'll rely on cobra being in
					 * the user's path.
					 */
					cobraExe = "cobra";
				}
			}

			proc.StartInfo.FileName = cobraExe;

			//use the project directory as the working directory unless it's not defined
			proc.StartInfo.WorkingDirectory = "./";

			if (configuration.ParentItem != null) {
				if (configuration.ParentItem.BaseDirectory != null) {
					proc.StartInfo.WorkingDirectory = configuration.ParentItem.BaseDirectory.ToString();
				}
			}

			proc.StartInfo.Arguments = cmdArgsBuilder.ToString();
			proc.StartInfo.UseShellExecute = false; //needs to be false so we can redirect the output
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;

			Console.WriteLine(proc.StartInfo.Arguments); //DEBUGGING

			proc.Start();
			proc.WaitForExit();
			
			var result = new BuildResult();
			
			var lines = new List<string>();
			lines.AddRange(proc.StandardError.ReadToEnd().Split('\n'));
			var stdOut = proc.StandardOutput.ReadToEnd();
			lines.AddRange(stdOut.Split('\n'));
			if (lines.Count > 0) {
				// examples:
				//      foo.cobra(4): error: Cannot find "b". There is a member named ".memberwiseClone" with a similar name.
				//      foo.cobra(4): warning: The value of variable "a" is never used.
				//      foo.cobra(4,15): error: Expecting an expression.
				// regex testing:
				//      http://regexhero.net/tester/
				var re = new Regex(@"(?<fileName>[^\(]+)\((?<lineNum>\d+)(,(?<col>\d+))?\):(\s)+(?<msgType>error|warning):(\s)*(?<msg>[^\r]+)", RegexOptions.Compiled);
				foreach (var line in lines) {
					var match = re.Match(line);
					if (match.Success) {
						var groups = match.Groups;
						var fileName = groups["fileName"].ToString();
						int lineNum = int.Parse(groups["lineNum"].ToString());
						int col;
						if (!int.TryParse(groups["col"].ToString(), out col)) {
							col = 1;
						}
						var msgType = groups["msgType"].ToString();
						var msg = groups["msg"].ToString();
						string errNum = "";

						if (msgType == "error") {
							result.AddError(fileName, lineNum, col, errNum, msg);
						}
						else {
							result.AddWarning(fileName, lineNum, col, errNum, msg);
						}
					}
					else if (line.Contains("error:")) {
						result.AddError(line);
					}
					else if (line.Contains("warning:")) {
						result.AddWarning(line);
					}
				}
			}
			
			result.CompilerOutput = stdOut;
			
			return result;
		}

		public ConfigurationParameters CreateCompilationParameters(XmlElement projectOptions) {
			//TODO
			return new CobraCompilerParameters();
		}

		public ProjectParameters CreateProjectParameters(XmlElement projectOptions)  {
			//TODO?
			return new ProjectParameters();
		}

		public CodeDomProvider GetCodeDomProvider() {
			//TODO
			// note that someone started a CodeDom provider awhile back. search the discussion forums and/or wiki
			return null;
		}

		public IParser Parser {
			get {
				//This interface has been removed in MonoDevelop 3.0+
				return null;
			}
		}
		
		public IRefactorer Refactorer {
			get {
				//This interface has been removed in MonoDevelop 3.0+
				return null;
			}
		}
	}
}

