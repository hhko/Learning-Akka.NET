using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WorkflowApp.Reader
{
    public class ConsoleReader : ReceiveActor
    {
        public const string ExitCommand = "exit";
        public IActorRef _validator;

        public static Props Props(IActorRef validator)
        {
            return Akka.Actor.Props.Create(() => new ConsoleReader(validator));
        }

        public ConsoleReader(IActorRef validator)
        {
            _validator = validator;

            Receive<StartMessage>(value => HandleStart());
            Receive<ContinueMessage>(value => HandleContinue());
        }

        public void HandleStart()
        {
            PrintInstructions();
            ReadText();
        }

        public void HandleContinue()
        {
            ReadText();
        }

        public void PrintInstructions()
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("텍스트를 입력해 주세요.");
            Console.Write("입력된 텍스트 유효성 검사 결과가");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" 파란색(짝수: 성공)");
            Console.ResetColor();
            Console.Write(" 또는 ");    
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("빨간색(홀수: 실패)");
            Console.ResetColor();
            Console.Write("으로 출력됩니다.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("프로그램 종료는 exit을 입력하시면 됩니다.\n");
        }

        public void ReadText()
        {
            string value = Console.ReadLine();

            //
            // Ctrl + C, Ctrl + Break 등과 같은 System 키 입력일 때는 value가 NULL이다.
            // NULL 입력은 처리하지 않는다.
            //
            if (value == null || string.Equals(value, ExitCommand, StringComparison.OrdinalIgnoreCase))
                Context.System.Terminate();
            else
                _validator.Tell(value);
        }
    }
}
