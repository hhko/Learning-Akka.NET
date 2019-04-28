using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode1
{
    public class SubscriberActor : ReceiveActor
    {
        #region Messages
        public sealed class ClusterJoined { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new SubscriberActor());
        }

        public SubscriberActor()
        {
            //IActorRef self = Self;
            //Cluster cluster = Cluster.Get(Context.System);
            //cluster.RegisterOnMemberUp(() =>
            //    {
            //        _log.Info($">>> RegisterOnMemberUp, {Self.Path}, {cluster.SelfAddress}");
            //        self.Tell(new ClusterJoined());
            //    });

            //Receive<ClusterJoined>(_ => Handle(_));

            //
            // "content" Topic을 등록한다.
            //
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("content", Self));

            Receive<string>(_ => Handle(_));
            Receive<SubscribeAck>(_ => Handle(_));
        }

        //private void Handle(ClusterJoined msg)
        //{
        //    //_log.Info($">>> Recevied message : {msg}, Sender: {Sender}");

        //    //var mediator = DistributedPubSub.Get(Context.System).Mediator;

        //    ////
        //    //// "content" Topic을 등록한다.
        //    ////
        //    //mediator.Tell(new Subscribe("content", Self));
        //}

        private void Handle(string msg)
        {
            _log.Info($">>> Recevied message : \"{msg}\", Sender: {Sender}");
        }

        private void Handle(SubscribeAck msg)
        {
            if (msg.Subscribe.Topic.Equals("content")
                && msg.Subscribe.Ref.Equals(Self)
                && msg.Subscribe.Group == null)
            {
                _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
            }
        }
    }
}
