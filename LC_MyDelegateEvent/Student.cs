using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Age { get; set; }

        public void Study()
        {
            Console.WriteLine("{0}-{1}开始学习公开课啦", this.Id, this.Name);
        }

        public static void StudyAdvanced()
        {
            Console.WriteLine("{0}-{1}开始学习VIP课啦");
        }

        public void Show()
        {
            Console.WriteLine("123");
        }

        //---------------------------------------------------------------------------------------

        /// <summary>
        /// 中国人--晚上好
        /// 美国人--good evening
        /// ...
        /// 优点：增加公共逻辑方便
        /// 缺点：假如要增加一个国家的人？就的修改方法，增加分支，那这个方法就经常改动，非常不稳定，难以维护
        /// </summary>
        /// <param name="name"></param>
        public void SayHi(string name,PeopleType peopleType)
        {
            //写log...
            switch (peopleType)
            {
                case PeopleType.Chines:
                    Console.WriteLine("{0}，晚上好", name);
                    break;
                case PeopleType.American:
                    Console.WriteLine("{0}，good evening", name);
                    break;

                //...

                default://建议：遇到不认识的枚举，说明调用有问题，那就不要隐藏，直接异常
                    throw new Exception("wrong PeopleType");
            }
        }

        //优点：逻辑分支维护简单
        //缺点：公共逻辑每个方法都需要写,违背了代码重用
        //这几个方法既有相同的东西，也有不同的东西
        //相同东西用一个方法实现，不同的各自去写，然后通过委托组合，实现代码重用和逻辑解耦
        public void SayHiChines(string name)
        {
            //写log...
            Console.WriteLine("{0}，晚上好", name);
        }
        public void SayHiAmerican(string name)
        {
            //写log...
            Console.WriteLine("{0}，good evening", name);
        }
        //...


        /// <summary>
        /// 有没有办法即增加公共逻辑方便，又逻辑分支维护简单，鱼肉熊掌怎么兼得呢？
        /// 自上往下--逻辑解耦，方便维护升级
        /// 自下往上--代码重用，去掉重复代码
        /// </summary>
        public void SayHiPerfact(string name, SayHiDelegate method)
        {
            //Action<string>
            //Func<string>
            Console.WriteLine();
            method.Invoke(name);
        }
    }

    public delegate void SayHiDelegate(string name);

    public enum PeopleType
    {
        Chines,
        American,
        //...
    }
}
