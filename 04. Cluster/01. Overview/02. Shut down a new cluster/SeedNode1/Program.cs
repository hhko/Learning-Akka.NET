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
// TODO: 3. TerminatedEvent 제거
// TODO: 4. README.md 파일 작성

//
// Cluster 생성 로그
//
// [INFO][2019-09-08 ?? 10:33:42][Thread 0001][remoting] Starting remoting
// [INFO][2019-09-08 ?? 10:33:42][Thread 0001][remoting] Remoting started; listening on addresses : [akka.tcp://Cluster-Lab@127.0.0.1:8081]
// [INFO][2019-09-08 ?? 10:33:42][Thread 0001][remoting] Remoting now listens on addresses: [akka.tcp://Cluster-Lab@127.0.0.1:8081]
// [INFO][2019-09-08 ?? 10:33:42][Thread 0001][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Starting up...
// [INFO][2019-09-08 ?? 10:33:42][Thread 0001][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Started up successfully
// [INFO][2019-09-08 ?? 10:33:43][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] is JOINING itself (with roles []) and forming a new cluster
// [INFO][2019-09-08 ?? 10:33:43][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8081] to [Up]
//
// Cluster 파괴 로그
// 
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Marked address [akka.tcp://Cluster-Lab@127.0.0.1:8081] as [Leaving]
// [INFO][2019-09-09 ?? 1:15:29][Thread 0006][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Exiting (leader), starting coordinated shutdown.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0006][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8081] to [Exiting]
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Exiting completed.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Shutting down...
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Successfully shut down
// [INFO][2019-09-09 ?? 1:15:29][Thread 0008][[akka://Cluster-Lab/system/remoting-terminator#471029042]] Shutting down remote daemon.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][akka://Cluster-Lab/system/cluster/core/daemon/heartbeatSender] Message MemberRemoved from akka://Cluster-Lab/system/cluster/core/publisher to akka://Cluster-Lab/system/cluster/core/daemon/heartbeatSender was not delivered. 1 dead letters encountered.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][akka://Cluster-Lab/system/cluster/$a] Message MemberRemoved from akka://Cluster-Lab/system/cluster/core/publisher to akka://Cluster-Lab/system/cluster/$a was not delivered. 2 dead letters encountered.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0008][[akka://Cluster-Lab/system/remoting-terminator#471029042]] Remote daemon shut down; proceeding with flushing remote transports.
// [INFO][2019-09-09 ?? 1:15:29][Thread 0018][remoting] Remoting shut down
// [INFO][2019-09-09 ?? 1:15:29][Thread 0008][[akka://Cluster-Lab/system/remoting-terminator#471029042]] Remoting shut down.
//
// Marked address [akka.tcp://Cluster-Lab@127.0.0.1:8081] as [Leaving]
// Exiting (leader), starting coordinated shutdown.
// Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8081] to [Exiting]
// Exiting completed.
// Shutting down...
// Successfully shut down
//

//
// 참고 자료: Akka.NET cluster node graceful shutdown
//      https://stackoverflow.com/questions/38309461/akka-net-cluster-node-graceful-shutdown/38325349
//      https://github.com/ZoolWay/akka-net-cluster-graceful-shutdown-samples
//

namespace SeedNode1
{
    class Program
    {
        private static readonly ManualResetEvent TerminatedEvent = new ManualResetEvent(false);

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
            cluster.RegisterOnMemberRemoved(() => MemberRemoved(system));
            cluster.Leave(cluster.SelfAddress);

            TerminatedEvent.WaitOne();
        }

        private static async void MemberRemoved(ActorSystem system)
        {
            await system.Terminate();
            TerminatedEvent.Set();
        }
    }
}
