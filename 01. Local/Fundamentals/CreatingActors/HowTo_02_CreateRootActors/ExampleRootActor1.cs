using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_02_CreateRootActors
{
    public class ExampleRootActor1 : ReceiveActor
    {
        // 팩토리 함수를 이용한 Actor 생성 방법: 추천
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Console.WriteLine(
                string.Format("{0:000} : ExampleRootActor1 Constructor", 
                    Thread.CurrentThread.ManagedThreadId));
        }
    }
}
