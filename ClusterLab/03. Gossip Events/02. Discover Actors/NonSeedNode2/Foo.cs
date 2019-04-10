using Akka.Actor;
using Akka.Cluster.Discovery;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode2
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private IActorRef _clusterActorDiscovery;

        public static Props Props(IActorRef clusterActorDiscovery)
        {
            return Akka.Actor.Props.Create(() => new FooActor(clusterActorDiscovery));
        }

        public FooActor(IActorRef clusterActorDiscovery)
        {
            _clusterActorDiscovery = clusterActorDiscovery;

            Receive<string>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");

        }

        protected override void PreStart()
        {
            base.PreStart();

            // 
            // 관심 액터를 등록한다.
            // "NonSeedNode2-FooActor"은 클러스터 환경에서 유일한 식별 값이어야 한다.
            //
            _clusterActorDiscovery.Tell(
                new ClusterActorDiscoveryMessage.RegisterActor(Self, "NonSeedNode2-FooActor"));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Message : {msg}, Sender: {Sender}");

            IActorRef nonSeedNode3Foo = Sender;
            nonSeedNode3Foo.Tell("Callback Hello");
        }
    }
}
