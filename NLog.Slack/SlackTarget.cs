using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Slack.Models;
using NLog.Targets;
using System;
using System.Diagnostics;

namespace NLog.Slack
{
    [Target("Slack")]
    public class SlackTarget : TargetWithLayout
    {
        private readonly Process _currentProcess = Process.GetCurrentProcess();

        [RequiredParameter]
        public string WebHookUrl { get; set; }

        public SimpleLayout Channel { get; set; }

        public SimpleLayout Username { get; set; }

        public string Icon { get; set; }

        public bool Compact { get; set; }

        protected override void InitializeTarget()
        {
            if (string.IsNullOrWhiteSpace(this.WebHookUrl))
            {
                throw new ArgumentOutOfRangeException("WebHookUrl", "Webhook URL cannot be empty.");
            }

            Uri uriResult;
            if (!Uri.TryCreate(this.WebHookUrl, UriKind.Absolute, out uriResult))
            {
                throw new ArgumentOutOfRangeException("WebHookUrl", "Webhook URL is an invalid URL.");
            }

            if (!string.IsNullOrWhiteSpace(this.Channel.Text)
                && (!this.Channel.Text.StartsWith("#") && !this.Channel.Text.StartsWith("@") && !this.Channel.Text.StartsWith("${")))
            {
                throw new ArgumentOutOfRangeException("Channel", "The Channel name is invalid. It must start with either a # or a @ symbol or use a variable.");
            }

            base.InitializeTarget();
        }
        
        protected override void Write(AsyncLogEventInfo info)
        {
            try
            {
                this.SendToSlack(info);
            }
            catch (Exception e)
            {
                info.Continuation(e);
            }
        }

        private void SendToSlack(AsyncLogEventInfo info)
        {
            var message = Layout.Render(info.LogEvent);
            var payload = new Payload()
            {
                Text = message
            };

            var channel = this.Channel.Render(info.LogEvent);
            if (!string.IsNullOrWhiteSpace(channel))
            {
                payload.Channel = channel;
            }

            if (!string.IsNullOrWhiteSpace(this.Icon))
            {
                payload.SetIcon(this.Icon);
            }

            string username = this.Username.Render(info.LogEvent);
            if (!string.IsNullOrWhiteSpace(username))
            {
                payload.Username = username;
            }

            if (!this.Compact)
            {
                var color = GetSlackColorFromLogLevel(info.LogEvent.Level);
                var attachment = new Attachment() { Fallback = message, Color = color };

                string machineName = _currentProcess.MachineName != "." ? _currentProcess.MachineName : Environment.MachineName;
                attachment.Fields.Add(new Field() { Title = "Process Name", Value = $"{machineName}\\{_currentProcess.ProcessName}", Short = true });
                attachment.Fields.Add(new Field() { Title = "Process PID", Value = _currentProcess.Id.ToString(), Short = true });
                payload.Attachments.Add(attachment);

                var exception = info.LogEvent.Exception;
                if (exception != null)
                {
                    var exceptionAttachment = new Attachment() { Fallback = exception.Message, Color = color };
                    exceptionAttachment.Fields.Add(new Field() { Title = "Type", Value = exception.GetType().FullName, Short = true });

                    if (!string.IsNullOrWhiteSpace(exception.StackTrace))
                        exceptionAttachment.Text = exception.StackTrace;

                    payload.Attachments.Add(exceptionAttachment);
                }

            }

            payload.SendTo(this.WebHookUrl);
        }
        
        private static string GetSlackColorFromLogLevel(LogLevel level)
        {
            switch (level.Name.ToLowerInvariant())
            {
                case "warn":
                    return "warning";

                case "error":
                case "fatal":
                    return "danger";

                case "info":
                    return "#2a80b9";

                default:
                    return "#cccccc";
            }
        }
    }
}