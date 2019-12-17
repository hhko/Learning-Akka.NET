using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using WorkflowApp.Reader;
using WorkflowApp.Writer;

namespace WorkflowApp.Validator
{
    public class ConsoleValidator : ReceiveActor
    {
        private IActorRef _consoleWriter;

        public static Props Props(IActorRef consoleWriter)
        {
            return Akka.Actor.Props.Create(() => new ConsoleValidator(consoleWriter));
        }

        public ConsoleValidator(IActorRef consoleWriter)
        {
            _consoleWriter = consoleWriter;

            Receive<string>(value => HandleText(value));
        }

        public void HandleText(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _consoleWriter.Tell(new EmptyFailureMessage());
            }
            else
            {
                if (IsValid(value))
                    _consoleWriter.Tell(new SuccessMessage("유효성 검사 성공(짝수): " + value));
                else
                    _consoleWriter.Tell(new ValidationFailureMessage("유효성 검사 실패(홀수): " + value));
            }

            Sender.Tell(new ContinueMessage());
        }

        public bool IsValid(string text)
        {
            return text.Length % 2 == 0;
        }
    }
}
