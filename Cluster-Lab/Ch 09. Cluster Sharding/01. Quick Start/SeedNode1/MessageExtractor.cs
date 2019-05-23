using Akka.Cluster.Sharding;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeedNode1
{
    // define, how shard id, entity id and message itself should be resolved
    public sealed class MessageExtractor : IMessageExtractor
    {
        public string EntityId(object message)
        {
            return (message as Envelope)?.EntityId.ToString();
        }

        public string ShardId(object message)
        {
            return (message as Envelope)?.ShardId.ToString();
        }

        public object EntityMessage(object message)
        {
            return (message as Envelope)?.Message;
        }
    }
}
