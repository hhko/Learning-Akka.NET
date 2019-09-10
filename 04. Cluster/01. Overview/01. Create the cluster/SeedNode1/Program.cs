using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

//
// TODO: 0. Hocon Multi-Lines 설정방법?
// TODO: 1. Serilog 적용
// TODO: 2. 프로그램 종료 개선
//      Console.CancelKeyPress += (sender, e) =>
//            {
//                quitEvent.Set();
//                e.Cancel = true;
//            };
// TODO: 3. README.md 파일 작성

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
// C:\Program Files\dotnet\dotnet.exe (process 5628) exited with code 0.
// To automatically close the console when debugging stops, enable Tools->Options->Debugging->Automatically close the console when debugging stops.
//
// Starting up...
// Started up successfully
// Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] is JOINING itself (with roles []) and forming a new cluster
//      자신이 스스로 새 클러스터를 생성한다.
// Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8081] to [Up]
//      자신을 Leader로 만든다.
//

namespace SeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // 환경 설정 파일 읽기: "./Configuration/App.Akka.conf"
            //
            var config = ConfigurationFactory.ParseString(
                File.ReadAllText(
                Path.Combine("Conf", "Akka.conf")));

            //
            // 콘솔 타이틀
            // 형식: [Service Name] - akka.tcp://[ActorSystem Name]@[Host Name]:[Port]
            // 예: 
            //      Service Name:       SeedNode1
            //      ActorSystem Name:   Cluster-Lab
            //      Host Name:          127.0.0.1
            //      Port:               8081
            //
            //      SeedNode1 - akka.tcp://Cluster-Lab@127.0.0.1@8081
            //
            Console.Title = $"{config.GetString("app.service-name")}" +
                $" - akka.tcp://{config.GetString("app.actorsystem-name")}" +
                $"@{config.GetString("akka.remote.dot-netty.tcp.hostname")}" +
                $":{config.GetString("akka.remote.dot-netty.tcp.port")}";

            // 
            // 액터 시스템 만들기(새 Cluster 만들기)
            //
            ActorSystem system = ActorSystem.Create(config.GetString("app.actorsystem-name"), config);

            Console.ReadLine();
        }
    }
}
