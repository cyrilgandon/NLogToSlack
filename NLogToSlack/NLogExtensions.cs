using NLog;

namespace NLogToSlack
{
    public static class NLogExtensions
    {
        /// <summary>
        /// Return a color string from the level of an NLog message
        /// </summary>
        /// <param name="level">The NLog message level</param>
        /// <returns>A string that can be one of [good, warning, danger], or grey #cccccc</returns>
        public static string ToSlackColor(this LogLevel level)
        {
            switch (level.Name.ToLowerInvariant())
            {
                case "warn":
                    return "warning";

                case "error":
                case "fatal":
                    return "danger";

                case "info":
                    return "good";

                default:
                    return "#cccccc";
            }
        }
    }
}
