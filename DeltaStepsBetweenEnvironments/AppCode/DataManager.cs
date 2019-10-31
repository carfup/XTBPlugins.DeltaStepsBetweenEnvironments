using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using Carfup.XTBPlugins.Entities;
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

      

        public PluginType GetPluginTypeName(IOrganizationService service, Guid pluginTypeGuid)
        {
            return service.GetFirstOrDefault<PluginType>(new ColumnSet("name"),
                PluginType.Fields.Id, pluginTypeGuid);
        }

        public PluginType GetPluginType(IOrganizationService service, string pluginTypeName)
        {
            return service.GetFirstOrDefault<PluginType>(new ColumnSet(false),
                    PluginType.Fields.TypeName, pluginTypeName);
        }

        //return the sdk message
        public SdkMessage GetSdkMessage(IOrganizationService service, string name)
        {
            return service.GetFirstOrDefault<SdkMessage>(new ColumnSet(false),
                  SdkMessage.Fields.Name, name);
        }

        public SdkMessageProcessingStepImage GetSdkMessageProcessingStepImage(IOrganizationService service,
            Guid sdkMessageGuid)
        {
            return service.GetFirstOrDefault<SdkMessageProcessingStepImage>(new ColumnSet(true),
                SdkMessageProcessingStepImage.Fields.SdkMessageProcessingStepId, sdkMessageGuid);
        }

        // return all sdk messages
        public List<SdkMessage> GetSdkMessages(IOrganizationService service)
        {
            return service.GetEntities<SdkMessage>(new ColumnSet(false),
                  SdkMessage.Fields.IsPrivate, false)
                .OrderBy(p => p.Name).ToList();
        }

        // return all users
        public List<SystemUser> GetUsers(IOrganizationService service)
        {
            return service.GetEntities<SystemUser>(new ColumnSet(SystemUser.Fields.SystemUserId, SystemUser.Fields.FullName, SystemUser.Fields.DomainName, SystemUser.Fields.InternalEMailAddress, SystemUser.Fields.IsDisabled))
                .OrderBy(p => p.FullName).ToList();
        }

        // return the message filter
        public SdkMessageFilter GetMessageFilter(IOrganizationService service, string primaryObjectTypeCode)
        {
            return service.GetFirstOrDefault<SdkMessageFilter>(new ColumnSet(false),
                                                               SdkMessageFilter.Fields.PrimaryObjectTypeCode, primaryObjectTypeCode);
        }

        public string[] LoadSolutions()
        {
            return Connection.SourceService.GetEntities<Solution>(s => new { s.UniqueName })
                             .Select(p => p.UniqueName)
                             .OrderBy(p => p)
                             .ToArray();
        }

        public string[] LoadAssemblies()
        {
            return Connection.SourceService.GetEntities<PluginAssembly>(s => new { s.Name })
                             .Select(p => p.Name)
                             .OrderBy(p => p)
                             .ToArray();
        }

        public bool SolutionExistsInTargetEnv(string solutionPluginStepsName)
        {
            return Connection.TargetService.GetFirstOrDefault<Solution>(new ColumnSet(false), Solution.Fields.UniqueName, solutionPluginStepsName) != null;
        }

        public bool AssemblyExistsInTargetEnv(string assemblyStepsName)
        {
            return Connection.TargetService.GetFirstOrDefault<PluginAssembly>(new ColumnSet(false), PluginAssembly.Fields.Name, assemblyStepsName) != null;
        }

        public bool UserHasPrivilege(string priv, Guid userId)
        {
            var privilege = Connection.SourceService.GetFirstOrDefault<Privilege>(Privilege.Fields.Name, priv);
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

        #endregion Methods
    }

    

}
