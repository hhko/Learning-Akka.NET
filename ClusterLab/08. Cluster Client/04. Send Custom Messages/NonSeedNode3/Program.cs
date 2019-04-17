using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Configuration;
using System;
using System.IO;

namespace NonSeedNode3
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));

            //
            // "{app-name} - akka.tcp://{actorysystem-name}@{hostname}:{port}"
            //
            Console.Title = $"{config.GetString("akka.system.app-name")}" +
                $" - akka.tcp://{config.GetString("akka.system.actorsystem-name")}" +
                $"@{config.GetString("akka.remote.dot-netty.tcp.hostname")}" +
                $":{config.GetString("akka.remote.dot-netty.tcp.port")}";

            ActorSystem system = ActorSystem.Create(config.GetString("akka.system.actorsystem-name"), config);

            IActorRef fooActor = system.ActorOf(FooActor.Props(), nameof(FooActor));

            // 등록 함수: RegisterService
            // 해제 함수: UnregisterService
            ClusterClientReceptionist.Get(system).RegisterService(fooActor);

            Console.WriteLine();
            Console.WriteLine("NonSeedNode3 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
