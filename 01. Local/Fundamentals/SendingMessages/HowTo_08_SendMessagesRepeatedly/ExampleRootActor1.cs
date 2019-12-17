using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_08_SendMessagesRepeatedly
{
    public class ExampleRootActor1 : ReceiveActor
    {
        private int _count = 0;

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Receive<string>(value => Handle(value));

            //
            // 현재 Actor에게 메시지 보내기
            //
            //  ScheduleOnce
            //  ScheduleOnceCancelable
            //  ScheduleRepeatedly
            //  ScheduleRepeatedlyCancelable
            //  ScheduleTellOnce
            //  ScheduleTellOnceCancelable
            //  ScheduleTellRepeatedly
            //  ScheduleTellRepeatedlyCancelable
            //
            // 함수 이름 의미
            //      Tell(있을 때): 메시지 전송
            //      Tell(없을 때): 람다 함수 실행
            //
            //      Once: 한번만 메시지 전달
            //      ScheduleTellRepeatedly: 반복적으로 메시지 전달
            //
            //      Cancelable: 취소할 수 있는 반복 메시지
            //
            ICancelable cancelable = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                0,                                      // 반복하기 전 대기 시간, int initialMillisecondsDelay
                500,                                    // 반복 시간, int millisecondsInterval
                Self,                                   // 메시지 수진자, ICanTell receiver
                "ScheduleTellRepeatedlyCancelable",     // 메시지, object message
                Self);                                  // 메시지 발신자, IActorRef sender

            // 5초 후 Cancel
            //  => 5초 후에 Cancel 처리가 되어 반복 메시지가 더 이상 보내지 않는다.
            cancelable.CancelAfter(5000);
        }

        private void Handle(string value)
        {
            _count++;

            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1",
                    string.Format("{0} {1}", value, _count)));
        }
    }
}
