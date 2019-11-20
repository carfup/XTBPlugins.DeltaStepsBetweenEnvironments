using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Documents;
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
    public enum CrmPluginStepInvocationSource
    {
        Parent = 0,
        Child = 1
    }
    public enum CrmPluginStepMode
    {
        Asynchronous = 1,
        Synchronous = 0
    }

    public enum SdkMessageProcessingStep_Mode
    {

        Asynchronous = 1,
        Synchronous = 0,
    }

    public enum SdkMessageProcessingStep_Stage
    {

        FinalPostoperation_Forinternaluseonly = 55,
        InitialPreoperation_Forinternaluseonly = 5,
        InternalPostoperationAfterExternalPlugins_Forinternaluseonly = 45,
        InternalPostoperationBeforeExternalPlugins_Forinternaluseonly = 35,
        InternalPreoperationAfterExternalPlugins_Forinternaluseonly = 25,
        InternalPreoperationBeforeExternalPlugins_Forinternaluseonly = 15,
        MainOperation_Forinternaluseonly = 30,
        Postoperation = 40,
        Postoperation_Deprecated = 50,
        Preoperation = 20,
        Prevalidation = 10,
    }

    [DebuggerDisplay("{PluginTypeName} {StepMessageName} {EntityName}")]
    public class CarfupStep
    {
        public DateTime CreateOn => Step.GetAttributeValue<DateTime>("createdon"); //getCreatedOn.GetValueOrDefault();
        public DateTime ModifiedOn => Step.GetAttributeValue<DateTime>("modifiedon");
        public Guid StepId => Step.Id;
        public Guid AssemblyId => Plugin.GetAttributeValue<EntityReference>("pluginassemblyid").Id;
        public Guid PluginTypeId => Plugin.GetAttributeValue<Guid>("plugintypeid");
        [Obsolete]
        public OptionSetValue StepInvocationSource => Step.GetAttributeValue<OptionSetValue>("invocationsource");

        public bool StepAsyncAutoDelete => Step.GetAttributeValue<bool>("asyncautodelete") == true;
        public int StepCustomizationLevel => Step.GetAttributeValue<int>("customizationlevel");//.GetValueOrDefault());
        public int StepRank => Step.GetAttributeValue<int>("rank"); //.GetValueOrDefault(1));
        public string AssemblyName => Plugin.GetAttributeValue<string>("assemblyname");
        public string EntityName => string.IsNullOrWhiteSpace(Filter.GetAttributeValue<string>("primaryobjecttypecode")) ? "none" : Filter.GetAttributeValue<string>("primaryobjecttypecode");
        public string Environment { get; set; }
        public string PluginTypeName => Plugin.GetAttributeValue<string>("typename");
        public string PluginFullTypeName => AssemblyName + "|" + PluginTypeName;
        public string StepConfiguration => Step.GetAttributeValue<string>("configuration");
        public string StepDescription => Step.GetAttributeValue<string>("description");
        public string StepFilteringAttributes => Step.GetAttributeValue<string>("filteringattributes");
        public Guid StepMessageId => Step.GetAttributeValue<EntityReference>("sdkmessageid").Id;
        public string StepMessageName => Step.GetAttributeValue<EntityReference>("sdkmessageid")?.Name;
        public string StepName => Step.GetAttributeValue<string>("name");
        public int? StepMode => Step.GetAttributeValue<OptionSetValue>("mode")?.Value;
        public string StepModeName => Plugin.GetFormattedAttributeValueOrNull("pmode");
        public int? StepStage => Step.GetAttributeValue<OptionSetValue>("stage")?.Value;
        public string StepStageName => Plugin.GetFormattedAttributeValueOrNull("stage");
        public int? StepSupportedDeployment => Step.GetAttributeValue<OptionSetValue>("supporteddeployment")?.Value;
        public string StepSupportedDeploymentName => Plugin.GetFormattedAttributeValueOrNull("supporteddeployment");
        public string StepSecureConfiguration => SecureConfig.GetAttributeValue<string>("secureconfig");
        public string RunAsUserName => Step.GetAttributeValue<EntityReference>("impersonationuserid") == null ? "Calling User" : Step.GetAttributeValue<EntityReference>("impersonationuserid").Name;
        public string ImageAttributes => Image.GetAttributeValue<string>("attributes");
        public string ImageAlias => Image.GetAttributeValue<string>("entityalias");
        public Guid ImageId => Image.GetAttributeValue<Guid>("sdkmessageprocessingstepimageid");
        public string ImageName => Image.GetAttributeValue<string>("name");
        public string ImageType => Plugin.GetFormattedAttributeValueOrNull("imagetype");
        public Guid RunAsUserId => Step.GetAttributeValue<EntityReference>("impersonationuserid").GetIdOrDefault();
        public ComparisonState State { get; set; }

        //public SdkMessageFilter Filter { get; }
        public Entity Filter { get; }
        //public SdkMessageProcessingStepImage Image { get; }
        public Entity Image { get; }
        //public SdkMessage Message { get; }
        public Entity Message { get; }
        //public PluginType Plugin { get; }
        public Entity Plugin { get; }
        //public SdkMessageProcessingStepSecureConfig SecureConfig { get; }
        public Entity SecureConfig { get; }
     //   public SdkMessageProcessingStepUnSecureConfig UnSecureConfig { get; }
        //public SdkMessageProcessingStep Step { get; set; }
        public Entity Step { get; set; }

        public CarfupStep(){}

        public CarfupStep(Entity plugin, string environment)
        {
            Step = DataManager.GetAliasedEntityLateBound(plugin, "sdkmessageprocessingstep", "sdkmessageprocessingstepid");
            //Step = plugin.GetAliasedEntity<Entity>("sdkmessageprocessingstep");
            Filter = DataManager.GetAliasedEntityLateBound(plugin, "sdkmessagefilter", "sdkmessagefilterid");
            Image = DataManager.GetAliasedEntityLateBound(plugin, "sdkmessageprocessingstepimage", "sdkmessageprocessingstepimageid");
            Message = DataManager.GetAliasedEntityLateBound(plugin, "sdkmessage", "sdkmessageid");
            
            Plugin = plugin;

            SecureConfig = DataManager.GetAliasedEntityLateBound(plugin, "sdkmessageprocessingstepsecureconfig", "sdkmessageprocessingstepsecureconfigid");

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

            var result = service.RetrieveMultiple(qe).Entities.ToList();

            return result.Select(x => new CarfupStep(x, "Source")).ToList();
        }

        private static void AddHiddenFilter(PluginSettings settings, QueryExpression qe)
        {
            if (settings.SkipHiddenSteps)
            {
                qe.LinkEntities.First(e => e.LinkToEntityName == "sdkmessageprocessingstep")
                  .WhereEqual("ishidden", false);
            }
        }

        private Entity GetStepDetails(EntityReference pluginType)
        {

            return new Entity();
        }

        private static QueryExpression GetStepsQuery()
        {
            var query = new QueryExpression()
            {
                EntityName = "plugintype",
                ColumnSet = new ColumnSet("assemblyname", "pluginassemblyid", "typename", "version", "name"),
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkFromEntityName = "plugintype",
                        LinkFromAttributeName = "plugintypeid",
                        LinkToEntityName = "sdkmessageprocessingstep",
                        LinkToAttributeName = "plugintypeid",
                        JoinOperator = JoinOperator.Inner,
                        Columns = new ColumnSet(true),
                        EntityAlias = "sdkmessageprocessingstep",
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessagefilterid",
                                LinkToEntityName = "sdkmessagefilter",
                                LinkToAttributeName = "sdkmessagefilterid",
                                JoinOperator = JoinOperator.LeftOuter,
                                Columns = new ColumnSet("primaryobjecttypecode"),
                                EntityAlias = "sdkmessagefilter"
                            },
                            new LinkEntity()
                            {
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessageid",
                                LinkToEntityName = "sdkmessage",
                                LinkToAttributeName = "sdkmessageid",
                                JoinOperator = JoinOperator.Inner,
                                Columns = new ColumnSet("name"),
                                EntityAlias = "sdkmessage"
                            },
                            new LinkEntity()
                            {
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessageprocessingstepid",
                                LinkToEntityName = "sdkmessageprocessingstepimage",
                                LinkToAttributeName = "sdkmessageprocessingstepid",
                                JoinOperator = JoinOperator.LeftOuter,
                                Columns = new ColumnSet("attributes", "description","name","imagetype", "entityalias"),
                                EntityAlias = "sdkmessageprocessingstepimage"
                            },
                            new LinkEntity()
                            {
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessageprocessingstepsecureconfigid",
                                LinkToEntityName = "sdkmessageprocessingstepsecureconfig",
                                LinkToAttributeName = "sdkmessageprocessingstepsecureconfigid",
                                JoinOperator = JoinOperator.LeftOuter,
                                Columns = new ColumnSet(true),
                                EntityAlias = "sdkmessageprocessingstepsecureconfig"
                            },
                        }
                    }
                }
            };


            /*var qe = QueryExpressionFactory.Create("plugintype",
                "assemblyname", "pluginassemblyid", "typename", "version"
               );

            var step = qe.AddLink("sdkmessageprocessingstep", "plugintypeid", "plugintypeid");
            step.Columns = new ColumnSet("asyncautodelete", "createdon", "configuration", "customizationlevel", "description",
            "filteringattributes", "invocationsource", "impersonatinguserid", "mode", "modifiedon", "name", pr);
            step.Columns = new ColumnSet(true);
            s => new
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
            var stepFilter = step.AddLink("sdkmessagefilter","sdkmessagefilterid", "sdkmessagefilterid");// primaryobjecttype
            stepFilter.Columns = new ColumnSet("primaryobjecttype");
            var stepMessage = stepFilter.AddLink("sdkmessage","sdkmessageid", "sdkmessageid"); //name
            stepMessage.Columns = new ColumnSet("name");

            var stepImage = stepMessage.AddLink("sdkmessageprocessingstepimage", "sdkmessageprocessingstepid", "sdkmessageprocessingstepid",
                JoinOperator.LeftOuter);
            stepImage.Columns = new ColumnSet(true);
            i => new
            {
                i.Attributes,
                i.Description,
                i.EntityAlias,
                i.Id,
                i.Name,
                i.ImageType
            });
            var stepSecure = stepImage.AddLink("sdkmessageprocessingstepsecureconfig", "secureconfid", "secureconfid",
                JoinOperator.LeftOuter);
            stepSecure.Columns = new ColumnSet("secureconfig");
            /*c => new {c.SecureConfig});*/
            return query;
        }

        private static void AddAssemblyFilter(string assemblyName, QueryExpression qe)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                qe.WhereEqual("assemblyname", assemblyName);
            }
        }

        private static void AddSolutionFilter(string solutionName, QueryExpression qe)
        {
            if (!string.IsNullOrWhiteSpace(solutionName))
            {
                qe.LinkEntities.First(l => l.LinkToEntityName == "sdkmessageprocessingstep")

                  .AddLink("solutioncomponent","sdkmessageprocessingstepid", "objectid")
                  .WhereEqual("componenttype", 92) //(int) ComponentType.SDKMessageProcessingStep
                  .AddLink("solution", "solutionid")
                  .WhereEqual("uniquename", solutionName);
            }
        }

        #endregion GetSteps
    }
}
