using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNodeDeployedActors
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor())
                .WithRouter(FromConfig.Instance);
        }

        public FooActor()
        {
            Cluster cluster = Cluster.Get(Context.System);
            _log.Info($">>> Foo Address : {cluster.SelfAddress}, {Self.Path.ToStringWithAddress()}");

            Receive<int>(_ => Handle(_));
        }

        //
        // 예외가 발생한 소스 라인 정보가 전달되지 않는다.
        //
        // [ERROR][2019-04-25 오전 10:52:33][Thread 0024][akka://ClusterLab/user/FooActor] Attempted to divide by zero.
        // Cause: System.DivideByZeroException: Attempted to divide by zero.
        //   at Akka.Actor.ActorCell.HandleFailed(Failed f)
        //   at Akka.Actor.ActorCell.SysMsgInvokeAll(EarliestFirstSystemMessageList messages, Int32 currentState)
        //
        private void Handle(int msg)
        {
            _log.Info($">>> Handle : {msg}");

            int ret = 2019 / msg;
        }
    }
}
