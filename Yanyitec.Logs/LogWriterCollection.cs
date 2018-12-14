using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Logs
{
    public class LogWriterCollection : ILogWriter
    {
        List<ILogWriter> _Writers;
        public LogWriterCollection()
        {
            this._Writers = new List<ILogWriter>();
        }

        public LogWriterCollection(IEnumerable<ILogWriter> writers) : this() {
            foreach (var w in writers) this.AddLogWriter(w);
        }
        public LogWriterCollection(params ILogWriter[] writers) : this()
        {
            foreach (var w in writers) this.AddLogWriter(w);
        }
        public bool IsCollection => true;

        public IDetailsFormater Formater {
            get { return this._Writers.FirstOrDefault()?.Formater; }
            set { foreach (var w in this._Writers) w.Formater = value; }
        }

        public void RecordLog(LogEntry entry)
        {
            foreach (var writer in _Writers) {
                writer.RecordLog(entry);
            }
        }

        public ILogWriter AddLogWriter(ILogWriter writer) {
            if (writer == null) return this;
            if (writer.IsCollection)
            {
                foreach (var wr in writer)
                {
                    
                    if (_Writers.Any(w => w.Equals(wr) )) continue;

                }
            }
            else {
                _Writers.Add(writer);
            }
            
            return this;

        }

        public Task PersistentLogs(WritingClainNode node)
        {
            throw new InvalidOperationException("LogWriterCollection不应该调用该方法，调用该方法前应该检查IsCollection属性");
        }

        public ILogWriter Clone(string category) {
            var ls = new List<ILogWriter>();
            foreach (var w in this._Writers) ls.Add(w.Clone(category));
            return new LogWriterCollection(ls);
        }

        public IEnumerator<ILogWriter> GetEnumerator()
        {
            return this._Writers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._Writers.GetEnumerator();
        }
    }
}
