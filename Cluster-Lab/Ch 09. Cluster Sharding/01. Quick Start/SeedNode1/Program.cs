using Akka.Actor;
using Akka.Configuration;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using System;
using System.IO;
using Akka.Cluster;
using System.Threading;
using Akka.Cluster.Sharding;

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

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            // register actor type as a sharded entity
            var region = ClusterSharding.Get(system).Start(
                typeName: "my-actor",
                entityProps: Props.Create<MyActor>(),
                settings: ClusterShardingSettings.Create(system),
                messageExtractor: new MessageExtractor());

            // send message to entity through shard region
            region.Tell(new Envelope(shardId: 1, entityId: 1, message: "hello"));

            while (true)
            {
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }
    }

    public class MyActor : ReceiveActor
    {
        public MyActor()
        {
            Console.WriteLine("Actor Instantiated");

            ReceiveAny(o =>
            {
                Console.WriteLine("Received type: " + o.GetType().Name);
            });
        }
    }
}
