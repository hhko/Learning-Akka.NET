using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode3
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor());
        }

        public FooActor()
        {
            _log.Info($">>> {_cluster.SelfAddress}, {Self.Path.ToStringWithoutAddress()}");

            Receive<string>(_ => Handle(_));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
        }
    }
}
