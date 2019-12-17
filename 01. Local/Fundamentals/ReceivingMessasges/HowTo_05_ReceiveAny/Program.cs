﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_05_ReceiveAny
{
    class Program
    {
        private static ActorSystem _system;

        private static void Main(string[] args)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-40} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "Main",
                    "프로그램 시작"));

            _system = ActorSystem.Create("actorsystem-name");

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                _system.Terminate();
            };

            IActorRef rootActor1 = _system.ActorOf(
                ExampleRootActor1.Props(),
                ActorPaths.RootActor1.Name);

            rootActor1.Tell(2002);
            rootActor1.Tell("Korea Japan World Cup");

            _system.WhenTerminated.Wait();
        }
    }
}
