using log4net;

namespace Common.Extensions
{
    /// <summary>
    /// Log4Net extensions to simplify Log4Net log entries
    /// </summary>
    public static class LogExtensions
    {
        #region Property
        public static ILog Logger { get; set; } = LogManager.GetLogger("FoodRatingAPi");
        #endregion

        #region Methods
        /// <summary>
        /// Log Info
        /// </summary>
        /// <param name="input">Log text</param>
        public static void Log(this string input)
        {
            Logger.Info(input);
        }
        /// <summary>
        /// Log Warning
        /// </summary>
        /// <param name="input">Warning text</param>
        public static void LogWarning(this string input)
        {
            Logger.Warn(input);
        }

        /// <summary>
        /// Log Error
        /// </summary>
        /// <param name="input">Error text</param>
        public static void LogError(this string input)
        {
            Logger.Error(input);
        }
        #endregion
    }
}
