using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace HandleExceptionsFromGroupRoutees
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
                    .WithRouter(FromConfig.Instance)
                    .WithSupervisorStrategy(new OneForOneStrategy(ex =>
                    {
                        //
                        // 그룹 Routee 예외가 발생될 때 호출되지 않는다.
                        //
                        system.Log.Info($">>> It is not called when exceptions are raised from group-routees");
                        return Directive.Restart;
                    })),
                    "MyGroupRouterActor");

            roundRobinGroupActor.Tell(0);
            roundRobinGroupActor.Tell(1);
            roundRobinGroupActor.Tell(2);
            roundRobinGroupActor.Tell(3);

            Console.ReadLine();
        }
    }
}
