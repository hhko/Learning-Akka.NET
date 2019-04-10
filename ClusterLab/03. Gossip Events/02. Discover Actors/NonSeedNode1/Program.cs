using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Discovery;
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
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            var cluster = Cluster.Get(system);
            IActorRef clusterActorDiscovery = system.ActorOf(Props.Create(() => new ClusterActorDiscovery(cluster)), "cluster_actor_discovery");
            system.ActorOf(FooActor.Props(clusterActorDiscovery), nameof(FooActor));

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
