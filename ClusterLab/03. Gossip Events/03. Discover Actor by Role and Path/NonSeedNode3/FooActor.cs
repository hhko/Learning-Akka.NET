using Akka.Actor;
using Akka.Cluster;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonSeedNode3
{
    public class FooActor : ReceiveActor
    {
        #region
        public sealed class ClusterJoined { }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor());
        }

        public FooActor()
        {
            Receive<string>(_ => Handle(_));
            Receive<ClusterJoined>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");

            //
            // Cluster로 합류할 때 액터를 찾는다.
            // RegisterOnMemberUp에서 호출되는 람다 Self 주소는 akka://ClusterLab/system/cluster/$a 이다.
            //
            IActorRef self = Self;
            Cluster cluster = Cluster.Get(Context.System);
            cluster.RegisterOnMemberUp(() =>
                {
                    _log.Info($">>> RegisterOnMemberUp, {Self.Path}, {cluster.SelfAddress}");

                    self.Tell(new ClusterJoined());
                });
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Message : {msg}, Sender: {Sender}");
        }

        private void Handle(ClusterJoined msg)
        {
            _log.Info($">>> Message : {msg}, Sender: {Sender}");

            // SeedNode1
            SendByRoleAndPath("Master", "/user/FooActor", "Hello from NonSeedNode3 to Master");

            // NonSeedNode1
            SendByRoleAndPath("Provider", "/user/FooActor", "Hello from NonSeedNode3 to Provider");

            // NonSeedNode2, NonSeedNode3(Call back 메시지를 보내지 않는다)
            SendByRoleAndPath("Worker", "/user/FooActor", "Hello from NonSeedNode3 to Worker");

            // NonSeedNode2
            SendByRoleAndPath("Scheduler", "/user/FooActor", "Hello from NonSeedNode3 to Worker");
        }

        private void SendByRoleAndPath(string roleName, string path, string message)
        {
            Cluster cluster = Cluster.Get(Context.System);
            IEnumerable<Member> members = cluster.State.Members.Where(member => member.Roles.Contains(roleName));
            foreach (Member member in members)
            {
                Context
                    .ActorSelection($"{member.Address}{path}")
                    //.ResolveOne(TimeSpan.FromSeconds(3))
                    //.Result
                    .Tell(message, Self);

                // 
                // TODO: async Task<IEnumerable<IActorRef>> ?
                // TODO: ResolveOne 예외 처리?
                //
            }
        }
    }
}
