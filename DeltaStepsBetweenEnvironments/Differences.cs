using System;
using System.Collections.Generic;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments
{
    public class Differences
    {
        public Dictionary<string,List<CarfupStep>> AssembliesMissingInSource {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> AssembliesMissingInTarget {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> SourceAssembliesMatched   {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> TargetAssembliesMatched   {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> PluginsMissingInSource    {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> PluginsMissingInTarget    {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> SourcePluginsMatched      {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<string,List<CarfupStep>> TargetPluginsMatched      {get;} = new Dictionary<string, List<CarfupStep>>();
        public Dictionary<Guid,List<CarfupStep>>   StepsMissingInSource      {get;} = new Dictionary<Guid, List<CarfupStep>>();
        public Dictionary<Guid,List<CarfupStep>>   StepsMissingInTarget      {get;} = new Dictionary<Guid, List<CarfupStep>>();
        public Dictionary<Guid,List<CarfupStep>>   SourceStepsMatched        {get;} = new Dictionary<Guid, List<CarfupStep>>();
        public Dictionary<Guid,List<CarfupStep>>   TargetStepsMatched        {get;} = new Dictionary<Guid, List<CarfupStep>>();

        public Differences()
        {
            
        }
    }
}
