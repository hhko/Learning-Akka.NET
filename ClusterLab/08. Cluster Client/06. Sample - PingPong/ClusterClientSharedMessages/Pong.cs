using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClusterClientSharedMessages
{
    public class Pong
    {
        public Pong(string rsp, Address replyAddr)
        {
            Rsp = rsp;
            ReplyAddress = replyAddr;
        }

        public string Rsp { get; }

        public Address ReplyAddress { get; }
    }
}
