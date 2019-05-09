using Akka.Actor;
using System;

namespace Persistent_01_ReReceiveMessages
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("PersistenceLab");

            IActorRef messi = system.ActorOf(PlayerActor.Props("Messi"));

            messi.Tell(new PlayerActor.Exercise(10));
            messi.Tell(new PlayerActor.Exercise(20));
            messi.Tell(new PlayerActor.Exercise(30));
            messi.Tell(new PlayerActor.Show());

            messi.Tell(new PlayerActor.ThrowException());
            messi.Tell(new PlayerActor.Show());

            system.WhenTerminated.Wait();
        }
    }
}
