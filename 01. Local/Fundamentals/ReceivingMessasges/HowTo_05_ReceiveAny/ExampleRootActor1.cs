using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_ReceiveAny
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Receive<int>(value => Handle(value));

            //
            // 모든 메시지(object 타입)를 처리할 ReceiveAny 함수는
            // 반드시 메시지 처리 우선순위 때문에 마지막 함수로 호출해야 한다.
            //
            // void ReceiveAny(
            //    Action<object> handler);
            //
            // ReceiveAny 함수는 결국 Receive<object>과 같다.
            // 
            ReceiveAny(value => Handle(value));
        }

        private void Handle(int value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <int>",
                    value));
        }

        private void Handle(object value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <object>",
                    value));
        }
    }
}
