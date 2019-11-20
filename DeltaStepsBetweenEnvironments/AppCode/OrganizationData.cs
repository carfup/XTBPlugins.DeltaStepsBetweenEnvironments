using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class CarfupMessage
    {
        Entity message { get; set; }
    }

    public class OrganizationData
    {
        public List<Entity> cMessages = new List<Entity>();
        public List<Entity> cUsers = new List<Entity>();

        public OrganizationData() { }

    }
}
