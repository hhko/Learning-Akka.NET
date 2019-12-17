using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaHelpers;

namespace HowTo_06_UnderstandActorLifecycle
{
    public static class ActorPaths
    {
        //
        // 모든 Actor를 기술한다.
        // => 
        //
        // 효과
        // 1. 모든 Actor의 계층 구조를 한번에 파악할 수 있다.
        // 2. Actor Name과 Actor Path에서 텍스트 구문이 클래스로 변경된다
        //
        //  "root-actor-name1"                              => ActorPaths.RootActor1.Name
        //  "akka://actorsystem-name/user/root-actor-name2"     => ActorPaths.RootActor2.Path

        public static readonly ActorMetaData RootActor1 = new ActorMetaData("root-actor-name1");
        public static readonly ActorMetaData RootActor2 = new ActorMetaData("root-actor-name2");
        public static readonly ActorMetaData ChildActor1 = new ActorMetaData("child-actor-name1", RootActor1);
    }
}
