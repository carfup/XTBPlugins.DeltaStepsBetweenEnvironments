using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfup.XTBPlugins.AppCode
{
    class DataManager
    {
        #region Variables
        /// <summary>
        /// Crm web service
        /// </summary>
        public ControllerManager connection = null;
        #endregion Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the class UserManager
        /// </summary>
        /// <param name="proxy">Details of the connected user</param>
        public DataManager(ControllerManager connection)
        {
            this.connection = connection;
        }

        #endregion Constructor

        #region Methods
        // Return the value from an aliasedvalue
        public object returnAliasedValue(Entity entity, string varName)
        {
            return entity.GetAttributeValue<AliasedValue>(varName) == null ? "" : entity.GetAttributeValue<AliasedValue>(varName).Value;
        }

        public string getStepNameValue(Comparing comparing, Entity entity)
        {
           return (comparing == Comparing.Solution) ? this.returnAliasedValue(entity, "step.name").ToString() : entity.GetAttributeValue<string>("name");
        }

        // return the plugintype
        public Entity getPluginType(string plugintype)
        {
            QueryExpression queryRetrievePluginType = new QueryExpression
            {
                EntityName = "plugintype",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("typename", ConditionOperator.Equal, plugintype)
                    }
                }
            };

            var pluginType = this.connection.targetService.RetrieveMultiple(queryRetrievePluginType).Entities;

            return pluginType.FirstOrDefault();
        }

        //return the sdk message
        public Entity getSdkMessage(string name)
        {
            QueryExpression queryRetrieveSdkMessage = new QueryExpression
            {
                EntityName = "sdkmessage",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("name", ConditionOperator.Equal, name)
                    }
                }
            };

            var sdkMessage = this.connection.targetService.RetrieveMultiple(queryRetrieveSdkMessage).Entities;

            return sdkMessage.FirstOrDefault();
        }

        // return the message filter
        public Entity getMessageFilter(string primaryobjecttypecode)
        {
            QueryExpression queryRetrieveMessageFilter = new QueryExpression
            {
                EntityName = "sdkmessagefilter",
                ColumnSet = new ColumnSet(),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("primaryobjecttypecode", ConditionOperator.Equal, primaryobjecttypecode)
                    }
                }
            };

            var messageFilter = this.connection.targetService.RetrieveMultiple(queryRetrieveMessageFilter).Entities;

            return messageFilter.FirstOrDefault();
        }

        public string[] loadSolutions()
        {
            return this.connection.sourceService.RetrieveMultiple(new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet("uniquename"),
            }).Entities.Select(p => p.Attributes["uniquename"].ToString()).OrderBy(p => p).ToArray();
        }

        public string[] loadAssemblies()
        {
            return this.connection.sourceService.RetrieveMultiple(new QueryExpression("pluginassembly")
            {
                ColumnSet = new ColumnSet("name"),
            }).Entities.Select(p => p.Attributes["name"].ToString()).OrderBy(p => p).ToArray();
        }

        public int isSolutionExistingInTargetEnv(string solutionPluginStepsName)
        {
            QueryExpression queryExisting = new QueryExpression()
            {
                EntityName = "solution",
                ColumnSet = new ColumnSet(false),
                Criteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("uniquename", ConditionOperator.Equal, solutionPluginStepsName)
                            }
                        }
            };

            return  connection.targetService.RetrieveMultiple(queryExisting).Entities.Count;
        }

        public int isAssemblyExistingInTargetEnv(string assemblyStepsName)
        {
            QueryExpression queryExisting = new QueryExpression()
            {
                EntityName = "pluginassembly",
                ColumnSet = new ColumnSet(false),
                Criteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("name", ConditionOperator.Equal, assemblyStepsName)
                            }
                        }
            };

            return connection.targetService.RetrieveMultiple(queryExisting).Entities.Count;
        }

        public List<CarfupStep> querySteps(IOrganizationService service, string solutionName)
        {
            QueryExpression queryExistingSteps = new QueryExpression()
            {

                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("componenttype"),
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkToEntityName = "sdkmessageprocessingstep",
                        LinkToAttributeName = "sdkmessageprocessingstepid",
                        LinkFromEntityName = "solutioncomponent",
                        LinkFromAttributeName = "objectid",
                        EntityAlias = "step",
                        Columns = new ColumnSet("name","createdon","modifiedon","configuration","mode","rank","stage","supporteddeployment","invocationsource","plugintypeid","sdkmessageid","sdkmessagefilterid","filteringattributes","description","asyncautodelete","customizationlevel"),
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkToEntityName = "sdkmessagefilter",
                                LinkToAttributeName = "sdkmessagefilterid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessagefilterid",
                                EntityAlias = "messagefilter",
                                Columns = new ColumnSet("primaryobjecttypecode"),
                                JoinOperator = JoinOperator.Inner
                            },
                            new LinkEntity()
                            {
                                LinkToEntityName = "sdkmessage",
                                LinkToAttributeName = "sdkmessageid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "sdkmessageid",
                                EntityAlias = "sdkmessage",
                                Columns = new ColumnSet("name"),
                                JoinOperator = JoinOperator.Inner
                            },
                            new LinkEntity()
                            {
                                LinkToEntityName = "plugintype",
                                LinkToAttributeName = "plugintypeid",
                                LinkFromEntityName = "sdkmessageprocessingstep",
                                LinkFromAttributeName = "plugintypeid",
                                EntityAlias = "plugintype",
                                Columns = new ColumnSet("typename"),
                                JoinOperator = JoinOperator.Inner
                            }
                        }
                    }
                    ,
                    new LinkEntity()
                    {
                        LinkToEntityName = "solution",
                        LinkToAttributeName = "solutionid",
                        LinkFromEntityName = "solutioncomponent",
                        LinkFromAttributeName = "solutionid",
                        LinkCriteria =
                        {
                            Conditions =
                            {
                                new ConditionExpression("uniquename", ConditionOperator.Equal, solutionName)
                            }
                        }
                    }
                }
                ,
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("componenttype", ConditionOperator.Equal, 92)
                    }
                }

            };

            return service.RetrieveMultiple(queryExistingSteps).Entities.Select(x => new CarfupStep
            {
                stepName = returnAliasedValue(x, "step.name").ToString(),
                entityName = returnAliasedValue(x, "messagefilter.primaryobjecttypecode").ToString(),
                messageName = returnAliasedValue(x, "sdkmessage.name").ToString(),
                plugintypeName = returnAliasedValue(x, "plugintype.name").ToString(),
                modifiedOn = (DateTime)returnAliasedValue(x, "step.modifiedon"),
                createOn = (DateTime)returnAliasedValue(x, "step.createdon"),
                stepAsyncautodelete = (bool)returnAliasedValue(x, "step.asyncautodelete"),
                stepConfiguration = returnAliasedValue(x, "step.configuration").ToString(),
                stepCustomizationlevel = (int)returnAliasedValue(x, "step.customizationlevel"),
                stepDescription = returnAliasedValue(x, "step.description").ToString(),
                stepFilteringattributes = returnAliasedValue(x, "step.filteringattributes").ToString(),
                stepInvocationsource = returnAliasedValue(x, "step.invocationsource").ToString(),
                stepMode = (int)returnAliasedValue(x, "step.mode"),
                stepRank = (int)returnAliasedValue(x, "step.rank"),
                stepStage = (int)returnAliasedValue(x, "step.stage"),
                stepSupporteddeployment = (int)returnAliasedValue(x, "step.supporteddeployment"),
                entity = x
            }).ToList();
        }

        public List<CarfupStep> queryStepsAssembly(IOrganizationService service, string assemblyName)
        {
            QueryExpression queryExistingSteps = new QueryExpression()
            {

                EntityName = "sdkmessageprocessingstep",
                ColumnSet = new ColumnSet("name", "createdon", "modifiedon", "configuration", "mode", "rank", "stage", "supporteddeployment", "invocationsource", "plugintypeid", "sdkmessageid", "sdkmessagefilterid", "filteringattributes", "description", "asyncautodelete", "customizationlevel"),
                LinkEntities =
                {
                    new LinkEntity()
                    {
                        LinkToEntityName = "sdkmessagefilter",
                        LinkToAttributeName = "sdkmessagefilterid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "sdkmessagefilterid",
                        EntityAlias = "messagefilter",
                        Columns = new ColumnSet("primaryobjecttypecode"),
                        JoinOperator = JoinOperator.Inner
                    },
                    new LinkEntity()
                    {
                        LinkToEntityName = "sdkmessage",
                        LinkToAttributeName = "sdkmessageid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "sdkmessageid",
                        EntityAlias = "sdkmessage",
                        Columns = new ColumnSet("name"),
                        JoinOperator = JoinOperator.Inner
                    },
                    new LinkEntity()
                    {
                        LinkToEntityName = "plugintype",
                        LinkToAttributeName = "plugintypeid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "plugintypeid",
                        EntityAlias = "plugintype",
                        Columns = new ColumnSet("typename"),
                        JoinOperator = JoinOperator.Inner,
                        LinkEntities =
                        {
                            new LinkEntity()
                            {
                                LinkToEntityName = "pluginassembly",
                                LinkToAttributeName = "pluginassemblyid",
                                LinkFromEntityName = "plugintype",
                                LinkFromAttributeName = "pluginassemblyid",
                                EntityAlias = "pluginassembly",
                                Columns = new ColumnSet("name"),
                                //JoinOperator = JoinOperator.Inner,
                                LinkCriteria =
                                {
                                    Conditions =
                                    {
                                        new ConditionExpression("name", ConditionOperator.Equal, assemblyName)
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return service.RetrieveMultiple(queryExistingSteps).Entities.Select(x => new CarfupStep
            {
                stepName = x.GetAttributeValue<string>("name"),
                entityName = returnAliasedValue(x, "messagefilter.primaryobjecttypecode").ToString(),
                messageName = returnAliasedValue(x, "sdkmessage.name").ToString(),
                plugintypeName = returnAliasedValue(x, "plugintype.name").ToString(),
                modifiedOn = x.GetAttributeValue<DateTime>("name"),
                createOn = x.GetAttributeValue<DateTime>("createdon"),
                stepAsyncautodelete = x.GetAttributeValue<bool>("asyncautodelete"),
                stepConfiguration = x.GetAttributeValue<string>("configuration"),
                stepCustomizationlevel = x.GetAttributeValue<int>("customizationlevel"),
                stepDescription = x.GetAttributeValue<string>("description"),
                stepFilteringattributes = x.GetAttributeValue<string>("filteringattributes"),
                stepInvocationsource = x.GetAttributeValue<string>("invocationsource"),
                stepMode = x.GetAttributeValue<int>("mode"),
                stepRank = x.GetAttributeValue<int>("rank"),
                stepStage = x.GetAttributeValue<int>("stage"),
                stepSupporteddeployment = x.GetAttributeValue<int>("supporteddeployment"),
                entity = x
            }).ToList();
        }
        #endregion Methods
    }

    // Will be used to implement the step 
    public class CarfupStep
    {
        public string stepName { get; set; }
        public string entityName { get; set; } //messagefilter.primaryobjecttypecode
        public string messageName { get; set; } //sdkmessage.name
        public string plugintypeName { get; set; } // plugintype.name
        public DateTime createOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string stepConfiguration { get; set; }
        public int stepMode { get; set; }
        public int stepRank { get; set; }
        public int stepStage { get; set; }
        public int stepSupporteddeployment { get; set; }
        public string stepInvocationsource { get; set; }
        public string stepFilteringattributes { get; set; }
        public string stepDescription { get; set; }
        public bool stepAsyncautodelete { get; set; }
        public int stepCustomizationlevel { get; set; }
        public Entity entity { get; set; }
    }
}
