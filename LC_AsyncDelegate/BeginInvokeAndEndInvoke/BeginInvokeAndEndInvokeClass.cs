using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_AsyncDelegate.BeginInvokeAndEndInvoke
{
    /// <summary>
    /// 阻塞模式异步调用
    /// </summary>
    public class BeginInvokeAndEndInvokeClass
    {
        #region 基础版
        public static void UsingEndInvoke1()
        {
            ThreadMethod.PrintInfo("UsingEndInvoke1");
            // 创建一个指向SleepOneSecond的委托
            Action invoker = new Action(SleepOneSecond1);

            // 开始执行SleepOneSecond，但这次异步调用我们传递一些参数
            // 观察Delegate.BeginInvoke()的第二个参数
            IAsyncResult tag = invoker.BeginInvoke(null, "passing some state");

            // 应用程序在此处会造成阻塞，直到SleepOneSecond执行完成
            invoker.EndInvoke(tag);

            // EndInvoke执行完毕，取得之前传递的参数内容
            string strState = (string)tag.AsyncState;

            Console.WriteLine("EndInvoke的传递参数" + tag.AsyncState.ToString());
            //Console.WriteLine("111111111111111111");
        }
        public static void SleepOneSecond1()
        {
            ThreadMethod.PrintInfo("SleepOneSecond1");
            //模拟业务处理耗时3秒
            Thread.Sleep(3000);
        }
        #endregion


        #region 异常捕获
        public static void UsingEndInvoke2()
        {
            ThreadMethod.PrintInfo("UsingEndInvoke2");
            // 创建一个指向SleepOneSecond的委托
            Action invoker = new Action(SleepOneSecond2);

            // 开始执行SleepOneSecond，但这次异步调用我们传递一些参数
            // 观察Delegate.BeginInvoke()的第二个参数
            IAsyncResult tag = invoker.BeginInvoke(null, "passing some state");

            try
            {
                // 应用程序在此处会造成阻塞，直到SleepOneSecond2执行完成
                invoker.EndInvoke(tag);
            }
            catch (Exception ex)
            {
                // 此处可以捕捉异常
                Console.WriteLine(ex.Message);
            }

            // EndInvoke执行完毕，取得之前传递的参数内容
            string strState = (string)tag.AsyncState;

            Console.WriteLine("EndInvoke的传递参数" + tag.AsyncState.ToString());
        }
        public static void SleepOneSecond2()
        {
            ThreadMethod.PrintInfo("SleepOneSecond2");
            //模拟业务处理耗时3秒
            Thread.Sleep(3000);

            throw new Exception("Here Is An Async Function Exception");
        }
        #endregion


        #region 向函数中传递参数
        public static void UsingEndInvoke3()
        {
            ThreadMethod.PrintInfo("UsingEndInvoke3");
            // 创建几个参数
            string strParam = "Param1";
            int intValue = 100;
            ArrayList list = new ArrayList();
            list.Add("Item1");

            // 创建委托对象
            Func<int, string, ArrayList, string> delFoo = new Func<int, string, ArrayList, string>(SleepOneSecond3);

            // 调用异步函数
            IAsyncResult tag = delFoo.BeginInvoke(intValue, strParam, list, null, null);

            // 通常调用线程会立即得到响应
            // 因此你可以在这里进行一些其他处理todo...

            try
            {
                // 应用程序在此处会造成阻塞，直到SleepOneSecond执行完成；执行EndInvoke来取得返回值
                delFoo.EndInvoke(tag);
            }
            catch (Exception ex)
            {
                // 此处可以捕捉异常
                Console.WriteLine(ex.Message);
            }

            // EndInvoke执行完毕，取得之前传递的参数内容
            string strResult = (string)tag.AsyncState;

            Console.WriteLine("param1: " + intValue);
            Console.WriteLine("param2: " + strParam);
            Console.WriteLine("ArrayList count: " + list.Count);//引用类型 2
        }
        public static string SleepOneSecond3(int param1, string param2, ArrayList param3)
        {
            ThreadMethod.PrintInfo("SleepOneSecond3");
            // 我们在这里改变参数值
            param1 = 100;
            param2 = "hello";
            param3.Add("Item2");

            //模拟业务处理耗时3秒
            Thread.Sleep(3000);

            return "thank you for reading me";
        }
        #endregion

        #region 向函数中传递参数 ref/out
        public delegate string DelegateWithParameters(out int param1, string param2, ref ArrayList param3);
        public static void UsingEndInvoke4()
        {
            ThreadMethod.PrintInfo("UsingEndInvoke4");
            // 创建几个参数
            string strParam = "Param1";
            int intValue = 100;
            ArrayList list = new ArrayList();
            list.Add("Item1");

            // 创建委托对象
            //Func<int, string, ArrayList, string> delFoo = new Func<int, string, ArrayList, string>(SleepOneSecond4);
            // 创建委托对象
            DelegateWithParameters delFoo = new DelegateWithParameters(SleepOneSecond4);

            // 调用异步函数
            IAsyncResult tag = delFoo.BeginInvoke(out intValue, strParam,ref list, null, null);

            // 通常调用线程会立即得到响应
            // 因此你可以在这里进行一些其他处理todo...

            try
            {
                // 应用程序在此处会造成阻塞，直到SleepOneSecond执行完成；执行EndInvoke来取得返回值
                delFoo.EndInvoke(out intValue, ref list, tag);
            }
            catch (Exception ex)
            {
                // 此处可以捕捉异常
                Console.WriteLine(ex.Message);
            }

            // EndInvoke执行完毕，取得之前传递的参数内容
            string strResult = (string)tag.AsyncState;

            Console.WriteLine("param1: " + intValue);
            Console.WriteLine("param2: " + strParam);
            Console.WriteLine("ArrayList count: " + list.Count);//ref  0
        }
        public static string SleepOneSecond4(out int param1, string param2,ref ArrayList param3)
        {
            ThreadMethod.PrintInfo("SleepOneSecond4");
            // 我们在这里改变参数值
            param1 = 10000;
            param2 = "hello1111";
            param3 = new ArrayList();

            //模拟业务处理耗时3秒
            Thread.Sleep(3000);

            return "thank you for reading me";
        }
        #endregion

    }
}
