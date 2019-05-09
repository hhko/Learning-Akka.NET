using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace Persistent_02_Sqlite
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("PersistenceLab", config);

            //
            // Case 1: 액터가 생성될 때 event_journal, journal_metadata 테이블이 만들어진다.
            //
            string playerName = "Messi";
            IActorRef messi = system.ActorOf(PlayerActor.Props(playerName), playerName);

            //
            // Case 2: Tell 메시지를 보내면 Persit 함수를 통해 메시지가 저장된다.
            // Case 3: Tell 메시지를 보내지 않으면 journal을 통해 상태가 복구된다.
            //
            //messi.Tell(new PlayerActor.Exercise(10));
            //messi.Tell(new PlayerActor.Exercise(20));
            //messi.Tell(new PlayerActor.Exercise(30));
            messi.Tell(new PlayerActor.Show());

            //
            // Case 4: 예외가 발생하여 액터가 다시 생성되고 journal을 통해 상태가 복구된다.
            //
            //messi.Tell(new PlayerActor.ThrowException());
            //messi.Tell(new PlayerActor.Show());

            system.WhenTerminated.Wait();
        }
    }
}
