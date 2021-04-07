using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LC_MyDelegateEvent
{
    //委托本质就是一个特殊的类
    //声明委托：写在类外面
    public delegate void NoReturnNoParaOutClass();
    public class MyDelegate //: System.MulticastDelegate 特殊类不能被继承
    {
        //声明委托：写在类里面
        /// <summary>
        /// 1 委托在IL里面就是一个类
        /// 2 继承自System.MulticastDelegate
        /// </summary>
        public delegate void NoReturnNoPara();
        public delegate void NoReturnWithPara(int x, int y);
        public delegate int WithReturnNoPara();
        public delegate MyDelegate WithReturnWithPara(out int x, ref int y);

        public void Show()
        {
            Student student = new Student()
            {
                Id = 1,
                Name = "小王"
            };
            student.Study();
            Console.WriteLine("*********************最简单的委托调用方式*********************");
            {
                //委托的实例化 要求传递一个参数类型 返回值都跟委托一致的方法
                NoReturnNoPara noReturnNoPara = new NoReturnNoPara(this.DoNothing);
                //启动一个线程完成计算(多线程)
                //noReturnNoPara.BeginInvoke(null, null);
                //调用方式一：委托实例的调用 参数和委托约束的一致
                noReturnNoPara.Invoke();
                //调用方式二：可以省略.Invoke()
                noReturnNoPara();
                
                //等待异步调用结束 等待noReturnNoPara.BeginInvoke(null, null)调用结束
                //noReturnNoPara.EndInvoke(null);

                //效果等同于直接调用方法
                this.DoNothing();
            }
            {
                WithReturnWithPara withReturnWithPara = new WithReturnWithPara(this.ReturnPara);//严格一致
                //调用和上面类似
            }
            {
                //Action Func  .NetFramework3.0出现的

                //Action 系统提供 0到16个泛型参数 不带返回值的委托
                //Action action = new Action(this.DoNothing);
                Action action = this.DoNothing;//是个语法糖，就是编译器帮我们添加上的new Action
                Action<int> action1 = this.ShowInt;//in逆变  协变
                //Action<int,string,DateTime,...> action16 = new Action<int, string, DateTime,...>(method);//最多支持16个参数

                //Func 系统提供 0到16个泛型参数 带泛型返回值的委托
                Func<int> func0 = this.Get;
                int iResult = func0.Invoke();
                Func<int, string> func1 = this.GetToString;
                string rResult = func1.Invoke(1);
                //Func<int,string,DateTime,...> action16 = new Func<int, string, DateTime,...>(method);//最多支持16个参数+1个返回参数
            }
            {
                Action action = this.DoNothing;
                NoReturnNoPara noReturnNoPara= this.DoNothing;
                //为什么框架要提供这种委托呢？框架提供这种封装，自然是希望大家都统一使用Action/Func
                this.DoAction(action);//这里可以
                //this.DoAction(noReturnNoPara);//这里为什么不行？因为委托的本质是类，它们是不同的类，虽然实例化都可以传相同的方法，但是没有父子关系，所以是不能替换的。

                //更进一步，在框架中出现过N多个委托,委托的签名是一致的，实例化完的对象不能通用
                //new Thread(new ThreadStart())
                //new Thread(new ParameterizedThreadStart())
                //ThreadPool.QueueUserWorkItem(new WaitCallback())
                //Task.Run(new Action<object>());
                //本身实例化的时候，参数都是一样的，但是确实不同的类型，导致无法通用，所以框架就提供了Action/Func
                //Action/Func框架预定义的，新的API一律基于这些委托类封装。

                //***因为.net是向前兼容的，以前框架版本定义的委托也是兼容支持保留着的，这是历史包袱，此后，大家就不要在定义新的委托了***
            }

            {
                //多种途径实例化：实例化的限制就是方法的参数列表+返回值必须和委托约束的一致
                {
                    NoReturnNoPara method = new NoReturnNoPara(this.DoNothing);//当前实例的方法
                    Action action = this.DoNothing;
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(DoNothingStatic);//当前类型静态方法
                    Action action = DoNothingStatic;
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(new Student().Study);//其它实例的方法
                    Action action = new Student().Study;
                }
                {
                    NoReturnNoPara method = new NoReturnNoPara(Student.StudyAdvanced);//其它类静态方法
                    Action action = Student.StudyAdvanced;
                }
            }
            {
                //多播委托：任何一个委托都是多播委托(MulticastDelegate)类型的子类，可以通过+=去添加方法
                //多播委托有啥用呢？一个委托实例包含多个方法，可以通过+=和-=去增加/移除方法，Invoke时可以按顺序执行全部动作。

                //+= 给委托的实例添加方法，会形成方法链，Invoke时，会按顺序执行系列方法
                Action action = this.DoNothing;//当前实例的方法
                action += DoNothingStatic;//当前类型静态方法
                action += new Student().Study;//其它实例的方法
                action += Student.StudyAdvanced;//其它类静态方法
                //action.BeginInvoke(null, null);//调用异常，多播委托不支持多线程调用
                foreach (Action item in action.GetInvocationList())
                {
                    item.Invoke();
                    //item.BeginInvoke(null,null);这样遍历调用就可以
                }
                action.Invoke();
                //-=给委托的实例移除方法，从方法链的尾部开始匹配，遇到第一个完全吻合的，移除，且只移除一个，如果没有匹配，就不会移除。
                action -= DoNothingStatic;
                action -= new Student().Study;//去不掉，原因是不同实例的相同方法，引用地址不一样无法移除之前的。
                action -= Student.StudyAdvanced;
                action.Invoke();
                //多播委托中间出现未捕获异常，方法链直接结束。

                Func<int> func = this.Get;
                func += this.Get2;
                func += this.Get3;
                int iResult = func.Invoke();//调用返回结果是3。以最后一个为准，前面的丢失了，所以一般多播委托用的是不带返回值的。

            }
        }
        private void DoAction(Action act)
        {
            act.Invoke();
        }
        private void ShowInt(int i)
        {

        }
        private void DoNothing()
        {
            Console.WriteLine("This is DoNothing");
        }
        private static void DoNothingStatic()
        {
            Console.WriteLine("This is DoNothingStatic");
        }
        private int Get()
        {
            return 1;
        }
        private int Get2()
        {
            return 2;
        }
        private int Get3()
        {
            return 3;
        }
        private string GetToString(int i)
        {
            return i.ToString();
        }

        private MyDelegate ReturnPara(out int x, ref int y)
        {
            throw new Exception("");
        }
    }
}
