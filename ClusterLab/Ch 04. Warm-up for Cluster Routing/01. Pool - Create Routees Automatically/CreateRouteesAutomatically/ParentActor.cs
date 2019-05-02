using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateRouteesAutomatically
{
    class ParentActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ParentActor());
        }

        public ParentActor()
        {
            IActorRef childActor = Context.ActorOf(ChildActor.Props(), nameof(ChildActor));

            childActor.Tell(0);
            childActor.Tell(1);
            childActor.Tell(2);
            childActor.Tell(3);
        }
    }
}
