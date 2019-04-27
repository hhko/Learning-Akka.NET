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

            Console.WriteLine();
            Console.WriteLine("Deployer is running...");
            Console.WriteLine();

            //
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

            //
            // 1. 배포된 액터에서 예외가 발생하면 배포한 액터에게 전달된다.
            // 2. 예외가 발생한 소스 라인 정보까지 제공한다.
            //
            // [ERROR]
            // [2019-04-25 오전 11:46:35]
            // [Thread 0003]
            // [akka.tcp://DeployerTarget@localhost:8091/remote/akka.tcp/Deployer@localhost:8081/user/DeployedEchoActor] Attempted to divide by zero.
            // Cause: System.DivideByZeroException: Attempted to divide by zero.
            //    at DeployerShared.DeployedEchoActor.Handle(Int32 msg) in C:\Labs\Akka.NET-Labs\ClusterLab\04. Warm-up For Cluster Routing\04. Deploy - Hanlde Exceptions Raised by Deployed Actors\DeployerShared\DeployedEchoActor.cs:line 41
            //    at DeployerShared.DeployedEchoActor.<.ctor>b__2_0(Int32 _) in C:\Labs\Akka.NET-Labs\ClusterLab\04. Warm-up For Cluster Routing\04. Deploy - Hanlde Exceptions Raised by Deployed Actors\DeployerShared\DeployedEchoActor.cs:line 34
            //    at lambda_method(Closure, Object, Action`1)
            //    at Akka.Actor.ReceiveActor.ExecutePartialMessageHandler(Object message, PartialAction`1 partialAction)
            //    at Akka.Actor.UntypedActor.Receive(Object message)
            //    at Akka.Actor.ActorBase.AroundReceive(Receive receive, Object message)
            //    at Akka.Actor.ActorCell.ReceiveMessage(Object message)
            //    at Akka.Actor.ActorCell.Invoke(Envelope envelope)
            //
            var deployedEchoActor = system.ActorOf(DeployedEchoActor.Props(), nameof(DeployedEchoActor));
            deployedEchoActor.Tell(0);

            Console.ReadLine();
        }
    }
}
