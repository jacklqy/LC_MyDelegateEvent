using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    public class Baby : IObserver
    {
        public void Action()
        {
            this.Cry();
        }

        public void Cry()
        {
            Console.WriteLine("{0} Cry", this.GetType().Name);
        }
    }
}
