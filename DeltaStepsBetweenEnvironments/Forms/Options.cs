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
            PopulateSettings(dbe.Settings);
        }

        private void PopulateSettings(PluginSettings settings)
        {
            if (settings == null)
            {
                settings = new PluginSettings();
            }

            checkboxAllowStats.Checked = settings.AllowLogUsage != false;
            checkBoxSkipHidden.Checked = settings.SkipHiddenSteps;
            radioButtonSortingOrderAsc.Checked = settings.SortOrderPref == SortOrder.Ascending || settings.SortOrderPref == null;
            radioButtoradioButtonSortingOrderDesc.Checked = !radioButtonSortingOrderAsc.Checked;
        }

        internal PluginSettings GetSettings()
        {
            var settings = dbe.Settings;
            settings.AllowLogUsage = checkboxAllowStats.Checked;
            settings.SkipHiddenSteps = checkBoxSkipHidden.Checked;
            settings.SortOrderPref = (radioButtonSortingOrderAsc.Checked || settings.SortOrderPref == null) ? SortOrder.Ascending : SortOrder.Descending;
            settings.CurrentVersion = DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments.CurrentVersion;

            return settings;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.dbe.Settings = GetSettings();
            this.dbe.SaveSettings();
            this.Close();
        }
    }
}
