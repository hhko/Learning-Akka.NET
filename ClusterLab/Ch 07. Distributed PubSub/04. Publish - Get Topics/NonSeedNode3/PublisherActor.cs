using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NonSeedNode3
{
    public class PublisherActor : ReceiveActor
    {
        #region Messages
        public sealed class TellSend { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PublisherActor());
        }

        public PublisherActor()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(6), Self, new TellSend(), Self);

            Receive<TellSend>(_ => Handle(_));
            Receive<CurrentTopics>(_ => Handle(_));
        }

        private void Handle(TellSend msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            var mediator = DistributedPubSub.Get(Context.System).Mediator;

            mediator.Tell(GetTopics.Instance);
        }

        private void Handle(CurrentTopics msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            foreach (string currentTopic in msg.Topics)
            {
                _log.Info($">>> Current Topic : {currentTopic}");
            }
        }
    }
}
