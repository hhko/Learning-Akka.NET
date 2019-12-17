using System;
using Akka.Actor;

namespace HowTo_02_FireAndForget_Note
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("basesystem");
            IActorRef RootActor = system.ActorOf(ExampleRootActor1.Props(), "root-actor-name1");
            IActorRef RootActor2 = system.ActorOf(ExampleRootActor2.Props(), "root-actor-name2");

            char index;
            for (int i = 0; i <= 25; i++)
            {
                index = Convert.ToChar(65 + i);
                RootActor.Tell(i);
                RootActor2.Tell(index);
            }


            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                //ActorSystem 종료
                system.Terminate();
            };

            //ActorSystem 종료 대기
            system.WhenTerminated.Wait();
        }
    }
}
