using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WorkflowApp.Writer
{
    public class ConsoleWriter : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ConsoleWriter());
        }

        public ConsoleWriter()
        {
            Receive<SuccessMessage>(value => HandleSuccess(value));
            Receive<FailureMessage>(value => HandleFailure(value));
        }

        public void HandleSuccess(SuccessMessage value)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(value.Reason);
            Console.ResetColor();
        }

        public void HandleFailure(FailureMessage value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value.Reason);
            Console.ResetColor();
        }
    }
}
