using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_10_MessageSendCover
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("main");
            IActorRef parentActor = system.ActorOf(ParentActor.Props(), ActorPaths.ParentActor.Name);
            IActorRef selectionActor = system.ActorOf(SelectionActor.Props(), ActorPaths.SelectActor.Name);
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                system.Terminate();
            };

            system.WhenTerminated.Wait();
        }
    }
}
