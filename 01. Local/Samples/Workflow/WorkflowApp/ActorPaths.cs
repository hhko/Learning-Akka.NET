using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkkaHelpers;

namespace WorkflowApp
{
    public static class ActorPaths
    {
        public static readonly string SystemName = "WorkflowApp";

        public static readonly ActorMetaData ConsoleReader = new ActorMetaData("ConsoleReaderActor");
        public static readonly ActorMetaData ConsoleWriter = new ActorMetaData("ConsoleWriter");
        public static readonly ActorMetaData ConsoleValidator = new ActorMetaData("ConsoleValidator");
    }
}
