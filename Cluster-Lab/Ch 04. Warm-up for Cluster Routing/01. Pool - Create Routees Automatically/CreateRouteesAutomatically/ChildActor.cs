using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateRouteesAutomatically
{
    public class ChildActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ChildActor())
                .WithRouter(FromConfig.Instance);
        }

        public ChildActor()
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, Constructor");

            Receive<int>(_ => Handle(_));
        }

        private void Handle(int msg)
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, {msg}");
        }
    }
}
