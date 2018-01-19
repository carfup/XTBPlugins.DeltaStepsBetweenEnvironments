using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Configuration;
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
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Reflection;
using System.Diagnostics;
using Carfup.XTBPlugins.Forms;
using Carfup.XTBPlugins.AppCode;

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
        List<Entity> stepsCrmSource = new List<Entity>();
        List<Entity> stepsCrmTarget = new List<Entity>();
        private static string solutionPluginStepsName = null;
        public event EventHandler OnRequestConnection;
        internal PluginSettings settings = new PluginSettings();
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

            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["INSIGHTS_INTRUMENTATIONKEY"];
            TelemetryClient telemetry = new TelemetryClient();
            telemetry.Context.Device.Id = System.AppDomain.CurrentDomain.FriendlyName.Replace(".exe", String.Empty);
            telemetry.Context.Component.Version = CurrentVersion;

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

        public static List<Entity> querySteps(IOrganizationService service, List<Entity> list)
        {
            QueryExpression queryExistingSteps = new QueryExpression()
            {

                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("componenttype"),
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkToEntityName = "sdkmessageprocessingstep",
                        LinkToAttributeName = "sdkmessageprocessingstepid",
                        LinkFromEntityName = "solutioncomponent",
                        LinkFromAttributeName = "objectid",
                        EntityAlias = "step",
                        Columns = new ColumnSet("name","configuration","mode","rank","stage","supporteddeployment","invocationsource","configuration","plugintypeid","sdkmessageid","sdkmessagefilterid","filteringattributes","description","asyncautodelete","customizationlevel"),
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkToEntityName = "sdkmessagefilter",
                                LinkToAttributeName = "sdkmessagefilterid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessagefilterid",
                                EntityAlias = "messagefilter",
                                Columns = new ColumnSet("primaryobjecttypecode"),
                                JoinOperator = JoinOperator.Inner
                            },
                            new LinkEntity()
                            {
                                LinkToEntityName = "sdkmessage",
                                LinkToAttributeName = "sdkmessageid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessageid",
                                EntityAlias = "sdkmessage",
                                Columns = new ColumnSet("name"),
                                JoinOperator = JoinOperator.Inner
                            },
                            new LinkEntity()
                            {
                                LinkToEntityName = "plugintype",
                                LinkToAttributeName = "plugintypeid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "plugintypeid",
                                EntityAlias = "plugintype",
                                Columns = new ColumnSet("typename"),
                                JoinOperator = JoinOperator.Inner
                            }
                        }
                    }
                    ,
                    new LinkEntity()
                    {
                        LinkToEntityName = "solution",
                        LinkToAttributeName = "solutionid",
                        LinkFromEntityName = "solutioncomponent",
                        LinkFromAttributeName = "solutionid",
                        LinkCriteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("uniquename", ConditionOperator.Equal, solutionPluginStepsName)
                            }
                        }
                    }
                }
                ,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("componenttype", ConditionOperator.Equal, 92)
                    }
                }

            };

            list = service.RetrieveMultiple(queryExistingSteps).Entities.ToList();

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
                    stepsCrmSource = querySteps(sourceService, stepsCrmSource);
                    stepsCrmTarget = querySteps(targetService, stepsCrmTarget);

                    diffCrmSourceTarget = stepsCrmSource.Except(stepsCrmTarget).Select(x => ((AliasedValue)x["step.name"]).Value.ToString()).ToArray();
                    diffCrmTargetSource = stepsCrmTarget.Except(stepsCrmSource).Select(x => ((AliasedValue)x["step.name"]).Value.ToString()).ToArray();
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

        private void buttonCopyTargetToSource_Click(object sender, EventArgs evt)
        {
            var selectedStep = stepsCrmTarget.Where(x => ((AliasedValue)x["step.name"]).Value.ToString() == listBoxTargetSource.SelectedItem.ToString()).FirstOrDefault();

            if (selectedStep == null)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating the step in the environment...",
                Work = (bw, e) =>
                {
                    var pluginType = getPluginType(returnAliasedValue(selectedStep, "plugintype.typename").ToString());
                    var sdkMessage = getSdkMessage(returnAliasedValue(selectedStep, "sdkmessage.name").ToString());
                    var messageFilter = getMessageFilter(returnAliasedValue(selectedStep, "messagefilter.primaryobjecttypecode").ToString());

                    if (pluginType == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary Plugin Type information in the destination system...");
                        return;
                    }

                    if (sdkMessage == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary SDK Message information in the destination system...");
                        return;
                    }

                    if (messageFilter == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary Message Filter information in the destination system...");
                        return;
                    }

                    Entity newStepToCreate = new Entity("sdkmessageprocessingstep");
                    newStepToCreate["plugintypeid"] = new EntityReference("plugintype", pluginType.Id);
                    newStepToCreate["sdkmessageid"] = new EntityReference("plugintype", sdkMessage.Id);
                    newStepToCreate["sdkmessagefilterid"] = new EntityReference("sdkmessagefilter", messageFilter.Id);
                    newStepToCreate["name"] = returnAliasedValue(selectedStep, "step.name");
                    newStepToCreate["configuration"] = returnAliasedValue(selectedStep, "step.configuration");
                    newStepToCreate["mode"] = returnAliasedValue(selectedStep, "step.mode");
                    newStepToCreate["rank"] = returnAliasedValue(selectedStep, "step.rank");
                    newStepToCreate["stage"] = returnAliasedValue(selectedStep, "step.stage");
                    newStepToCreate["supporteddeployment"] = returnAliasedValue(selectedStep, "step.supporteddeployment");
                    newStepToCreate["invocationsource"] = returnAliasedValue(selectedStep, "step.invocationsource");
                    newStepToCreate["configuration"] = returnAliasedValue(selectedStep, "step.configuration");
                    newStepToCreate["filteringattributes"] = returnAliasedValue(selectedStep, "step.filteringattributes");
                    newStepToCreate["description"] = returnAliasedValue(selectedStep, "step.description");
                    newStepToCreate["asyncautodelete"] = returnAliasedValue(selectedStep, "step.asyncautodelete");
                    newStepToCreate["customizationlevel"] = returnAliasedValue(selectedStep, "step.customizationlevel");

                    e.Result = targetService.Create(newStepToCreate);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if ((Guid)e.Result != null)
                    {
                        MessageBox.Show($"Your step was successfully copied");
                        listBoxSourceTarget.Items.Add(returnAliasedValue(selectedStep, "step.name"));
                    }


                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void buttonCopySourceToTarget_Click(object sender, EventArgs evt)
        {
            var selectedStep = stepsCrmSource.Where(x => ((AliasedValue)x["step.name"]).Value.ToString() == listBoxSourceTarget.SelectedItem.ToString()).FirstOrDefault();

            if (selectedStep == null)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating the step in the environment...",
                Work = (bw, e) =>
                {
                    var pluginType = getPluginType(returnAliasedValue(selectedStep, "plugintype.typename").ToString());
                    var sdkMessage = getSdkMessage(returnAliasedValue(selectedStep, "sdkmessage.name").ToString());
                    var messageFilter = getMessageFilter(returnAliasedValue(selectedStep, "messagefilter.primaryobjecttypecode").ToString());

                    if (pluginType == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary Plugin Type information in the destination system...");
                        return;
                    }

                    if (sdkMessage == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary SDK Message information in the destination system...");
                        return;
                    }

                    if (messageFilter == null)
                    {
                        MessageBox.Show($"Sorry, but we didn't find the necessary Message Filter information in the destination system...");
                        return;
                    }

                    Entity newStepToCreate = new Entity("sdkmessageprocessingstep");
                    newStepToCreate["plugintypeid"] = new EntityReference("plugintype", pluginType.Id);
                    newStepToCreate["sdkmessageid"] = new EntityReference("plugintype", sdkMessage.Id);
                    newStepToCreate["sdkmessagefilterid"] = new EntityReference("sdkmessagefilter", messageFilter.Id);
                    newStepToCreate["name"] = returnAliasedValue(selectedStep, "step.name");
                    newStepToCreate["configuration"] = returnAliasedValue(selectedStep, "step.configuration");
                    newStepToCreate["mode"] = returnAliasedValue(selectedStep, "step.mode");
                    newStepToCreate["rank"] = returnAliasedValue(selectedStep, "step.rank");
                    newStepToCreate["stage"] = returnAliasedValue(selectedStep, "step.stage");
                    newStepToCreate["supporteddeployment"] = returnAliasedValue(selectedStep, "step.supporteddeployment");
                    newStepToCreate["invocationsource"] = returnAliasedValue(selectedStep, "step.invocationsource");
                    newStepToCreate["configuration"] = returnAliasedValue(selectedStep, "step.configuration");
                    newStepToCreate["filteringattributes"] = returnAliasedValue(selectedStep, "step.filteringattributes");
                    newStepToCreate["description"] = returnAliasedValue(selectedStep, "step.description");
                    newStepToCreate["asyncautodelete"] = returnAliasedValue(selectedStep, "step.asyncautodelete");
                    newStepToCreate["customizationlevel"] = returnAliasedValue(selectedStep, "step.customizationlevel");

                    e.Result = targetService.Create(newStepToCreate);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if((Guid)e.Result != null)
                    {
                        MessageBox.Show($"Your step was successfully copied.");
                        listBoxTargetSource.Items.Add(returnAliasedValue(selectedStep, "step.name"));
                    }
                        

                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        public object returnAliasedValue(Entity entity, string varName)
        {
            return entity.GetAttributeValue<AliasedValue>(varName) == null ? "" : entity.GetAttributeValue<AliasedValue>(varName).Value;
        }

        public Entity getPluginType(string plugintype)
        {
            QueryExpression queryRetrievePluginType = new QueryExpression
            {
                EntityName = "plugintype",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("typename", ConditionOperator.Equal, plugintype)
                    }
                }
            };

            var pluginType = targetService.RetrieveMultiple(queryRetrievePluginType).Entities;

            return pluginType.FirstOrDefault();
        }

        public Entity getSdkMessage(string name)
        {
            QueryExpression queryRetrieveSdkMessage = new QueryExpression
            {
                EntityName = "sdkmessage",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("name", ConditionOperator.Equal, name)
                    }
                }
            };

            var sdkMessage = targetService.RetrieveMultiple(queryRetrieveSdkMessage).Entities;

            return sdkMessage.FirstOrDefault();
        }

        public Entity getMessageFilter(string primaryobjecttypecode)
        {
            QueryExpression queryRetrieveMessageFilter = new QueryExpression
            {
                EntityName = "sdkmessagefilter",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("primaryobjecttypecode", ConditionOperator.Equal, primaryobjecttypecode)
                    }
                }
            };

            var messageFilter = targetService.RetrieveMultiple(queryRetrieveMessageFilter).Entities;

            return messageFilter.FirstOrDefault();
        }

        public static string CurrentVersion
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fileVersionInfo.ProductVersion;
            }
        }

        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            var allowLogUsage = settings.AllowLogUsage;
            var options = new Options(this);
            
            var optionDlg = new Options(this);
            if (optionDlg.ShowDialog(this) == DialogResult.OK)
            {
                settings = optionDlg.GetSettings();
                if (allowLogUsage != settings.AllowLogUsage)
                {
                    if (settings.AllowLogUsage == true)
                    {
                       // LogUse("Accept", true);
                    }
                    else if (!settings.AllowLogUsage == true)
                    {
                       // LogUse("Deny", true);
                    }
                }
            }
        }

        private void SaveSetting()
        {
            SettingsManager.Instance.Save(typeof(DeltaStepsBetweenEnvironments), settings, "[Common]");
        }

        private void DeltaStepsBetweenEnvironments_Load(object sender, EventArgs e)
        {
            LoadSetting();
        }

        private void LoadSetting()
        {
            try
            {
                if (SettingsManager.Instance.TryLoad<PluginSettings>(typeof(DeltaStepsBetweenEnvironments), out settings, "[Common]"))
                {
                    return;
                }
            }
            catch (InvalidOperationException) { }
            settings = new PluginSettings();
        }
    }
}
