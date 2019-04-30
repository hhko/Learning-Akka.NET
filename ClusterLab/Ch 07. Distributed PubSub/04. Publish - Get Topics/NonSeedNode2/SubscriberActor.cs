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
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("NamedTopic-2", Self));

            Receive<SubscribeAck>(_ => Handle(_));
        }

        private void Handle(SubscribeAck msg)
        {
            if (msg.Subscribe.Topic.Equals("NamedTopic-2")
                && msg.Subscribe.Ref.Equals(Self)
                && msg.Subscribe.Group == null)
            {
                _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
            }
        }
    }
}
