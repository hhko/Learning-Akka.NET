using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_07_ReplyToMessages
{
    public class ExampleRootActor2 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor2());
        }

        public ExampleRootActor2()
        {
            Receive<int>(value => Handle(value));
        }

        private void Handle(int value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor2",
                    value));

            //
            // 메시지를 보낸 Actor에게 다시 메시지 보내기
            //
            Context.Sender.Tell(string.Format("{0} Korea Japan World Cup", value));

            // Sender.Tell도 가능하다.
            //Sender.Tell(string.Format("{0} Korea Japan World Cup", value));
        }
    }
}
