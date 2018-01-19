using System;
using Carfup.XTBPlugins.AppCode;
using System.Windows.Forms;

namespace Carfup.XTBPlugins.Forms
{
    public partial class Options : Form
    {
        private DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe;
        public Options(DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe)
        {
            InitializeComponent();
            this.dbe = dbe;
            PopulateSettings(dbe.settings);
        }

        private void PopulateSettings(PluginSettings settings)
        {
            if (settings == null)
            {
                settings = new PluginSettings();
            }

            checkboxAllowStats.Checked = settings.AllowLogUsage != false;
        }

        internal PluginSettings GetSettings()
        {
            var settings = dbe.settings;
            settings.AllowLogUsage = checkboxAllowStats.Checked;

            return settings;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.dbe.settings = GetSettings();
            this.dbe.SaveSettings();
            this.Close();
        }
    }
}
