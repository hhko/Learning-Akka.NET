using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_TerminateActorStoppingChildren
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            IActorRef childActor = Context.ActorOf(ExampleChildActor1.Props(), ActorPaths.ChildActor1.Name);
            childActor.Tell("1");
            childActor.Tell("2");
        }

        protected override void PostStop()
        {
            //
            // Actor 종료 절차
            //
            // 1. 메일박스로부터 다음 메시지 처리를 중단한다.
            // 2. 모든 자식 Actor에게 종료 메시지(Terminate.Instance, Stop)를 보낸다.
            // 3. 모든 자식 Actor에게 종료 완료 메시지를 대기한다.
            // 4. 자신을 종료한다.
            //      finally terminating itself
            //      (invoking PostStop, 
            //          dumping mailbox, 
            //          publishing Terminated on the DeathWatch, 
            //          telling its supervisor). 
            //

            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 PostStop"));
        }
    }
}
