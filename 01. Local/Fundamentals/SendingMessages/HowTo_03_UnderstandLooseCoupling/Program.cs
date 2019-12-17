using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_UnderstandLooseCoupling
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

            //
            // IActorRef 인터페이스에 의존한다.
            //  -> 컴파일 타임에 F12(Go To Definition)으로 구현 클래스을 찾을 수 없다(모른다).
            //
            // ExampleRootActor1 생성될 때까지 기다리지 않는다.
            //
            IActorRef rootActor1 = system.ActorOf(
                ExampleRootActor1.Props(),
                ActorPaths.RootActor1.Name);

            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "RootActor1 생성"));

            //
            // Tell 함수는 ActorSystem을 통해 간접 호출된다.
            //  -> 런타임에 F11(Step into)로 구현 클래스을 찾을 수 없다(모른다).
            //
            // 메시지가 처리될 때까지 기다리지 않는다.
            //
            rootActor1.Tell("Hello World");

            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "RootActor1 메시지 전송"));

            system.WhenTerminated.Wait();
        }
    }
}
