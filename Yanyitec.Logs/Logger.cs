using System;
using System.Collections.Generic;
using System.Text;

namespace Yanyitec.Logs
{
    public class Logger : ILogger
    {
        public static Logger Default = new Logger(System.Net.Dns.GetHostName(),LogWriter.Default,"",null,null);
        public Logger(string host,ILogWriter categoryWriter, string category,ILogWriter traceWriter, string traceId) {
            this.Host = host;
            this.CategoryWriter = categoryWriter;
            if(traceWriter!=null)this.TraceWriter = traceWriter.AddLogWriter(categoryWriter);
            this.TraceId = traceId;

        }
        public string Host { get; private set; }
        public string Category { get; private set; }

        public string TraceId { get; private set; }

        public ILogWriter TraceWriter { get; private set; }
        public ILogWriter CategoryWriter { get; private set; }

        public void Message(object details,string message, params object[] args) {
            var entry = new LogEntry()
            {
                Level = LogLevels.All,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }
        public void Message( string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.All,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };

            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }
        public void Debug(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Debug,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Debug(string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Debug,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }
        public void Success(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Success,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Success( string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Success,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Info(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Info,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Info( string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Info,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Notice(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Notice,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Notice(string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Notice,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }

        public void Warn(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Warn,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }
        public void Warn(string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Warn,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            this.CategoryWriter.RecordLog(entry);
        }

        public void Error(object details, string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Error,
                Message = message,
                DetailsObject = details,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            else this.CategoryWriter.RecordLog(entry);
        }
        public void Error(string message, params object[] args)
        {
            var entry = new LogEntry()
            {
                Level = LogLevels.Error,
                Message = message,
                TraceId = TraceId,
                Category = Category,
				Host = Host,
                MessageReplacements = args
            };
            
            if (this.TraceWriter != null && this.TraceId!=null) this.TraceWriter.RecordLog(entry);
            this.CategoryWriter.RecordLog(entry);
        }
    }
}
