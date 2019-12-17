using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_02_TerminateActorAsTheNextMessage
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props(IActorRef rootActor2)
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1(rootActor2));
        }

        public ExampleRootActor1(IActorRef rootActor2)
        {
            rootActor2.Tell("1");
            rootActor2.Tell("2");

            //
            // RootActor2은 메시지 처리 시간이 1초다.
            // 두번째 메시지 "2"가 처리 전에 
            // Stop 메시지(내부적으로는 "Terminate.Instance" 시스템 메시지가 전송된다)를 보낸다.
            //
            // "Terminate.Instance" 메시지는 "2" 메시지 보다 먼저 처리되어 Actor가 종료된다.
            // 그 결과
            // "2" 메시지는 처리하지 못한 메시지(Dead Letter)로 처리된다.
            //
            Thread.Sleep(100);
            Context.Stop(rootActor2);
        }

        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 PostStop"));
        }
    }
}
