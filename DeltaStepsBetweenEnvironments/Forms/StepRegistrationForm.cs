// =====================================================================
//
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.Forms
{
    //using Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode;
   
    using System.Linq;

    public partial class StepRegistrationForm : Form
    {
        #region Private Fields

        private CarfupStep currentStep;
        private OrganizationData orgData;
      
        private DeltaStepsBetweenEnvironments m_orgControl;
        private bool m_secureConfigurationIdIsInvalid = false;

        private string m_stepName = string.Empty;

        #endregion Private Fields

        #region Public Constructors

        public StepRegistrationForm(OrganizationData orgData, CarfupStep step = null)
        {
            currentStep = step;
            orgData = orgData;

            InitializeComponent();

            // Init of the fields 
            //Initialize the auto-complete on the Message field
            var msgList = new AutoCompleteStringCollection();
            foreach (SdkMessage msg in orgData.cMessages)
            {
                msgList.Add(msg.Name);
            }
            txtMessageName.AutoCompleteCustomSource = msgList;

            cmbUsers.Sorted = false;
            foreach (SystemUser user in orgData.cUsers)
            {
                // Added this check to to prevent OutofMemoryExcetion - When an org is imported, the administrator
                // does not have a name associated with it, Adding Null Names to ComboBox throws OutofMemoryExcetion
                if (null != user && !user.IsDisabled.Value && user.FullName != null)
                {
                    cmbUsers.Items.Add(user.FullName);
                }
                // Special case to add System user
                if (user.FullName == "SYSTEM" && user.IsDisabled.Value)
                {
                    cmbUsers.Items.Add(user.FullName);
                }
            }

            if (cmbUsers.Items.Count != 0)
            {
                cmbUsers.SelectedIndex = 0;
            }

            if (currentStep != null)
            {
                m_stepName = orgData.cMessages.FirstOrDefault(x => x.SdkMessageId == currentStep.StepMessageId).Name;
                txtMessageName.Text = m_stepName;
                txtPrimaryEntity.Text = currentStep.EntityName;
                txtName.Text = currentStep.StepName;
                txtRank.Text = currentStep.StepRank.ToString();

                if (currentStep.RunAsUserId == Guid.Empty)
                {
                    cmbUsers.SelectedIndex = 0;
                }
                else
                {
                    cmbUsers.SelectedItem = orgData.cUsers.FirstOrDefault(x => x.SystemUserId == currentStep.RunAsUserId).FullName;
                }

                switch (currentStep.StepStage)
                {
                    case SdkMessageProcessingStep_Stage.Prevalidation:
                        radStagePreValidation.Checked = true;
                        break;

                    case SdkMessageProcessingStep_Stage.Preoperation:
                        radStagePreOperation.Checked = true;
                        break;

                    case SdkMessageProcessingStep_Stage.Postoperation:
                        radStagePostOperation.Checked = true;
                        break;

                    case SdkMessageProcessingStep_Stage.Postoperation_Deprecated:
                        radStagePostOperationDeprecated.Checked = true;
                        break;

                    default:
                        throw new NotImplementedException("StepStage = " + currentStep.StepStage.ToString());
                }

                switch (currentStep.StepMode)
                {
                    case SdkMessageProcessingStep_Mode.Asynchronous:

                        radModeAsync.Checked = true;
                        break;

                    case SdkMessageProcessingStep_Mode.Synchronous:

                        radModeSync.Checked = true;
                        break;

                    default:
                        throw new NotImplementedException("Mode = " + currentStep.StepMode.ToString());
                }

                switch (currentStep.StepSupportedDeployment)
                {
                    case SdkMessageProcessingStep_SupportedDeployment.Both:

                        chkDeploymentOffline.Checked = true;
                        chkDeploymentServer.Checked = true;
                        break;

                    case SdkMessageProcessingStep_SupportedDeployment.ServerOnly:

                        chkDeploymentOffline.Checked = false;
                        chkDeploymentServer.Checked = true;
                        break;

                    //case SdkMessageProcessingStep_SupportedDeployment.OfflineOnly:

                    //    chkDeploymentOffline.Checked = true;
                    //    chkDeploymentServer.Checked = false;
                    //    break;

                    default:
                        throw new NotImplementedException("Deployment = " + currentStep.StepSupportedDeployment.ToString());
                }

                //switch (currentStep.StepInvocationSource)
                //{
                //    case null:
                //    case invo.Parent:

                //        radInvocationParent.Checked = true;
                //        break;

                //    case CrmPluginStepInvocationSource.Child:

                //        radInvocationChild.Checked = true;
                //        break;

                //    default:
                //        throw new NotImplementedException("InvocationSource = " + m_currentStep.InvocationSource.ToString());
                //}

                txtDescription.Text = currentStep.StepDescription;

                txtSecureConfig.Text = currentStep.StepSecureConfiguration;

                string stepName;
                //if (this.m_currentStep.IsProfiled && org.Plugins[this.m_currentStep.PluginId].IsProfilerPlugin)
                //{
                //    //If the current step is a profiler step, the form that is displayed should use the configuration from the original step.
                //    // ProfilerConfiguration profilerConfig = OrganizationHelper.RetrieveProfilerConfiguration(this.m_currentStep);
                //    // stepName = profilerConfig.OriginalEventHandlerName;
                //    // txtUnsecureConfiguration.Text = profilerConfig.Configuration;
                //}
                //else
                //{
                //txtUnsecureConfiguration.Text = currentStep.secure;
                stepName = currentStep.StepName;
                //}

                if (stepName == GenerateDescription())
                {
                    m_stepName = GenerateDescription();
                }
                else
                {
                    m_stepName = null;
                    txtName.Text = stepName;
                }

                //if (MessageEntity != null)
                //{
                //    crmFilteringAttributes.EntityName = MessageEntity.PrimaryEntity;
                //}

                //crmFilteringAttributes.Attributes = m_currentStep.FilteringAttributes;
                chkDeleteAsyncOperationIfSuccessful.Checked = currentStep.StepAsyncAutoDelete;
                chkDeleteAsyncOperationIfSuccessful.Enabled = (currentStep.StepMode == SdkMessageProcessingStep_Mode.Asynchronous);

                Text = "Update Existing Step";
                btnRegister.Text = "Update";
            }
        }

        #endregion Public Constructors

        #region Public Properties

        //public CrmMessage Message
        //{
        //    get
        //    {
        //        if (txtMessageName.TextLength != 0)
        //        {
        //            string message = txtMessageName.Text.Trim();
        //            if (!message.Equals(m_messageRetrieved, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                m_messageRetrieved = message;
        //                m_message = m_currentStep.Message;

        //                m_messageEntityPrimaryRetrieved = null;
        //                m_messageEntitySecondaryRetrieved = null;
        //                m_messageEntityRetrieved = null;
        //            }
        //        }
        //        else
        //        {
        //            m_messageRetrieved = null;
        //            m_message = null;
        //            m_messageEntityPrimaryRetrieved = null;
        //            m_messageEntitySecondaryRetrieved = null;
        //            m_messageEntityRetrieved = null;
        //        }

        //        return m_message;
        //    }
        //}

        //public CrmMessageEntity MessageEntity
        //{
        //    get
        //    {
        //        if (Message != null)
        //        {
        //            string primaryEntity = txtPrimaryEntity.Text.Trim();
        //            if (string.IsNullOrEmpty(primaryEntity))
        //            {
        //                primaryEntity = "none";
        //            }

        //            string secondaryEntity = txtSecondaryEntity.Text.Trim();
        //            if (string.IsNullOrEmpty(secondaryEntity))
        //            {
        //                secondaryEntity = "none";
        //            }

        //            if (!string.Equals(primaryEntity, m_messageEntityPrimaryRetrieved, StringComparison.InvariantCultureIgnoreCase) ||
        //                !string.Equals(secondaryEntity, m_messageEntitySecondaryRetrieved, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                m_messageEntityPrimaryRetrieved = primaryEntity;
        //                m_messageEntitySecondaryRetrieved = secondaryEntity;
        //                m_messageEntityRetrieved = Message.FindMessageEntity(primaryEntity, secondaryEntity);
        //            }
        //        }
        //        else
        //        {
        //            m_messageEntityPrimaryRetrieved = null;
        //            m_messageEntitySecondaryRetrieved = null;
        //            m_messageEntityRetrieved = null;
        //        }

        //        return m_messageEntityRetrieved;
        //    }
        //}

        #endregion Public Properties

        #region Private Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
        }

        private void CheckAttributesSupported()
        {
            
        }

        private void CheckDeploymentSupported()
        {
            
        }

        private void cmbPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Determine what boxes to enable
            var enableV4Controls = false;
            var enable2011Controls = true;

            //Todo: After removing SdkVersion property, removing v4 based features.
            //Enable the correct controls _(CRM2011 and above)
            grpInvocation.Enabled = enableV4Controls;
            radStagePreOperation.Enabled = enable2011Controls;
            radStagePostOperation.Enabled = enable2011Controls;
            radStagePostOperationDeprecated.Enabled = enableV4Controls;
        }

        private string GenerateDescription()
        {
            //Retrieve metadata about the filter
            string messageName;
            string primaryEntity = txtPrimaryEntity.Text;
            string secondaryEntity = txtSecondaryEntity.Text;
            //if (Message == null)
            //{
            //    messageName = txtMessageName.Text;
            //}
            //else
            //{
            //    messageName = Message.Name;

            //    if (MessageEntity != null)
            //    {
            //        primaryEntity = MessageEntity.PrimaryEntity;
            //        secondaryEntity = MessageEntity.SecondaryEntity;
            //    }
            //}

            ////Retrieve the name of the type
            //string typeName;
            //if (cmbServiceEndpoint.Visible)
            //{
            //    if (null == cmbServiceEndpoint.SelectedItem)
            //    {
            //        typeName = null;
            //    }
            //    else
            //    {
            //        typeName = ((CrmServiceEndpoint)cmbServiceEndpoint.SelectedItem).Name;
            //    }
            //}
            //else
            //{
            //    if (null == cmbPlugins.SelectedItem)
            //    {
            //        typeName = null;
            //    }
            //    else
            //    {
            //        CrmPlugin plugin = ((CrmPlugin)cmbPlugins.SelectedItem);
            //        if (plugin.IsProfilerPlugin)
            //        {
            //            typeName = "Plug-in Profiler";
            //        }
            //        else
            //        {
            //            typeName = ((CrmPlugin)cmbPlugins.SelectedItem).TypeName;
            //        }
            //    }
            //}

            return StepRegistration.GenerateStepDescription("tpyeName", "messageName", primaryEntity, secondaryEntity);
            //return StepRegistration.GenerateStepDescription(typeName, messageName, primaryEntity, secondaryEntity);
        }

        private void lnkInvalidSecureConfigurationId_Click(object sender, EventArgs e)
        {
           
        }

        private void LoadEntities()
        {
            
        }

        private void MessageData_TextChanged(object sender, EventArgs e)
        {
            if (m_stepName != null)
            {
                m_stepName = GenerateDescription();
                txtName.Text = m_stepName;
            }
        }

        private void MessageEntityData_Leave(object sender, EventArgs e)
        {
            
        }

        private void radInvocationChild_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_stepName != null)
            {
                m_stepName = GenerateDescription();
                txtName.Text = m_stepName;
            }
        }

        private void radInvocationParent_CheckedChanged(object sender, System.EventArgs e)
        {
            if (m_stepName != null)
            {
                m_stepName = GenerateDescription();
                txtName.Text = m_stepName;
            }
        }

        private void radMode_CheckedChanged(object sender, EventArgs e)
        {
            chkDeleteAsyncOperationIfSuccessful.Enabled = radModeAsync.Checked;
        }

        private void txtMessageName_Leave(object sender, EventArgs e)
        {
            LoadEntities();
            CheckAttributesSupported();
        }

        private void txtMessageName_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.TextLength == 0)
            {
                m_stepName = string.Empty;
                txtName.Text = m_stepName;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (m_stepName != null &&
                !m_stepName.Equals(txtName.Text, StringComparison.InvariantCultureIgnoreCase))
            {
                m_stepName = GenerateDescription();
            }
        }

        private void txtRank_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void UpdatePluginEventHandlerControls(bool isServiceEndpoint)
        {
            cmbServiceEndpoint.Location = cmbPlugins.Location;
            cmbServiceEndpoint.Size = cmbPlugins.Size;

            grpStage.Enabled = !isServiceEndpoint;

            cmbPlugins.Enabled = !isServiceEndpoint;
            cmbPlugins.Visible = !isServiceEndpoint;

            cmbServiceEndpoint.Visible = isServiceEndpoint;
            cmbServiceEndpoint.Enabled = isServiceEndpoint;

            txtUnsecureConfiguration.Enabled = !isServiceEndpoint;
            txtSecureConfig.Enabled = !isServiceEndpoint;

            grpDeployment.Enabled = !isServiceEndpoint;
            grpMode.Enabled = !isServiceEndpoint;

            if (isServiceEndpoint)
            {
                chkDeploymentOffline.Checked = false;
                chkDeploymentServer.Checked = true;
                radStagePostOperation.Checked = true;
                radModeAsync.Checked = true;
            }
        }

        #endregion Private Methods
    }
}