using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaHelpers;

namespace HowTo_09_SendMessagesByPath
{
    public static class ActorPaths
    {
        public static readonly ActorMetaData RootActor1 = new ActorMetaData("root-actor-name1");
        public static readonly ActorMetaData RootActor2 = new ActorMetaData("root-actor-name2");
    }
}
