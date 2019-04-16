using Akka.Actor;
using Akka.Cluster.Tools.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterClientApp
{
    public class ClusterClientSendActor : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ClusterClientSendActor());
        }

        public ClusterClientSendActor()
        {
            //
            // ClusterClient 액터를 생성한다.
            //
            var c = Context.ActorOf(
                ClusterClient
                    .Props(ClusterClientSettings.Create(Context.System)),
                "ClusterClientActor");

            c.Tell(new ClusterClient.Publish("Topic1", "Hello1"));
            c.Tell(new ClusterClient.Publish("Topic1", "Hello2"));
            c.Tell(new ClusterClient.Publish("Topic2", "Hello3"));
        }
    }
}
