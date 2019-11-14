using System;
using System.Linq;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
using System.Windows.Forms;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Forms
{
    public partial class StepDiffDetails : Form
    {
        private CarfupStep step;
        public StepDiffDetails(CarfupStep step)
        {
            InitializeComponent();
            this.step = step;

            foreach (var s in step.GetType().GetProperties().OrderBy(x => x.Name))
            {
                dataGridViewStepDetails.Rows.Add(new string[]{ s.Name, s.GetValue(step, null)?.ToString()});
            }

            dataGridViewStepDetails.AutoResizeColumn(0);
            dataGridViewStepDetails.AutoResizeColumn(1);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
