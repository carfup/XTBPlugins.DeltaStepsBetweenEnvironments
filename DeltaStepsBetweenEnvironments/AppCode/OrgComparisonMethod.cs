using System;
using System.Collections.Generic;
using System.IO;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
using Carfup.XTBPlugins.Entities;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Common;
using Source.DLaB.Xrm;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class OrgComparisonMethod: IComparisonMethod
    {
        public static IComparisonMethod Instance { get; } = new OrgComparisonMethod();
        public string Name => "organization";
        public string PluralName => "organizations";
        public string LogActionOnExistsInTarget => LogAction.SolutionExistingInTargetEnvChecked;
        public string LogActionOnCompare => LogAction.SolutionsCompared;
        public string LogActionOnLoadItems => LogAction.SolutionsLoaded;
        public string RequiredPrivilege => "prvReadSolution";
        public bool SolutionSpecified => false;
        public bool RequiresItemSelection => false;

        private OrgComparisonMethod(){}

        public bool ExistsInTarget(DataManager manager, string solutionAssemblyPluginStepsName)
        {
            return true;
        }

        private string _org = "Source";
        public List<CarfupStep> GetSteps(IOrganizationService service, PluginSettings settings, string filterName)
        {
            var org = _org;
            var steps = new List<CarfupStep>();
            _org = _org == "Source" ? "Target" : "Source";
            if (service == null)
            {
                foreach(var file in Directory.GetFiles(@"C:\Temp\XML\" + org + @"\"))
                {
                    steps.Add(new CarfupStep(File.ReadAllText(file).DeserializeEntity().ToEntity<PluginType>(), org));
                }

                return steps;
            }
            steps = CarfupStep.GetSteps(service, settings, null, null);
            if (Config.GetAppSettingOrDefault("SavePluginStepsForUnitTesting", false))
            {
                foreach (var step in steps)
                {
                    var id = step.StepId.ToString();
                    if (step.ImageId != Guid.Empty)
                    {
                        id += "." + step.ImageId;
                    }
                    File.WriteAllText(@"C:\Temp\XML\" + org + @"\" + id + @".xml", step.Plugin.Serialize());
                }
            }

            return steps;
        }

        public string[] GetNames(DataManager manager)
        {
            return new string[0];
        }
    }
}
