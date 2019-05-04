using Akka.Actor;
using Akka.Cluster.Discovery;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode3
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private IActorRef _clusterActorDiscovery;

        public static Props Props(IActorRef clusterActorDiscovery)
        {
            return Akka.Actor.Props.Create(() => new FooActor(clusterActorDiscovery));
        }

        public FooActor(IActorRef clusterActorDiscovery)
        {
            _clusterActorDiscovery = clusterActorDiscovery;

            Receive<string>(_ => Handle(_));
            Receive<ClusterActorDiscoveryMessage.ActorUp>(_ => Handle(_));
            Receive<ClusterActorDiscoveryMessage.ActorDown>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");

        }

        protected override void PreStart()
        {
            base.PreStart();

            // 
            // 관심 액터 찾기를 감시한다.
            //
            _clusterActorDiscovery.Tell(
                new ClusterActorDiscoveryMessage.MonitorActor("SeedNode1-FooActor"));

            _clusterActorDiscovery.Tell(
                new ClusterActorDiscoveryMessage.MonitorActor("NonSeedNode1-FooActor"));

            _clusterActorDiscovery.Tell(
                new ClusterActorDiscoveryMessage.MonitorActor("NonSeedNode2-FooActor"));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Message : {msg}, Sender: {Sender}");
        }

        // 
        // 관심 액터가 클러스터에 합류하였다.
        //
        private void Handle(ClusterActorDiscoveryMessage.ActorUp msg)
        {
            _log.Info($">>> ActorUp : {msg.Tag}, {msg.Actor.Path.ToStringWithoutAddress()}");
            msg.Actor.Tell("Hello from NonSeedNode3");
        }

        // 
        // 관심 액터가 클러스터에서 제거되었다.
        //
        private void Handle(ClusterActorDiscoveryMessage.ActorDown msg)
        {
            _log.Info($">>> ActorDown : {msg.Tag}, {msg.Actor.Path.ToStringWithoutAddress()}");
        }
    }
}
