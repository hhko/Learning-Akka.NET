using System;

namespace NonSeedNode3
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
