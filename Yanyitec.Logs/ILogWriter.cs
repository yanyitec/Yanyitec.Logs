using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Logs
{
    public interface ILogWriter: IEnumerable<ILogWriter>
    {
        void RecordLog(LogEntry entry);

        Task PersistentLogs(WritingClainNode node);

        ILogWriter AddLogWriter(ILogWriter writer);

        bool IsCollection { get; }

        IDetailsFormater Formater { get; set; }

        ILogWriter Clone();
    }
}
