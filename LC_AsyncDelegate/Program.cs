using LC_AsyncDelegate.BeginInvoke;
using LC_AsyncDelegate.BeginInvokeAndEndInvoke;
using LC_AsyncDelegate.BeginInvokeAndEndInvokeCallback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_AsyncDelegate
{
    /// <summary>
    /// 异步多线程委托代码示例
    /// 重点：.NET会将所有的异步请求队列加入线程池，以线程池内的线程处理所有的异步请求。每次异步调用都会使用其中某个线程执行，但我们并不能控制具体使用哪一个线程，具体调用策略由操作系统决定。
    /// 异步方法其实是执行在线程池中的某个具体线程对象中
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ThreadMethod.TestTask();
                //1、
                BeginInvokeClass.UsingEndInvoke0();

                Console.WriteLine("**********************************************");
                //2、
                BeginInvokeAndEndInvokeClass.UsingEndInvoke1();
                Console.WriteLine("**********************************************");
                BeginInvokeAndEndInvokeClass.UsingEndInvoke2();
                Console.WriteLine("**********************************************");
                BeginInvokeAndEndInvokeClass.UsingEndInvoke3();
                Console.WriteLine("**********************************************");
                BeginInvokeAndEndInvokeClass.UsingEndInvoke4();
                Console.WriteLine("**********************************************");
                //3、
                BeginInvokeAndEndInvokeCallbackClass.CallSleepWithoutOutAndRefParameterWithCallback();

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
