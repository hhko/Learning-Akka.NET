using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using System;
using System.IO;
using System.Threading;

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

            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.RegisterCommandPalette(ClusterCommands.Instance);
            cmd.Start();

            ////
            //// Cluster Group Routing
            ////
            //IActorRef worker = system.ActorOf(
            //    Props
            //        .Empty
            //        .WithRouter(FromConfig.Instance), "Distributor");

            ////
            //// TODO?: Routing되기 전에 바로 메일을 전송하면 전달되지 않는다.
            ////        Dead Letter에게도 전달되지 않는다.
            ////
            //Thread.Sleep(20000);

            //worker.Tell("Hi 1");
            //worker.Tell("Hi 2");
            //worker.Tell("Hi 3");

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
