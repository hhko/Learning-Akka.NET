using System;
using Akka.Actor;

namespace HowTo_01_CreateActor
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
        private readonly IActorRef _consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            // 출력 Actor 객체를 저장한다.
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            //
            // 시작 메시지를 문자열로 구분한다.
            // - HowTo_02_SendMessage에서 상수로 개선된다.
            //
            if (message.Equals("start"))
            {
                DoPrintInstructions();
            }

            //
            // Null과 Validation(홀수/짝수 구분) 처리가 없다.
            // - HowTo_02_SendMessage에서 추가된다.
            //

            GetAndValidateInput();
        }

        private void GetAndValidateInput()
        { 
            var message = Console.ReadLine();

            //if (!string.IsNullOrEmpty(message) && string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            if (string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
            }
            else
            {
                // 입력 문자열 처리를 위해 메시지를 전달한다.
                _consoleWriterActor.Tell(message);

                // 자기 자신에게 메시지를 보낸다
                // - "continue" 문자열 값은 아무 의미가 없다. 단순히 반복 처리를 하기 위함이다.
                Self.Tell("continue");
            }
        }

        private void DoPrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");     // 입력한 문자열이 짝수일 때는
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" red ");                         // 빨간색
            Console.ResetColor();
            Console.Write(" and others will appear as");    // 홀수 일때는
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");                      // 녹색
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}
