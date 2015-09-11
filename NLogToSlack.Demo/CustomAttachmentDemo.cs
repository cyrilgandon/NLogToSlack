using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLogToSlack.Models;

namespace NLogToSlack.Demo
{
    /// <summary>
    /// Demonstration of the implementation of ISlackLoggable
    /// </summary>
    public class CustomAttachmentDemo : ISlackLoggable
    {

        public readonly string CustomText;

        public CustomAttachmentDemo(string customText)
        {
            this.CustomText = customText;
        }

        public Attachment ToAttachment(LogEventInfo info)
        {
            return new Attachment()
            {
                Title = "I'm a custom object",
                Text = CustomText
            };
        }

    }
}
