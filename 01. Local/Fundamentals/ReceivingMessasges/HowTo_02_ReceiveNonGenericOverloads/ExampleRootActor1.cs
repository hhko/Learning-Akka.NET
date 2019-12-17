using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_02_ReceiveNonGenericOverloads
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
            // 메시지 처리할 타입을 typeof로 명시한다
            //
            Receive(typeof(int), value => Handle((int)value));
            Receive(typeof(string), value => Handle((string)value));
            Receive(typeof(float), value => Handle((float)value));
            Receive(typeof(double), value => Handle((double)value));
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
