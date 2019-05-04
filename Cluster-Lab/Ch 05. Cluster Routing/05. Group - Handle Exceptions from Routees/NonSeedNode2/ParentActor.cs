using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode2
{
    public class ParentActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ParentActor());
        }

        public ParentActor()
        {
            Context.ActorOf(FooActor.Props(), nameof(FooActor));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            _log.Info($">>> SupervisorStrategy, Sender: {Sender}");

            return new OneForOneStrategy(exp => 
                {
                    _log.Info($">>> OneForOneStrategy, Sender: {Sender}, Message: {exp.ToString()}");
                    return Directive.Restart;
                });

            //return base.SupervisorStrategy();
        }
    }
}
