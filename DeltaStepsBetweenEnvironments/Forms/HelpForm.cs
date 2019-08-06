using System;
using Carfup.XTBPlugins.AppCode;
using System.Windows.Forms;

namespace Carfup.XTBPlugins.Forms
{
    public partial class HelpForm : Form
    {
        private DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe;

        public HelpForm(DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe)
        {
            InitializeComponent();
            this.dbe = dbe;
            this.dbe.Log.LogData(EventType.Event, LogAction.ShowHelpScreen);
            //PopulateSettings(dbe.settings);
        }

        //internal PluginSettings GetSettings()
        //{
        //    var settings = dbe.settings;
        //    settings.ShowHelpOnStartUp = !checkBoxShowHelpStartup.Checked;
        //    settings.CurrentVersion = DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments.CurrentVersion;

        //    return settings;
        //}

        //private void PopulateSettings(PluginSettings settings)
        //{
        //    if (settings == null)
        //    {
        //        settings = new PluginSettings();
        //    }

        //    checkBoxShowHelpStartup.Checked = settings.ShowHelpOnStartUp != false;
        //}

        private void buttonCloseHelp_Click(object sender, EventArgs e)
        {
            //this.dbe.settings = GetSettings();
            //this.dbe.SaveSettings();
            Close();
        }
    }
}
