using System;

namespace Yanyitec.Logs
{
    public interface ILogger
    {
        ILoggerFactory LoggerFactory { get; }
        string Category { get; }
        ILogWriter CategoryWriter { get; }
        ILogWriter TraceWriter { get;  }

        void DebugWithDetails(object details, string message, params object[] args);
        void Debug(string message, params object[] args);
        void ErrorWithDetails(object details, string message, params object[] args);
        void Error(string message, params object[] args);

        void Error(Exception ex, string message=null, params object[] args);
        void InfoWithDetails(object details, string message, params object[] args);
        void Info(string message, params object[] args);
        void MessageWithDetails(object details, string message, params object[] args);
        void Message(string message, params object[] args);
        void NoticeWithDetails(object details, string message, params object[] args);
        void Notice(string message, params object[] args);
        void SuccessWithDetails(object details, string message, params object[] args);
        void Success(string message, params object[] args);
        void WarnWithDetails(object details, string message, params object[] args);
        void Warn(string message, params object[] args);
    }
}