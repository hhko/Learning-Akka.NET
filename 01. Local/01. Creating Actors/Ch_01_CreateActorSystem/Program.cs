using System;
using Akka.Actor;

namespace Ch_01_CreateActorSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create ActorSystem.
            // 액터시스템을 생성한다.
            ActorSystem system = ActorSystem.Create("Local-Lab");

            // "Ctrl + Break"
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Console.WriteLine("Exit Application.");

                eventArgs.Cancel = true;

                // Terminate ActorSystem.
                // 액터시스템 종료한다.
                system.Terminate();
            };

            Console.WriteLine("Create ActorSystem.");

            // Wait for ActorSystem termination.
            // 액터시스템 종료를 대기한다.
            system.WhenTerminated.Wait();
        }
    }
}
