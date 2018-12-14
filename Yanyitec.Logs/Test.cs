using System;
using System.Collections.Generic;
using System.Text;

namespace Yanyitec.Logs
{
    public static class Test
    {
        public static void Main(string[] args) {
            TestLogger(Logger.Default);
            Console.WriteLine("Press any to exit...");
            Console.ReadKey();
        }

        static void TestLogger(ILogger logger) {
            logger.Debug("我是{0}","Debug");
            logger.Success("我是{0}", "Success");
            logger.Info("我是{0}", "Info");
            logger.Notice("我是{0}", "Notice");
            logger.Warn("我是{0}", "Warn");
            logger.Error("我是{0}", "Error");
        }
    }
}
