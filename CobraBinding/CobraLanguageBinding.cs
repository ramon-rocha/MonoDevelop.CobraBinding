using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using MonoDevelop.Core;
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
		
		// to-do: block comments in Cobra are actually /# ... #/ and """ ... """ or doc strings.
		//        maybe look at how C# does raw strings for an idea

		public string BlockCommentEndTag {
            get { return "\"\"\""; }
        }

        public string BlockCommentStartTag {
            get { return "\"\"\""; }
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

        public BuildResult Compile(ProjectItemCollection items, DotNetProjectConfiguration configuration, ConfigurationSelector configSelector, IProgressMonitor monitor)
        {
            StringBuilder cmdArgsBuilder = new StringBuilder("-compile");
			
			//TODO: make this conditional
			cmdArgsBuilder.Append(" -debug:full");
			
			//TODO: references
                        
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

            //execute the cobra compiler
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
			
			if (_isWindows) {
				//gross, you probably also have to run MonoDevelop as admin
				proc.StartInfo.FileName = "c:\\cobra\\bin\\cobra.bat";
			}
			else {
				proc.StartInfo.FileName = "cobra";
			}
			
            proc.StartInfo.Arguments = cmdArgsBuilder.ToString();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.RedirectStandardError = true;

            proc.Start();
            proc.WaitForExit();
			
			var result = new BuildResult();
			
			var lines = new List<string>();
			lines.AddRange(proc.StandardError.ReadToEnd().Split('\n'));
			var stdOut = proc.StandardOutput.ReadToEnd();
			lines.AddRange(stdOut.Split('\n'));
			if (lines.Count > 0) {
				// examples:
				//     foo.cobra(4): error: Cannot find "b". There is a member named ".memberwiseClone" with a similar name.
				//     foo.cobra(4): warning: The value of variable "a" is never used.
				// regex testing:
				//     http://regexhero.net/tester/
				var re = new Regex(@"(?<fileName>[^\(]+)\((?<lineNum>\d+)\):(\s)+(?<msgType>error|warning):(\s)*(?<msg>[^\r]+)", RegexOptions.Compiled);
				foreach (var line in lines) {
					var match = re.Match(line);
					if (match.Success) {
						var groups = match.Groups;
						var fileName = groups["fileName"].ToString();
						int lineNum = int.Parse(groups["lineNum"].ToString());
						var msg = groups["msg"].ToString();
						int col = 1; string errNum = "";
						result.AddError(fileName, lineNum, col, errNum, msg);
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
				//TODO?
				return null;
			}
		}
		
		public IRefactorer Refactorer {
			get {
				//TODO?
				return null;
			}
		}
    }
}

