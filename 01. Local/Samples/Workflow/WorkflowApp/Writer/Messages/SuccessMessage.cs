using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowApp.Writer
{
    public class SuccessMessage
    {
        public SuccessMessage(string reason)
        {
            Reason = reason;
        }

        public string Reason { get; private set; }
    }
}
