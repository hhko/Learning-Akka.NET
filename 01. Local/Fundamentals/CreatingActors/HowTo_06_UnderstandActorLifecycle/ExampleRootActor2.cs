using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_06_UnderstandActorLifecycle
{
    public class ExampleRootActor2 : ReceiveActor
    {
        public ExampleRootActor2()
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleRootActor2 Constructor",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));
        }

        // 생성될 때 호출(생성자 호출 후)
        protected override void PreStart()
        {
            base.PreStart();

            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleRootActor2 PreStart",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));
        }

        // 파괴될 때 호출
        protected override void PostStop()
        {
            base.PostStop();

            Console.WriteLine(
                string.Format("{0:000} {1,-65} : ExampleRootActor2 PostStop",
                    Thread.CurrentThread.ManagedThreadId,
                    Self.Path));
        }
    }
}
