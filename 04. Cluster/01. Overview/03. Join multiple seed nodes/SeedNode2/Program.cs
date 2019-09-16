using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;
using System.Threading;

//
// TODO: 1. Serilog 적용
// TODO: 2. 프로그램 종료 개선
//      Console.CancelKeyPress += (sender, e) =>
//            {
//                quitEvent.Set();
//                e.Cancel = true;
//            };
// TODO: 3. README.md 파일 작성
//

namespace SeedNode2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(
                File.ReadAllText(
                Path.Combine("Conf", "Akka.conf")));

            Console.Title = $"{config.GetString("app.service-name")}" +
                $" - akka.tcp://{config.GetString("app.actorsystem-name")}" +
                $"@{config.GetString("akka.remote.dot-netty.tcp.hostname")}" +
                $":{config.GetString("akka.remote.dot-netty.tcp.port")}";

            ActorSystem system = ActorSystem.Create(config.GetString("app.actorsystem-name"), config);

            Console.ReadLine();

            var cluster = Akka.Cluster.Cluster.Get(system);
            cluster.RegisterOnMemberRemoved(() => system.Terminate());
            cluster.Leave(cluster.SelfAddress);

            system.WhenTerminated.Wait();
        }
    }
}
