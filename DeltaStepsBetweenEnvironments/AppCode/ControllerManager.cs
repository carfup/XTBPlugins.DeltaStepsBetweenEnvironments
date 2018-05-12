using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfup.XTBPlugins.AppCode
{
    class ControllerManager
    {
        public IOrganizationService sourceService { get; set; } = null;
        public IOrganizationService targetService { get; set; } = null;
        public DataManager dataManager { get; set; } = null;

        public ControllerManager(IOrganizationService sourceService, IOrganizationService targetService)
        {
            this.sourceService = sourceService;
            this.targetService = targetService;
            this.dataManager = new DataManager(this);
        }
    }
}
