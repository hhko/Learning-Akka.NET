using System;
using Akka.Actor;

namespace HowTo_02_FireAndForget_Note
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            Receive<int>(value => Console.Write(Convert.ToString(value) + " "));
        }
    }

    public class ExampleRootActor2 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor2());
        }

        public ExampleRootActor2()
        {
            Receive<char>(value => Console.Write(Convert.ToString(value) + " "));
        }
    }
}
