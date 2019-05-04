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
        public sealed class TellPublish { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PublisherActor());
        }

        public PublisherActor()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(6), Self, new TellPublish(), Self);

            Receive<TellPublish>(_ => Handle(_));
        }

        private void Handle(TellPublish msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            //
            // sendOneMessageToEachGroup: false
            //      => Topic이 일치하고 GroupId가 없는 모든 액터에게 메시지를 보낸다.
            // 
            // sendOneMessageToEachGroup: true
            //      => GroupId가 같을 때(Send)
            //              Topic이 일치하고 GroupId가 있는 한 액터에게 메시지를 보낸다.
            //
            //      If all the subscribed actors have the same group id, 
            //              then this works just like Send and each message is only delivered to one subscriber.
            //
            //      => GroupId가 다를 때(Publish)
            //              Topic이 일치하고 GroupId가 있는 모든 액터에게 메시지를 보낸다.
            //
            //      If all the subscribed actors have different group names, 
            //              then this works like normal Publish and each message is broadcasted to all subscribers.
            //
            //====================================================================
            // 1. Publish	= SendToAll(excludeSelf: true)
            //      sendOneMessageToEachGroup: false
            //      sendOneMessageToEachGroup: true & Different Group Names
            // 2. Publish   = Send(LocalAffinity: false)
            //      sendOneMessageToEachGroup: true & Same Group Name
            //====================================================================
            //
            var mediator = DistributedPubSub.Get(Context.System).Mediator;

            //
            // Group Id가 모두 같을 때 
            //      => Topic이 일치하고 GroupId가 있는 한 액터에게 메시지를 보낸다.
            //
            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello1",
                sendOneMessageToEachGroup: true));

            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello2",
                sendOneMessageToEachGroup: true));

            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello3",
                sendOneMessageToEachGroup: true));
        }
    }
}
