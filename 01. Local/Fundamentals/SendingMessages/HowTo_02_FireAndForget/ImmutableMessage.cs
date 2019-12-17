using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowTo_02_FireAndForget
{
    public class ImmutableMessage
    {
        public ImmutableMessage(int sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }
        public int SequenceNumber { get; private set; }
    }
}
