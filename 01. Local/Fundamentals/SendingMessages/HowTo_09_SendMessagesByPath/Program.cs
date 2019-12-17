using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_09_SendMessagesByPath
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

            system.ActorOf(
                ExampleRootActor1.Props(),
                ActorPaths.RootActor1.Name);

            system.ActorOf(
                ExampleRootActor2.Props(),
                ActorPaths.RootActor2.Name);

            system.WhenTerminated.Wait();
        }
    }
}
