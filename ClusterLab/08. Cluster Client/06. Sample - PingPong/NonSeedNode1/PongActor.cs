using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using ClusterClientSharedMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode1
{
    public class PongActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PongActor());
        }

        public PongActor()
        {
            Receive<Ping>(_ => Handle(_));
        }

        private void Handle(Ping msg)
        {
            _log.Info($">>> Recevied message : {msg.Msg}, Sender: {Sender}");

            IActorRef clusterClient = Sender;
            clusterClient.Tell(new Pong(msg.Msg, _cluster.SelfAddress));
        }
    }
}
