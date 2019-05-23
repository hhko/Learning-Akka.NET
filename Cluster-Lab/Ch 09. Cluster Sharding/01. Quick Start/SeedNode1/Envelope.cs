using System;
using System.Collections.Generic;
using System.Text;

namespace SeedNode1
{
    // define envelope used to message routing
    public sealed class Envelope
    {
        public readonly int ShardId;
        public readonly int EntityId;
        public readonly object Message;

        public Envelope(int shardId, int entityId, object message)
        {
            this.ShardId = shardId;
            this.EntityId = entityId;
            this.Message = message;
        }
    }
}
