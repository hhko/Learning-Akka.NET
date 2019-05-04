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
                    // .WithInitialContacts
                "ClusterClientActor");

            // 
            // TODO ClusterClientSettings 세부 설정
            //  - WithBufferSize
            //  - WithEstablishingGetContactsInterval
            //  - WithHeartbeatInterval
            //  - WithInitialContacts
            //  - WithReconnectTimeout
            //  - WithRefreshContactsInterval
            //

            //
            // 메시지를 경로가 일치하는 한 액터에게 보낸다.
            // The message will be delivered to one recipient with a matching path, if any such exists
            //
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello-localAffinity", localAffinity: true));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello-localAffinity", localAffinity: true));

            //
            // Send 기본적으로 경로가 일치하는 액터 중에서 랜덤으로 대상을 선택하여 메시지를 보낸다.
            // random to any other matching entry
            //
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));

            //
            // 메시지를 경로가 일치하는 모든 액터에게 보낸다.
            // The message will be delivered to all recipients with a matching path
            //
            c.Tell(new ClusterClient.SendToAll("/user/FooActor", "hi, everybody"));
        }
    }
}
