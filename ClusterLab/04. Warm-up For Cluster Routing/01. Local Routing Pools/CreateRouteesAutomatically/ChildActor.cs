using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CreateRouteesAutomatically
{
    public class ChildActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ChildActor())
                .WithRouter(FromConfig.Instance);
                //.WithRouter(new RoundRobinPool(10));
                //
                // [[[ Router의 기본 장애 처리 정략 변경하기 ]]]
                // Router 기본 전략은 Escalate이다.
                //
                //  .WithSupervisorStrategy(new OneForOneStrategy(ex =>
                //    {
                //        return Directive.Escalate;
                //    }))
                //)

                //// 자식이 없기 때문에 WithSupervisorStrategy에 정의된 함수는 호출되지 않는다.
                //.WithSupervisorStrategy(new OneForOneStrategy(ex =>
                //{
                //    return Directive.Stop;
                //}));
        }

        public ChildActor()
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, Constructor");

            Receive<int>(_ => Handle(_));
        }

        // 자식이 없기 때문에 WithSupervisorStrategy에 정의된 함수는 호출되지 않는다.
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return base.SupervisorStrategy();
        }

        private void Handle(int msg)
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, {msg}");

            if (msg == 3)
                throw new Exception(msg.ToString());
        }
    }
}
