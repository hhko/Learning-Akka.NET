using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace HowTo_03_HandleDeadLetters
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "프로그램 시작"));

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

            //
            // Dead Letter를 처리하기 위한 Actor를 생성한다.
            //
            IActorRef rootDeadletterActor = system.ActorOf(
                ExampleRootDeadletterActor.Props(),
                ActorPaths.RootDeadletterActor.Name);

            // 
            // Dead Letter 타입 메시지를 Actor에게 연결 시킨다.
            //
            system.EventStream.Subscribe(rootDeadletterActor, typeof(DeadLetter));

            system.WhenTerminated.Wait();
        }
    }
}
