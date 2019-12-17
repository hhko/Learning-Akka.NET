using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_04_ReceivePredicate
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
            // 처리할 메시지 조건을 첫번째 Argument에 람다 표현식으로 기술한다.
            //
            //  void Receive<T>(
            //    Predicate<T> shouldHandle,        // bool 값을 리턴한는 delegate이다.
            //    Action<T> handler);
            //
            //  public delegate bool Predicate<in T>(T obj);
            //
            Receive<string>(value => value.Length > 10, value => HandleGreaterThan10(value));
            Receive<string>(value => value.Length > 5, value => HandleGreaterThan5(value));
            Receive<string>(value => Handle(value));
        }

        private void HandleGreaterThan10(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <string> & Length > 10",
                    value));
        }

        private void HandleGreaterThan5(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <string> & Length > 5",
                    value));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <string>",
                    value));
        }
    }
}
