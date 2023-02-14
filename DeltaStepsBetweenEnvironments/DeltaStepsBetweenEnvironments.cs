using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using Carfup.XTBPlugins.Forms;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using XrmToolBox.Extensibility.Args;
using Label = Microsoft.Xrm.Sdk.Label;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Forms;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public partial class DeltaStepsBetweenEnvironments : PluginControlBase, IXrmToolBoxPluginControl, IGitHubPlugin, IStatusBarMessenger
    {
        #region Properties

        private IOrganizationService SourceService { get; set; }
        private IOrganizationService TargetService { get; set; }
        private OrganizationData SourceOrgData { get; set; }
        private OrganizationData TargetOrgData { get; set; }
        private List<CarfupStep> StepsCrmSource { get; set; }
        private List<CarfupStep> StepsCrmTarget { get; set; }
        private static string SolutionAssemblyPluginStepsName { get; set; }
        public PluginSettings Settings {get; set; } = new PluginSettings();
        public LogUsage Log { get;set; }
        private IComparisonMethod ComparisonMethod { get; set; }
        private ControllerManager Controller { get; set; }
        private int CurrentColumnOrder { get; set; }
        public event EventHandler OnRequestConnection; // Should this be an override?  Is this even needed?
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;
        public string compareBy = CompareBy.Guid;

        public string RepositoryName =>  "XTBPlugins.DeltaStepsBetweenEnvironments";
        public string UserName => "carfup";

        #endregion Properties

        public DeltaStepsBetweenEnvironments()
        {
            InitializeComponent();
            SourceService = Service;
            Controller = new ControllerManager(Service, Service)
            {
                Source = ConnectionDetail,
                Target = ConnectionDetail
            };
            ComparisonMethod = OrgComparisonMethod.Instance;
            StepsCrmSource = new List<CarfupStep>();
            StepsCrmTarget = new List<CarfupStep>();
            buttonCompare.Visible = false;
            ManageRadioButtonsAssemblySolution();
        }

        private void toolStripButtonClose_Click(object sender, EventArgs e)
        {
            Log.LogData(EventType.Event, LogAction.PluginClosed);

            // Saving settings for the next usage of plugin
            SaveSettings();

            // Making sure that all message are sent if stats are enabled
            Log.Flush();
            CloseTool();
        }
        private void ToolStripButtonExport_Click(object sender, EventArgs e)
        {
            var fileName = (Controller?.Source?.ConnectionName ?? string.Empty).Replace(" ", "") + "To" + (Controller?.Target?.ConnectionName ?? string.Empty).Replace(" ", "") + ".csv";
            if (!string.IsNullOrWhiteSpace(saveFileDialog1.FileName))
            {
                var directory = Path.GetDirectoryName(saveFileDialog1.FileName);
                fileName = Path.Combine(directory, fileName);
            }

            saveFileDialog1.FileName = fileName;

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var file = saveFileDialog1.FileName;
            var export = new List<CarfupStep>();
            export.AddRange(StepsCrmSource);
            export.AddRange(StepsCrmTarget);

            var csv = export.OrderBy(s => s.AssemblyName)
                                .ThenBy(s => s.PluginTypeName)
                                .ThenBy(s => s.StepName)
                                .Select(s => s.ToCsv()).ToList();
            csv.Insert(0, CarfupStep.GetCsvColumns());
            File.WriteAllText(file, string.Join(Environment.NewLine, csv));

            if (DialogResult.Yes == MessageBox.Show("Do you want to open the generated document?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Process.Start(file);
            }
        }

        // We compare the same solution name in both environments
        private void buttonCompare_Click(object sender, EventArgs evt)
        {
            Compare();
        }

        private void Compare()
        {
            if (ComparisonMethod.RequiresItemSelection 
                && SolutionAssemblyPluginStepsName == null)
            {
                MessageBox.Show($@"Please select a {ComparisonMethod.Name} first.");
                return;
            }

            Guid[] diffCrmSourceTarget = null;
            Guid[] diffCrmTargetSource = null;

            StepsCrmSource.Clear();
            StepsCrmTarget.Clear();

            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Comparing the 2 {ComparisonMethod.PluralName.Capitalize()}...",
                Work = (bw, e) =>
                {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, "Fetching steps from source environment..."));
                    StepsCrmSource = ComparisonMethod.GetSteps(SourceService, Settings, SolutionAssemblyPluginStepsName);
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(30, "Fetching steps from target environment..."));
                    StepsCrmTarget = ComparisonMethod.GetSteps(TargetService, Settings, SolutionAssemblyPluginStepsName);
                    foreach(var step in StepsCrmTarget)
                    {
                        step.Environment = "Target";
                    }

                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(60, "Comparing steps..."));

                    Comparer.Compare(StepsCrmSource, StepsCrmTarget);
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(90, "Finding Differences..."));

                    if(compareBy == CompareBy.Guid)
                    {
                        diffCrmSourceTarget = StepsCrmSource.Select(x => x.StepId)
                            .Except(StepsCrmTarget.Select(x => x.StepId)).ToArray();
                        diffCrmTargetSource = StepsCrmTarget.Select(x => x.StepId)
                            .Except(StepsCrmSource.Select(x => x.StepId)).ToArray();
                    }
                    else if (compareBy == CompareBy.Name)
                    {
                        diffCrmSourceTarget = StepsCrmSource.Where(xx => (StepsCrmSource.Select(x => x.StepName)
                            .Except(StepsCrmTarget.Select(x => x.StepName))).Contains(xx.StepName)).Select(x => x.StepId).ToArray();
                        diffCrmTargetSource = StepsCrmTarget.Where(xx => (StepsCrmTarget.Select(x => x.StepName)
                            .Except(StepsCrmSource.Select(x => x.StepName))).Contains(xx.StepName)).Select(x => x.StepId).ToArray();
                    }
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(100, "Done!"));
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        Log.LogData(EventType.Exception, ComparisonMethod.LogActionOnCompare, e.Error);
                        MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        toolStripButtonExport.Enabled = false;
                        return;
                    }

                    toolStripButtonExport.Enabled = true;

                    if (diffCrmSourceTarget.Length == 0)
                    {
                        listViewSourceTarget.Items.Clear();
                        labelSourceTargetMatch.Visible = true;

                    }
                    else // there are steps in source but not target
                    {
                        labelSourceTargetMatch.Visible = false;
                        listViewSourceTarget.Visible = true;
                        FillListViewItems(listViewSourceTarget, StepsCrmSource, diffCrmSourceTarget);
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
                        FillListViewItems(listViewTargetSource, StepsCrmTarget, diffCrmTargetSource);
                    }

                    Log.LogData(EventType.Event, ComparisonMethod.LogActionOnCompare);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                }
            });
        }

        #region Source/Target Selection

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail connectionDetail, string actionName, object parameter)
        {
            toolStripButtonExport.Enabled = false;
            if (actionName == "TargetOrganization")
            {
                TargetService = newService;
                SetConnectionLabel(connectionDetail, labelTargetEnvironment);
                Controller.TargetService = TargetService;
                Controller.Target = connectionDetail;
                TargetOrgData = new OrganizationData();
                LoadOrganizationData(TargetService, TargetOrgData);
            }
            else
            {
                SourceService = newService;
                SetConnectionLabel(connectionDetail, labelSourceEnvironment);
                Controller.SourceService = SourceService;
                Controller.Source = connectionDetail;
                SourceOrgData = new OrganizationData();
                LoadOrganizationData(SourceService, SourceOrgData);
            }

            buttonCompare.Visible = TargetService != null && SourceService != null;
            checkBoxCompareByName.Visible = buttonCompare.Visible;
            checkBoxCompareByGuid.Visible = buttonCompare.Visible;
        }

        private void LoadOrganizationData(IOrganizationService service,  OrganizationData orgData)
        {
            orgData.cMessages = Controller.DataManager.GetSdkMessages(service);
            orgData.cUsers = Controller.DataManager.GetUsers(service);
        }

        private void SetConnectionLabel(ConnectionDetail detail, System.Windows.Forms.Label label)
        {
            label.Text = detail.ConnectionName;
            label.ForeColor = Color.Green;
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
        public bool CanProceed()
        {
            if (SourceService == null || TargetService == null)
            {
                Log.LogData(EventType.Event, LogAction.CanProceed);
                MessageBox.Show(@"Make sure you are connected to a Source AND Target environments first.");
                return false;
            }

            return true;
        }

        #endregion Source/Target Selection

        // Copying a step from the target to source environment
        private void buttonCopyTargetToSource_Click(object sender, EventArgs evt)
        {
            CreateStepProcess(listViewTargetSource, StepsCrmTarget, SourceService);
        }

        // Copying a step from the source to target environment
        private void buttonCopySourceToTarget_Click(object sender, EventArgs evt)
        {
            CreateStepProcess(listViewSourceTarget, StepsCrmSource, TargetService);
        }

        public void CreateStepProcess(ListView listView, List<CarfupStep> stepsList, IOrganizationService service)
        {
            var addToSolution = false;
            var itemCount = listView.CheckedItems.Count;

            if (itemCount < 1)
            {
                MessageBox.Show(@"Make sure you checked at least one step before trying to perform a Copy action.", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ComparisonMethod.SolutionSpecified)
            {
                var whichDestination = MessageBox.Show(@"Since you have specified a solution, do you want to copy the step to the specified solution?\r\rYes : Custom Solution.\rNo : Default Solution.\rCancel : Abort operation.", @"Which destination do you want?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (whichDestination == DialogResult.Yes)
                    addToSolution = true;
                else if (whichDestination == DialogResult.Cancel)
                    return;
            }

            // Getting list of selected Items
            var stepsGuid = new ListViewItem[itemCount];
            var logAction = (stepsList == StepsCrmSource) ? LogAction.StepCreatedSourceToTarget : LogAction.StepCreatedTargetToSource;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating the step(s) in the environment...",
                Work = (bw, e) =>
                {
                    Invoke(new Action(() =>
                    {
                        listView.CheckedItems.CopyTo(stepsGuid, 0);
                    }));

                    var i = 1;
                    foreach (var itemView in stepsGuid)
                    {
                        var percentage = (i * 100) / itemCount;
                        SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(percentage, $"Creating the step(s) in the environment : {i}/{itemCount}"));
                        var selectedStep = stepsList.FirstOrDefault(x => x.StepName == itemView.Text);
                        var stepCreated = CreateStep(selectedStep, (stepsList == StepsCrmSource));

                        if (stepCreated == null)
                            return;

                        if (addToSolution)
                        {
                            bw.ReportProgress(percentage, $"Adding the new created step to the {SolutionAssemblyPluginStepsName} solution...");

                            service.Execute(new AddSolutionComponentRequest
                            {
                                ComponentId = stepCreated.Value,
                                ComponentType = 92,
                                SolutionUniqueName = SolutionAssemblyPluginStepsName
                            });
                        }

                        i++;
                    }
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        Log.LogData(EventType.Exception, logAction, e.Error);
                        MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Log.LogData(EventType.Event, logAction);
                    MessageBox.Show($@"Your step(s) were successfully copied to the {(addToSolution ? SolutionAssemblyPluginStepsName : "default")} solution in the {((logAction == LogAction.StepCreatedSourceToTarget) ? "target" : "source")} environment.");
                    labelSourceTargetMatch.Visible = false;
                    labelTargetSourceMatch.Visible = false;

                    Compare();
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });

            
        }

        public Guid? CreateStep(CarfupStep selectedStep, bool toTarget = true)
        {
            var pluginTypeRetrievedLogAction = (toTarget) ? LogAction.PluginTypeRetrievedSourceToTarget : LogAction.PluginTypeRetrievedTargetToSource;
            var sdkMessageRetrievedLogAction = (toTarget) ? LogAction.SdkMessageRetrievedSourceToTarget : LogAction.SdkMessageRetrievedTargetToSource;
            var messageFilterRetrievedLogAction = (toTarget) ? LogAction.MessageFilterRetrievedSourceToTarget : LogAction.MessageFilterRetrievedTargetToSource;
            var service = (toTarget) ? TargetService : SourceService;
            var serviceFrom = (toTarget) ? SourceService : TargetService;

            // retrieving the 3 data mandatory to have a proper step created
            var pluginTypeName = selectedStep.PluginTypeName == null
                ? Controller.DataManager.GetPluginTypeName(serviceFrom, selectedStep.Plugin.Id).GetAttributeValue<string>("name")
                : selectedStep.PluginTypeName;
            var pluginType = Controller.DataManager.GetPluginType(service, pluginTypeName);
            var sdkMessage = Controller.DataManager.GetSdkMessage(service, selectedStep.StepMessageName);
            var messageFilter = Controller.DataManager.GetMessageFilter(service, selectedStep.EntityName);
            


            //if (pluginType == null)
            //{
            //    Log.LogData(EventType.Exception, pluginTypeRetrievedLogAction);
            //    MessageBox.Show(@"Sorry, but we didn't find the necessary Plugin Type information in the destination system...\r\rThis can occur because you didn't load the same assembly in the destination system.");
            //    return null;
            //}

            if (sdkMessage == null)
            {
                Log.LogData(EventType.Exception, sdkMessageRetrievedLogAction);
                MessageBox.Show(@"Sorry, but we didn't find the necessary SDK Message information in the destination system...");
                return null;
            }

            if (messageFilter == null)
            {
                Log.LogData(EventType.Exception, messageFilterRetrievedLogAction);
                MessageBox.Show(@"Sorry, but we didn't find the necessary Message Filter information in the destination system...");
                return null;
            }


            var sdkMessageImage = Controller.DataManager.GetSdkMessageProcessingStepImage(serviceFrom, selectedStep.StepId);

            Log.LogData(EventType.Event, pluginTypeRetrievedLogAction);
            Log.LogData(EventType.Event, sdkMessageRetrievedLogAction);
            Log.LogData(EventType.Event, messageFilterRetrievedLogAction);

            // Preparing the object step
            Entity newStepToCreate = new Entity("sdkmessageprocessingstep");
            newStepToCreate["plugintypeid"] = pluginType.ToEntityReference();
            newStepToCreate["sdkmessageid"] = sdkMessage.ToEntityReference();
            newStepToCreate["sdkmessagefilterid"] = messageFilter.ToEntityReference();
            newStepToCreate["name"] = selectedStep.StepName;
            newStepToCreate["mode"] = new OptionSetValue((int)selectedStep.StepMode);
            newStepToCreate["rank"] = selectedStep.StepRank;
            newStepToCreate["stage"] = new OptionSetValue((int)selectedStep.StepStage);
            newStepToCreate["supporteddeployment"] = new OptionSetValue((int)selectedStep.StepSupportedDeployment);
            newStepToCreate["invocationsource"] = selectedStep.StepInvocationSource;
            newStepToCreate["configuration"] = selectedStep.StepConfiguration;
            newStepToCreate["filteringattributes"] = selectedStep.StepFilteringAttributes;
            newStepToCreate["description"] = selectedStep.StepDescription;
            newStepToCreate["asyncautodelete"] = selectedStep.StepAsyncAutoDelete;
            newStepToCreate["customizationlevel"] = selectedStep.StepCustomizationLevel;

            var  createdStep = service.Create(newStepToCreate);

            if (sdkMessageImage != null)
            {
                Entity newStepImageToCreate = new Entity("sdkmessageprocessingstepimage");
                newStepImageToCreate["messagepropertyname"] = sdkMessageImage.GetAttributeValue<string>("messagepropertyname");
                newStepImageToCreate["customizationlevel"] = sdkMessageImage.GetAttributeValue<int?>("customizationlevel");
                newStepImageToCreate["ismanaged"] = sdkMessageImage.GetAttributeValue<bool?>("ismanaged");
                newStepImageToCreate["imagetype"] = sdkMessageImage.GetAttributeValue<OptionSetValue>("imagetype");
                newStepImageToCreate["name"] = sdkMessageImage.GetAttributeValue<string>("name");
                newStepImageToCreate["entityalias"] = sdkMessageImage.GetAttributeValue<string>("entityalias");
                newStepImageToCreate["attributes"] = sdkMessageImage.GetAttributeValue<string>("attributes");
                newStepImageToCreate["description"] = sdkMessageImage.GetAttributeValue<string>("description");
                newStepImageToCreate["relatedattributename"] = sdkMessageImage.GetAttributeValue<string>("relatedattributename");
                newStepImageToCreate["sdkmessageprocessingstepid"] = new EntityReference("sdkmessageprocessingstep", createdStep);

                service.Create(newStepImageToCreate);
            }

            return createdStep;
        }

        #region Log/Settings

        /// <summary>
        /// Action when the option form is opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOptions_Click(object sender, EventArgs e)
        {
            var allowLogUsage = Settings.AllowLogUsage;            
            var optionDlg = new Options(this);
            if (optionDlg.ShowDialog(this) == DialogResult.OK)
            {
                Settings = optionDlg.GetSettings();
                if (allowLogUsage != Settings.AllowLogUsage)
                {
                    if (Settings.AllowLogUsage == true)
                    {
                        Log.updateForceLog();
                        Log.LogData(EventType.Event, LogAction.StatsAccepted);
                    }
                    else if (!Settings.AllowLogUsage == true)
                    {
                        Log.updateForceLog();
                        Log.LogData(EventType.Event, LogAction.StatsDenied);
                    }
                }
            }
        }

        /// <summary>
        /// Saves personal settings
        /// </summary>
        public void SaveSettings()
        {
            Log.LogData(EventType.Event, LogAction.SettingsSaved);
            SettingsManager.Instance.Save(typeof(DeltaStepsBetweenEnvironments), Settings);

            //reordering columns if necessary
            SortListView(listViewSourceTarget, 0, Settings.SortOrderPref);
            SortListView(listViewTargetSource, 0, Settings.SortOrderPref);
        }

        private void DeltaStepsBetweenEnvironments_Load(object sender, EventArgs e)
        {
            // initializing log class
            Log = new LogUsage(this);
            Log.LogData(EventType.Event, LogAction.PluginOpened);
            LoadSetting();
        }

        // either loading previous settings from user or creating default ones and prompt the messagae for  the stats
        private void LoadSetting()
        {
            try
            {
                Settings = SettingsManager.Instance.TryLoad<PluginSettings>(typeof(DeltaStepsBetweenEnvironments), out var settings) 
                    ? settings 
                    : new PluginSettings();
            }
            catch (InvalidOperationException ex) {
                Log.LogData(EventType.Exception, LogAction.SettingLoaded, ex);
            }

            Log.LogData(EventType.Event, LogAction.SettingLoaded);

            if (!Settings.AllowLogUsage.HasValue)
            {
                Log.PromptToLog();
                SaveSettings();
            }
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

        #endregion Log/Settings

        #region Details GroupBox

        /// <summary>
        /// Select the solution from where we will query the steps
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxSolutionsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanProceed())
            {
                if (ComparisonMethod.RequiresItemSelection)
                {
                    if(comboBoxSolutionsAssembliesList.SelectedItem != null)
                    {
                        SolutionAssemblyPluginStepsName = comboBoxSolutionsAssembliesList.SelectedItem.ToString();

                        IsSolutionOrAssemblyExistingInTargetEnv();
                    }
                }
            }
        }

        private void IsSolutionOrAssemblyExistingInTargetEnv()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Checking if the {ComparisonMethod.Name} name exists in the target environment...",
                Work = (bw, e) =>
                {
                    e.Result = ComparisonMethod.ExistsInTarget(Controller.DataManager, SolutionAssemblyPluginStepsName);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        Log.LogData(EventType.Exception, ComparisonMethod.LogActionOnExistsInTarget, e.Error);
                        MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!((bool) e.Result))
                    {
                        MessageBox.Show($@"The {ComparisonMethod.Name} doesn't exist in the Target environment. \rThe compare function will return a ""Perfect match"" in this case.\r\r You will still have the possibility to copy steps from the Source to Target environment.", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        /// <summary>
        /// Loading solutions from the Source environment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="evt"></param>
        private void buttonLoadSolutionsAssemblies_Click(object sender, EventArgs evt)
        {
            if (!CanProceed())
            {
                return;
            }

            LoadItems();
        }

        private void LoadItems()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = $"Loading CRM {ComparisonMethod.PluralName.Capitalize()}...",
                Work = (bw, e) =>
                {
                    if (!Controller.DataManager.UserHasPrivilege(ComparisonMethod.RequiredPrivilege, Controller.DataManager.WhoAmI()))
                    {
                        MessageBox.Show($@"Make sure your user has the '{ComparisonMethod.RequiredPrivilege}' privilege to load the {ComparisonMethod.PluralName}.{Environment.NewLine}Aborting action.");
                        return;
                    }
                    e.Result = ComparisonMethod.GetNames(Controller.DataManager);
                },

                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        Log.LogData(EventType.Exception, ComparisonMethod.LogActionOnLoadItems, e.Error);
                        MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var values = (string[])e.Result;

                    comboBoxSolutionsAssembliesList.Items.Clear();
                    Log.LogData(EventType.Event, $"{ComparisonMethod.PluralName.Capitalize()} retrieved");
                    if (values != null)
                    {
                        comboBoxSolutionsAssembliesList.Items.AddRange(values.Cast<object>().ToArray());
                    }

                    Log.LogData(EventType.Event, ComparisonMethod.LogActionOnLoadItems);
                },
                ProgressChanged = e => { SetWorkingMessage(e.UserState.ToString()); }
            });
        }

        private void radioButtonCompareSolution_Click(object sender, EventArgs e)
        {
            ComparisonMethod = SolutionComparisonMethod.Instance;
            ManageRadioButtonsAssemblySolution();
        }

        private void radioButtonCompareAssembly_Click(object sender, EventArgs e)
        {
            ComparisonMethod = AssemblyComparisonMethod.Instance;
            ManageRadioButtonsAssemblySolution();
        }

        private void RadioButtonCompareOrg_CheckedChanged(object sender, EventArgs e)
        {
            ComparisonMethod = OrgComparisonMethod.Instance;
            ManageRadioButtonsAssemblySolution();
        }

        private void ManageRadioButtonsAssemblySolution()
        {
            if (ComparisonMethod.RequiresItemSelection)
            {

                buttonLoadSolutionsAssemblies.Text = @"Load " + ComparisonMethod.PluralName.Capitalize();
                labelComparing.Text = $@"Select the {ComparisonMethod.Name} to compare: ";

                comboBoxSolutionsAssembliesList.SelectedIndex = -1;
                comboBoxSolutionsAssembliesList.Items.Clear();
            }
            var visible = ComparisonMethod.RequiresItemSelection;
            labelComparing.Visible = visible;
            comboBoxSolutionsAssembliesList.Visible = visible;
            buttonLoadSolutionsAssemblies.Visible = visible;
        }

        #endregion Details GroupBox

        private void FillListViewItems(ListView listView, List<CarfupStep> stepsList, Guid[] diff)
        {
            listView.Items.Clear();

            var imgList = new ImageList();
            imgList.Images.Add("imgDetails",new Bitmap(Properties.Resources.details));


            foreach (var step in stepsList.Where(x => diff.Contains(x.StepId)))
            {
                string createon = step.CreateOn.ToLocalTime().ToString("dd-MMM-yyyy HH:mm");
                string modifiedon = step.ModifiedOn.ToLocalTime().ToString("dd-MMM-yyyy HH:mm");
                string isManaged = step.IsManaged ? "X" : "";

                var item = new ListViewItem(new string[] {step.StepName, step.EntityName, step.StepMessageName, createon, modifiedon, isManaged }, "imgDetails");
                //item.ImageKey = "imgDetails";
                //item.Text = step.StepName;
                //item.SubItems.Add(step.EntityName);
                //item.SubItems.Add(step.StepMessageName);
                //item.SubItems.Add(createon);
                //item.SubItems.Add(modifiedon);
                item.Tag = step.StepId;

                listView.Items.Add((ListViewItem)item.Clone());
            }
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonDeleteStep_Click(object sender, EventArgs evt)
        {
            if (listViewTargetSource.CheckedItems.Count == 0 && listViewSourceTarget.CheckedItems.Count == 0)
            {
                MessageBox.Show(@"Make sure you checked at least one step before trying to perform a Delete action.", @"Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // assuming selection was made on SourceToTarget
            IOrganizationService service = SourceService;
            ListView listViewToProceed = listViewSourceTarget;
            

            if (listViewSourceTarget.CheckedItems.Count == 0)
            {
                listViewToProceed = listViewTargetSource;
                service = TargetService;
            }

            // Getting list of selected Items
            ListViewItem[] stepsGuid = new ListViewItem[listViewToProceed.CheckedItems.Count];


            var areYouSure = MessageBox.Show($@"Do you really want to delete the step(s) ? {Environment.NewLine} You won't be able to get it back after that.", @"Warning !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        Log.LogData(EventType.Exception, LogAction.StepsDeleted, e.Error);
                        MessageBox.Show(this, e.Error.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (ListViewItem step in stepsGuid.ToList())
                    {
                        listViewToProceed.Items.Remove(step);
                    }

                    Log.LogData(EventType.Event, LogAction.StepsDeleted);
                    MessageBox.Show(@"Step(s) are now deleted !");

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
            SortListView(listViewSourceTarget, e.Column);
        }

        private void listViewTargetSource_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortListView(listViewTargetSource, e.Column);
        }

        public void SortListView(ListView listView, int columnIndex, SortOrder? sort = null)
        {
            if(sort != null)
            {
                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, sort.Value);
            }
            else if (columnIndex == CurrentColumnOrder)
            {
                listView.Sorting = listView.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;

                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, listView.Sorting);
            }
            else
            {
                CurrentColumnOrder = columnIndex;
                listView.ListViewItemSorter = new ListViewItemComparer(columnIndex, SortOrder.Ascending);
            }
        }

        private void ListViewSourceTarget_DoubleClick(object sender, EventArgs e)
        {
            var itemFrom = ((ListView)sender).FocusedItem.Tag.ToString();
            var step = StepsCrmSource.Where(x => itemFrom == x.StepId.ToString()).FirstOrDefault();

            if (step == null)
                return;

            var stepDetails = new StepDiffDetails(step);
            stepDetails.Show();
        }

        private void ListViewSourceTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            var check = listViewSourceTarget.SelectedItems.Count == 1;
        }


        private void ListViewTargetSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            var check = listViewTargetSource.SelectedItems.Count == 1;
        }

        private void checkBoxCompareByGuid_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCompareByName.Checked = !checkBoxCompareByGuid.Checked;
            compareBy = CompareBy.Guid;
        }

        private void checkBoxCompareByName_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxCompareByGuid.Checked = !checkBoxCompareByName.Checked;
            compareBy = CompareBy.Name;
        }

        private void listViewTargetSource_DoubleClick(object sender, EventArgs e)
        {
            var itemFrom = ((ListView)sender).FocusedItem.Tag.ToString();
            var step = StepsCrmTarget.Where(x => itemFrom == x.StepId.ToString()).FirstOrDefault();

            if (step == null)
                return;

            var stepDetails = new StepDiffDetails(step);
            stepDetails.Show();
        }
    }
}