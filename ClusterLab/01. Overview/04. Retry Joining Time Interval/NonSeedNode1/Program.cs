using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace NonSeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            var clusterName = config.GetString("akka.cluster.name");

            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            system.Log.Info(">>> Non-SeedNode1 is running.");

            Console.ReadLine();
        }
    }
}
