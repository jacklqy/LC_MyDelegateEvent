using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyDelegateEvent
{
    public class Mother : IObserver
    {
        public void Action()
        {
            this.Whisper();
        }

        public void Whisper()
        {
            Console.WriteLine("{0} Whisper", this.GetType().Name);
        }
    }
}
