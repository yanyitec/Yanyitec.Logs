using System;
using System.Collections.Generic;
using System.Text;

namespace Yanyitec.Logs
{
    public static class Test
    {
        public static void Main(string[] args) {
            TestLogger();
            Console.WriteLine("Press any to exit...");
            Console.ReadKey();
        }

        static void TestLogger() {
            Logger.Default.Debug("我是{0}","Debug");
            Logger.Default.Success("我是{0}", "Success");
            Logger.Default.Info("我是{0}", "Info");
            Logger.Default.Notice("我是{0}", "Notice");
            Logger.Default.Warn("我是{0}", "Warn");
            Logger.Default.Error("我是{0}", "Error");
        }
    }
}
