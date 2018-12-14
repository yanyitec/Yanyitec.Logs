namespace Yanyitec.Logs
{
    public interface ILoggerFactory
    {
        ILogWriter CategoryLogWriter { get; set; }
        IDetailsFormater DetailsFormater { get; }

        LogLevels LogLevel { get; set; }
        string Host { get; }
        ILogWriter TraceLogWriter { get; set; }

        void AddCategoryWriter(ILogWriter writer);
        void AddTraceWriter(ILogWriter writer);
        ILogger GetOrCreateLogger(string category, string traceId = null);
    }
}