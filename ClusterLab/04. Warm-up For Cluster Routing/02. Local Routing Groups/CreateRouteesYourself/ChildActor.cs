using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateRouteesYourself
{
    public class ChildActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ChildActor());
        }

        public ChildActor()
        {
            _log.Info($">>> Current: {Self}");

            Receive<string>(_ => Handle(_));
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Handle: {Self}, {msg}");

            int error = 0;
            int reult = 2019 / error;
        }
    }
}
