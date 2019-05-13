using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtLeastOnceDelivery_01_Basics
{
    public class Msg
    {
        public Msg(long deliveryId, string message)
        {
            DeliveryId = deliveryId;
            Message = message;
        }

        public long DeliveryId { get; }

        public string Message { get; }
    }

    public class Confirm
    {
        public Confirm(long deliveryId)
        {
            DeliveryId = deliveryId;
        }

        public long DeliveryId { get; }
    }

    public interface IEvent
    {

    }

    public class MsgSent : IEvent
    {
        public MsgSent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class MsgConfirmed : IEvent
    {
        public MsgConfirmed(long deliveryId)
        {
            DeliveryId = deliveryId;
        }

        public long DeliveryId { get; }
    }

    public class AtLeastOnceDeliverySenderActor : AtLeastOnceDeliveryReceiveActor
    {
        public override string PersistenceId => Context.Self.Path.Name;
        private readonly IActorRef _destionationActor;
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new AtLeastOnceDeliverySenderActor());
        }

        public AtLeastOnceDeliverySenderActor()
        {
            Console.WriteLine($">>>=============>>> AtLeastOnceDeliverySenderActor Ctor");
            _destionationActor = Context.ActorOf<ExampleDestinationAtLeastOnceDeliveryReceiveActor>();

            //UnconfirmedCount = 3;
            Recover<MsgSent>(msgSent => Handler(msgSent));
            Recover<MsgConfirmed>(msgConfirmed => Handler(msgConfirmed));

            Command<string>(str =>
                {
                    Persist(new MsgSent(str), Handler);
                });

            Command<Confirm>(confirm =>
                {
                    Persist(new MsgConfirmed(confirm.DeliveryId), Handler);
                });

            //
            // akka.persistence.at-least-once-delivery.warn-after-number-of-unconfirmed-attempts = 5
            //  "N + 1" 재시도 이후에 'UnconfirmedWarning' 메시지를 받는다.
            //
            //Command<UnconfirmedWarning>(_ =>
            //    {
            //    });
        }

        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine($">>>=============>>> PostStop");
        }

        private void Handler(MsgSent msgSent)
        {
            Console.WriteLine($">>> MsgSent...ing: {msgSent.Message}");
            //Deliver(_destionationActor.Path, l => new Msg(l, msgSent.Message));
            Deliver(Context.ActorSelection("/user/foo"), l => new Msg(l, msgSent.Message));
            Console.WriteLine($">>> MsgSent...ed : {msgSent.Message}");
        }

        private void Handler(MsgConfirmed msgConfirmed)
        {
            Console.WriteLine($">>> MsgConfirmed...ing: {msgConfirmed.DeliveryId}");
            ConfirmDelivery(msgConfirmed.DeliveryId);
            Console.WriteLine($">>> MsgConfirmed...ed : {msgConfirmed.DeliveryId}");
        }

        //bool ConfirmDelivery(long deliveryId);
        //void Deliver(ActorPath destination, Func<long, object> deliveryMessageMapper);
        //void Deliver(ActorSelection destination, Func<long, object> deliveryMessageMapper);

        //virtual TimeSpan RedeliverInterval { get; }
        //int RedeliveryBurstLimit { get; }
        //int WarnAfterNumberOfUnconfirmedAttempts { get; }
        //int MaxUnconfirmedMessages { get; }
        //int UnconfirmedCount { get; }

        //void SetDeliverySnapshot(AtLeastOnceDeliverySnapshot snapshot);
        //AtLeastOnceDeliverySnapshot GetDeliverySnapshot();
    }

    public class ExampleDestinationAtLeastOnceDeliveryReceiveActor : ReceiveActor
    {

        public ExampleDestinationAtLeastOnceDeliveryReceiveActor()
        {
            //Receive<Msg>(msg =>
            //{
            //    Console.WriteLine($">>> Recipient...ing: {msg.DeliveryId}, {msg.Message}");        
            //    //Sender.Tell(new Confirm(msg.DeliveryId), Self);
            //    //Console.WriteLine($">>> Recipient...ed : {msg.DeliveryId}, {msg.Message}");
            //});
        }
    }
}
