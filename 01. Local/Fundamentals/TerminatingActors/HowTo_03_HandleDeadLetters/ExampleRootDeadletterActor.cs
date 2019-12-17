using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace HowTo_03_HandleDeadLetters
{
    public class ExampleRootDeadletterActor : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootDeadletterActor());
        }

        public ExampleRootDeadletterActor()
        {
            Receive<DeadLetter>(dl => Handle(dl));
        }

        private void Handle(DeadLetter dl)
        {
            Console.WriteLine(string.Format("DeadLetter captured: {0}\n\t sender: {1}\n\t recipient: {2}",
                dl.Message, dl.Sender, dl.Recipient));
        }
    }
}
