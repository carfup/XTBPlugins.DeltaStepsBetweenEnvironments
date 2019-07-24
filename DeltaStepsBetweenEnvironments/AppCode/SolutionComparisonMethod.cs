using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfup.XTBPlugins.AppCode;
using Microsoft.Xrm.Sdk;
using Source.DLaB.Common;
using Source.DLaB.Xrm;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class SolutionComparisonMethod: IComparisonMethod
    {
        public static IComparisonMethod Instance { get; } = new SolutionComparisonMethod();
        public string Name => "solution";
        public string PluralName => "solutions";
        public string LogActionOnExistsInTarget => LogAction.SolutionExistingInTargetEnvChecked;
        public string LogActionOnCompare => LogAction.SolutionsCompared;
        public string LogActionOnLoadItems => LogAction.SolutionsLoaded;
        public string RequiredPrivilege => "prvReadSolution";
        public bool SolutionSpecified => true;
        public bool RequiresItemSelection => true;

        private SolutionComparisonMethod(){}

        public bool ExistsInTarget(DataManager manager, string solutionAssemblyPluginStepsName)
        {
            return manager.IsSolutionExistingInTargetEnv(solutionAssemblyPluginStepsName) >= 0;
        }

        public List<CarfupStep> GetSteps(IOrganizationService service, PluginSettings settings, string filterName)
        {
            return CarfupStep.GetSteps(service, settings, null, filterName);
        }

        public string[] GetNames(DataManager manager)
        {
            return manager.LoadSolutions();
        }
    }
}
