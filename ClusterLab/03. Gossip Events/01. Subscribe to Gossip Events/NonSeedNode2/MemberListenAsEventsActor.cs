using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode2
{
    public class MemberListenAsEventsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new MemberListenAsEventsActor());
        }

        public MemberListenAsEventsActor()
        {
            //
            // "ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot"일 때만 자동으로 호출된다.
            // 또는
            // _cluster.SendCurrentClusterState(Self); 명시적으로 호추할 때 호출된다.
            //
            Receive<ClusterEvent.CurrentClusterState>(_ => Handle(_));

            //
            // ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot
            // ClusterEvent.SubscriptionInitialStateMode.InitialStateAsEvents 
            // 모두에서 자동으로 호출된다.
            //
            Receive<ClusterEvent.MemberUp>(_ => Handle(_));
        }

        protected override void PreStart()
        {
            base.PreStart();

            //
            // Gossip 이벤트 수집을 등록한다.
            //  "ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot"과 같다.
            //_cluster.Subscribe(Self, new[] { typeof(ClusterEvent.MemberUp),  });
            //
            //_cluster.Subscribe(Self, 
            //    ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot, 
            //    new[] { typeof(ClusterEvent.MemberUp) });

            _cluster.Subscribe(Self,
                ClusterEvent.SubscriptionInitialStateMode.InitialStateAsEvents,
                new[] { typeof(ClusterEvent.MemberUp) });
                //
                // 2개 이상 등록할 때
                //
                //new[] { typeof(ClusterEvent.MemberUp), typeof(ClusterEvent.IReachabilityEvent) });

            _cluster.RegisterOnMemberUp(() =>
                {
                    //
                    // 자신이 클러스터에 합류할 때([Up]될 때) 호출된다.
                    //
                    _log.Info(">>> RegisterOnMemerUp Callback Method");
                });
        }

        protected override void PostStop()
        {
            //
            // Gossip 이벤트 수집을 해제한다.
            //
            _cluster.Unsubscribe(Self);
        }

        private void Handle(ClusterEvent.CurrentClusterState msg)
        {
            _log.Info(">>> ClusterEvent.CurrentClusterState");
            var state = (ClusterEvent.CurrentClusterState)msg;
            foreach (var member in state.Members)
            {
                _log.Info($">>> ClusterEvent.CurrentClusterState : {member.Address}");

                if (member.Status == MemberStatus.Up)
                {
                    Register(member);
                }
            }
        }

        private void Handle(ClusterEvent.MemberUp msg)
        {
            var memberUp = (ClusterEvent.MemberUp)msg;

            _log.Info($">>> ClusterEvent.MemberUp : {memberUp.Member.Address}");
            Register(memberUp.Member);
        }

        private void Register(Member member)
        {
            // Role 이름은 대/소문자를 구분한다.
            if (member.HasRole("Provider"))
            {
                // Address은 대/소문자를 구분한다.
                _log.Info($">>> Address : {member.Address}/user/FooActor");
                Context.ActorSelection(member.Address + "/user/FooActor").Tell("Hello from NonSeedNode2.");
            }
        }
    }
}
