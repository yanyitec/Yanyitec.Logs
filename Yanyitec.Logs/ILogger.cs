namespace Yanyitec.Logs
{
    public interface ILogger
    {
        string Category { get; }
        ILogWriter CategoryWriter { get; }
        string TraceId { get; }
        ILogWriter TraceWriter { get;  }

        void Debug(object details, string message, params object[] args);
        void Debug(string message, params object[] args);
        void Error(object details, string message, params object[] args);
        void Error(string message, params object[] args);
        void Info(object details, string message, params object[] args);
        void Info(string message, params object[] args);
        void Message(object details, string message, params object[] args);
        void Message(string message, params object[] args);
        void Notice(object details, string message, params object[] args);
        void Notice(string message, params object[] args);
        void Success(object details, string message, params object[] args);
        void Success(string message, params object[] args);
        void Warn(object details, string message, params object[] args);
        void Warn(string message, params object[] args);
    }
}