using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_09_SendMessagesByPath
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
            // 고유한 Actor Path을 통해 IActorRef 핸들 없이 메시지를 보낸다.
            //
            Context.ActorSelection(ActorPaths.RootActor2.Path)
                .Tell(2002);
        }
    }
}
