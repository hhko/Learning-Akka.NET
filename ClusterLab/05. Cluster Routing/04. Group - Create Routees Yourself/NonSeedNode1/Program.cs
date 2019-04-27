using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
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
            //system.ActorOf(FooActor.Props(), nameof(FooActor));

            //
            // Cluster Group Routing
            //
            IActorRef worker = system.ActorOf(
                Props
                    .Empty
                    .WithRouter(FromConfig.Instance), "Distributor");

            // TODO?
            //      1. ActorPath가 모두 일치해야 한다?
            //          Ex.
            //              routees.paths = [ "/user/FooActor" ]
            //                  vs.
            //              routees.paths = [ "/user/FooActor1", "/user/FooActor2" ]    <-- 동작하지 않는다.
            //                  // 다른 노드에 있는 /user/FooActor2에게 메시지가 전송되지 않는다(DeadLetter).
            //
            //      2. Group Router 생성 후(20초) 메시지를 전송해야 한다?
            //          Routing되기 전에 바로 메일을 전송하면 전달되지 않는다.
            //          (확인 필요: Dead Letter에게도 전달되지 않는다.)
            //
            //Thread.Sleep(20000);

            worker.Tell("Hi 1");
            worker.Tell("Hi 2");
            worker.Tell("Hi 3");

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
