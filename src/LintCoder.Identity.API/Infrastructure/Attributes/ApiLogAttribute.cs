namespace LintCoder.Identity.API.Infrastructure.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiLogAttribute : Attribute
    {
        public ApiLogEvent[] ApiLogEvents { get; set; }

        public LogLevel LogLevel { get; set; }

        public ApiLogAttribute()
        {
            ApiLogEvents = new ApiLogEvent[] { ApiLogEvent.WriteToConsole };
            LogLevel = LogLevel.Debug;
        }

        public ApiLogAttribute(LogLevel logLevel, params ApiLogEvent[] apiLogEvents)
        {
            ApiLogEvents = apiLogEvents;
            logLevel = logLevel;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ApiLogEvent : int
    {
        WriteToConsole = 0,
        WriteToFile = 1,
        WriteToMongoDB = 2,
        WriteToDB = 3
    }
}
