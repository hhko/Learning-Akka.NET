using Akka.Actor;
using Akka.Cluster;
using Akka.Configuration;
using System;
using System.IO;

namespace NonSeedNode2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            system.ActorOf(FooActor.Props(), nameof(FooActor));

            Console.WriteLine();
            Console.WriteLine("NonSeedNode2 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
