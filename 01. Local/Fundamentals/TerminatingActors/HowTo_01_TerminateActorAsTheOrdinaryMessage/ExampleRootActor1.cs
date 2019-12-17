using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_01_TerminateActorAsTheOrdinaryMessage
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

            Thread.Sleep(100);

            //
            // RootActor2은 메시지 처리 시간이 1초다.
            // 두번째 메시지 "2"가 처리 전에
            // PoisonPill.Instance 메시지를 보낸다.
            //
            // "PoisonPill.Instance" 메시지는 일반(Ordinary) 메시지 처럼 처리된다.
            // 그 결과
            // "2" 메시지는 처리 후 Actor가 종료된다.
            //
            rootActor2.Tell(PoisonPill.Instance);
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
