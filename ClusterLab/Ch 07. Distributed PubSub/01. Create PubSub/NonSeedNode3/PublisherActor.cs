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
        public sealed class ClusterJoined { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();
        private IActorRef _mediator;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PublisherActor());
        }

        public PublisherActor()
        {
            // [INFO][2019-04-28 오후 3:02:24][Thread 0006][akka://ClusterLab/system/distributedPubSubMediator] Message String from akka://ClusterLab/system/cluster/$a to akka://ClusterLab/system/distributedPubSubMediator was not delivered. 1 dead letters encountered.
            // [INFO][2019-04-28 오후 3:02:24][Thread 0006][akka://ClusterLab/system/cluster/$a] Message MemberUp from akka://ClusterLab/system/cluster/core/publisher to akka://ClusterLab/system/cluster/$a was not delivered. 2 dead letters encountered.

            
            IActorRef self = Self;
            Cluster cluster = Cluster.Get(Context.System);
            cluster.RegisterOnMemberUp(() =>
                {
                    _log.Info($">>> RegisterOnMemberUp, {Self.Path}, {cluster.SelfAddress}");
                    self.Tell(new ClusterJoined());
                });

            Receive<ClusterJoined>(_ => Handle(_));
        }

        private void Handle(ClusterJoined msg)
        {
            _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

            //
            // 3초 후에 메시지를 전송한다.
            // 
            Thread.Sleep(3000);
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Publish("content", "Pub/Sub Pattern in Akka.Cluster"));

            //
            // TODO? DeadLetter 1개가 발생한다?
            //      [INFO][2019-04-28 오후 3:11:51][Thread 0019][akka://ClusterLab/system/cluster/$a] 
            //          Message MemberUp from akka://ClusterLab/system/cluster/core/publisher 
            //          to akka://ClusterLab/system/cluster/$a was not delivered. 
            //          1 dead letters encountered.
            //
            // TODO? Subscriber가 1개일 때만 발생한다?
            //
        }
    }
}
