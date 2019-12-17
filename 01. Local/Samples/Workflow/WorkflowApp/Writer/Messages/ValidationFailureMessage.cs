using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowApp.Writer
{
    public class ValidationFailureMessage : FailureMessage
    {
        public ValidationFailureMessage(string reason) : base(reason)
        {

        }
    }
}
