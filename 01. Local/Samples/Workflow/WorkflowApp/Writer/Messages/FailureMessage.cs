using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowApp.Writer
{
    public abstract class FailureMessage
    {
        public FailureMessage(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; private set; }
    }
}
