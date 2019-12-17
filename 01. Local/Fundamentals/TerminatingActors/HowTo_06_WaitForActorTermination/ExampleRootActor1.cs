using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_WaitForActorTermination
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props(IActorRef rootActor2)
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1(rootActor2));
        }

        public ExampleRootActor1(IActorRef rootActor2)
        {
            rootActor2.Tell("1");
            rootActor2.Tell("2");

            try
            {
                //
                // RootActor2는 현재 한 메시지를 처리하는 시간이 2초다.
                // 그러므로
                // "2개 메시지 * 2초 = 총 4초" 시간이 소요된다.
                //

                //
                // Case1 : 시간내에 Actor가 종료할 때(성공)
                //
                // GracefulStop 함수를 호출할 때 10초 내로 Actor 종료를 기대한다.
                // 10초는 RootActor2가 모든 메시지를 처리하고 종료할 때까지 충분한 시간이다.
                // 그 결과 "true"가 리턴된다.
                //
                rootActor2.GracefulStop(TimeSpan.FromSeconds(10)).Wait();

                //
                // Case2. : 시간내에 Actor가 종료하지 못할 때(실패)
                //
                // GracefulStop 함수를 호출할 때 3초 내로 Actor 종료를 기대한다.
                // RootActor2는 모든 메시지를 처리하는대 총 4초가 필요하기 때문에
                // 그 결과 TaskCanceledException 예외가 발생한다.
                //
                //rootActor2.GracefulStop(TimeSpan.FromSeconds(3)).Wait();
            }
            catch (AggregateException e)
            {
                foreach (TaskCanceledException innerEx in e.InnerExceptions)
                {
                    // RootActor2는 모든 메시지를 처리하는대 총 4초가 필요하기 때문에
                    // 3초안에 종료할 수 없다.
                    // 그 결과 예외가 발생된다.

                    Console.WriteLine(
                        string.Format("{0:000} {1,-40} : {2}",
                            Thread.CurrentThread.ManagedThreadId,
                            "ExampleRootActor1 AggregateException",
                            innerEx));
                }
            }
        }

        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 PostStop"));
        }
    }
}
