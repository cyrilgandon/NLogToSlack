using NLog;
using NLogToSlack.Models;

namespace NLogToSlack
{
    /// <summary>
    /// Implement this interface to have custom Attachment for your object
    /// </summary>
    public interface ISlackLoggable
    {
        Attachment ToAttachment(LogEventInfo info);
    }
}
