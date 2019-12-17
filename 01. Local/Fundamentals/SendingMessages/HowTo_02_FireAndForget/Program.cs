using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_02_FireAndForget
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-20} : Main",
                    Thread.CurrentThread.ManagedThreadId,
                    string.Empty));

            ActorSystem system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                system.Terminate();
            };

            IActorRef rootActor1 = system.ActorOf(
                ExampleRootActor1.Props(),
                ActorPaths.RootActor1.Name);

            IActorRef rootActor2 = system.ActorOf(
                ExampleRootActor2.Props(),
                ActorPaths.RootActor2.Name);

            //
            // Tell 호출 순서와 상관없이 메시지는 비동기적으로 전달된다.
            //  => rootActor2가 rootActor1 보다 먼저 메ㅣ지를 받을 수도 있다.
            //
            // 그러나.
            //     rootActor1.Tell("Hello World");              - 1
            //     rootActor1.Tell(new ImmutableMessage(2002)); - 2
            // 위와 같이 Tell 호출 순서 대로 메시지가 전달되는 것을 보장한다.
            //
            rootActor1.Tell("Hello World");
            rootActor1.Tell(new ImmutableMessage(2002));
            rootActor2.Tell(2017);

            system.WhenTerminated.Wait();
        }
    }
}
