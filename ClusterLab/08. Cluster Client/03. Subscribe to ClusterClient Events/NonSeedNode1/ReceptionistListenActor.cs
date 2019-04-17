using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode1
{
    public class ReceptionistListenActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly IActorRef _receptionistActor;

        public static Props Props(IActorRef receptionistActor)
        {
            return Akka.Actor.Props.Create(() => new ReceptionistListenActor(receptionistActor));
        }

        public ReceptionistListenActor(IActorRef receptionistActor)
        {
            _receptionistActor = receptionistActor;

            Receive<ClusterClients>(_ => Handle(_));
            Receive<ClusterClientUp>(_ => Handle(_));
            Receive<ClusterClientUnreachable>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            //
            // 이벤트 등록: SubscribeClusterClients.Instance
            //      -> Receive<ClusterClients>
            //      -> Receive<ClusterClientUp>
            //      -> Receive<ClusterClientUnreachable>
            // 이벤트 해제: UnsubscribeClusterClients.Instance
            //
            //   vs.
            //
            // 명시적 확인: .Tell(GetClusterClients.Instance) 
            //      -> Receive<ClusterClients>
            //
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
            // TODO?: ClusterClients 목록이 정상적으로 전될하지 못한다.
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
            // TODO?: ClusterClient가 접속될 때 바로 전달되지 않는다.
            //
            _log.Info(">>> Received - ClusterClientUp");
            _log.Info($"\t{msg.ClusterClient.Path.ToStringWithAddress()}");
        }

        private void Handle(ClusterClientUnreachable msg)
        {
            // 
            // TODO?: 언제 호출되는지 아직 모른다.
            //          ClusterClient 종료될 때: 메시지가 전달되지 않는다.
            //          ClusterClient 정보에 변화가 있을 때: ???? 어떻게 런타임에 변경하지?
            //
            _log.Info(">>> Received - ClusterClientUnreachable");
            _log.Info($"\t{msg.ClusterClient.Path.ToStringWithAddress()}");
        }
    }
}
