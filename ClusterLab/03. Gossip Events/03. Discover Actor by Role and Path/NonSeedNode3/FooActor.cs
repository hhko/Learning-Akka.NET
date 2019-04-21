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
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new FooActor());
        }

        public FooActor()
        {
            Receive<string>(_ => Handle(_));

            _log.Info($">>> Foo Address : {Self.Path.ToStringWithAddress()}");

            //
            // Cluster로 합류할 때 액터를 찾는다.
            //
            Cluster cluster = Cluster.Get(Context.System);
            cluster.RegisterOnMemberUp(() =>
            {
                _log.Info($">>> RegisterOnMemberUp");

                SendByRoleAndPath("Master", "/user/FooActor", "Hello from NonSeedNode3 to Master");
                SendByRoleAndPath("Provider", "/user/FooActor", "Hello from NonSeedNode3 to Provider");
                SendByRoleAndPath("Worker", "/user/FooActor", "Hello from NonSeedNode3 to Worker");
                SendByRoleAndPath("Scheduler", "/user/FooActor", "Hello from NonSeedNode3 to Worker");
            });
        }

        private void Handle(string msg)
        {
            _log.Info($">>> Message : {msg}, Sender: {Sender}");
        }

        private void SendByRoleAndPath(string roleName, string path, string message)
        {
            Cluster cluster = Cluster.Get(Context.System);
            IEnumerable<Member> members = cluster.State.Members.Where(member => member.Roles.Contains(roleName));
            foreach (Member member in members)
            {
                Context
                    .ActorSelection($"{member.Address}{path}")
                    .ResolveOne(TimeSpan.FromSeconds(3))
                    .Result
                    .Tell(message, Self);

                //
                // TODO?: Sender가 경로가 이상한다.
                //      리턴 메시지를 받을 수 없다.
                //      akka.tcp://ClusterLab@localhost:8093/system/cluster$a
                //

                // 
                // TODO: async Task<IEnumerable<IActorRef>> ?
                // TODO: ResolveOne 예외 처리?
                //
            }
        }
    }
}
