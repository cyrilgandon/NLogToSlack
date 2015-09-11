using NLog;
using System;

namespace NLogToSlack.Demo
{
    public static class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        
        public static void Main(string[] args)
        {
            _logger.Info("This is a piece of useful information.");
            _logger.Debug("I be debuggin' all night long");
            _logger.Warn("Warning, warning... I'm UTF8 ready : 你好世界.");
            _logger.Info("<https://alert-system.com/alerts/1234|Link to something> just to show off!");

            try
            {
                CreateBigStackTrace(20);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "KABOOM!");
            }

            var customAttachemnetDemo = new AttachmentDemoWithAuthor("Hey, you can create your own attachment too! Just pass me in the list of params of NLog methods");
            _logger.Info(customAttachemnetDemo);

            _logger.Info(new AttachmentDemoWithImage());

            Console.WriteLine("Done - check your Slack channel!");
            Console.ReadLine();
        }
        
        private static void CreateBigStackTrace(int lines)
        {
            if (lines < 1)
            {
                throw new ApplicationException("I'm the exception to the rule.");
            }
            else
            {
                CreateBigStackTrace(lines - 1);
            }
        }
    }
}