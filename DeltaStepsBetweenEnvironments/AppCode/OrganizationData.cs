using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfup.XTBPlugins.Entities;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class CarfupMessage
    {
        SdkMessage message { get; set; }
    }

    public class OrganizationData
    {
        public List<SdkMessage> cMessages = new List<SdkMessage>();
        public List<SystemUser> cUsers = new List<SystemUser>();

        public OrganizationData() { }

    }
}
