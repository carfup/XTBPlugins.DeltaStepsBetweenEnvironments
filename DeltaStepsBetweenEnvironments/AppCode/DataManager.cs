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

        public List<Entity> querySteps(IOrganizationService service, List<Entity> list, string solutionPluginStepsName)
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
                        Columns = new ColumnSet("name","configuration","mode","rank","stage","supporteddeployment","invocationsource","configuration","plugintypeid","sdkmessageid","sdkmessagefilterid","filteringattributes","description","asyncautodelete","customizationlevel"),
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
                                new ConditionExpression("uniquename", ConditionOperator.Equal, solutionPluginStepsName)
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

            list = service.RetrieveMultiple(queryExistingSteps).Entities.ToList();

            return list;
        }

        #endregion Methods
    }
}
