using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfup.XTBPlugins.AppCode
{
    public class PluginSettings
    {
        public bool? AllowLogUsage { get; set; }
        public string CurrentVersion { get; set; } = DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments.CurrentVersion;
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
        public static string INSIGHTS_INTRUMENTATIONKEY = "INSIGHTS_INTRUMENTATIONKEY_TOREPLACE";
    }

    static class LogAction
    {
        public const string PluginClosed = "PluginClosed";
        public const string StatsAccepted = "StatsAccepted";
        public const string StatsDenied = "StatsDenied";
        public const string SettingsSaved = "SettingsSaved";
        public const string SettingLoaded = "SettingLoaded";
        public const string SolutionsCompared = "SolutionsCompared";
        public const string PluginOpened = "PluginOpened";
        public const string SolutionsLoaded = "SolutionsLoaded";
        public const string CRMAssembliesLoaded = "CRMAssembliesLoaded";
        public const string AssemblyLoaded = "AssemblyLoaded";
        public const string PluginsLoaded = "PluginsLoaded";
        public const string PluginsCompared = "PluginsCompared";
        public const string CanProceed = "CanProceed";
        public const string StepCreatedTargetToSource = "StepCreated (Target To Source)";
        public const string StepCreeatedSourceToTarget = "StepCreated (Source To Target)";
        public const string PluginTypeRetrievedTargetToSource = "PluginTypeRetrieved (Target To Source)";
        public const string SDKMessageRetrievedTargetToSource = "SDKMessageRetrieved (Target To Source)";
        public const string MessageFilterRetrievedTargetToSource = "MessageFilterRetrieved (Target To Source)";
        public const string PluginTypeRetrievedSourceToTarget = "PluginTypeRetrieved (Source To Target)";
        public const string SDKMessageRetrievedSourceToTarget = "SDKMessageRetrieved (Source To Target)";
        public const string MessageFilterRetrievedSourceToTarget = "MessageFilterRetrieved (Source To Target)";
    }

}
