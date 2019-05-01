using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode1
{
    public class SubscriberActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new SubscriberActor());
        }

        public SubscriberActor()
        {
            //
            // "content" Topic을 등록한다.
            //      등록: Put
            //      해제: Remove
            // 액터가 종료되면 자동으로 Remove 처리된다.
            //
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Put(Self)); 

            Receive<string>(_ => Handle(_));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Recevied message : \"{msg}\", Sender: {Sender}");
        }
    }
}
