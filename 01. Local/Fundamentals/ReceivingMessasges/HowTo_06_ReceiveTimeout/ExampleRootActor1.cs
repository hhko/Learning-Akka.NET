using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_ReceiveTimeout
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
            // 1초 안에 새 메시지를 받지 못하면
            // Actor System으로부터 ReceiveTimeout 메시지를 받는다.
            //
            Context.SetReceiveTimeout(TimeSpan.FromMilliseconds(1000));

            Receive<string>(value => Handle(value));
            Receive<ReceiveTimeout>(value => HandleTimeout(value));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} {2} : {3}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <string>",
                    DateTime.Now.ToString("hh:mm:ss.fff"),
                    value));
        }

        private void HandleTimeout(ReceiveTimeout value)
        {
            //
            // 메시지 Timeout 설정을 해제한다.
            //
            //Context.SetReceiveTimeout(null);

            // 만약
            // "Context.SetReceiveTimeout(null);"을 주석처리하면
            // "1초" 안에 새 메시지를 받지 못하면
            // 계속 ActorSystem으로부터 ReceiveTimeout 메시지를 받는다.
            //

            Console.WriteLine(
                string.Format("{0:000} {1,-40} {2} : {3}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <ReceiveTimeout>",
                    DateTime.Now.ToString("hh:mm:ss.fff"),
                    value));
        }
    }
}
