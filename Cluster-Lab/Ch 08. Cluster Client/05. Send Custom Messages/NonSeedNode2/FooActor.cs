using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ClusterClientSharedMessages.SendMessages;

namespace NonSeedNode2
{
    public class FooActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor());
        }

        public FooActor()
        {
            Receive<CustomWelcome>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");
        }

        private void Handle(CustomWelcome msg)
        {
            _log.Info($">>> Recevied message : CustomWelcome - {msg.Text}, Sender: {Sender}");
        }
    }
}
