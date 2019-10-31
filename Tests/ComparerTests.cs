using System;
using System.IO;
using System.Linq;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ComparerTests
    {
        [TestMethod]
        public void Compare_EntireOrg_Should_Compare()
        {
            var settings = new PluginSettings();
            var sourceStep = OrgComparisonMethod.Instance.GetSteps(null, settings, null);
            var targetSteps = OrgComparisonMethod.Instance.GetSteps(null, settings, null);
            Comparer.Compare(sourceStep, targetSteps);

            sourceStep.AddRange(targetSteps);

            var csv = sourceStep.OrderBy(s => s.AssemblyName)
                                .ThenBy(s => s.PluginTypeName)
                                .ThenBy(s => s.StepName)
                                .Select(s => s.ToCsv()).ToList();
            csv.Insert(0, CarfupStep.GetCsvColumns());
            File.WriteAllText(@"C:\Temp\UnitTest.csv", string.Join(Environment.NewLine, csv));
        }
    }
}
