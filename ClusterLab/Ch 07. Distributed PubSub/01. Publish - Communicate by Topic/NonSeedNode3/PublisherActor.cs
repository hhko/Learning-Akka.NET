using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NonSeedNode3
{
    public class PublisherActor : ReceiveActor
    {
        #region Messages
        public sealed class TellPublish { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PublisherActor());
        }

        public PublisherActor()
        {
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(6), Self, new TellPublish(), Self);

            Receive<TellPublish>(_ => Handle(_));
        }

        private void Handle(TellPublish msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            //
            // sendOneMessageToEachGroup: false
            //      => Topic이 일치하고 GroupId가 없는 모든 액터에게 메시지를 보낸다.
            // 
            // sendOneMessageToEachGroup: true
            //      => GroupId가 같을 때(Send)
            //              Topic이 일치하고 GroupId가 있는 한 액터에게 메시지를 보낸다.
            //
            //      If all the subscribed actors have the same group id, 
            //              then this works just like Send and each message is only delivered to one subscriber.
            //
            //      => GroupId가 다를 때(Publish)
            //              Topic이 일치하고 GroupId가 있는 모든 액터에게 메시지를 보낸다.
            //
            //      If all the subscribed actors have different group names, 
            //              then this works like normal Publish and each message is broadcasted to all subscribers.
            // 
            //====================================================================
            // 1. Publish	= SendToAll(excludeSelf: true)
            //      sendOneMessageToEachGroup: false
            //      sendOneMessageToEachGroup: true & Different Group Names
            // 2. Publish   = Send(LocalAffinity: true)
            //      sendOneMessageToEachGroup: true & Same Group Name
            //====================================================================
            //
            var mediator = DistributedPubSub.Get(Context.System).Mediator;

            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello1",
                sendOneMessageToEachGroup: false));

            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello2",
                sendOneMessageToEachGroup: false));

            mediator.Tell(new Publish(
                "NamedTopic",
                "Hello3",
                sendOneMessageToEachGroup: false));

            //
            // TODO? Subscriber가 1개일 때 mediator.Tell 함수를 호출하면 DeadLetter 1개가 발생한다?
            //
            //      [INFO][2019-04-28 오후 3:11:51][Thread 0019][akka://ClusterLab/system/cluster/$a] 
            //          Message MemberUp from akka://ClusterLab/system/cluster/core/publisher 
            //          to akka://ClusterLab/system/cluster/$a was not delivered. 
            //          1 dead letters encountered.
            //

            //
            // mediator.Tell 메시지가 전달안될 때는 DeadLetter 2개가 발생한다.
            //
            // [INFO][2019-04-28 오후 3:02:24][Thread 0006][akka://ClusterLab/system/distributedPubSubMediator] 
            //      Message String 
            //          from akka://ClusterLab/system/cluster/$a 
            //          to akka://ClusterLab/system/distributedPubSubMediator was not delivered. 
            //      1 dead letters encountered.
            // [INFO][2019-04-28 오후 3:02:24][Thread 0006][akka://ClusterLab/system/cluster/$a] 
            //      Message MemberUp 
            //          from akka://ClusterLab/system/cluster/core/publisher 
            //          to akka://ClusterLab/system/cluster/$a was not delivered. 
            //      2 dead letters encountered.
            //
        }
    }
}
