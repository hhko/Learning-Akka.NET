using Akka.Actor;
using Akka.Configuration;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace SeedNode1
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

            //_targetReceptionist.Tell(SubscribeClusterClients.Instance);

            var cmd = PetabridgeCmd.Get(system);
            cmd.RegisterCommandPalette(ClusterCommands.Instance);
            cmd.Start();

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();

            system.Terminate().Wait();
        }
    }
}
