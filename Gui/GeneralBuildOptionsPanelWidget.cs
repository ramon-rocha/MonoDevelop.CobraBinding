using System;

namespace MonoDevelop.CobraBinding.Gui
{
	/*
	 * This class provides the widget seen when navigating to the
	 * Project > Options > Build > General panel.  The logic for
	 * interaction of the various widgets is not handled by this
	 * class.  It's just the GUI.
	 */
	[System.ComponentModel.ToolboxItem(true)]
	public partial class GeneralBuildOptionsPanelWidget : Gtk.Bin
	{
		public GeneralBuildOptionsPanelWidget()
		{
			this.Build();
		}
		
		public Gtk.ComboBox CompileTargetComboBox {
			get { return this.comboboxCompileTarget; }
		}
		
		public Gtk.Label MainClassLabel {
			get { return this.labelMainClass; }
		}
		
		public Gtk.ComboBoxEntry MainClassComboBoxEntry {
			get { return this.comboboxentryMainClass; }
		}
		
		public Gtk.ComboBoxEntry TestRunnerComboBoxEntry {
			get { return this.comboboxentryTestRunner; }
		}
		
		public Gtk.FileChooserButton ApplicationIconFileChooser {
			get { return this.filechooserApplicationIcon; }
		}
		
		public Gtk.Label ApplicationIconLabel {
			get { return this.labelApplicationIcon; }
		}
	}
}

