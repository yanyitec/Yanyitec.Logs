using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Yanyitec.Logs
{
    public abstract class DbLogWriter : LogWriter
    {
        public DbLogWriter(string logDbName) {
            this.DbName = logDbName;
        }

        
        public string DbName { get; private set; }

        public override bool Equals(ILogWriter obj)
        {
            var other = obj as DbLogWriter;
            if (other == null) return false;
            return other.DbName == this.DbName;
        }

        

        public override async Task PersistentLogs(WritingClainNode node)
        {
            using (var conn = this.GetOrCreateConnection(this.DbName)) {
                conn.Open();
                while (node != null)
                {
                    var cmd = this.BuildCommand(conn,node.Entry);
                    try {
                        cmd.ExecuteNonQuery();
                    } catch(Exception ex) {
                        var c = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.StackTrace);
                        Console.ForegroundColor = c;
                    }
                    
                    node = node.Next;
                }
            }
                
        }

        protected virtual string MakeSql() {
            var sql = "INSERT INTO __Logs (Category,Details,Host,Level,LogTime,Message,TraceId) VALUES(?,?,?,?,?,?,?)";
            return sql;
        }

        protected abstract IDbConnection GetOrCreateConnection(string connName);
        protected IDbCommand Command;
        protected virtual IDbCommand BuildCommand( IDbConnection conn, LogEntry entry) {
            if (Command != null)
            {
                Command.Parameters.Clear();
                AddParameters(Command, entry, this.ParameterNeedName);
            }
            else {
                Command = conn.CreateCommand();
                AddParameters(Command, entry, this.ParameterNeedName);
            }
            return Command;
        }

        protected virtual bool ParameterNeedName{get;set;}

        void AddParameters(IDbCommand cmd, LogEntry entry,bool namedParam=false) {
            var dbParam = cmd.CreateParameter();
            if(namedParam)dbParam.ParameterName = "@Category";
            dbParam.DbType = DbType.String;
            dbParam.Value = entry.Category;
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam)dbParam.ParameterName = "@Details";
            dbParam.DbType = DbType.String;
            dbParam.Value = entry.DetailsObject==null?string.Empty:(Formater==null?entry.DetailsObject.ToString():Formater.Format(entry.DetailsObject));
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam) dbParam.ParameterName = "@Host";
            dbParam.DbType = DbType.String;
            dbParam.Value = entry.Host;
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam) dbParam.ParameterName = "@Level";
            dbParam.DbType = DbType.String;
            dbParam.Value = Enum.GetName(typeof(LogLevels),entry.Level);
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam) dbParam.ParameterName = "@LogTime";
            dbParam.DbType = DbType.DateTime;
            dbParam.Value = entry.LogTime;
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam) dbParam.ParameterName = "@Message";
            dbParam.DbType = DbType.String;
            dbParam.Value = entry.Message==null?string.Empty:(entry.MessageReplacements==null?entry.Message:string.Format(entry.Message,entry.MessageReplacements));
            cmd.Parameters.Add(dbParam);

            dbParam = cmd.CreateParameter();
            if (namedParam) dbParam.ParameterName = "@TraceId";
            dbParam.DbType = DbType.String;
            dbParam.Value = entry.TraceId;
            cmd.Parameters.Add(dbParam);
        }
    }
}
