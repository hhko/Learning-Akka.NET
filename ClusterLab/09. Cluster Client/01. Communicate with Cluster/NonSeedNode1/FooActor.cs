using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;
using static ClusterClientSharedMessages.SendMessages;

namespace NonSeedNode1
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
            Receive<string>(_ => Handle(_));
            Receive<Welcome>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
        }

        private void Handle(Welcome msg)
        {
            _log.Info($">>> Recevied message : Welcome, Sender: {Sender}");
        }
    }
}
