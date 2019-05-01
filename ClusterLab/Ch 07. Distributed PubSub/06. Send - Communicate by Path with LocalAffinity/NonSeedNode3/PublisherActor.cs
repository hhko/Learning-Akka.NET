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
            // localAffinity: false 
            //      => 액터 경로가 같은 모든 액터에게 메시지를 랜덤 순서로 보낸다.
            // localAffinity: true
            //      => 이전 메시지를 받은 액터에게 다시 메시지를 보낸다.
            //
            mediator.Tell(new Send("/user/SubscriberActor", 
                "Hello1",
                localAffinity: false));

            mediator.Tell(new Send("/user/SubscriberActor", 
                "Hello2",
                localAffinity: true));

            mediator.Tell(new Send("/user/SubscriberActor",
                "Hello3",
                localAffinity: true));
        }
    }
}
