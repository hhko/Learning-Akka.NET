using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandleExceptionsFromGroupRoutees
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

            Receive<int>(_ => Handle(_));
        }

        private void Handle(int msg)
        {
            _log.Info($">>> Handle: {Self}, {msg}");

            if (msg == 3)
                throw new Exception(msg.ToString());
        }
    }
}
