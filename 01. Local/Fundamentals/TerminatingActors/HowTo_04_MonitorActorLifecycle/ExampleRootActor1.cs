using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_04_MonitorActorLifecycle
{
    public class ExampleRootActor1 : ReceiveActor
    {
        private IActorRef _rootActor2;

        public static Props Props(IActorRef rootActor2)
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1(rootActor2));
        }

        public ExampleRootActor1(IActorRef rootActor2)
        {
            _rootActor2 = rootActor2;

            //
            // 감시되고 있는 Actor가 종료되면 Terminated 메시지가 전송된다.
            //
            Receive<Terminated>(value => HandleTerminated(value));

            //
            // Actor Lifecycle 감시(생성과 종료)를 시작한다.
            //
            Context.Watch(_rootActor2);

            _rootActor2.Tell("1");
            _rootActor2.Tell("2");

            //
            // Root Actor2를 종료시킨다.
            //
            Thread.Sleep(100);
            _rootActor2.Tell(PoisonPill.Instance);
        }

        private void HandleTerminated(Terminated terminated)
        {
            // 
            // 감시 Actor을 식별한다.
            //
            if (terminated.ActorRef.Equals(_rootActor2))
            {
                //
                // Actor Lifecycle 감시를 중지한다.
                //
                Context.Unwatch(_rootActor2);

                Console.WriteLine(
                    string.Format("{0:000} {1,-40} : {2}",
                        Thread.CurrentThread.ManagedThreadId,
                        "ExampleRootActor1 HandleTerminated",
                        terminated.ActorRef.Path));
            }
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
