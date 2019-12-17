using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_03_ReceivePriority
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            //
            // Receive 함수 호출 순서가 메시지 처리 우선순위가 된다.
            //  => HandleFirst가 우선순위가 높기 때문에 HandleSecond은 호출되지 않는다.
            //  => int 타입 메시지가 왔을 때
            //      HandleSecond은 object 타입으로 int 타입 데이터를 처리할 수 있지만
            //      HandleFirst가 우선순위가 더 높기 때문에 HandleFirst가 호출된다.
            //
            //Receive<int>(value => HandleFirst(value));          // 우선순위 1
            //Receive<object>(value => HandleSecond(value));       // 우선순위 2


            ////
            //// 런타임 예외를 발생 시킨다.
            ////
            Receive<object>(value => HandleSecond(value));       // 우선순위 1
            Receive<int>(value => HandleFirst(value));          // 우선순위 2
        }

        private void HandleFirst(int value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 HandleFirst<int>",
                    value));
        }

        private void HandleSecond(object value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 HandleSecond<object>",
                    value));
        }
    }
}
