using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaHelpers;

namespace HowTo_05_TerminateActorStoppingChildren
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData RootActor1 = new ActorMetaData("root-actor-name1");
        public static readonly ActorMetaData ChildActor1 = new ActorMetaData("child-actor-name1", RootActor1);
    }
}
