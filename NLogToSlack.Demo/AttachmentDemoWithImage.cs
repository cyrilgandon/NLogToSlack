using NLog;
using NLogToSlack.Models;

namespace NLogToSlack.Demo
{
    /// <summary>
    /// Demonstration of the implementation of ISlackLoggable
    /// </summary>
    public class AttachmentDemoWithImage : ISlackLoggable
    {
     
        public Attachment ToAttachment(LogEventInfo info)
        {
            return new Attachment()
            {
                Fallback = "Network traffic (kb/s): How does this look? @slack-ops - Sent by Julie Dodd - https://datadog.com/path/to/event",
                Title = "Network traffic (kb/s)",
                TitleLink = "https://datadog.com/path/to/event",
                Text = "How does this look? @slack-ops - Sent by Julie Dodd",
                ImageUrl = "http://i.stack.imgur.com/fD1qh.png",
                Color = "#764FA5"
            };
        }
    }
}
