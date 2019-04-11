using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace NonSeedNode3
{
    public class MemberListenAsSnapshotActor : ReceiveActor
    {
        #region Messages
        public sealed class RefreshCurrentClusterState { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new MemberListenAsSnapshotActor());
        }

        public MemberListenAsSnapshotActor()
        {
            //
            // 3초 후에 Snapshot으로 Gossip 정보를 받는다.
            //
            Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(3), Self, new RefreshCurrentClusterState(), Self);
            _cluster.SendCurrentClusterState(Self);

            //
            // "ClusterEvent.SubscriptionInitialStateMode.InitialStateAsSnapshot"일 때만 자동으로 호출된다.
            // 또는
            // _cluster.SendCurrentClusterState(Self); 명시적으로 호추할 때 호출된다.
            //
            Receive<ClusterEvent.CurrentClusterState>(_ => Handle(_));

            Receive<RefreshCurrentClusterState>(_ => Handle(_));
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

        private void Handle(RefreshCurrentClusterState msg)
        {
            _cluster.SendCurrentClusterState(Self);
        }

        private void Register(Member member)
        {
            // Role 이름은 대/소문자를 구분한다.
            if (member.HasRole("Provider"))
            {
                // Address은 대/소문자를 구분한다.
                _log.Info($">>> Address : {member.Address}/user/FooActor");
                Context.ActorSelection(member.Address + $"/user/FooActor").Tell("Hello from NonSeedNode3.");
            }
        }
    }
}
