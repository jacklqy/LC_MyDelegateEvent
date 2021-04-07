using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    /// <summary>
    /// 事情起源
    /// 大半夜猫叫了一声
    /// 
    /// baby cry
    /// dog wang
    /// father roar
    /// mother whisper
    /// ...
    /// </summary>
   public class Cat
    {

        #region 方式一
        //违背设计模式六大原则：单一职责原则和迪米特法则
        //需求是猫miao一声---触发一系列动作---代码还指定了动作
        public void Miao()
        {
            Console.WriteLine("{0} Miao 一声", this.GetType().Name);
            //触发一系列后续动作
            new Mouse().Run();//老鼠跑
            new Dog().Wang();//狗狗叫
            new Baby().Cry();//宝宝哭
            new Father().Roar();//爸爸怒
            new Mother().Whisper();//妈妈安抚
        }
        #endregion

        #region 方式二：面向对象-》观察者模式
        private List<IObserver> _iObservers = new List<IObserver>();
        public void AddObserver(IObserver observer)
        {
            this._iObservers.Add(observer);
        }
        public void MiaoObserver()
        {
            Console.WriteLine("{0} MiaoObserver 一声[面向对象-》观察者模式]", this.GetType().Name);
            //if (this.CatMiaoEvent != null)
            foreach (var observer in this._iObservers)
            {
                observer.Action();
            }
        }
        #endregion

        #region 方式三：多播委托
        public Action CatMiaoDelegate;
        public void MiaoDelegate()
        {
            Console.WriteLine("{0} MiaoDelegate 一声[多播委托]", this.GetType().Name);
            //if (this.CatMiaoEvent != null)
            CatMiaoDelegate?.Invoke();
        }
        #endregion

        #region 方式四：事件委托
        //事件Event：一个委托的实例，带一个event关键字
        //***event限制权限，只允许在事件声明类里面去invoke和赋值，不允许外面，甚至子类***
        //***事件和委托的区别与联系？委托是一种类型，事件是委托类型的一个实例，加上了event的权限控制，仅此而已***
        public event Action CatMiaoEventHandler;
        public void MiaoEvent()
        {
            Console.WriteLine("{0} CatMiaoEventHandler 一声[事件委托]", this.GetType().Name);
            //if (this.CatMiaoEvent != null)
            CatMiaoEventHandler?.Invoke();
        }
        #endregion

    }

    public class MiniCat : Cat
    {
        public void Do()
        {
            //***event限制权限，只允许在事件声明类里面去invoke和赋值，不允许外面，甚至子类***
            //this.CatMiaoEventHandler = null;//即使是子类 也不行
        }
    }
}
