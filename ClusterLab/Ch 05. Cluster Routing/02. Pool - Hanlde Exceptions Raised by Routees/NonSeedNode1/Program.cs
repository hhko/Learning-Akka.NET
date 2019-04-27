using Akka.Actor;
using Akka.Configuration;
using NonSeedNodeDeployedActors;
using System;
using System.IO;
using System.Threading;

namespace NonSeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            IActorRef fooActor = system.ActorOf(FooActor.Props(), nameof(FooActor));

            //
            // TODO? : 배포된 이후에 Tell 한것 만 메시지가 전송된다.
            //
            Thread.Sleep(20000);
            fooActor.Tell(0);   // NonSeedNode2(Remote Deploy), 예외

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
