using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace CreateRouteesYourself
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("WarmupForClusterRouting", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.Start();

            system.ActorOf(ParentActor.Props(), nameof(ParentActor));

            var roundRobinGroupActor = system.ActorOf(Props.Empty
                    .WithRouter(FromConfig.Instance),
                    "MyGroupRouterActor");

            roundRobinGroupActor.Tell(0);
            roundRobinGroupActor.Tell(1);
            roundRobinGroupActor.Tell(2);
            roundRobinGroupActor.Tell(3);

            Console.ReadLine();
        }
    }
}
