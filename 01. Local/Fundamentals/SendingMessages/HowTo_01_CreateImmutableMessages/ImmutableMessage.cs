using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowTo_01_CreateImmutableMessages
{
    public class ImmutableMessage
    {
        public ImmutableMessage(int sequenceNumber, List<string> values)
        {
            SequenceNumber = sequenceNumber;
            Values = values.AsReadOnly();
        }

        // 가변
        // "set;"로 인해 외부에서 수정될 수 있다.
        //
        //public int SequenceNumber { get; set; }

        // 불변
        //
        public int SequenceNumber { get; private set; }

        // 가변
        // IList 인터페이스로 인해 "Values.Add("xyz");"와 같이 외부에서 수정될 수 있다.
        //
        //public IList<string> Values { get; private set; }

        // 불변
        //
        public IReadOnlyCollection<string> Values { get; private set; }
    }
}
