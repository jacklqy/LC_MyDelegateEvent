using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_AsyncDelegate
{
    public class ThreadMethod
    {
        private static readonly object _locke = new object();
        public static void PrintInfo(string methodName)
        {
            lock (_locke)
            {
                int intAvailableThreads, intAvailableIoAsynThreds;

                // 取得线程池内的可用线程数目，我们只关心第一个参数即可
                ThreadPool.GetAvailableThreads(out intAvailableThreads, out intAvailableIoAsynThreds);

                // 线程信息
                string strMessage =
                String.Format("是否是线程池线程：{0},线程托管ID：{1},线程池可用线程数：{2}",
                Thread.CurrentThread.IsThreadPoolThread.ToString(),
                Thread.CurrentThread.GetHashCode(),
                intAvailableThreads);

                Console.WriteLine("{0}：{1}", methodName, strMessage);
            }
        }

        public static void TestTask()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    PrintInfo("TestTask...");
                    Thread.Sleep(10000);
                }
            });
        }
    }
}
