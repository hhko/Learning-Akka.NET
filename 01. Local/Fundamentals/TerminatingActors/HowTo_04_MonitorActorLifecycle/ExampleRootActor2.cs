using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_04_MonitorActorLifecycle
{
    public class ExampleRootActor2 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor2());
        }

        public ExampleRootActor2()
        {
            Receive<string>(value => Handle(value));
        }

        private void Handle(string value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}...ing",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor2 Handle",
                    value));

            Thread.Sleep(2000);

            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}...ed",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor2 Handle",
                    value));
        }

        // 생성될 때 호출(생성자 호출 후)
        protected override void PreStart()
        {
            base.PreStart();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor2 PreStart"));
        }

        // 파괴될 때 호출
        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-40}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor2 PostStop"));
        }
    }
}
