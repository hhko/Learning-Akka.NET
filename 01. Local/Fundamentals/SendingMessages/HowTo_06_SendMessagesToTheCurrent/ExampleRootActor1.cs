using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_SendMessagesToTheCurrent
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Receive<string>(value => Handle(value));

            //
            // 현재 Actor에게 메시지 보내기
            //
            Context.Self.Tell("Self.Tell");

            // Self.Tell도 가능하다.
            //Self.Tell("Self.Tell");
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1",
                    value));
        }
    }
}
