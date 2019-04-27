using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNodeDeployedActors
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor())
                .WithRouter(FromConfig.Instance);
        }

        public FooActor()
        {
            Cluster cluster = Cluster.Get(Context.System);
            _log.Info($">>> Foo Address : {cluster.SelfAddress}, {Self.Path.ToStringWithAddress()}");
        }
    }
}
