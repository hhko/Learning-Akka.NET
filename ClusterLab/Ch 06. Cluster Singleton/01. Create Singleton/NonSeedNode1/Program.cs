using Akka.Actor;
using Akka.Cluster.Tools.Singleton;
using Akka.Configuration;
using Akka.Routing;
using NonSeedNodeSingletonActors;
using System;
using System.IO;
using System.Threading;

namespace NonSeedNode1
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

            system.ActorOf(ClusterSingletonManager
                .Props(
                    singletonProps: MySingletonActor.Props(),
                    terminationMessage: PoisonPill.Instance,
                    settings: ClusterSingletonManagerSettings.Create(system)
                        .WithRole("Provider")),
                name: "ConsumerSingleton");

            system.ActorOf(ClusterSingletonProxy
                .Props(
                    singletonManagerPath: "/user/ConsumerSingleton",
                    settings: ClusterSingletonProxySettings.Create(system)
                        .WithRole("Provider")),
                name: "ConsumerProxy");

            //
            // TODO ClusterSingletonManagerSettings 세부 설정
            //  - WithHandOverRetryInterval
            //  - WithRemovalMargin
            //  - WithRole
            //  - WithSingletonName
            //

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
