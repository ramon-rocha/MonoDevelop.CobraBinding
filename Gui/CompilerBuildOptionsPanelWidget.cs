using System;
namespace MonoDevelop.CobraBinding.Gui
{
	/*
	 * This class provides the widget seen when navigating to the
	 * Project > Options > Build > Compiler panel.  The logic for
	 * interaction of the various widgets is not handled by this
	 * class.  It's just the GUI.
	 */
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CompilerBuildOptionsPanelWidget : Gtk.Bin
	{
		public CompilerBuildOptionsPanelWidget()
		{
			this.Build();
		}

		#region "Performance and Quality Options"
		public Gtk.RadioButton PerfQualDefaultRadioButton
		{
			get { return this.radioPerfQualDefault; }
		}

		public Gtk.RadioButton PerfQualTurboRadioButton
		{
			get { return this.radioPerfQualTurbo; }
		}

		public Gtk.RadioButton PerfQualCustomRadioButton
		{
			get { return this.radioPerfQualCustom; }
		}

		public Gtk.Label ContractsLabel
		{
			get { return this.labelContracts; }
		}

		public Gtk.ComboBox ContractsComboBox
		{
			get { return this.comboContracts; }
		}

		public Gtk.CheckButton IncludeAssertsCheckButton
		{
			get { return this.checkAsserts; }
		}

		public Gtk.CheckButton IncludeNilChecksCheckButton
		{
			get { return this.checkNilChecks; }
		}

		public Gtk.CheckButton OptimizeCheckButton
		{
			get { return this.checkOptimize; }
		}

		public Gtk.CheckButton IncludeTestsCheckButton
		{
			get { return this.checkTests; }
		}

		public Gtk.CheckButton IncludeTracesCheckButton
		{
			get { return this.checkTraces; }
		}
		#endregion

		#region "Development and Deployment Options"
		public Gtk.CheckButton EmbedRunTimeCheckButton
		{
			get { return this.checkEmbedRunTime; }
		}

		public Gtk.CheckButton KeepIntermediateFilesCheckButton
		{
			get { return this.checkKeepIntermediateFiles; }
		}

		public Gtk.ComboBox DebugInfoComboBox
		{
			get { return this.comboDebugInfo; }
		}

		public Gtk.ComboBox NumberTypeComboBox
		{
			get { return this.comboNumberType; }
		}

		public Gtk.Entry CobraArgsEntry
		{
			get { return this.entryCobraArgs; }
		}
		#endregion

		#region "Back-End Native Compiler Options"
		public Gtk.ComboBox BackEndComboBox
		{
			get { return this.comboCompilerBackEnd; }
		}

		public Gtk.FileChooserButton NativeCompilerFileChooser
		{
			get { return this.filechooserNativeCompilerPath; }
		}

		public Gtk.Entry NativeCompilerArgsEntry
		{
			get { return this.entryNativeCompilerArgs; }
		}
		#endregion
	}
}