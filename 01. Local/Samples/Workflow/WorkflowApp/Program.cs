using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using WorkflowApp.Reader;
using WorkflowApp.Validator;
using WorkflowApp.Writer;

namespace WorkflowApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create(ActorPaths.SystemName);

            Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    system.Terminate();
                };

            IActorRef consoleWriterActor = system.ActorOf(
                ConsoleWriter.Props(),
                ActorPaths.ConsoleWriter.Name);

            IActorRef validatorActor = system.ActorOf(
                ConsoleValidator.Props(consoleWriterActor),
                ActorPaths.ConsoleValidator.Name);

            IActorRef consoleReaderActor = system.ActorOf(
                ConsoleReader.Props(validatorActor),
                ActorPaths.ConsoleReader.Name);

            consoleReaderActor.Tell(new StartMessage());

            system.WhenTerminated.Wait();
        }
    }
}
