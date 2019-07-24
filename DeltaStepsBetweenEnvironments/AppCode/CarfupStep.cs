using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Documents;
using Carfup.XTBPlugins.AppCode;
using Carfup.XTBPlugins.Entities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Source.DLaB.Xrm;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public enum ComparisonState
    {
        Match,
        NoAssemblyMatchFound,
        NoPluginMatchFound,
        NoPluginStepMatchFound,
        NoImageMatchFound,
        Different
    }
    [DebuggerDisplay("{PluginTypeName} {StepMessageName} {EntityName}")]
    public class CarfupStep
    {
        public DateTime CreateOn => Step.CreatedOn.GetValueOrDefault();
        public DateTime ModifiedOn => Step.ModifiedOn.GetValueOrDefault();
        public Guid StepId => Step.Id;
        public Guid AssemblyId => Plugin.PluginAssemblyId.Id;
        public Guid PluginTypeId => Plugin.Id;
        [Obsolete]
        public OptionSetValue StepInvocationSource => Step.InvocationSource;

        public bool StepAsyncAutoDelete => Step.AsyncAutoDelete == true;
        public int StepCustomizationLevel => Step.CustomizationLevel.GetValueOrDefault();
        public int StepRank => Step.Rank.GetValueOrDefault(1);
        public string AssemblyName => Plugin.AssemblyName;
        public string EntityName => string.IsNullOrWhiteSpace(Filter.PrimaryObjectTypeCode) ? "none" : Filter.PrimaryObjectTypeCode;
        public string Environment { get; set; }
        public string PluginTypeName => Plugin.Name;
        public string PluginFullTypeName => AssemblyName + "|" + PluginTypeName;
        public string StepConfiguration => Step.Configuration;
        public string StepDescription => Step.Description;
        public string StepFilteringAttributes => Step.FilteringAttributes;
        public string StepMessageName => Step.SdkMessageId?.Name;
        public string StepName => Step.Name;
        public SdkMessageProcessingStep_Mode StepMode => Step.ModeEnum.GetValueOrDefault();
        public string StepModeName => Plugin.GetFormattedAttributeValueOrNull(SdkMessageProcessingStep.Fields.Mode);
        public SdkMessageProcessingStep_Stage StepStage => Step.StageEnum.GetValueOrDefault();
        public string StepStageName => Plugin.GetFormattedAttributeValueOrNull(SdkMessageProcessingStep.Fields.Stage);
        public SdkMessageProcessingStep_SupportedDeployment StepSupportedDeployment => Step.SupportedDeploymentEnum.GetValueOrDefault();
        public string StepSupportedDeploymentName => Plugin.GetFormattedAttributeValueOrNull(SdkMessageProcessingStep.Fields.SupportedDeployment);
        public string StepSecureConfiguration => SecureConfig.SecureConfig;
        public string RunAsUserName => Step.ImpersonatingUserId == null ? "Calling User" : Step.ImpersonatingUserId?.Name;
        public string ImageAttributes => Image.Attributes1;
        public string ImageAlias => Image.EntityAlias;
        public Guid ImageId => Image.Id;
        public string ImageName => Image.Name;
        public string ImageType => Plugin.GetFormattedAttributeValueOrNull(SdkMessageProcessingStepImage.Fields.ImageType);
        public Guid RunAsUserId => Step.ImpersonatingUserId.GetIdOrDefault();
        public ComparisonState State { get; set; }

        public SdkMessageFilter Filter { get; }
        public SdkMessageProcessingStepImage Image { get; }
        public SdkMessage Message { get; }
        public PluginType Plugin { get; }
        public SdkMessageProcessingStepSecureConfig SecureConfig { get; }
        public SdkMessageProcessingStep Step { get; }

        public CarfupStep(){}

        public CarfupStep(PluginType plugin, string environment)
        {
            Filter = plugin.GetAliasedEntity<SdkMessageFilter>();
            Image = plugin.GetAliasedEntity<SdkMessageProcessingStepImage>();
            Message = plugin.GetAliasedEntity<SdkMessage>();
            Plugin = plugin;
            Step = plugin.GetAliasedEntity<SdkMessageProcessingStep>();
            SecureConfig = plugin.GetAliasedEntity<SdkMessageProcessingStepSecureConfig>();

            Environment = environment;
        }

        public string ToCsv()
        {
            return string.Join(",",
                //AssemblyId.ToString().FormatForCsv(), 
                AssemblyName.FormatForCsv(), 
                //PluginTypeId.ToString().FormatForCsv(), 
                PluginTypeName, StepId.ToString().FormatForCsv(), 
                StepName.FormatForCsv(), 
                FormatForCsv(ImageId),
                ImageName.FormatForCsv(),
                Environment.FormatForCsv(),
                GetStateText(Environment == "Source" ? "target": "source").FormatForCsv(),
                StepMessageName.FormatForCsv(), 
                EntityName.FormatForCsv(), 
                StepFilteringAttributes.FormatForCsv(), 
                FormatForCsv(RunAsUserId),
                RunAsUserName.FormatForCsv(), 
                StepRank.ToString().FormatForCsv(), 
                StepDescription.FormatForCsv(), 
                StepStageName.FormatForCsv(), 
                StepModeName.FormatForCsv(), 
                StepSupportedDeploymentName.FormatForCsv(), 
                StepAsyncAutoDelete.ToString(), 
                StepConfiguration.FormatForCsv(), 
                StepSecureConfiguration.FormatForCsv(), 
                ImageAttributes.FormatForCsv()
            );
        }

        private string FormatForCsv(Guid id)
        {
            return id == Guid.Empty ? string.Empty : id.ToString().FormatForCsv();
        }

        private string GetStateText(string otherEnv)
        {
            switch (State)
            {
                case ComparisonState.NoAssemblyMatchFound:
                    return "Assembly not found in "+ otherEnv;
                case ComparisonState.NoPluginMatchFound:
                    return "Plugin not found in " + otherEnv;
                case ComparisonState.NoPluginStepMatchFound:
                    return "Step not found in " + otherEnv;
                default:
                    return State.ToString();
            }
        }

        public static string GetCsvColumns()
        {
            return string.Join(",", 
                //"Assembly Id",
                "Assembly",
                //"Plugin Type Id",
                "Plugin Type",
                "Step Id",
                "Step",
                "Image Id",
                "Image",
                "Environment",
                "State",
                "Step Message",
                "Entity",
                "Step Filtering Attributes",
                "Run As User Id",
                "Run As User",
                "Step Rank",
                "Step Description",
                "Step Stage",
                "Step Mode",
                "Step Supported Deployment",
                "Step Async Auto Delete",
                "Step Configuration",
                "Step Secure Configuration",
                "Image Attributes"
            );
        }

        #region GetSteps

        public static List<CarfupStep> GetSteps(IOrganizationService service, PluginSettings settings, string assemblyName, string solutionName)
        {
            var qe = GetStepsQuery();
            AddAssemblyFilter(assemblyName, qe);
            AddSolutionFilter(solutionName, qe);
            AddHiddenFilter(settings, qe);

            return service.GetAllEntities(qe).Select(x => new CarfupStep(x, "Source")).ToList();
        }

        private static void AddHiddenFilter(PluginSettings settings, TypedQueryExpression<PluginType> qe)
        {
            if (settings.SkipHiddenSteps)
            {
                qe.LinkEntities.First(e => e.LinkToEntityName == SdkMessageProcessingStep.EntityLogicalName)
                  .WhereEqual(SdkMessageProcessingStep.Fields.IsHidden, false);
            }
        }

        private static TypedQueryExpression<PluginType> GetStepsQuery()
        {
            var qe = QueryExpressionFactory.Create<PluginType>(p => new
            {
                p.AssemblyName,
                p.PluginAssemblyId,
                p.TypeName,
                p.Version
            });
            var step = qe.AddLink<SdkMessageProcessingStep>(PluginType.Fields.Id, s => new
            {
                s.AsyncAutoDelete,
                s.CreatedOn,
                s.Configuration,
                s.CustomizationLevel,
                s.Description,
                s.FilteringAttributes,
                s.InvocationSource,
                s.ImpersonatingUserId,
                s.Mode,
                s.ModifiedOn,
                s.Name,
                s.PluginTypeId,
                s.Rank,
                s.SdkMessageFilterId,
                s.SdkMessageId,
                s.SdkMessageProcessingStepId,
                s.Stage,
                s.StateCode,
                s.SupportedDeployment,
            });
            step.AddLink<SdkMessageFilter>(SdkMessageFilter.Fields.Id, JoinOperator.LeftOuter, f => new {f.PrimaryObjectTypeCode});
            step.AddLink<SdkMessage>(SdkMessage.Fields.Id, m => new {m.Name});
            step.AddLink<SdkMessageProcessingStepImage>(SdkMessageProcessingStep.Fields.Id, JoinOperator.LeftOuter, i => new
            {
                i.Attributes,
                i.Description,
                i.EntityAlias,
                i.Id,
                i.Name,
                i.ImageType
            });
            step.AddLink<SdkMessageProcessingStepSecureConfig>(SdkMessageProcessingStepSecureConfig.Fields.Id, JoinOperator.LeftOuter, c => new {c.SecureConfig});
            return qe;
        }

        private static void AddAssemblyFilter(string assemblyName, TypedQueryExpression<PluginType> qe)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                qe.WhereEqual(PluginType.Fields.AssemblyName, assemblyName);
            }
        }

        private static void AddSolutionFilter(string solutionName, TypedQueryExpression<PluginType> qe)
        {
            if (!string.IsNullOrWhiteSpace(solutionName))
            {
                qe.LinkEntities.First(l => l.LinkToEntityName == SdkMessageProcessingStep.EntityLogicalName)
                  .AddLink<SolutionComponent>(SdkMessageProcessingStep.Fields.Id, SolutionComponent.Fields.ObjectId)
                  .WhereEqual(SolutionComponent.Fields.ComponentType, (int) ComponentType.SDKMessageProcessingStep)
                  .AddLink<Solution>(Solution.Fields.Id)
                  .WhereEqual(Solution.Fields.UniqueName, solutionName);
            }
        }

        #endregion GetSteps
    }
}
