﻿using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public class ControllerManager
    {
        public IOrganizationService SourceService { get; set; }
        public IOrganizationService TargetService { get; set; }
        public ConnectionDetail Source { get; set; }
        public ConnectionDetail Target { get; set; }
        public DataManager DataManager { get; set; }

        public ControllerManager(IOrganizationService sourceService, IOrganizationService targetService)
        {
            SourceService = sourceService;
            TargetService = targetService;
            DataManager = new DataManager(this);
        }
    }
}
