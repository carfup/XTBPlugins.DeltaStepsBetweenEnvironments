using Source.DLaB.Xrm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carfup.XTBPlugins.Entities;

namespace Carfup.XTBPlugins.DeltaStepsBetweenEnvironments.AppCode
{
    public static class StepRegistration
    {
        /// <summary>
        /// Generates a step description for the given message, primary and secondary entities, for a Step
        /// </summary>
        /// <param name="typeName">Name of the plug-in type</param>
        /// <param name="messageName">Message Name</param>
        /// <param name="primaryEntity">Primary Entity</param>
        /// <param name="secondaryEntity">Secondary Entity</param>
        /// <returns>Description that the Step should use</returns>
        public static string GenerateStepDescription(string typeName, string messageName, string primaryEntity, string secondaryEntity)
        {
            var descriptionBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(typeName))
            {
                descriptionBuilder.AppendFormat("{0}: ", typeName);
            }

            if (string.IsNullOrEmpty(messageName))
            {
                descriptionBuilder.Append("Not Specified of ");
            }
            else
            {
                descriptionBuilder.AppendFormat("{0} of ", messageName);
            }

            bool hasPrimaryEntity = false;
            if (!string.IsNullOrEmpty(primaryEntity) &&
                !string.Equals(primaryEntity, "none", StringComparison.InvariantCultureIgnoreCase))
            {
                hasPrimaryEntity = true;
                descriptionBuilder.Append(primaryEntity);
            }

            if (!string.IsNullOrEmpty(secondaryEntity) &&
                !string.Equals(secondaryEntity, "none", StringComparison.InvariantCultureIgnoreCase))
            {
                string format;
                if (hasPrimaryEntity)
                {
                    format = "and {0}";
                }
                else
                {
                    format = "{0}";
                }

                descriptionBuilder.AppendFormat(format, secondaryEntity);
            }
            else if (!hasPrimaryEntity)
            {
                descriptionBuilder.Append(" any Entity");
            }

            return descriptionBuilder.ToString();
        }

        private static void LoadUsers(DeltaStepsBetweenEnvironments dsbe)
        {
           
            //Retrieve all of the users
            //var users = QueryExpressionFactory.Create<SystemUser>(p => new
            //{
            //    p.AssemblyName,
            //    p.PluginAssemblyId,
            //    p.TypeName,
            //    p.Version
            //});
        }
    }
}
