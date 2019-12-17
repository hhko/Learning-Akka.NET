using System;
using Akka.Actor;

namespace HowTo_01_CreateActor
{
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // ActorSystem 만들기
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // Actor 만들기
            // - 모든 Actor는 IActor 인터페이스 타입으로 처리한다.
            // - 모든 Actor는 Props(소품)으로 생성한다(간접 생성).
            IActorRef consoleWriterActor = 
                MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()),
                    "consoleWriterActor");  // Actor 이름

            IActorRef consoleReaderActor =
                MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriterActor)),
                    "consoleReaderActor");  // Actor 이름

            // 메시지 보내기
            // - 모든 메시지는 비동기로 전달된다(메시지 처리될 때까지 대기하지 않는다).
            consoleReaderActor.Tell("start");

            // ActorSystem 종료 대기
            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
