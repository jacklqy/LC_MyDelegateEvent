using System;

namespace LC_MyDelegateEvent
{
    /// <summary>
    /// 1 委托的声明、实例化和调用
    /// 2 委托的意义：代码重用和逻辑解耦
    /// 3 泛型委托--Func Action
    /// 
    /// 委托无处不在
    /// Func Action 异步多线程 事件
    /// Framework1.0---4.7  Core到处都是委托
    /// 
    /// 4 委托的意义：异步多线程
    /// 5 委托的意义：多播委托
    /// 6 事件 观察者模式
    /// 
    /// 委托就是一个类 为什么要用委托？
    /// 这个类的实例可以放入一个方法，实例Invoke时，就调用方法，说起来还是在执行方法，为啥要做成三个步骤呢？唯一的差别，就是把方法当做一个参数放入了一个对象/变量。
    /// 自上往下--逻辑解耦，方便维护升级
    /// 自下往上--代码重用，去掉重复代码
    /// ***因为.net是向前兼容的，以前框架版本定义的委托也是兼容支持保留着的，这是历史包袱，此后，大家就不要在定义新的委托了***
    /// 
    /// --------------------------------------------------------------------------------
    /// 
    /// 事件Event：一个委托的实例，带一个event关键字
    /// ***event限制权限，只允许在事件声明类里面去invoke和赋值，不允许外面，甚至子类***
    /// ***事件和委托的区别与联系？委托是一种类型，事件是委托类型的一个实例，加上了event的权限控制，仅此而已***
    ///
    /// 事件event真的是无处不在的
    /// winform无处不在---WPF--webform服务端控件/请求级事件等...
    /// 为啥要用事件？事件究竟能干嘛？
    /// 事件（观察者模式）能把固定动作和可变动作分开，完成固定动作，把可变动作分离出去，由外部控制(扩展)
    /// 搭建框架时，恰好就需要这个特点，可以通过事件去分离可变动作，支持扩展。(事件就是一个开发的扩展接口)
    /// event为什么要限制权限？避免外部乱来。
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("**********************委托*******************");
                //{
                //    MyDelegate myDelegate = new MyDelegate();
                //    myDelegate.Show();
                //}


                //Student student = new Student()
                //{
                //    Id = 123,
                //    Name = "jack",
                //    Age = 23
                //};
                //{
                //    student.Study();
                //    student.SayHi("XXX", PeopleType.Chines);
                //    student.SayHiChines("XXX");
                //}

                //{
                //    SayHiDelegate method = new SayHiDelegate(student.SayHiChines);
                //    student.SayHiPerfact("China", method);
                //}
                //{
                //    SayHiDelegate method = new SayHiDelegate(student.SayHiAmerican);
                //    student.SayHiPerfact("USA", method);
                //}

                {
                    //面向对象-》观察者模式
                    Cat cat = new Cat();
                    //cat.Miao();
                    cat.AddObserver(new Mouse());//老鼠跑
                    cat.AddObserver(new Dog());//狗狗叫
                    cat.AddObserver(new Baby());//宝宝哭
                    cat.AddObserver(new Father());//爸爸怒
                    cat.AddObserver(new Mother());//妈妈安抚
                    cat.MiaoObserver();
                    //去除依赖，Cat就稳定了；还可以有多个Cat实例
                }
                Console.WriteLine("***************************************");
                {
                    //多播委托
                    Cat cat = new Cat();
                    //cat.Miao();
                    cat.CatMiaoDelegate += new Mouse().Run;//老鼠跑
                    cat.CatMiaoDelegate += new Dog().Wang;//狗狗叫
                    cat.CatMiaoDelegate += new Baby().Cry;//宝宝哭
                    cat.CatMiaoDelegate += new Father().Roar;//爸爸怒
                    cat.CatMiaoDelegate += new Mother().Whisper;//妈妈安抚
                    cat.MiaoDelegate();
                    //去除依赖，Cat就稳定了；还可以有多个Cat实例
                }
                Console.WriteLine("***************************************");
                {
                    //事件委托
                    Cat cat = new Cat();
                    //cat.Miao();
                    cat.CatMiaoEventHandler += new Mouse().Run;//老鼠跑
                    cat.CatMiaoEventHandler += new Dog().Wang;//狗狗叫
                    cat.CatMiaoEventHandler += new Baby().Cry;//宝宝哭
                    cat.CatMiaoEventHandler += new Father().Roar;//爸爸怒
                    cat.CatMiaoEventHandler += new Mother().Whisper;//妈妈安抚
                    cat.MiaoEvent();
                    //去除依赖，Cat就稳定了；还可以有多个Cat实例
                }
                Console.WriteLine("*********************标准事件******************");
                {
                    EventStandard.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }
    }
}
