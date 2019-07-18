using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Carfup.XTBPlugins.AppCode
{
    public class DataManager
    {
        #region Variables
        /// <summary>
        /// Crm web service
        /// </summary>
        public ControllerManager Connection { get; set; }
        #endregion Variables

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the class UserManager
        /// </summary>
        /// <param name="proxy">Details of the connected user</param>
        public DataManager(ControllerManager connection)
        {
            this.Connection = connection;
        }

        #endregion Constructor

        #region Methods
        // Return the value from an aliasedvalue
        public object returnAliasedValue(Entity entity, string varName)
        {
            return entity.GetAttributeValue<AliasedValue>(varName) == null ? "" : entity.GetAttributeValue<AliasedValue>(varName).Value;
        }

        //public string getStepNameValue(Comparing comparing, Entity entity)
        //{
        //   return (comparing == Comparing.Solution) ? this.returnAliasedValue(entity, "step.name").ToString() : entity.GetAttributeValue<string>("name");
        //}

        // return the plugintype
        public Entity getPluginType(string plugintype, IOrganizationService service)
        {
            QueryExpression queryRetrievePluginType = new QueryExpression
            {
                EntityName = "plugintype",
                ColumnSet = new ColumnSet(false),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("typename", ConditionOperator.Equal, plugintype)
                    }
                }
            };

            return service.RetrieveMultiple(queryRetrievePluginType).Entities.FirstOrDefault();
        }

        //return the sdk message
        public Entity getSdkMessage(string name, IOrganizationService service)
        {
            QueryExpression queryRetrieveSdkMessage = new QueryExpression
            {
                EntityName = "sdkmessage",
                ColumnSet = new ColumnSet(false),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("name", ConditionOperator.Equal, name)
                    }
                }
            };

            return service.RetrieveMultiple(queryRetrieveSdkMessage).Entities.FirstOrDefault();
        }

        // return the message filter
        public Entity getMessageFilter(string primaryobjecttypecode, IOrganizationService service)
        {
            QueryExpression queryRetrieveMessageFilter = new QueryExpression
            {
                EntityName = "sdkmessagefilter",
                ColumnSet = new ColumnSet(false),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("primaryobjecttypecode", ConditionOperator.Equal, primaryobjecttypecode)
                    }
                }
            };

            return service.RetrieveMultiple(queryRetrieveMessageFilter).Entities.FirstOrDefault();
        }

        public string[] loadSolutions()
        {
            return this.Connection.SourceService.RetrieveMultiple(new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet("uniquename"),
            }).Entities.Select(p => p.Attributes["uniquename"].ToString()).OrderBy(p => p).ToArray();
        }

        public string[] loadAssemblies()
        {
            return this.Connection.SourceService.RetrieveMultiple(new QueryExpression("pluginassembly")
            {
                ColumnSet = new ColumnSet("name"),
            }).Entities.Select(p => p.Attributes["name"].ToString()).OrderBy(p => p).ToArray();
        }

        public int IsSolutionExistingInTargetEnv(string solutionPluginStepsName)
        {
            var queryExisting = new QueryExpression()
            {
                TopCount = 1,
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

            return Connection.TargetService.RetrieveMultiple(queryExisting).Entities.Count;
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

            return Connection.TargetService.RetrieveMultiple(queryExisting).Entities.Count;
        }

        public List<CarfupStep> GetSteps(IOrganizationService service, string assemblyName, string solutionName)
        {
            var pluginTypeLink = new LinkEntity
                    {
                        LinkToEntityName = "plugintype",
                        LinkToAttributeName = "plugintypeid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "plugintypeid",
                        EntityAlias = "plugintype",
                        Columns = new ColumnSet("typename", "version", "assemblyname"),
                        JoinOperator = JoinOperator.Inner,
                    };

            var qe = GetStepsQuery(pluginTypeLink);
            AddAssemblyFilter(assemblyName, pluginTypeLink);
            AddSolutionFilter(solutionName, qe);


            return service.RetrieveMultiple(qe).Entities.Select(x => new CarfupStep
            {
                stepName = x.GetAttributeValue<string>("name"),
                entityName = returnAliasedValue(x, "messagefilter.primaryobjecttypecode").ToString(),
                stepMessageName = returnAliasedValue(x, "sdkmessage.name").ToString(),
                plugintypeName = returnAliasedValue(x, "plugintype.typename").ToString(),
                modifiedOn = x.GetAttributeValue<DateTime>("modifiedon"),
                createOn = x.GetAttributeValue<DateTime>("createdon"),
                stepAsyncautodelete = x.GetAttributeValue<bool>("asyncautodelete"),
                stepConfiguration = x.GetAttributeValue<string>("configuration"),
                stepCustomizationlevel = x.GetAttributeValue<int>("customizationlevel"),
                stepDescription = x.GetAttributeValue<string>("description"),
                stepFilteringattributes = x.GetAttributeValue<string>("filteringattributes"),
                stepInvocationsource = x.GetAttributeValue<OptionSetValue>("invocationsource"),
                stepMode = x.GetAttributeValue<OptionSetValue>("mode"),
                stepRank = x.GetAttributeValue<int>("rank"),
                stepStage = x.GetAttributeValue<OptionSetValue>("stage"),
                stepSupporteddeployment = x.GetAttributeValue<OptionSetValue>("supporteddeployment"),
                entity = x
            }).ToList();
        }

        private static QueryExpression GetStepsQuery(LinkEntity pluginTypeLink)
        {
            var queryExistingSteps = new QueryExpression
            {
                EntityName = "sdkmessageprocessingstep",
                ColumnSet = new ColumnSet(
                                          "asyncautodelete",
                                          "configuration",
                                          "customizationlevel",
                                          "description",
                                          "filteringattributes",
                                          "invocationsource",
                                          "impersonatinguserid",
                                          "mode",
                                          "name",
                                          "plugintypeid",
                                          "rank",
                                          "sdkmessagefilterid",
                                          "sdkmessageid",
                                          "stage",
                                          "statecode",
                                          "supporteddeployment"),
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkToEntityName = "sdkmessagefilter",
                        LinkToAttributeName = "sdkmessagefilterid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "sdkmessagefilterid",
                        EntityAlias = "messagefilter",
                        Columns = new ColumnSet("primaryobjecttypecode"),
                        JoinOperator = JoinOperator.LeftOuter
                    },
                    new LinkEntity
                    {
                        LinkToEntityName = "sdkmessage",
                        LinkToAttributeName = "sdkmessageid",
                        LinkFromEntityName = "sdkmessageprocessingstep",
                        LinkFromAttributeName = "sdkmessageid",
                        EntityAlias = "sdkmessage",
                        Columns = new ColumnSet("name"),
                        JoinOperator = JoinOperator.Inner
                    },
                    pluginTypeLink
                }
            };
            return queryExistingSteps;
        }

        private static void AddAssemblyFilter(string assemblyName, LinkEntity pluginType)
        {
            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                pluginType.LinkCriteria.AddCondition("assemblyname", ConditionOperator.Equal, assemblyName);
            }
        }

        private static void AddSolutionFilter(string solutionName, QueryExpression queryExistingSteps)
        {
            if (!string.IsNullOrWhiteSpace(solutionName))
            {
                queryExistingSteps.LinkEntities.Add(new LinkEntity()
                {
                    LinkToEntityName = "solutioncomponent",
                    LinkToAttributeName = "objectid",
                    LinkFromEntityName = "sdkmessageprocessingstep",
                    LinkFromAttributeName = "sdkmessageprocessingstepid",
                    EntityAlias = "solutioncomponent",
                    LinkCriteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("componenttype", ConditionOperator.Equal, 92)
                        }
                    },
                    LinkEntities =
                    {
                        new LinkEntity("solutioncomponent", "solution", "solutionid", "solutionid", JoinOperator.Inner)
                        {
                            LinkCriteria = new FilterExpression
                            {
                                Conditions =
                                {
                                    new ConditionExpression("uniquename", ConditionOperator.Equal, solutionName)
                                }
                            }
                        }
                    }
                });
            }
        }

        public bool UserHasPrivilege(string priv, Guid userId)
        {
            bool userHasPrivilege = false;

            ConditionExpression privilegeCondition =
                new ConditionExpression("name", ConditionOperator.Equal, priv); // name of the privilege
            FilterExpression privilegeFilter = new FilterExpression(LogicalOperator.And);
            privilegeFilter.Conditions.Add(privilegeCondition);

            QueryExpression privilegeQuery = new QueryExpression
            {
                EntityName = "privilege",
                ColumnSet = new ColumnSet(true),
                Criteria = privilegeFilter
            };

            EntityCollection retrievedPrivileges = this.Connection.SourceService.RetrieveMultiple(privilegeQuery);
            if (retrievedPrivileges.Entities.Count == 1)
            {
                RetrieveUserPrivilegesRequest request = new RetrieveUserPrivilegesRequest();
                request.UserId = userId; // Id of the User
                RetrieveUserPrivilegesResponse response = (RetrieveUserPrivilegesResponse)this.Connection.SourceService.Execute(request);
                foreach (RolePrivilege rolePrivilege in response.RolePrivileges)
                {
                    if (rolePrivilege.PrivilegeId == retrievedPrivileges.Entities[0].Id)
                    {
                        userHasPrivilege = true;
                        break;
                    }
                }
            }

            return userHasPrivilege;
        }

        public Guid WhoAmI()
        {
            return ((WhoAmIResponse)this.Connection.SourceService.Execute(new WhoAmIRequest())).UserId;
        }

        #endregion Methods
    }

    

}
