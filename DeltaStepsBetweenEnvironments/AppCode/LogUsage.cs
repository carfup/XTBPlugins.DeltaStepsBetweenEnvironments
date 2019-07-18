using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace Carfup.XTBPlugins.AppCode
{
    public class LogUsage
    {

        private TelemetryClient telemetry = null;
        private bool forceLog { get; set; } = false;

        private DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe = null;
        public LogUsage(DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments dbe)
        {
            this.dbe = dbe;

            TelemetryConfiguration.Active.InstrumentationKey = CustomParameter.INSIGHTS_INTRUMENTATIONKEY;
            this.telemetry = new TelemetryClient();
            this.telemetry.Context.Component.Version = DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments.CurrentVersion;
            this.telemetry.Context.Device.Id = this.dbe.GetType().Name;
            this.telemetry.Context.User.Id = Guid.NewGuid().ToString();
        }

        public void updateForceLog()
        {
            this.forceLog = true;
        }

        public void LogData(string type, string action, Exception exception = null)
        {
            if (this.dbe.Settings.AllowLogUsage == true || this.forceLog)
            {
                switch (type)
                {
                    case EventType.Event:
                        this.telemetry.TrackEvent(action, completeLog(action));
                        break;
                    case EventType.Dependency:
                        //this.telemetry.TrackDependency(todo);
                        break;
                    case EventType.Exception:
                        this.telemetry.TrackException(exception, completeLog(action));
                        break;
                    case EventType.Trace:
                        this.telemetry.TrackTrace(action, completeLog(action));
                        break;
                }
            }

            if (this.forceLog)
                this.forceLog = false;
        }

        public void Flush()
        {
            this.telemetry.Flush();
        }


        public Dictionary<string, string> completeLog(string action = null)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "plugin", telemetry.Context.Device.Id },
                { "xtbversion", Assembly.GetEntryAssembly().GetName().Version.ToString() },
                { "pluginversion", DeltaStepsBetweenEnvironments.DeltaStepsBetweenEnvironments.CurrentVersion }
            };

            if (action != null)
                dictionary.Add("action", action);

            return dictionary;
        }

        internal void PromptToLog()
        {
            var msg = "Anonymous statistics will be collected to improve plugin functionalities.\n\n" +
                      "You can change this setting in plugin's options anytime.\n\n" +
                      "Thanks!";

            this.dbe.Settings.AllowLogUsage = true;
            MessageBox.Show(msg, "Information");
        }
    }
}
