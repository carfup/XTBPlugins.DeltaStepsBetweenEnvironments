using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using Source.DLaB.Xrm;
using System.Collections.Generic;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
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

      

        public Entity GetPluginTypeName(IOrganizationService service, Guid pluginTypeGuid)
        {
            return service.GetFirstOrDefault("plugintype", new ColumnSet("name"),
                "plugintypeid", pluginTypeGuid);
        }

        public Entity GetPluginType(IOrganizationService service, string pluginTypeName)
        {
            return service.GetFirstOrDefault("plugintype", new ColumnSet(false),
                    "typename", pluginTypeName);
        }

        //return the sdk message
        public Entity GetSdkMessage(IOrganizationService service, string name)
        {
            return service.GetFirstOrDefault("sdkmessage", new ColumnSet(false),
                  "name", name);
        }

        public Entity GetSdkMessageProcessingStepImage(IOrganizationService service,
            Guid sdkMessageGuid)
        {
            return service.GetFirstOrDefault("sdkmessageprocessingstepimage", new ColumnSet(true),
                "sdkmessageprocessingstepid", sdkMessageGuid);
        }

        // return all sdk messages
        public List<Entity> GetSdkMessages(IOrganizationService service)
        {
            return service.GetEntities("sdkmessage",new ColumnSet(false),
                  "isprivate", false)
                .OrderBy(p => p.GetAttributeValue<string>("Name")).ToList();
        }

        // return all users
        public List<Entity> GetUsers(IOrganizationService service)
        {
            return service.GetEntities("systemuser", new ColumnSet("systemuserid", "fullname", "domainname", "internalemailaddress","isdisabled"))
                .OrderBy(p => p.GetAttributeValue<string>("fullName")).ToList();
        }

        // return the message filter
        public Entity GetMessageFilter(IOrganizationService service, string primaryObjectTypeCode)
        {
            return service.GetFirstOrDefault("sdkmessagefilter",new ColumnSet(false),
                                                               "primaryobjecttypecode", primaryObjectTypeCode);
        }

        public string[] LoadSolutions()
        {
            return Connection.SourceService.GetEntities("solution","uniuename")
                             .Select(p => p.GetAttributeValue<string>("uniquename"))
                             .OrderBy(p => p)
                             .ToArray();
        }

        public string[] LoadAssemblies()
        {
            
            return Connection.SourceService.GetEntities("pluginassembly", "name")
                             .Select(p => p.GetAttributeValue<string>("name"))
                             .OrderBy(p => p)
                             .ToArray();
        }

        public bool SolutionExistsInTargetEnv(string solutionPluginStepsName)
        {
            return Connection.TargetService.GetFirstOrDefault("solution",new ColumnSet(false), "uniquename", solutionPluginStepsName) != null;
        }

        public bool AssemblyExistsInTargetEnv(string assemblyStepsName)
        {
            return Connection.TargetService.GetFirstOrDefault("pluginassembly", new ColumnSet(false), "name", assemblyStepsName) != null;
        }

        public bool UserHasPrivilege(string priv, Guid userId)
        {
            var privilege = Connection.SourceService.GetFirstOrDefault("privilege", "name", priv);
            if (privilege == null)
            {
                return false;
            }
            var request = new RetrieveUserPrivilegesRequest
            {
                UserId = userId
            };
            var response = (RetrieveUserPrivilegesResponse)Connection.SourceService.Execute(request);
            return response.RolePrivileges.Any(p => p.PrivilegeId == privilege.Id);
        }

        public Guid WhoAmI()
        {
            return ((WhoAmIResponse)Connection.SourceService.Execute(new WhoAmIRequest())).UserId;
        }


        public static Entity GetAliasedEntityLateBound(Entity entity , string aliasedEntityName, string aliasedEntityIdField)
        {
            var entityLogicalName = aliasedEntityName; //EntityHelper.GetEntityLogicalName<T>();
            var aliasedEntity = new Entity();
            var idAttribute = aliasedEntityIdField;

            foreach (var entry in entity.Attributes)
            {
                AliasedValue aliased = null;

                if (entry.Value is AliasedValue)
                    aliased = (AliasedValue) entry.Value;

                if (entry.Value is AliasedValue && aliased.EntityLogicalName == entityLogicalName &&
                    (
                        (aliasedEntityName == null && // No Entity Attribute Name Specified
                            (entry.Key.Contains(".") || entry.Key == aliased.AttributeLogicalName || aliased.AttributeLogicalName == idAttribute))
                    // And the key contains a "." or is an exact match on the aliased attribute logical name.  This keeps groupings that may not be the same type (Date Group by Day) from populating the aliased entity
                    // The one exception for this is the Id. which we want to include if we can
                    ||
                        (aliasedEntityName != null && // Or an Entity Attribute Name is specified, and 
                            entry.Key.StartsWith(aliasedEntityName + ".")) // it starts with the aliasedEntityName + ".
                    ))
                {
                    aliasedEntity[aliased.AttributeLogicalName] = aliased.Value;
                    if (entity.FormattedValues.TryGetValue(entry.Key, out string formattedValue))
                    {
                        aliasedEntity.FormattedValues[aliased.AttributeLogicalName] = formattedValue;
                    }
                }
            }

            // Remove names for Entity References.  
            foreach (var entry in aliasedEntity.Attributes.Where(a => a.Key.EndsWith("name")).ToList())
            {
                var nonNameAttribute = entry.Key.Substring(0, entry.Key.Length - "name".Length);
                if (aliasedEntity.Contains(nonNameAttribute))
                {
                    if (aliasedEntity[nonNameAttribute] is EntityReference entityRef && entityRef.Name == (string)entry.Value)
                    {
                        aliasedEntity.Attributes.Remove(entry.Key);
                    }
                }
            }


            if (aliasedEntity.Attributes.Contains(idAttribute))
            {
                aliasedEntity.Id = (Guid)aliasedEntity[idAttribute];
            }

            return aliasedEntity;
        }
        #endregion Methods
    }

    

}
