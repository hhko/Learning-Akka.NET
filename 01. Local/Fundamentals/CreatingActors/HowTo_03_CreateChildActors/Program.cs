using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_CreateChildActors
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} : Main",
                    Thread.CurrentThread.ManagedThreadId));

            ActorSystem system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                system.Terminate();
            };
            
            IActorRef rootActor1 = system.ActorOf(
                ExampleRootActor1.Props(), 
                "root-actor-name1");
            IActorRef rootActor2 = system.ActorOf(
                Props.Create(() => new ExampleRootActor2()), 
                "root-actor-name2");

            system.WhenTerminated.Wait();
        }
    }
}
