using NLog.Common;

namespace NLogToSlack.Tests
{
    public class TestableSlackTarget : SlackTarget
    {
        public void Initialize()
        {
            InitializeTarget();
        }
        
        public new void Write(AsyncLogEventInfo eventInfo)
        {
            base.Write(eventInfo);
        }
    }
}