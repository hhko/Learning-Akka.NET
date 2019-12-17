using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowApp.Writer
{
    public class EmptyFailureMessage : FailureMessage
    {
        public EmptyFailureMessage() : base("유효성 검사 실패: 빈 문자열이 입력 되었다.")
        {
            
        }
    }
}
