using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_SendMessagesToTheChildren
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
            // 자식 Actor에게 메시지 보내기
            //

            // Case1. 생성할 때 자식 Actor 핸들 얻기
            IActorRef childActorRef1 = Context.ActorOf(ExampleChildActor1.Props(), ActorPaths.ChildActor1.Name);
            childActorRef1.Tell("Context.ActorOf");

            // Case2. 이름으로 자식 Actor 핸들 얻기
            IActorRef childActorRef2 = Context.Child(ActorPaths.ChildActor1.Name);
            childActorRef2.Tell("Context.Child");

            // Case3. 목록으로 자식 Actor 핸들 얻기
            foreach (IActorRef childActorRef3 in Context.GetChildren())
            {
                childActorRef3.Tell("Context.GetChildren");
            }
        }
    }
}
