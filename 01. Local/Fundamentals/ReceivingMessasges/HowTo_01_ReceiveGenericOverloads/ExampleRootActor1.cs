using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_01_ReceiveGenericOverloads
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
            // 메시지 처리할 타입을 Receive<T>에 명시한다
            //
            Receive<int>(value => Handle(value));
            Receive<string>(value => Handle(value));
            Receive<float>(value => Handle(value));
            Receive<double>(value => Handle(value));
        }

        private void Handle(int value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <int>",
                    value));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <string>",
                    value));
        }

        private void Handle(float value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <float>",
                    value));
        }

        private void Handle(double value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <double>",
                    value));
        }
    }
}
