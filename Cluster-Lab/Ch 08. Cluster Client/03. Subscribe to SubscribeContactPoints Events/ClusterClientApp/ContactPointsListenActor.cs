using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ClusterClientApp
{
    public class ContactPointsListenActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private IActorRef _clusterClientActor;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ContactPointsListenActor());
        }

        public ContactPointsListenActor()
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
            // Seed Node 중에서 새로 접속될 때 호출된다.
            //  -> Seed Node 정보가 N개 있어도 1개만 접속한다.
            //  -> Seed Node가 Cluster에서 접속되어도 호출되지 않는다(이미지 ClusterClient는 접속되어 있기 때문이다).
            //
            _log.Info(">>> Received - ContactPointAdded");
            _log.Info($"\t{msg.ContactPoint.ToStringWithAddress()}");
        }

        private void Handle(ContactPointRemoved msg)
        {
            // 
            // 접속 시도한 Seed Node 중에서 접속되지 않을 때 호출된다.
            // 접속된 Seed Node가 접속되지 않을 때 호출된다.
            //  -> 접속 중인 Seed Node가 아니 Seed Node 종료일 때는 호출되지 않는다.
            //
            _log.Info(">>> Received - ContactPointRemoved");
            _log.Info($"\t{msg.ContactPoint.ToStringWithAddress()}");
        }
    }
}
