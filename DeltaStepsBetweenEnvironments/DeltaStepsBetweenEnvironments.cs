using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;



using Microsoft.Xrm.Sdk.Query;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public partial class DeltaStepsBetweenEnvironments : PluginControlBase, IXrmToolBoxPluginControl, IGitHubPlugin
    {
        #region varibables
        private string[] solutionsList = null;
        private ConnectionDetail sourceDetail = null;
        private ConnectionDetail targetDetail = null;
        IOrganizationService sourceService = null;
        IOrganizationService targetService = null;
        List<string> stepsCrmSource = new List<string>();
        List<string> stepsCrmTarget = new List<string>();
        private static string solutionPluginStepsName = null;
        public event EventHandler OnRequestConnection;
        public string RepositoryName
        {
            get
            {
                return "XTBPlugins.DeltaStepsBetweenEnvironments";
            }
        }

        public string UserName
        {
            get
            {
                return "carfup";
            }
        }

        #endregion

        public DeltaStepsBetweenEnvironments()
        {
            InitializeComponent();

            sourceDetail = this.ConnectionDetail;
            sourceService = this.Service;
            buttonCompare.Visible = false;
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void comboBoxSolutionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (canProceed())
            {
                solutionPluginStepsName = comboBoxSolutionsList.SelectedItem.ToString();
            }
        }

        public static List<string> querySteps(IOrganizationService service, List<string> list)
        {
            QueryExpression queryExistingAssemblies = new QueryExpression()
            {
                EntityName = "solution",
                ColumnSet = new ColumnSet()
            };

            queryExistingAssemblies.Criteria.AddCondition("uniquename", ConditionOperator.Equal, solutionPluginStepsName);
            var crmlocalAssemblyClasses = service.RetrieveMultiple(queryExistingAssemblies).Entities.FirstOrDefault();

            QueryExpression queryExistingSteps = new QueryExpression()
            {
                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet(true)
            };

            queryExistingSteps.Criteria.AddCondition("componenttype", ConditionOperator.Equal, 92);
            queryExistingSteps.Criteria.AddCondition("solutionid", ConditionOperator.Equal, crmlocalAssemblyClasses.Id);
            var crmExistingComponentsSteps = service.RetrieveMultiple(queryExistingSteps).Entities;

            foreach (var step in crmExistingComponentsSteps)
            {
                Entity stepdetail = service.Retrieve("sdkmessageprocessingstep", new Guid(step["objectid"].ToString()), new ColumnSet(true));
                var name = ((EntityReference)stepdetail["eventhandler"]).Name;
                list.Add(name);
            }

            return list;
        }

        private void comboBoxTargetEnvironmentList_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var aaa = new RequestConnectionEventArgs
                {
                    ActionName = "TargetOrganization",
                    Control = this
                };
                OnRequestConnection(this, aaa);
            }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName = "", object parameter = null)
        {
            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                targetDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Target");
            }
            else
            {
                sourceService = newService;
                sourceDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Source");
            }

            if (targetService != null && sourceService != null)
                buttonCompare.Visible = true;
            else
                buttonCompare.Visible = false;
        }
        private void SetConnectionLabel(ConnectionDetail detail, string serviceType)
        {
            switch (serviceType)
            {
                case "Source":
                    labelSourceEnvironment.Text = detail.ConnectionName;
                    labelSourceEnvironment.ForeColor = Color.Green;
                    break;

                case "Target":
                    labelTargetEnvironment.Text = detail.ConnectionName;
                    labelTargetEnvironment.ForeColor = Color.Green;
                    break;
            }
        }

        private void btnChangeTargetEnvironment_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var arg = new RequestConnectionEventArgs
                {
                    ActionName = "TargetOrganization",
                    Control = this
                };
                OnRequestConnection(this, arg);
            }
        }

        private void buttonCompare_Click(object sender, EventArgs evt)
        {
            if (solutionPluginStepsName == null)
            {
                MessageBox.Show($"Please select a solution first.");
                return;
            }

            listBoxSourceTarget.Items.Clear();
            listBoxTargetSource.Items.Clear();

            string[] diffCrmSourceTarget = null;
            string[] diffCrmTargetSource = null;

            stepsCrmSource.Clear();
            stepsCrmTarget.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Comparing the 2 Solutions...",
                Work = (bw, e) =>
                {
                    querySteps(sourceService, stepsCrmSource);
                    querySteps(targetService, stepsCrmTarget);

                    diffCrmSourceTarget = stepsCrmSource.Except(stepsCrmTarget).ToArray();
                    diffCrmTargetSource = stepsCrmTarget.Except(stepsCrmSource).ToArray();
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (diffCrmSourceTarget.Count() == 0)
                    {
                        //listBoxSourceTarget.Visible = false;
                        labelSourceTargetMatch.Visible = true;
                    }
                    else
                    {
                        listBoxSourceTarget.Visible = true;
                        listBoxSourceTarget.Items.AddRange(diffCrmSourceTarget);
                        labelSourceTargetMatch.Visible = false;

                    }

                    if (diffCrmTargetSource.Count() == 0)
                    {
                        //listBoxTargetSource.Visible = false;
                        labelTargetSourceMatch.Visible = true;
                    }
                    else
                    {
                        listBoxTargetSource.Items.AddRange(diffCrmTargetSource);
                        listBoxTargetSource.Visible = true;
                        labelTargetSourceMatch.Visible = false;
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void buttonLoadSolutions_Click(object sender, EventArgs evt)
        {
            if (canProceed())
            {
                comboBoxSolutionsList.Items.Clear();
                WorkAsync(new WorkAsyncInfo
                {
                    Message = "Loading CRM Solutions...",
                    Work = (bw, e) =>
                    {
                        solutionsList = sourceService.RetrieveMultiple(new QueryExpression("solution")
                        {
                            ColumnSet = new ColumnSet("uniquename"),
                        }).Entities.Select(p => p.Attributes["uniquename"].ToString()).OrderBy(p => p).ToArray();
                    },
                    PostWorkCallBack = e =>
                    {
                        if (e.Error != null)
                        {
                            MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (solutionsList != null)
                            comboBoxSolutionsList.Items.AddRange(solutionsList);

                    },
                    ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
                });
            }
        }

        private void buttonChangeSource_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var arg = new RequestConnectionEventArgs
                {
                    ActionName = "SourceOrganization",
                    Control = this
                };
                OnRequestConnection(this, arg);
            }
        }


        public bool canProceed()
        {
            if (sourceService == null || targetService == null)
            {
                MessageBox.Show("Make sure you are connected to a Source AND Target environments first.");
                return false;
            }
            return true;
        }

        private void buttonCopyTargetToSource_Click(object sender, EventArgs e)
        {

        }

        private void buttonCopySourceToTarget_Click(object sender, EventArgs e)
        {

        }
    }
}
