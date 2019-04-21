using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Configuration;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace ClusterClientApp1
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

            var cmd = PetabridgeCmd.Get(system);
            cmd.Start();

            //
            // Cluster 접속하기
            //
            system.ActorOf(
                ClusterClient
                    .Props(ClusterClientSettings.Create(system)),
                "ClusterClientActor");

            Console.WriteLine();
            Console.WriteLine("ClusterClientApp1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
