using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_02_FireAndForget
{
    public class ExampleRootActor2 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor2());
        }

        public ExampleRootActor2()
        {
            // 
            // 메시지 받을 Type을 명시한다.
            //
            Receive<int>(value =>
            {
                Console.WriteLine(
                    string.Format("{0:000} {1,-20} : {2}",
                        Thread.CurrentThread.ManagedThreadId,
                        Self.Path.Name,
                        value));
            });
        }
    }
}
