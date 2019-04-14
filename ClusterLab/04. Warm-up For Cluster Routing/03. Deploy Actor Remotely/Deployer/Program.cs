using Akka.Actor;
using Akka.Configuration;
using DeployerShared;
using Petabridge.Cmd.Host;
using System;
using System.IO;

//
// Akka.Actor.ActorInitializationException 예외는?
//      Deploy 대상 ActorSystem에 Deploy될 Actor가 참조되어 있지 않을 때 발생한다.
//
// 예.
//[ERROR][2019-04-14 오전 7:13:13][Thread 0003][akka.tcp://DeployerTarget@localhost:8091/remote/akka.tcp/Deployer@localhost:8081/user/DeployedEchoActor1] Error while creating actor instance of type Akka.Actor.ActorBase with 0 args: ()
//Cause: [akka.tcp://DeployerTarget@localhost:8091/remote/akka.tcp/Deployer@localhost:8081/user/DeployedEchoActor1#1420962621]: Akka.Actor.ActorInitializationException: Exception during creation ---> System.TypeLoadException: Error while creating actor instance of type Akka.Actor.ActorBase with 0 args: () ---> System.InvalidOperationException: No actor producer specified!
//   at Akka.Actor.Props.DefaultProducer.Produce()
//   at Akka.Actor.Props.NewActor()
//   --- End of inner exception stack trace ---
//   at Akka.Actor.Props.NewActor()
//   at Akka.Actor.ActorCell.CreateNewActorInstance()
//   at Akka.Actor.ActorCell.<>c__DisplayClass109_0.<NewActor>b__0()
//   at Akka.Actor.ActorCell.UseThreadContext(Action action)
//   at Akka.Actor.ActorCell.NewActor()
//   at Akka.Actor.ActorCell.Create(Exception failure)
//   --- End of inner exception stack trace ---
//   at Akka.Actor.ActorCell.Create(Exception failure)
//   at Akka.Actor.ActorCell.SysMsgInvokeAll(EarliestFirstSystemMessageList messages, Int32 currentState)
//

namespace Deployer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("Deployer", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.Start();

            // 환경 설정으로 배포 설정하기
            //actor {
            //    provider = remote
            //    deployment {
            //        /DeployedEchoActor1 {
            //            remote = "akka.tcp://DeployerTarget@localhost:8091"
            //        }
            //    }
            //}
            //
            // FAQ. "EchoActor1" 이름이 일치하지 않으면 로컬로 배포된다.
            var deployedEchoActor1 = system.ActorOf(DeployedEchoActor.Props(), nameof(DeployedEchoActor) + "1");

            //
            // vs.
            //

            // 코드로 배포 설정하기
            var deployedEchoActor2 =
                system.ActorOf(
                    DeployedEchoActor
                        .Props()
                        .WithDeploy(Deploy.None.WithScope(new RemoteScope(
                            Address.Parse("akka.tcp://DeployerTarget@localhost:8091")))),
                    nameof(DeployedEchoActor) + "2");

            system.ActorOf(LocalActor.Props(deployedEchoActor1), nameof(LocalActor) + "1");
            system.ActorOf(LocalActor.Props(deployedEchoActor2), nameof(LocalActor) + "2");

            Console.WriteLine();
            Console.WriteLine("Deployer is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
