using Akka.Actor;
using Akka.Event;
using DeployerShared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deployer
{
    public sealed class LocalActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();
        private int _counter;

        public static Props Props(IActorRef deployedEchoActor)
        {
            return Akka.Actor.Props.Create(() => new LocalActor(deployedEchoActor));
        }

        public LocalActor(IActorRef deployedEchoActor)
        {
            deployedEchoActor.Tell(new SharedMessages.Hello($"Hello-{_counter++}"));
            deployedEchoActor.Tell(new SharedMessages.Hello($"Hello-{_counter++}"));
            deployedEchoActor.Tell(new SharedMessages.Hello($"Hello-{_counter++}"));

            Receive<SharedMessages.Hello>(hello =>
            {
                _log.Info($"Received '{hello.Message}' from {Sender} to {Self}.");
            });
        }
    }
}
