using Microsoft.Xrm.Sdk;
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

    enum Comparing {
        Solution, Assembly
    };

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
        public const string AssembliesCompared = "AssembliesCompared";
        public const string PluginOpened = "PluginOpened";
        public const string SolutionsLoaded = "SolutionsLoaded";
        public const string AssembliesLoaded = "AssembliesLoaded";
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
        public const string SolutionExistingInTargetEnvChecked = "SolutionExistingInTargetEnvChecked";
        public const string AssemblyExistingInTargetEnvChecked = "AssemblyExistingInTargetEnvChecked";
        public const string ShowHelp = "ShowHelp";
    }

    static class Wording
    {
        public static string getComparingInfo(Comparing comparing, bool plural = false, bool uppercase = false)
        {
            string value = "solution";
            if(comparing == Comparing.Assembly)
            {
                value = "assembly";
            }

            if(plural)
            {
                if (value == "solution")
                    value = "solutions";
                else
                    value = "assemblies";
            }

            if (uppercase)
                value = value.First().ToString().ToUpper() + value.Substring(1);

            return value;
        }

    }

    // Will be used to implement the step 
    public class CarfupStep
    {
        public string stepName { get; set; }
        public string entityName { get; set; } //messagefilter.primaryobjecttypecode
        public string stepMessageName { get; set; } //sdkmessage.name
        public string plugintypeName { get; set; } // plugintype.name
        public DateTime createOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string stepConfiguration { get; set; }
        public OptionSetValue stepMode { get; set; }
        public int stepRank { get; set; }
        public OptionSetValue stepStage { get; set; }
        public OptionSetValue stepSupporteddeployment { get; set; }
        public OptionSetValue stepInvocationsource { get; set; }
        public string stepFilteringattributes { get; set; }
        public string stepDescription { get; set; }
        public bool stepAsyncautodelete { get; set; }
        public int stepCustomizationlevel { get; set; }
        public Entity entity { get; set; }
    }
}
