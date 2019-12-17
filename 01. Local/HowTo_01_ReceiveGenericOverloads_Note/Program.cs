using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Akka.Actor;

namespace HowTo_01_ReceiveGenericOverloads_Note
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "프로그램 시작"));

            ActorSystem system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                system.Terminate();
            };

            IActorRef rootActor1 = system.ActorOf(
                ExampleRootActor1.Props(),
                ActorPaths.RootActor1.Name);

            rootActor1.Tell(new IntMessage(300));
            rootActor1.Tell(new StringMessage("Hello"));

            system.WhenTerminated.Wait();
        }
    }
}
