using System;

namespace ClusterClientSharedMessages
{
    public class Ping
    {
        public Ping(string msg)
        {
            Msg = msg;
        }

        public string Msg { get; }
    }
}
