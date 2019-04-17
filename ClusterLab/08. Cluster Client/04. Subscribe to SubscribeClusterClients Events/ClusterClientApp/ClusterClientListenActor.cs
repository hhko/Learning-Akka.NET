using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ClusterClientApp
{
    public class ClusterClientListenActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private IActorRef _clusterClientActor;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ClusterClientListenActor());
        }

        public ClusterClientListenActor()
        {
            Receive<ContactPoints>(_ => Handle(_));
            Receive<ContactPointAdded>(_ => Handle(_));
            Receive<ContactPointRemoved>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            _clusterClientActor = Context.ActorOf(
                ClusterClient
                    .Props(ClusterClientSettings.Create(Context.System)),
                "ClusterClientActor");

            //
            // 이벤트 등록: SubscribeContactPoints.Instance 
            //      -> Receive<ContactPoints>
            //      -> Receive<ContactPointAdded>
            //      -> Receive<ContactPointRemoved>
            // 이벤트 제거: UnsubscribeContactPoints.Instance
            //
            //   vs.
            //
            // 명시적 확인: GetContactPoints.Instance 
            //      -> Receive<ContactPoints>
            //
            _clusterClientActor.Tell(SubscribeContactPoints.Instance);
        }

        protected override void PostStop()
        {
            base.PostStop();

            _clusterClientActor.Tell(UnsubscribeContactPoints.Instance);
        }

        private void Handle(ContactPoints msg)
        {
            //
            // 시작하면 바로 메시지가 전달된다.
            //
            _log.Info(">>> Received - ContactPoints");
            foreach (ActorPath contactPoint in msg.ContactPointsList)
            {
                _log.Info($"\t{contactPoint.ToStringWithAddress()}");
            }
        }

        private void Handle(ContactPointAdded msg)
        {
            // 
            // TODO?: 처음 실행할 때 접속된 ContactPoint 정보만 전달된다.
            //          나중에 Cluster로 합류한 ContactPoint에 대한 메시지는 전될지 않는다. 
            //
            _log.Info(">>> Received - ContactPointAdded");
            _log.Info($"\t{msg.ContactPoint.ToStringWithAddress()}");
        }

        private void Handle(ContactPointRemoved msg)
        {
            // 
            // TODO?: 언제 호출되는지 아직 모른다.
            //          ContactPoint 종료될 때: 메시지가 전달되지 않는다.
            //          ContactPoint 정보에 변화가 있을 때: ???? 어떻게 런타임에 변경하지?
            //
            _log.Info(">>> Received - ContactPointRemoved");
            _log.Info($"\t{msg.ContactPoint.ToStringWithAddress()}");
        }
    }
}
