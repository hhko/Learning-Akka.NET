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
        }

        private void Handle(TellSend msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            var mediator = DistributedPubSub.Get(Context.System).Mediator;

            //
            // excludeSelf: false 
            //      => 액터 경로가 같은 모든 액터에게 메시지를 다 보낸다.
            // excludeSelf: true
            //      => 자신의 노드에 있는 액터 경로가 같은 액터는 제외하여 
            //          다른 노드에 있는 액터 경로가 같은 모든 액터에게 메시지를 다 보낸다.
            //
            mediator.Tell(new SendToAll("/user/SubscriberActor", 
                "Hello1",
                excludeSelf: true));

            mediator.Tell(new SendToAll("/user/SubscriberActor", 
                "Hello2",
                excludeSelf: false));

            mediator.Tell(new SendToAll("/user/SubscriberActor",
                "Hello3",
                excludeSelf: true));
        }
    }
}
