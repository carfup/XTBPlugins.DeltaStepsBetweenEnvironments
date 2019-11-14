using System.Windows.Forms;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class PluginSettings
    {
        public bool? AllowLogUsage { get; set; }
        //public bool? ShowHelpOnStartUp { get; set; }
        public SortOrder? SortOrderPref { get; set; } = SortOrder.Ascending;
        public bool SkipHiddenSteps { get; set; } = true;
        public string CurrentVersion { get; set; } = DeltaStepsBetweenEnvironments.CurrentVersion;
    }

    static class EventType
    {
        public const string Event = "event";
        public const string Trace = "trace";
        public const string Dependency = "dependency";
        public const string Exception = "exception";
    }

    public static class CustomParameter
    {
        public static string InsightsInstrumentationKey = "INSIGHTS_INTRUMENTATIONKEY_TOREPLACE";
    }

    static class LogAction
    {
        public const string PluginClosed = "PluginClosed";
        public const string StatsAccepted = "StatsAccepted";
        public const string StatsDenied = "StatsDenied";
        public const string SettingsSaved = "SettingsSaved";
        public const string SettingLoaded = "SettingLoaded";
        public const string SolutionsCompared = "SolutionsCompared";
        public const string AssembliesCompared = "AssembliesCompared";
        public const string PluginOpened = "PluginOpened";
        public const string SolutionsLoaded = "SolutionsLoaded";
        public const string AssembliesLoaded = "AssembliesLoaded";
        public const string CrmAssembliesLoaded = "CRMAssembliesLoaded";
        public const string AssemblyLoaded = "AssemblyLoaded";
        public const string PluginsLoaded = "PluginsLoaded";
        public const string PluginsCompared = "PluginsCompared";
        public const string CanProceed = "CanProceed";
        public const string StepCreatedTargetToSource = "StepCreated (Target To Source)";
        public const string StepCreatedSourceToTarget = "StepCreated (Source To Target)";
        public const string PluginTypeRetrievedTargetToSource = "PluginTypeRetrieved (Target To Source)";
        public const string SdkMessageRetrievedTargetToSource = "SDKMessageRetrieved (Target To Source)";
        public const string MessageFilterRetrievedTargetToSource = "MessageFilterRetrieved (Target To Source)";
        public const string PluginTypeRetrievedSourceToTarget = "PluginTypeRetrieved (Source To Target)";
        public const string SdkMessageRetrievedSourceToTarget = "SDKMessageRetrieved (Source To Target)";
        public const string MessageFilterRetrievedSourceToTarget = "MessageFilterRetrieved (Source To Target)";
        public const string SolutionExistingInTargetEnvChecked = "SolutionExistingInTargetEnvChecked";
        public const string AssemblyExistingInTargetEnvChecked = "AssemblyExistingInTargetEnvChecked";
        public const string ShowHelpScreen = "ShowHelpScreen";
        public const string StepsDeleted = "StepsDeleted";
    }
}
