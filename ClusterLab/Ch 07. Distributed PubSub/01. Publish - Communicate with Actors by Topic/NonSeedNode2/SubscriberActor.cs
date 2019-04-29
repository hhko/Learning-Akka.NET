using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode2
{
    public class SubscriberActor : ReceiveActor
    {
        #region Messages
        public sealed class ClusterJoined { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new SubscriberActor());
        }

        public SubscriberActor()
        {
            //
            // "content" Topic을 등록한다.
            //      등록: Subscribe, SubscribeAck
            //      해제: Unsubscribe, UnsubscribeAck
            // 액터가 종료되면 자동으로 Unsubscribe가 처리된다.
            //
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("NamedTopic", Self));

            Receive<SubscribeAck>(_ => Handle(_));
            Receive<string>(_ => Handle(_));
        }

        private void Handle(SubscribeAck msg)
        {
            if (msg.Subscribe.Topic.Equals("NamedTopic")
                && msg.Subscribe.Ref.Equals(Self)
                && msg.Subscribe.Group == null)
            {
                _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
            }
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Recevied message : \"{msg}\", Sender: {Sender}");
        }
    }
}
