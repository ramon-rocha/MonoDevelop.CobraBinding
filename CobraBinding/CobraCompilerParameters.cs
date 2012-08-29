using System;
using MonoDevelop.Projects;
using MonoDevelop.Core.Serialization;

namespace MonoDevelop.CobraBinding
{
	/*
	 * This class is to be the backend to the controls displayed for the
	 * Project Options window under the Build > Compiler section.
	 */
	public class CobraCompilerParameters : MonoDevelop.Projects.ConfigurationParameters
	{
		public CobraCompilerParameters() : base()
		{
		}

		// #define symbols to be passed to the C# compiler.
		// Cobra doesn't use these but they are abstract in the base class.
		public override void AddDefineSymbol(string symbol)
		{
		}

		public override void RemoveDefineSymbol(string symbol)
		{
		}

		// TODO: non-boolean option values should come from local
		// vars/constants instead of hardcoded literals

		// Back-end Native Compiler Options
		[ItemProperty("BackEnd", DefaultValue = "none")]
		string _backEndCompiler = "none";

		[ItemProperty("NativeCompiler", DefaultValue = "auto")]
		string _nativeCompiler = "auto";
		
		[ItemProperty("NativeCompilerArgs", DefaultValue = "")]
		string _nativeCompilerArgs = "";


		// Performance and Quality Options
		[ItemProperty("Turbo", DefaultValue = false)]
		bool _turbo = false;

		[ItemProperty("Contracts", DefaultValue = "inline")]
		string _contracts = "inline";

		[ItemProperty("IncludeAsserts", DefaultValue = true)]
		bool _includeAsserts = true;

		[ItemProperty("IncludeNilChecks", DefaultValue = true)]
		bool _includeNilChecks = true;

		[ItemProperty("IncludeTests", DefaultValue = true)]
		bool _includeTests = true;

		[ItemProperty("IncludeTraces", DefaultValue = true)]
		bool _includeTraces = true;

		[ItemProperty("Optimize", DefaultValue = true)]
		bool _optimize = true;


		//Development and Deployment Options
		[ItemProperty("EmbedRunTime", DefaultValue = false)]
		bool _embedRunTime = false;

		[ItemProperty("Number", DefaultValue = "decimal")]
		string _number = "decimal";

		[ItemProperty("Debug", DefaultValue = "full")]
		string _debug = "full";

		[ItemProperty("DebuggingTips", DefaultValue = true)]
		bool _debuggingTips = true;

		[ItemProperty("KeepIntermediateFiles", DefaultValue = false)]
		bool _keepIntermediateFiles = false;

		[ItemProperty("TimeIt", DefaultValue = false)]
		bool _timeIt = false;

		[ItemProperty("Verbosity", DefaultValue = 0)]
		int _verbosity = 0;

		[ItemProperty("VerbosityRef", DefaultValue = 0)]
		int _verbosityRef = 0;

		// Which back-end to use
		public string BackEnd {
				get { return _backEndCompiler; }
				set { _backEndCompiler = value; }
		}

		// The path to the back-end native compiler
		public string NativeCompiler {
				get { return _nativeCompiler; }
				set { _nativeCompiler = value; }
		}

		// Additional arguments to pass to the native back-end compiler
		public string NativeCompilerArgs {
				get { return _nativeCompilerArgs; }
				set { _nativeCompilerArgs = value; }
		}

		// Maximize run-time performance (override -contracts param and others)
		public bool Turbo {
				get { return _turbo; }
				set { _turbo = value; }
		}

		// Control treatment of code generation for contracts
		public string Contracts {
				get { return _contracts; }
				set { _contracts = value; }
		}

		// Include assert statements in program
		public bool IncludeAsserts {
				get { return _includeAsserts; }
				set { _includeAsserts = value; }
		}

		// Include checks on non-nilable vars, method args, and type casts
		public bool IncludeNilChecks {
				get { return _includeNilChecks; }
				set { _includeNilChecks = value; }
		}

		// Include unit tests in the output assembly
		public bool IncludeTests {
				get { return _includeTests; }
				set { _includeTests = value; }
		}

		// Include trace statements in the output assembly
		public bool IncludeTraces {
				get { return _includeTraces; }
				set { _includeTraces = value; }
		}

		// Enable optimizations
		public bool Optimize {
				get { return _optimize; }
				set { _optimize = value; }
		}

		// Embed the Cobra run-time support code in the assembly
		public bool EmbedRunTime {
				get { return _embedRunTime; }
				set { _embedRunTime = value; }
		}

		// Set the real numeric type for 'number' types and factional literals
		public string Number {
				get { return _number; }
				set { _number = value; }
		}

		// Turn on system debugging information
		public string Debug {
				get { return _debug; }
				set { _debug = value; }
		}

		// Display debugging tips when an unhandled exception occurs
		public bool DebuggingTips {
				get { return _debuggingTips; }
				set { _debuggingTips = value; }
		}

		// Keep any generated intermediate files (such as *.cobra.cs files)
		public bool KeepIntermediateFiles {
				get { return _keepIntermediateFiles; }
				set { _keepIntermediateFiles = value; }
		}

		// Gives the total duration of running cobra and the target program
		public bool TimeIt {
				get { return _timeIt; }
				set { _timeIt = value; }
		}

		// Enable extra output from Cobra
		public int Verbosity {
				get { return _verbosity; }
				set { _verbosity = value; }
		}

		// Enable extra output regarding resolution of references to libraries
		public int VerbosityRef {
				get { return _verbosityRef; }
				set { _verbosityRef = value; }
		}
	}
}
