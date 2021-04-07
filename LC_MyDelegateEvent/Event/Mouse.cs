using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    public class Mouse : IObserver
    {
        public void Action()
        {
            this.Run();
        }

        public void Run()
        {
            Console.WriteLine("{0} Run", this.GetType().Name);
        }
    }
}
