using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
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

            //
            // "{app-name} - akka.tcp://{actorysystem-name}@{hostname}:{port}"
            //
            Console.Title = $"{config.GetString("akka.system.app-name")}" +
                $" - akka.tcp://{config.GetString("akka.system.actorsystem-name")}" +
                $"@{config.GetString("akka.remote.dot-netty.tcp.hostname")}" +
                $":{config.GetString("akka.remote.dot-netty.tcp.port")}";

            ActorSystem system = ActorSystem.Create("ClusterLab", config);
            //system.ActorOf(FooActor.Props(), nameof(FooActor));

            //
            // Cluster Group Routing
            //
            IActorRef worker = system.ActorOf(
                Props
                    .Empty
                    .WithRouter(FromConfig.Instance), "Distributor");

            Thread.Sleep(20000);

            worker.Tell(0);
            worker.Tell(0);
            worker.Tell(0);

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
