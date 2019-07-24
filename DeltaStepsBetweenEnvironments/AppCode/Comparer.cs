using System;
using System.Collections.Generic;
using System.Linq;
using Source.DLaB.Common;
using Source.DLaB.Xrm;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public static class Comparer
    {
        public static Differences Compare(List<CarfupStep> stepsCrmSource, List<CarfupStep> stepsCrmTarget)
        {
            var differences = new Differences();
            var sourcePlugins = stepsCrmSource.ToDictionaryList(x => x.AssemblyName);
            var targetPlugins = stepsCrmTarget.ToDictionaryList(x => x.AssemblyName);
            MatchKeys(sourcePlugins, targetPlugins, differences.SourceAssembliesMatched, differences.AssembliesMissingInTarget, ComparisonState.NoAssemblyMatchFound);
            MatchKeys(targetPlugins, sourcePlugins, differences.TargetAssembliesMatched, differences.AssembliesMissingInSource, ComparisonState.NoAssemblyMatchFound);

            foreach (var assembly in differences.SourceAssembliesMatched)
            {
                MatchPlugins(differences, assembly.Value.ToDictionaryList(s => s.PluginFullTypeName), differences.TargetAssembliesMatched[assembly.Key].ToDictionaryList(s => s.PluginFullTypeName));
            }

            foreach (var plugin in differences.SourcePluginsMatched)
            {
                MatchSteps(differences, plugin.Value.ToDictionaryList(s => s.StepId), differences.TargetPluginsMatched[plugin.Key].ToDictionaryList(s => s.StepId));
            }

            foreach (var step in differences.SourceStepsMatched)
            {
                CompareStep(step.Value, differences.TargetStepsMatched[step.Key]);
            }

            return differences;
        }

        private static void MatchPlugins(Differences differences, Dictionary<string, List<CarfupStep>> sourcePlugins, Dictionary<string, List<CarfupStep>> targetPlugins)
        {
            MatchKeys(sourcePlugins, targetPlugins, differences.SourcePluginsMatched, differences.PluginsMissingInTarget, ComparisonState.NoPluginMatchFound);
            MatchKeys(targetPlugins, sourcePlugins, differences.TargetPluginsMatched, differences.PluginsMissingInSource, ComparisonState.NoPluginMatchFound);
        }

        private static void MatchKeys<T>(Dictionary<T, List<CarfupStep>> stepGrouping, Dictionary<T, List<CarfupStep>> otherStepGrouping, Dictionary<T, List<CarfupStep>> matchedDict, Dictionary<T, List<CarfupStep>> missingDict, ComparisonState noMatchFound)
        {
            foreach (var steps in stepGrouping)
            {
                var dict = missingDict;

                if (otherStepGrouping.ContainsKey(steps.Key))
                {
                    dict = matchedDict;
                }
                else
                {
                    foreach (var step in steps.Value)
                    {
                        step.State = noMatchFound;
                    }
                }

                dict.Add(steps.Key, steps.Value);
            }
        }

        private static void MatchSteps(Differences differences, Dictionary<Guid, List<CarfupStep>> sourceSteps, Dictionary<Guid, List<CarfupStep>> targetSteps)
        {
            MatchKeys(sourceSteps, targetSteps, differences.SourceStepsMatched, differences.StepsMissingInTarget, ComparisonState.NoPluginStepMatchFound);
            MatchKeys(targetSteps, sourceSteps, differences.TargetStepsMatched, differences.StepsMissingInSource, ComparisonState.NoPluginStepMatchFound);
        }

        private static void CompareImages(Dictionary<string, CarfupStep> steps, Dictionary<string, CarfupStep> otherSteps)
        {
            foreach (var step in steps)
            {
                if (otherSteps.TryGetValue(step.Key, out var image))
                {
                    var state = step.Value.ImageName == image.ImageName
                                && step.Value.ImageAlias == image.ImageAlias
                                && step.Value.ImageAttributes == image.ImageAttributes
                                && step.Value.ImageType == image.ImageType
                        ? ComparisonState.Match
                        : ComparisonState.Different;
                    step.Value.State = state;
                    image.State = state;
                }
                else
                {
                    step.Value.State = ComparisonState.NoImageMatchFound;
                }
            }
        }

        private static void CompareStep(List<CarfupStep> sources, List<CarfupStep> targets)
        {
            var sourceImages = sources.ToDictionary(s => s.ImageAlias ?? string.Empty);
            var targetImages = targets.ToDictionary(s => s.ImageAlias ?? string.Empty);
            CompareImages(sourceImages, targetImages);
            CompareImages(targetImages, sourceImages);

            var source = sources.FirstOrDefault(s => s.State == ComparisonState.Match);
            if (source == null)
            {
                // None matched or all where different. Nothing more to check
                return;
            }
            var target = targets.First(s => s.State == ComparisonState.Match);
            var state = source.StepAsyncAutoDelete == target.StepAsyncAutoDelete
                && source.EntityName == target.EntityName
                && source.PluginTypeName == target.PluginTypeName
                // && source.RunAsUserId == target.RunAsUserId
                && source.RunAsUserName == target.RunAsUserName
                && source.StepConfiguration == target.StepConfiguration
                && source.StepCustomizationLevel == target.StepCustomizationLevel
                && source.StepDescription == target.StepDescription
                && source.StepFilteringAttributes == target.StepFilteringAttributes
                && source.StepInvocationSource.GetValueOrDefault() == target.StepInvocationSource.GetValueOrDefault()
                && source.StepMessageName == target.StepMessageName
                && source.StepMode == target.StepMode
                && source.StepName == target.StepName
                && source.StepRank == target.StepRank
                && source.StepStage == target.StepStage
                && source.StepSupportedDeployment == target.StepSupportedDeployment
                && source.StepSecureConfiguration == target.StepSecureConfiguration
                ? ComparisonState.Match
                : ComparisonState.Different;

            if (state == ComparisonState.Different)
            {
                // Update all that were matched
                UpdateAllMatchedToDifferent(sources);
                UpdateAllMatchedToDifferent(targets);
            }
        }

        private static void UpdateAllMatchedToDifferent(List<CarfupStep> sources)
        {
            foreach (var step in sources.Where(s => s.State == ComparisonState.Match))
            {
                step.State = ComparisonState.Different;
            }
        }
    }
}
