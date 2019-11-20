using System;
using System.Linq;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Forms
{
    public partial class StepDiffDetails : Form
    {
        private CarfupStep step;
        public StepDiffDetails(CarfupStep step)
        {
            InitializeComponent();
            this.step = step;
            this.Text = $"Step Details : {step.StepName}";

            foreach (var s in step.GetType().GetProperties().OrderBy(x => x.Name))
            {
                if (s.GetValue(step, null) is Entity)
                    continue;

                dataGridViewStepDetails.Rows.Add(new string[]{ s.Name, s.GetValue(step, null)?.ToString()});
            }

            dataGridViewStepDetails.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewStepDetails.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewStepDetails.AutoResizeColumns();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
