using System;
using MonoDevelop.Projects;
using MonoDevelop.Core.Serialization;

namespace MonoDevelop.Cobra
{
	public class CobraCompilerParameters : ConfigurationParameters
	{
		//TODO: Figure out what to do with this.  Right now it's junk
		//Example: https://github.com/mono/monodevelop/blob/master/main/src/addins/CSharpBinding/MonoDevelop.CSharp.Project/CSharpCompilerParameters.cs				
		public CobraCompilerParameters () : base()
		{
			System.Console.WriteLine("In MonoDevelop.Cobra.CobraCompilerParameters constructor.");
		}

		[ItemProperty("DefineConstants", DefaultValue = "")]
		string definesymbols = String.Empty;

		public override void AddDefineSymbol(string symbol)
		{
			definesymbols += symbol + ";";
		}

		public override void RemoveDefineSymbol(string symbol)
		{
			definesymbols = definesymbols.Replace(symbol + ";", "");
		}
		
	}
}

