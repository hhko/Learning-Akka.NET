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

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(ex =>
                {
                    //
                    // Group Routee 예외가 발생되면 호출된다.
                    //
                    _log.Info($">>> Current, OneForOneStrategy : {Self}, {ex.ToString()}");

                    return Directive.Restart;
                });
        }
    }
}
