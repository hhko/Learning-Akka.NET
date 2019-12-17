using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowTo_10_MessageSendCover
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData ParentActor = new ActorMetaData("parent-actor-name");
        public static readonly ActorMetaData SelectActor = new ActorMetaData("select-actor-name");
        public static readonly ActorMetaData MyActor = new ActorMetaData("my-acter-name", ParentActor );
    }
}
