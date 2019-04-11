using Akka.Actor;
using Akka.Configuration;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace CreateOwnWorkerActors
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("LocalRoutingPool", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.Start();

            system.ActorOf(ParentActor.Props(), nameof(ParentActor));

            Console.ReadLine();
        }
    }
}
