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
        private readonly IActorRef _clusterClientActor;

        public static Props Props(IActorRef clusterClientActor)
        {
            return Akka.Actor.Props.Create(() => new ClusterClientListenActor(clusterClientActor));
        }

        public ClusterClientListenActor(IActorRef clusterClientActor)
        {
            _clusterClientActor = clusterClientActor;

            Receive<ContactPoints>(_ => Handle(_));
            Receive<ContactPointAdded>(_ => Handle(_));
            Receive<ContactPointRemoved>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            //
            // 이벤트 등록하기: SubscribeContactPoints.Instance
            // 이벤트 제거하기: UnsubscribeContactPoints.Instance
            //   vs.
            // 명시적으로 확인하기: .Tell(GetContactPoints.Instance) -> Receive<ContactPoints>
            //
            _clusterClientActor.Tell(SubscribeContactPoints.Instance);
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
            // ContactPoint에 접속되면 전달된다.
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
        }
    }
}
}
