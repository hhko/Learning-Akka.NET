using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeployerShared
{
    public class DeployedEchoActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            //
            // Deploy할 때 환경 읽기를
            // ".WithDeploy(FromConfig.Instance);" 처럼 명시적으로 환경 설정을 지정하지 않는다.
            //
            //actor {
            //    provider = remote
            //    deployment {
            //        /DeployedEchoActor1 {
            //            remote = "akka.tcp://DeployerTarget@localhost:8091"
            //        }
            //    }
            //}
            //
            return Akka.Actor.Props.Create(() => new DeployedEchoActor());
        }

        public DeployedEchoActor()
        {
            Receive<SharedMessages.Hello>(hello =>
            {
                _log.Info($">>> Received '{hello.Message}' from {Sender} to {Self}.");

                IActorRef localActor = Sender;
                localActor.Tell(hello);
            });
        }
    }
}
