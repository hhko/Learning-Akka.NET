using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_WaitForActorTermination
{
    class Program
    {
        private static ActorSystem _system;

        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "프로그램 시작"));

            _system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                _system.Terminate();
            };

            IActorRef rootActor2 = _system.ActorOf(
                ExampleRootActor2.Props(),
                ActorPaths.RootActor2.Name);

            _system.ActorOf(
                ExampleRootActor1.Props(rootActor2),
                ActorPaths.RootActor1.Name);

            _system.WhenTerminated.Wait();
        }
    }
}
