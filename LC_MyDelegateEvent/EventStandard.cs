using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    /// <summary>
    /// 标准事件Demo
    /// 事件三角色：事件发布者、订户、订阅者(将事件发布者和订户关联起来)
    /// </summary>
    public class EventStandard
    {
        public static void Show()
        {
            iPhoneX iPhone = new iPhoneX()
            {
                Id = 123,
                Tag = "iPhoneX10",
                Price = 5999
            };
            //订阅：把订户和发布者的事件关联起来
            iPhone.DiscountHandler += new Student().Buy;
            iPhone.DiscountHandler += new Worker().Follow;
            iPhone.Price = 4999;
        }

        /// <summary>
        /// 订户---学生 （事件发生后，自己做出对应的动作）
        /// </summary>
        public class Student
        {
            /// <summary>
            /// 购买
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void Buy(object sender, EventArgs e)
            {
                iPhoneX iPhone = (iPhoneX)sender;
                Console.WriteLine($"This is {iPhone.Tag} iPhoneX");
                XEventArgs args = (XEventArgs)e;
                Console.WriteLine($"之前价格{args.OldPrice}");
                Console.WriteLine($"现在价格{args.NewPrice}");
                Console.WriteLine("果断买啦...");
            }
        }
        /// <summary>
        /// 订户---上班族  （事件发生后，自己做出对应的动作）
        /// </summary>
        public class Worker
        {
            /// <summary>
            /// 关注
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public void Follow(object sender, EventArgs e)
            {
                iPhoneX iPhone = (iPhoneX)sender;
                Console.WriteLine($"This is {iPhone.Tag} iPhoneX");
                XEventArgs args = (XEventArgs)e;
                Console.WriteLine($"之前价格{args.OldPrice}");
                Console.WriteLine($"现在价格{args.NewPrice}");
                Console.WriteLine("关注关注...");
            }
        }


        /// <summary>
        /// 事件参数  一般会为特定的事件去封装个参数类型
        /// </summary>
        public class XEventArgs : EventArgs
        {
            public decimal OldPrice { get; set; }
            public decimal NewPrice { get; set; }
        }

        /// <summary>
        /// 事件的发布者，发布事件并且在满足条件的时候，触发事件
        /// </summary>
        public class iPhoneX
        {
            public int Id { get; set; }
            public string Tag { get; set; }

            private decimal _price;
            public decimal Price
            {
                get
                {
                    return this._price;
                }
                set
                {
                    //价格变化就触发事件
                    if (value < this._price)
                    {
                        this.DiscountHandler?.Invoke(this, new XEventArgs()
                        {
                            OldPrice = this._price,
                            NewPrice = value
                        });
                    }
                    this._price = value;
                }
            }

            /// <summary>
            /// 打折事件
            /// </summary>
            public event EventHandler DiscountHandler;
        }
    }
}
