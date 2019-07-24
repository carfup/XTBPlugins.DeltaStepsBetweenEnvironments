using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using Carfup.XTBPlugins.Entities;
using Source.DLaB.Xrm;

namespace Carfup.XTBPlugins.AppCode
{
    public class DataManager
    {
        #region Properties

        /// <summary>
        /// Crm web service
        /// </summary>
        public ControllerManager Connection { get; }

        #endregion Properties

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the class UserManager
        /// </summary>
        /// <param name="connection">Details of the connected user</param>
        public DataManager(ControllerManager connection)
        {
            Connection = connection;
        }

        #endregion Constructor

        #region Methods

        //public string getStepNameValue(Comparing comparing, Entity entity)
        //{
        //   return (comparing == Comparing.Solution) ? this.returnAliasedValue(entity, "step.name").ToString() : entity.GetAttributeValue<string>("name");
        //}

        public PluginType GetPluginType(IOrganizationService service, string pluginTypeName)
        {
            return service.GetFirstOrDefault<PluginType>(new ColumnSet(false),
                    PluginType.Fields.TypeName, pluginTypeName);
        }

        //return the sdk message
        public Entity GetSdkMessage(string name, IOrganizationService service)
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
        public Entity GetMessageFilter(string primaryobjecttypecode, IOrganizationService service)
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

        public string[] LoadSolutions()
        {
            return Connection.SourceService.RetrieveMultiple(new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet("uniquename"),
            }).Entities.Select(p => p.Attributes["uniquename"].ToString()).OrderBy(p => p).ToArray();
        }

        public string[] LoadAssemblies()
        {
            return Connection.SourceService.RetrieveMultiple(new QueryExpression("pluginassembly")
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

        public int IsAssemblyExistingInTargetEnv(string assemblyStepsName)
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

            EntityCollection retrievedPrivileges = Connection.SourceService.RetrieveMultiple(privilegeQuery);
            if (retrievedPrivileges.Entities.Count == 1)
            {
                RetrieveUserPrivilegesRequest request = new RetrieveUserPrivilegesRequest();
                request.UserId = userId; // Id of the User
                RetrieveUserPrivilegesResponse response = (RetrieveUserPrivilegesResponse)Connection.SourceService.Execute(request);
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
            return ((WhoAmIResponse)Connection.SourceService.Execute(new WhoAmIRequest())).UserId;
        }

        #endregion Methods
    }

    

}
