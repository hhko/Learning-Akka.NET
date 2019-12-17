using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_07_ReplyToMessages
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : Main",
                    Thread.CurrentThread.ManagedThreadId,
                    string.Empty));

            ActorSystem system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                system.Terminate();
            };

            IActorRef rootActor2 = system.ActorOf(
                ExampleRootActor2.Props(),
                ActorPaths.RootActor2.Name);

            system.ActorOf(
                ExampleRootActor1.Props(rootActor2),
                ActorPaths.RootActor1.Name);


            system.WhenTerminated.Wait();
        }
    }
}
