using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_CreateChildActors
{
    public class ExampleRootActor2 : ReceiveActor
    {
        public ExampleRootActor2()
        {
            Console.WriteLine(
                string.Format("{0:000} : ExampleRootActor2 Constructor",
                    Thread.CurrentThread.ManagedThreadId));
        }
    }
}
