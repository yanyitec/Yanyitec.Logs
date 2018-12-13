using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Logs
{
    public class ConsoleLogWriter:LogWriter
    {
        static Dictionary<LogLevels, ConsoleFormat> Formats = new Dictionary<LogLevels, ConsoleFormat>() {
            { LogLevels.All, new ConsoleFormat(ConsoleColor.White,"All","    ") }
            ,{ LogLevels.Debug, new ConsoleFormat(ConsoleColor.Blue,"Debug","  ") }
            ,{ LogLevels.Success, new ConsoleFormat(ConsoleColor.Green,"Success","") }
            ,{ LogLevels.Info,new ConsoleFormat( ConsoleColor.White,"Info","   ") }
            ,{ LogLevels.Notice, new ConsoleFormat(ConsoleColor.Yellow,"Notice"," ") }
            ,{ LogLevels.Warn, new ConsoleFormat(ConsoleColor.Magenta,"Warn","   ") }
            ,{ LogLevels.Error,new ConsoleFormat( ConsoleColor.Red,"Error","  ")}
        };
        struct ConsoleFormat
        {
            public ConsoleFormat(ConsoleColor color, string name, string space)
            {
                this.Color = color;
                this.LevelName = name;
                this.Space = space;
            }
            public ConsoleColor Color;
            public string LevelName;
            public string Space;
        }
        protected override async Task WriteLog(LogEntry entry)
        {
            var fmt = Formats[entry.Level];
            Console.ForegroundColor = fmt.Color;

            Console.Write(" [");
            Console.Write(fmt.LevelName);
            Console.Write("]:");
            Console.Write(fmt.Space);

            Console.WriteLine(entry.LogTime);
            if (entry.Message != null)
            {
                Console.ForegroundColor = fmt.Color;
                Console.Write("#MESSAGE: ");
                Console.ForegroundColor = ConsoleColor.White;
                var message = entry.MessageReplacements == null ? entry.Message : string.Format(entry.Message, entry.MessageReplacements);
                Console.WriteLine(message);
            }

            if (entry.TraceId != null)
            {
                Console.ForegroundColor = fmt.Color;
                Console.Write("#TRACE: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(entry.TraceId);
            }

            if (entry.Category != null)
            {
                Console.ForegroundColor = fmt.Color;
                Console.Write("#CATEGORY: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(entry.Category);
            }

            if (entry.Host != null)
            {
                Console.ForegroundColor = fmt.Color;
                Console.Write("#HOST: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(entry.Host);
            }

            if (entry.DetailsObject != null)
            {
                Console.ForegroundColor = fmt.Color;
                Console.Write("#DETAILS: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(this.Formater == null ? entry.DetailsObject.ToString() : this.Formater.Format(entry.DetailsObject));
            }
            Console.WriteLine();

        }

    }
}
