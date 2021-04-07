using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    public class Father : IObserver
    {
        public void Action()
        {
            this.Roar();
        }

        public void Roar()
        {
            Console.WriteLine("{0} Roar", this.GetType().Name);
        }
    }
}
