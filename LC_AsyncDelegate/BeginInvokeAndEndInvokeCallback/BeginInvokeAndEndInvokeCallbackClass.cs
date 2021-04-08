using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LC_AsyncDelegate.BeginInvokeAndEndInvokeCallback
{
    /// <summary>
    /// 异步函数和回调函数都是通一个线程池中的线程，回调函数实际上是异步函数的一种延续。
    /// 为什么要这样做？也许是因为这样我们就不必过多的耗费线程池中的线程，达到线程复用的效果；通过执行在相同的线程，也可以避免不同的线程间传递上下文环境带来的损耗问题。
    /// 
    /// 
    /// 写下本文的主要目的在于总结以非阻塞模式调用函数的方法，我们应当了解以下结论：
    ///     ●  Delegate会对BeginInvoke与EndInvoke的调用生成正确的参数，所有的传出参数、返回值与异常都可以在EndInvoke中取得。
    ///     ●  不要忘记BeginInvoke是取自线程池中的线程，要注意防止异步任务的数量超过了线程池的线程上限值。
    ///     ●  CallBack委托表示对与异步任务的回调，它将使我们从阻塞的困扰中彻底解脱。
    /// </summary>
    public class BeginInvokeAndEndInvokeCallbackClass
    {

        #region 使用Callback委托：好莱坞原则”不要联系我，我会联系你”
        public static void CallSleepWithoutOutAndRefParameterWithCallback()
        {
            ThreadMethod.PrintInfo("CallSleepWithoutOutAndRefParameterWithCallback");
            // 创建几个参数
            string strParam = "Param1";
            int intValue = 100;
            ArrayList list = new ArrayList();
            list.Add("Item1");

            // 创建委托对象
            Func<int, string, ArrayList, string> delSleep = new Func<int, string, ArrayList, string>(FuncWithParameters);

            delSleep.BeginInvoke(intValue, strParam, list, new AsyncCallback(CallBack), null);
        }
        private static string FuncWithParameters(int param1, string param2, ArrayList param3)
        {
            ThreadMethod.PrintInfo("FuncWithParameters");
            // 我们在这里改变参数值
            //param1 = 1000000;
            //param2 = "hello1111";
            //param3 = new ArrayList();
            return $"param1={param1},param2={param2},param3.Count={param3.Count}";
        }


        private static void CallBack(IAsyncResult tag)
        {
            ThreadMethod.PrintInfo("CallBack");
            //  IAsyncResult实际上就是AsyncResult对象，
            //  取得它也就可以从中取得用于调用函数的委托对象
            AsyncResult result = (AsyncResult)tag;

            //  取得委托
            Func<int, string, ArrayList, string> del = (Func<int, string, ArrayList, string>)result.AsyncDelegate;

            //  取得委托后，我们需要在其上执行EndInvoke。
            //  这样就可以取得函数中的执行结果。
            string strReturnValue = del.EndInvoke(tag);

            Console.WriteLine(strReturnValue);
        }
        #endregion

        ////应用场景模拟：
        ////现在我们了解了BeginInvoke、EndInvoke、Callback的使用及特点，如何将他们运用到我们的Win Form程序中，使数据的获取不再阻塞UI线程，实现异步加载数据的效果？我们现在通过一个具体实例来加以说明。
        ////场景描述：将系统的操作日志从数据库中查询出来，并且加载到前端的ListBox控件中。
        ////要求：查询数据库的过程是个时间复杂度较高的作业，但我们的窗体在执行查询时，不允许出现”假死”的情况。
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    GetLogDelegate getLogDel = new GetLogDelegate(GetLogs);

        //    getLogDel.BeginInvoke(new AsyncCallback(LogTableCallBack), null);
        //}

        //public delegate DataTable GetLogDelegate();

        ///// <summary>
        ///// 从数据库中获取操作日志，该操作耗费时间较长，
        ///// 且返回数据量较大，日志记录可能超过万条。
        ///// </summary>
        ///// <returns></returns>
        //private DataTable GetLogs()
        //{
        //    string sql = "select * from ***";
        //    DataSet ds = new DataSet();

        //    using (OracleConnection cn = new OracleConnection(connectionString))
        //    {
        //        cn.Open();

        //        OracleCommand cmd = new OracleCommand(sql, cn);

        //        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
        //        adapter.Fill(ds);
        //    }

        //    return ds.Tables[0];
        //}

        ///// <summary>
        ///// 绑定日志到ListBox控件。
        ///// </summary>
        ///// <param name="tag"></param>
        //private void LogTableCallBack(IAsyncResult tag)
        //{
        //    AsyncResult result = (AsyncResult)tag;
        //    GetLogDelegate del = (GetLogDelegate)result.AsyncDelegate;

        //    DataTable logTable = del.EndInvoke(tag);

        //    if (this.listBox1.InvokeRequired)
        //    {
        //        this.listBox1.Invoke(new MethodInvoker(delegate ()
        //        {
        //            BindLog(logTable);
        //        }));
        //    }
        //    else
        //    {
        //        BindLog(logTable);
        //    }
        //}

        //private void BindLog(DataTable logTable)
        //{
        //    this.listBox1.DataSource = logTable;
        //}
        //以上代码在获取数据时，将不会带来任何UI线程的阻塞。


        //谈.Net委托与线程——解决窗体假死   相关链接：https://www.cnblogs.com/smartls/archive/2011/04/08/2008981.html
    }
}
