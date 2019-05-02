using Akka.Actor;
using Akka.Event;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandleExceptionsFromPoolRoutees
{
    public class ChildActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Context.GetLogger();

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ChildActor())
                //.WithRouter(FromConfig.Instance);
                .WithRouter(FromConfig.Instance.WithSupervisorStrategy(new OneForOneStrategy(ex =>
                {
                    // 
                    // Router이기 때문에 예외 발생시 호출된다.
                    // 기본값은 부모에게 전달하는 Escalate이다.
                    //
                    return Directive.Escalate;
                })));

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

            //
            // Routee이기 때문에(자식이 없기 때문에) WithSupervisorStrategy에 정의된 함수는 호출되지 않는다.
            //  예.
            //      /user/ ... /$a
            //.WithRouter(FromConfig.Instance)
            //.WithSupervisorStrategy(new OneForOneStrategy(ex =>
            //{
            //    return Directive.Stop;
            //}));
            //
        }

        public ChildActor()
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, Constructor");

            Receive<int>(_ => Handle(_));
        }

        //
        // Routee이기 때문에(자식이 없기 때문에) WithSupervisorStrategy에 정의된 함수는 호출되지 않는다.
        //  예.
        //      /user/ ... /$a
        //
        protected override SupervisorStrategy SupervisorStrategy()
        {
            //return base.SupervisorStrategy();
            return new OneForOneStrategy(exp =>
                {
                    _log.Info($">>> Self: {Self}, Sender: {Sender}, Exception: {exp}");
                    return Directive.Restart;
                });
        }

        private void Handle(int msg)
        {
            _log.Info($">>> {Self.Path.ToStringWithoutAddress()}, {msg}");

            if (msg == 3)
                throw new Exception(msg.ToString());
        }
    }
}
