using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_01_CreateActorSystem
{
    class Program
    {
        private static void Main(string[] args)
        {
            // ActorSystem 생성
            ActorSystem system = ActorSystem.Create("actorsystem-name");

            // "Ctrl + Break" 단축키
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;

                // ActorSystem 종료
                system.Terminate();
            };

            // ActorSystem 종료 대기
            system.WhenTerminated.Wait();
        }
    }
}
