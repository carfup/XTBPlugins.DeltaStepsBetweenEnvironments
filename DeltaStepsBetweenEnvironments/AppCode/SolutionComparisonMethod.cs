using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

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
            return manager.SolutionExistsInTargetEnv(solutionAssemblyPluginStepsName);
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
