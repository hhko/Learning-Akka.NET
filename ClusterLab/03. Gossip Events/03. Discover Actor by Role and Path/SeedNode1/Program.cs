using Akka.Actor;
using Akka.Cluster;
using Akka.Configuration;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.RegisterCommandPalette(ClusterCommands.Instance);
            cmd.Start();

            system.ActorOf(FooActor.Props(), nameof(FooActor));

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
