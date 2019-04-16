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
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            IActorRef fooActor = system.ActorOf(FooActor.Props(), nameof(FooActor) + "2");
            ClusterClientReceptionist.Get(system).RegisterService(fooActor);

            Console.WriteLine();
            Console.WriteLine("NonSeedNode3 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
