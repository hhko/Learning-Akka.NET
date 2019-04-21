using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeedNode1
{
    public class ClusterClientListenActor : ReceiveActor
    {

        private readonly ILoggingAdapter _log = Context.GetLogger();
        private IActorRef _receptionistActor;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ClusterClientListenActor());
        }

        public ClusterClientListenActor()
        {
            Receive<ClusterClients>(_ => Handle(_));
            Receive<ClusterClientUp>(_ => Handle(_));
            Receive<ClusterClientUnreachable>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            base.PreStart();

            _receptionistActor = ClusterClientReceptionist.Get(Context.System).Underlying;

            // 
            // 이벤트 등록: SubscribeClusterClients.Instance
            //      -> Receive<ClusterClients>
            //      -> Receive<ClusterClientUp>
            //      -> Receive<ClusterClientUnreachable >
            // 이벤트 제거: UnsubscribeClusterClients.Instance
            //
            //   vs.
            //
            // 명시적 확인 : GetClusterClients.Instance
            //      -> 
            _receptionistActor.Tell(SubscribeClusterClients.Instance);
        }

        protected override void PostStop()
        {
            base.PostStop();

            _receptionistActor.Tell(UnsubscribeClusterClients.Instance);
        }

        private void Handle(ClusterClients msg)
        {
            //
            // 시작하면 바로 메시지가 전달된다.
            //
            _log.Info(">>> Received - ClusterClients");
            foreach (ActorPath clusterClient in msg.ClusterClientsList)
            {
                _log.Info($"\t{clusterClient.ToStringWithAddress()}");
            }
        }

        private void Handle(ClusterClientUp msg)
        {
            // 
            //
            _log.Info(">>> Received - ClusterClientUp");
            _log.Info($"\t{msg.ClusterClient.Path.ToStringWithAddress()}");
        }

        private void Handle(ClusterClientUnreachable msg)
        {
            // 
            //
            _log.Info(">>> Received - ClusterClientUnreachable");
            _log.Info($"\t{msg.ClusterClient.Path.ToStringWithAddress()}");
        }
    }
}
