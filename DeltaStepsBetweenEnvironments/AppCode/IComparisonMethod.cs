using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public interface IComparisonMethod
    {
        string Name { get; }      
        string PluralName { get; }
        string LogActionOnExistsInTarget { get; }
        string LogActionOnCompare { get; }
        string LogActionOnLoadItems { get; }
        string RequiredPrivilege { get; }
        bool SolutionSpecified { get; }
        bool RequiresItemSelection { get; }

        bool ExistsInTarget(DataManager manager, string solutionAssemblyPluginStepsName);
        List<CarfupStep> GetSteps(IOrganizationService service, PluginSettings settings, string filterName);
        string[] GetNames(DataManager manager);
    }
}
