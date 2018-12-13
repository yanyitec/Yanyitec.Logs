using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Logs
{
    public abstract class LogWriter : ILogWriter {
        public static LogWriter Default = new ConsoleLogWriter(); 
        public IDetailsFormater Formater { get; set; }
        public WritingClainNode Head { get; private set; }
        public WritingClainNode Tail { get; private set; }
        Task _WritingTask;
        public bool IsCollection => false;
        public void RecordLog(LogEntry entry) {
            var node = new WritingClainNode(entry);
            lock (this)
            {
                
                if (Head == null)
                {
                    Head = Tail = node;
                }
                else
                {

                    Tail.Next = node;
                    Tail = node;
                }
                if (_WritingTask == null)
                {
                    _WritingTask = Task.Run(this.WriteLogs);
                }
            }
        }


        async Task WriteLogs() {
            var totalKeepCount = 10;
            var keepCount = totalKeepCount;           
            var delayMilliseconds = 200;
            while (true) {
                
                WritingClainNode node = null;
                lock (this) {
                    node = this.Head;
                    this.Head = this.Tail = null;
                    
                }
                if (node == null)
                {
                    if (--keepCount <= 0)
                    {
                        this._WritingTask = null;
                        return;
                    }
                } else {
                    keepCount = totalKeepCount;
                    await this.PersistentLogs(node);
                }
                await Task.Delay(delayMilliseconds);


            }
        }

        public virtual async Task PersistentLogs(WritingClainNode node) {
            while (node != null) {
                await this.WriteLog(node.Entry);
                node = node.Next;
            }
        }

        protected abstract Task WriteLog(LogEntry entry);

        
        public ILogWriter Clone() { return this; }

        public ILogWriter AddLogWriter(ILogWriter writer)
        {
            if (this==writer) return this;
            var rs = new LogWriterCollection();
            rs.AddLogWriter(this);
            rs.AddLogWriter(writer);
            return rs;

        }

        class InternalEnumerator : IEnumerator<ILogWriter>
        {
            public InternalEnumerator(LogWriter writer) {
                this.Current = writer;
            }


            public bool IsRemoved { get; private set; }
            public ILogWriter Current { get; private set; }

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (this.IsRemoved) return false;
                return this.IsRemoved = true;
                
            }

            public void Reset()
            {
                this.IsRemoved = false;
            }
        }

        public IEnumerator<ILogWriter> GetEnumerator()
        {
            return new InternalEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new InternalEnumerator(this);
        }
    }
}
