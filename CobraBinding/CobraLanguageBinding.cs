using System;
using System.Xml;
using System.Text;
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
			
			BuildResult result = new BuildResult();
			
			if (proc.ExitCode != 0) {
				result.AddError(proc.StandardError.ReadToEnd());
				result.AddError(proc.StandardOutput.ReadToEnd());
			}
			
			result.CompilerOutput = proc.StandardOutput.ReadToEnd();
			
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

