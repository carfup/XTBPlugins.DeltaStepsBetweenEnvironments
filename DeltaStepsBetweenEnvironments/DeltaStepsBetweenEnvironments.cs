using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System.Reflection;
using System.Diagnostics;
using Carfup.XTBPlugins.Forms;
using Carfup.XTBPlugins.AppCode;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility.Args;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public partial class DeltaStepsBetweenEnvironments : PluginControlBase, IXrmToolBoxPluginControl, IGitHubPlugin, IStatusBarMessager
    {
        #region varibables
        private string[] solutionsList = null;
        private ConnectionDetail sourceDetail = null;
        private ConnectionDetail targetDetail = null;
        IOrganizationService sourceService = null;
        IOrganizationService targetService = null;
        List<CarfupStep> stepsCrmSource = new List<CarfupStep>();
        List<CarfupStep> stepsCrmTarget = new List<CarfupStep>();
        private static string solutionAssemblyPluginStepsName = null;
        public event EventHandler OnRequestConnection;
        internal PluginSettings settings = new PluginSettings();
        public LogUsage log = null;
        Comparing comparing = Comparing.Solution;
        ControllerManager controller = null;
        private int currentColumnOrder;
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

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
            this.log.LogData(EventType.Event, LogAction.PluginClosed);

            // Saving settings for the next usage of plugin
            SaveSettings();

            // Making sure that all message are sent if stats are enabled
            this.log.Flush();
            CloseTool();
        }

        //Select the solution from where we will query the steps
        private void comboBoxSolutionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (canProceed() && comboBoxSolutionsAssembliesList.SelectedItem != null)
            {
                solutionAssemblyPluginStepsName = comboBoxSolutionsAssembliesList.SelectedItem.ToString();

                isSolutionOrAssemblyExistingInTargetEnv();
            }
        }

        private void isSolutionOrAssemblyExistingInTargetEnv()
        {
            string whatToCompare = Wording.getComparingInfo(comparing);

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Checking if the {whatToCompare} name exists in the target environment...",
                Work = (bw, e) =>
                {
                    e.Result = (comparing == Comparing.Solution) ? controller.dataManager.isSolutionExistingInTargetEnv(solutionAssemblyPluginStepsName) : controller.dataManager.isAssemblyExistingInTargetEnv(solutionAssemblyPluginStepsName);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        string logAction = (comparing == Comparing.Solution) ? LogAction.SolutionExistingInTargetEnvChecked : LogAction.AssemblyExistingInTargetEnvChecked;
                        this.log.LogData(EventType.Exception, logAction, e.Error);

                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if((int)e.Result != 1)
                        MessageBox.Show($"The {whatToCompare} doesn't exist in the Target environment. \rThe compare function will return a \"Perfect match\" in this case.\r\r You will still have the possibility to copy steps from the Source to Target environment.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
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
            controller = new ControllerManager(sourceService, targetService);

            if (actionName == "TargetOrganization")
            {
                targetService = newService;
                targetDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Target");
                controller.targetService = targetService;
            }
            else
            {
                sourceService = newService;
                sourceDetail = connectionDetail;
                SetConnectionLabel(connectionDetail, "Source");
                controller.sourceService = sourceService;
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

        // We compare the same solution name in both environments
        private void buttonCompare_Click(object sender, EventArgs evt)
        {
            compareBothSolutionsAssemblies();
        }


        private void compareBothSolutionsAssemblies()
        {
            if (solutionAssemblyPluginStepsName == null)
            {
                MessageBox.Show($"Please select a {Wording.getComparingInfo(comparing)} first.");
                return;
            }

            string[] diffCrmSourceTarget = null;
            string[] diffCrmTargetSource = null;

            stepsCrmSource.Clear();
            stepsCrmTarget.Clear();

            string logAction = (comparing == Comparing.Solution) ? LogAction.SolutionsCompared : LogAction.AssembliesCompared;

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Comparing the 2 {Wording.getComparingInfo(comparing, true, true)}...",
                Work = (bw, e) =>
                {
                    if(comparing == Comparing.Solution)
                    {
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, "Fetching steps from source environment..."));
                        stepsCrmSource = controller.dataManager.querySteps(sourceService, solutionAssemblyPluginStepsName);  //querySteps(sourceService, stepsCrmSource);
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(30, "Fetching steps from source environment..."));
                        stepsCrmTarget = controller.dataManager.querySteps(targetService, solutionAssemblyPluginStepsName);  //querySteps(targetService, stepsCrmTarget);
                    }
                    else if(comparing == Comparing.Assembly)
                    {
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, "Fetching steps from source environment..."));
                        stepsCrmSource = controller.dataManager.queryStepsAssembly(sourceService, solutionAssemblyPluginStepsName);  //querySteps(sourceService, stepsCrmSource);
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(30, "Fetching steps from source environment..."));
                        stepsCrmTarget = controller.dataManager.queryStepsAssembly(targetService, solutionAssemblyPluginStepsName);  //querySteps(targetService, stepsCrmTarget);
                    }

                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(60, "Comparing steps..."));
                    diffCrmSourceTarget = stepsCrmSource.Select(x => x.stepName).Except(stepsCrmTarget.Select(x => x.stepName)).ToArray();
                    diffCrmTargetSource = stepsCrmTarget.Select(x => x.stepName).Except(stepsCrmSource.Select(x => x.stepName)).ToArray();
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(100, "Comparing steps..."));
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        
                        this.log.LogData(EventType.Exception, logAction, e.Error);
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (diffCrmSourceTarget.Count() == 0)
                    {
                        listViewSourceTarget.Items.Clear();
                        labelSourceTargetMatch.Visible = true;
                        
                    }
                    else // there are steps in source but not target
                    {
                        labelSourceTargetMatch.Visible = false;
                        listViewSourceTarget.Visible = true;
                        fillListViewItems(listViewSourceTarget, stepsCrmSource, diffCrmSourceTarget);
                    }

                    if (diffCrmTargetSource.Count() == 0)
                    {
                        listViewTargetSource.Items.Clear();
                        labelTargetSourceMatch.Visible = true;
                    }
                    else // there are steps in source but not target
                    {
                        labelTargetSourceMatch.Visible = false;
                        listViewTargetSource.Visible = true;
                        fillListViewItems(listViewTargetSource, stepsCrmTarget, diffCrmTargetSource);
                    }

                    this.log.LogData(EventType.Event, logAction);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }
        // Loading solutions from the Source environment
        private void buttonLoadSolutionsAssemblies_Click(object sender, EventArgs evt)
        {
            if (canProceed())
            {
                comboBoxSolutionsAssembliesList.Items.Clear();
                string logAction = (comparing == Comparing.Solution) ? LogAction.SolutionsLoaded : LogAction.AssembliesLoaded;

                WorkAsync(new WorkAsyncInfo
                {
                    Message = $"Loading CRM {Wording.getComparingInfo(comparing, true, true)}...",
                    Work = (bw, e) =>
                    {
                        solutionsList = (comparing == Comparing.Solution) ? controller.dataManager.loadSolutions() : controller.dataManager.loadAssemblies();
                    },
                    PostWorkCallBack = e =>
                    {
                        if (e.Error != null)
                        {
                            
                            this.log.LogData(EventType.Exception, logAction, e.Error);
                            MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        this.log.LogData(EventType.Event, $"{Wording.getComparingInfo(comparing, true, true)} retrieved");
                        if (solutionsList != null)
                            comboBoxSolutionsAssembliesList.Items.AddRange(solutionsList);

                        this.log.LogData(EventType.Event, logAction);
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

        // We check if both environments are selected otherwise : error message
        public bool canProceed()
        {
            if (sourceService == null || targetService == null)
            {
                this.log.LogData(EventType.Event, LogAction.CanProceed);
                MessageBox.Show("Make sure you are connected to a Source AND Target environments first.");
                return false;
            }

            return true;
        }

        // Copying a step from the target to source environment
        private void buttonCopyTargetToSource_Click(object sender, EventArgs evt)
        {
            creatingStepProcess(listViewTargetSource, stepsCrmTarget, sourceService);
        }

        // Copying a step from the source to target environment
        private void buttonCopySourceToTarget_Click(object sender, EventArgs evt)
        {
            creatingStepProcess(listViewSourceTarget, stepsCrmSource, targetService);
        }
       
        // action when the option form is opened
        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            var allowLogUsage = settings.AllowLogUsage;            
            var optionDlg = new Options(this);
            if (optionDlg.ShowDialog(this) == DialogResult.OK)
            {
                settings = optionDlg.GetSettings();
                if (allowLogUsage != settings.AllowLogUsage)
                {
                    if (settings.AllowLogUsage == true)
                    {
                        this.log.updateForceLog();
                        this.log.LogData(EventType.Event, LogAction.StatsAccepted);
                    }
                    else if (!settings.AllowLogUsage == true)
                    {
                        this.log.updateForceLog();
                        this.log.LogData(EventType.Event, LogAction.StatsDenied);
                    }
                }
            }
        }

        public void creatingStepProcess(ListView listView, List<CarfupStep> stepsList, IOrganizationService service)
        {
            bool toDefaultSolution = true;
            int itemCount = listView.CheckedItems.Count;

            if (itemCount < 1)
            {
                MessageBox.Show($"Make sure you checked at least one step before trying to perform a Copy action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(comparing == Comparing.Solution)
            {
                var whichDestination = MessageBox.Show($"Since you are copying a step from a solution, do you want to copy the step to the custom solution ?\r\rYes : Custom Solution.\rNo : Default Solution.\rCancel : Abort operation.", "Which destination you want ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (whichDestination == DialogResult.Yes)
                    toDefaultSolution = false;
                else if (whichDestination == DialogResult.Cancel)
                    return;
            }

            // Getting list of selected Items
            ListViewItem[] stepsGuid = new ListViewItem[itemCount];

            string logAction = (stepsList == stepsCrmSource) ? LogAction.StepCreeatedSourceToTarget : LogAction.StepCreatedTargetToSource;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating the step(s) in the environment...",
                Work = (bw, e) =>
                {
                    Invoke(new Action(() =>
                    {
                        listView.CheckedItems.CopyTo(stepsGuid, 0);
                    }));

                    if (stepsGuid == null)
                        return;

                    int i = 1;

                    foreach (ListViewItem itemView in stepsGuid)
                    {
                        int percentage = (i * 100) / itemCount;
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(percentage, $"Creating the step(s) in the environment : {i}/{itemCount}"));
                        var selectedStep = stepsList.Where(x => x.stepName == itemView.Text).FirstOrDefault();
                        Guid? stepCreated = creatingStep(selectedStep, (stepsList == stepsCrmSource));

                        if (stepCreated == null)
                            return;

                        if (comparing == Comparing.Solution && !toDefaultSolution)
                        {
                            bw.ReportProgress(percentage, $"Adding the new created step to the {solutionAssemblyPluginStepsName} solution...");

                            AddSolutionComponentRequest ascr = new AddSolutionComponentRequest()
                            {
                                ComponentId = stepCreated.Value,
                                ComponentType = 92,
                                SolutionUniqueName = solutionAssemblyPluginStepsName
                            };

                            service.Execute(ascr);
                        }
                        i++;
                    }
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        this.log.LogData(EventType.Exception, logAction, e.Error);
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        this.log.LogData(EventType.Event, logAction);
                        MessageBox.Show($"Your step(s) were successfully copied to the {((toDefaultSolution) ? "default" : solutionAssemblyPluginStepsName)} solution of {((logAction == LogAction.StepCreeatedSourceToTarget) ? "target" : "source")} environment.");
                        labelSourceTargetMatch.Visible = false;
                        labelTargetSourceMatch.Visible = false;
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        public Guid? creatingStep(CarfupStep selectedStep, bool toTarget = true)
        {
            string PluginTypeRetrievedLogAction = (toTarget) ? LogAction.PluginTypeRetrievedSourceToTarget : LogAction.PluginTypeRetrievedTargetToSource;
            string SDKMessageRetrievedLogAction = (toTarget) ? LogAction.SDKMessageRetrievedSourceToTarget : LogAction.SDKMessageRetrievedTargetToSource;
            string MessageFilterRetrievedLogAction = (toTarget) ? LogAction.MessageFilterRetrievedSourceToTarget : LogAction.MessageFilterRetrievedTargetToSource;
            IOrganizationService service = (toTarget) ? targetService : sourceService;

            // retrieving the 3 data mandatory to have a proper step created
            var pluginType = controller.dataManager.getPluginType(selectedStep.plugintypeName, service);
            var sdkMessage = controller.dataManager.getSdkMessage(selectedStep.stepMessageName, service);
            var messageFilter = controller.dataManager.getMessageFilter(selectedStep.entityName, service);

            if (pluginType == null)
            {
                this.log.LogData(EventType.Exception, PluginTypeRetrievedLogAction);
                MessageBox.Show($"Sorry, but we didn't find the necessary Plugin Type information in the destination system...\r\rThis can occur because you didn't load the same assembly in the destination system.");
                return null;
            }

            if (sdkMessage == null)
            {
                this.log.LogData(EventType.Exception, SDKMessageRetrievedLogAction);
                MessageBox.Show($"Sorry, but we didn't find the necessary SDK Message information in the destination system...");
                return null;
            }

            if (messageFilter == null)
            {
                this.log.LogData(EventType.Exception, MessageFilterRetrievedLogAction);
                MessageBox.Show($"Sorry, but we didn't find the necessary Message Filter information in the destination system...");
                return null;
            }


            this.log.LogData(EventType.Event, PluginTypeRetrievedLogAction);
            this.log.LogData(EventType.Event, SDKMessageRetrievedLogAction);
            this.log.LogData(EventType.Event, MessageFilterRetrievedLogAction);

            // Preparing the object step
            Entity newStepToCreate = new Entity("sdkmessageprocessingstep");
            newStepToCreate["plugintypeid"] = new EntityReference("plugintype", pluginType.Id);
            newStepToCreate["sdkmessageid"] = new EntityReference("sdkmessage", sdkMessage.Id);
            newStepToCreate["sdkmessagefilterid"] = new EntityReference("sdkmessagefilter", messageFilter.Id);
            newStepToCreate["name"] = selectedStep.stepName;
            newStepToCreate["mode"] = selectedStep.stepMode;
            newStepToCreate["rank"] = selectedStep.stepRank;
            newStepToCreate["stage"] = selectedStep.stepStage;
            newStepToCreate["supporteddeployment"] = selectedStep.stepSupporteddeployment;
            newStepToCreate["invocationsource"] = selectedStep.stepInvocationsource;
            newStepToCreate["configuration"] = selectedStep.stepConfiguration;
            newStepToCreate["filteringattributes"] = selectedStep.stepFilteringattributes;
            newStepToCreate["description"] = selectedStep.stepDescription;
            newStepToCreate["asyncautodelete"] = selectedStep.stepAsyncautodelete;
            newStepToCreate["customizationlevel"] = selectedStep.stepCustomizationlevel;

            return service.Create(newStepToCreate);
        }

        // will save personal settings
        public void SaveSettings()
        {
            this.log.LogData(EventType.Event, LogAction.SettingsSaved);
            SettingsManager.Instance.Save(typeof(DeltaStepsBetweenEnvironments), settings);

            //reordering columns if necessary
            sortListView(listViewSourceTarget, 0, settings.SortOrderPref);
            sortListView(listViewTargetSource, 0, settings.SortOrderPref);
        }

        private void DeltaStepsBetweenEnvironments_Load(object sender, EventArgs e)
        {
            // initializing log class
            log = new LogUsage(this);
            this.log.LogData(EventType.Event, LogAction.PluginOpened);
            LoadSetting();
        }

        // either loading previous settings from user or creating default ones and prompt the messagae for  the stats
        private void LoadSetting()
        {
            try
            {
                if (SettingsManager.Instance.TryLoad<PluginSettings>(typeof(DeltaStepsBetweenEnvironments), out settings))
                {
                    //if (!settings.ShowHelpOnStartUp.HasValue)
                    //{
                    //    var helpDlg = new HelpForm(this);
                    //    helpDlg.ShowDialog(this);
                    //}
                    //return;
                }
                else
                    settings = new PluginSettings();
            }
            catch (InvalidOperationException ex) {
                this.log.LogData(EventType.Exception, LogAction.SettingLoaded, ex);
            }

            this.log.LogData(EventType.Event, LogAction.SettingLoaded);

            if (!settings.AllowLogUsage.HasValue)
            {
                this.log.PromptToLog();
                this.SaveSettings();
            }

            // display showhelp or not
            //if(!settings.ShowHelpOnStartUp.HasValue)
            //{
            //    var helpDlg = new HelpForm(this);
            //    helpDlg.ShowDialog(this);
            //}
        }

        // return the current version of the plugin
        public static string CurrentVersion
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fileVersionInfo.ProductVersion;
            }
        }

        private void radioButtonCompareSolution_Click(object sender, EventArgs e)
        {
            comparing = Comparing.Solution;
            manageRadioButtonsAssemblySolution();
        }

        private void radioButtonCompareAssembly_Click(object sender, EventArgs e)
        {
            comparing = Comparing.Assembly;
            manageRadioButtonsAssemblySolution();
        }

        private void manageRadioButtonsAssemblySolution()
        {
            
            if(comparing == Comparing.Solution)
            {
                buttonLoadSolutionsAssemblies.Text = "Load Solutions";
                labelComparing.Text = "Select the solution to compare :";
            }
            else if(comparing == Comparing.Assembly)
            {
                buttonLoadSolutionsAssemblies.Text = "Load Assemblies";
                labelComparing.Text = "Select the assembly to compare :";
            }

            comboBoxSolutionsAssembliesList.SelectedIndex = -1;
            comboBoxSolutionsAssembliesList.Items.Clear();
        }

        private void fillListViewItems(ListView listView, List<CarfupStep> stepsList, string[] diff)
        {
            listView.Items.Clear();

            foreach (var step in stepsList.Where(x => diff.Contains(x.stepName)))
            {
                string createon = step.createOn.ToLocalTime().ToString("dd-MMM-yyyy HH:mm");
                string modifiedon = step.modifiedOn.ToLocalTime().ToString("dd-MMM-yyyy HH:mm");

                var item = new ListViewItem();
                item.Text = step.stepName;
                item.SubItems.Add(step.entityName);
                item.SubItems.Add(step.stepMessageName);
                item.SubItems.Add(createon);
                item.SubItems.Add(modifiedon);
                item.Tag = step.entity.Id;

                listView.Items.Add((ListViewItem)item.Clone());
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonDeleteStep_Click(object sender, EventArgs evt)
        {
            if (listViewTargetSource.CheckedItems.Count == 0 && listViewSourceTarget.CheckedItems.Count == 0)
            {
                MessageBox.Show($"Make sure you checked at least one step before trying to perform a Delete action.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // assuming selection was made on SourceToTarget
            IOrganizationService service = sourceService;
            ListView listViewToProceed = listViewSourceTarget;
            

            if (listViewSourceTarget.CheckedItems.Count == 0)
            {
                listViewToProceed = listViewTargetSource;
                service = targetService;
            }

            // Getting list of selected Items
            ListViewItem[] stepsGuid = new ListViewItem[listViewToProceed.CheckedItems.Count];


            var areYouSure = MessageBox.Show($"Do you really want to delete the step(s) ? \rYou won't be able to get it back after that.", "Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (areYouSure == DialogResult.No)
                return;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Deleting the step(s) ...",
                Work = (bw, e) =>
                {
                    Invoke(new Action(() =>
                    {
                        listViewToProceed.CheckedItems.CopyTo(stepsGuid, 0);
                    }));

                    if (stepsGuid == null)
                        return;

                    bw.ReportProgress(0, "Deleting the step(s)...");
                    foreach (ListViewItem itemView in stepsGuid)
                    {
                        DeleteRequest dr = new DeleteRequest
                        { 
                            Target = new EntityReference("sdkmessageprocessingstep", (Guid)itemView.Tag)
                        };

                        service.Execute(dr);
                    }
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        this.log.LogData(EventType.Exception, LogAction.StepsDeleted, e.Error);
                        MessageBox.Show(this, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        foreach (ListViewItem step in stepsGuid.ToList())
                            listViewToProceed.Items.Remove(step);

                        this.log.LogData(EventType.Event, LogAction.StepsDeleted);
                        MessageBox.Show("Step(s) are now deleted !");
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            var helpDlg = new HelpForm(this);
            helpDlg.ShowDialog(this);
        }

        private void listViewSourceTarget_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sortListView(listViewSourceTarget, e.Column);
        }

        private void listViewTargetSource_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            sortListView(listViewTargetSource, e.Column);
        }

        public void sortListView(ListView listView, int columnIndex, SortOrder? sort = null)
        {
            if(sort != null)
            {
                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, sort.Value);
            }
            else if (columnIndex == currentColumnOrder)
            {
                listView.Sorting = listView.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, listView.Sorting);
            }
            else
            {
                currentColumnOrder = columnIndex;
                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, SortOrder.Ascending);
            }
        }
    }
}