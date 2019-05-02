using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateRouteesYourself
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
            Context.ActorOf(ChildActor.Props(), nameof(ChildActor) + "1");
            Context.ActorOf(ChildActor.Props(), nameof(ChildActor) + "2");
            Context.ActorOf(ChildActor.Props(), nameof(ChildActor) + "3");
        }
    }
}
