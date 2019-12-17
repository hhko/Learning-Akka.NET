using System;
using Akka.Actor;

namespace HowTo_01_CreateActor
{
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var msg = message as string;

            if (string.IsNullOrEmpty(msg))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please provide an input.\n");
                Console.ResetColor();
                return;
            }

            bool valid = IsValid(msg);
            var color = valid ? ConsoleColor.Red : ConsoleColor.Green;
            var alert = valid ? "Your string had an even # of characters.\n" : "Your string had an odd # of characters.\n";
            Console.ForegroundColor = color;
            Console.WriteLine(alert);
            Console.ResetColor();
        }

        private bool IsValid(string message)
        {
            return message.Length % 2 == 0;
        }
    }
}
