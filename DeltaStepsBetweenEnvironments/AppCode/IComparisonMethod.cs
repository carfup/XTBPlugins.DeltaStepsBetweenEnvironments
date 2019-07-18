using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfup.XTBPlugins.AppCode;
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

        bool ExistsInTarget(DataManager manager, string solutionAssemblyPluginStepsName);
        List<CarfupStep> GetSteps( IOrganizationService service, DataManager manager, string filterName);
        string[] GetNames(DataManager manager);
    }
}
