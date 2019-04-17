using Akka.Actor;
using Akka.Cluster.Tools.Client;
using System;
using System.Collections.Generic;
using System.Text;
using static ClusterClientSharedMessages.SendMessages;

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
            var c = Context.ActorOf(
                ClusterClient
                    .Props(ClusterClientSettings.Create(Context.System)),
                "ClusterClientActor");

            //var initialContacts = new List<ActorPath>()
            //    {
            //        ActorPath.Parse("akka.tcp://OtherSys@host1:2552/system/receptionist"),
            //        ActorPath.Parse("akka.tcp://OtherSys@host2:2552/system/receptionist")
            //    };
            //ClusterClientSettings.Create(Context.System).WithInitialContacts(initialContacts)), "client");


            c.Tell(new ClusterClient.Send("/user/FooActor", "hello-localAffinity", localAffinity: true));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello-localAffinity", localAffinity: true));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));
            c.Tell(new ClusterClient.Send("/user/FooActor", "hello"));

            c.Tell(new ClusterClient.SendToAll("/user/FooActor", "hi, everybody"));

            ////
            //// 사용자 정의 메시지는 Seed Node에 참조되어야 한다.
            //// TODO: 해결 방법이 없을까?, 메시지를 받는 Node만 참조할 수 없을까?
            ////
            //c.Tell(new ClusterClient.Send("/user/FooActor", new Welcome()));
        }
    }
}
