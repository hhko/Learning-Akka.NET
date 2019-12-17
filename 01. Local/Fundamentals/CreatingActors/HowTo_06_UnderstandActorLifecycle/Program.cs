using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_UnderstandActorLifecycle
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-65} : Main",
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
                Props.Create(() => new ExampleRootActor2()),
                ActorPaths.RootActor2.Name);

            system.WhenTerminated.Wait();
        }
    }
}
