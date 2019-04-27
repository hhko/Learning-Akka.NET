using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Akka.Event;
using ClusterClientSharedMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterClientApp
{
    public class PingActor : ReceiveActor
    {
        #region Messages
        private class TryPing
        {
            public static readonly TryPing Instance = new TryPing();

            private TryPing()
            {
            }
        }

        private class PongTimeout
        {
            public PongTimeout(string originalMsg, TimeSpan timeout)
            {
                OriginalMsg = originalMsg;
                Timeout = timeout;
            }

            public string OriginalMsg { get; }

            public TimeSpan Timeout { get; }
        }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        private IActorRef _clusterClient;
        private int _pingCounter;
        private ICancelable _pingTask;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new PingActor());
        }

        public PingActor()
        {
            Receive<TryPing>(_ => Handle(_));
            Receive<Pong>(_ => Handle(_));
            Receive<PongTimeout>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            _clusterClient = Context.ActorOf(
                        ClusterClient
                            .Props(ClusterClientSettings.Create(Context.System)),
                        "ClusterClientActor");

            _pingTask = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1), Self, TryPing.Instance, ActorRefs.NoSender);
        }

        protected override void PostStop()
        {
            _pingTask.Cancel();
        }

        private void Handle(TryPing _)
        {
            var msg = new Ping("Hi!" + _pingCounter++);
            var timeout = TimeSpan.FromSeconds(3);
            var self = Self;

            //
            // 등록된 Actor Path로 액터에게 메시지를 보낸다.
            //
            _clusterClient.Ask(new ClusterClient.Send("/user/PongActor", msg), timeout)
                .ContinueWith(tr =>
                    {
                        //
                        // 실패 처리를 한다.
                        //
                        if (tr.IsCanceled || tr.IsFaulted)
                            return new PongTimeout(msg.Msg, timeout);

                        return tr.Result;
                    }).PipeTo(self);
        }

        private void Handle(Pong msg)
        {
            _log.Info($">>> Reply [{msg.Rsp}] was successfully processed and sent by node [{msg.ReplyAddress}]");
        }

        private void Handle(PongTimeout msg)
        {
            _log.Warning($">>> Attempt to send message [{msg.OriginalMsg}] timed out after {msg.Timeout} - no nodes responded in time");
        }
    }
}
