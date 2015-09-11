using NLog;
using NLogToSlack.Models;

namespace NLogToSlack.Demo
{
    /// <summary>
    /// Demonstration of the implementation of ISlackLoggable
    /// </summary>
    public class AttachmentDemoWithAuthor : ISlackLoggable
    {

        public readonly string CustomText;

        public AttachmentDemoWithAuthor(string customText)
        {
            this.CustomText = customText;
        }

        public Attachment ToAttachment(LogEventInfo info)
        {
            return new Attachment()
            {
                Title = "I'm a custom object",
                Text = CustomText, 
                AuthorName = "Bill Gates",
                AuthorIcon = "http://www.ie.edu/IE/site/img/iconowlive.gif",
                AuthorLink = "https://fr.wikipedia.org/wiki/Bill_Gates",
                ImageUrl = "https://datadoghq.com/snapshot/path/to/snapshot.png",
                ThumbUrl = "https://datadoghq.com/snapshot/path/to/snapshot.png",
                Color = "#FF1493" // pink is the new black
            };
        }
    }
}
