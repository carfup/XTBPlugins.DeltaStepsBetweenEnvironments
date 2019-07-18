using System.Collections.Generic;
using Carfup.XTBPlugins.AppCode;
using Microsoft.Xrm.Sdk;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class AssemblyComparisonMethod: IComparisonMethod
    {
        public static IComparisonMethod Instance { get; } = new AssemblyComparisonMethod();

        public string Name => "assembly";
        public string PluralName => "assemblies";
        public string LogActionOnExistsInTarget => LogAction.AssemblyExistingInTargetEnvChecked;
        public string LogActionOnCompare => LogAction.AssembliesCompared;
        public string LogActionOnLoadItems => LogAction.AssembliesLoaded;
        public string RequiredPrivilege => "prvReadPluginAssembly";
        public bool SolutionSpecified => false;

        private AssemblyComparisonMethod(){ }

        public bool ExistsInTarget(DataManager manager, string solutionAssemblyPluginStepsName)
        {
            return manager.isAssemblyExistingInTargetEnv(solutionAssemblyPluginStepsName) >= 0;
        }

        public List<CarfupStep> GetSteps(IOrganizationService service, DataManager manager, string filterName)
        {
            return manager.GetSteps(service, filterName, null);
        }

        public string[] GetNames(DataManager manager)
        {
            return manager.loadAssemblies();
        }
    }
}
