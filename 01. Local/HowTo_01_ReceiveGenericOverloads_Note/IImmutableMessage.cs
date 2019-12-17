using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowTo_01_ReceiveGenericOverloads_Note
{
    public interface IImmutableMessage
    {
        string GetMessage();
    }

    public class IntMessage : IImmutableMessage
    {
        public int message { get; private set; }

        public IntMessage(int message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return Convert.ToString(message);
        }
    }

    public class StringMessage : IImmutableMessage
    {
        public string message { get; private set; }

        public StringMessage(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
