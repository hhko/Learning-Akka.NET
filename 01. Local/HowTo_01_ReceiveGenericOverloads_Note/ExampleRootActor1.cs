using System;
using Akka.Actor;
using System.Threading;

namespace HowTo_01_ReceiveGenericOverloads_Note
{
    public class ExampleRootActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleRootActor1());
        }

        public ExampleRootActor1()
        {
            IActorRef ChildActor = Context.ActorOf(ExampleChildActor1.Props(), ActorPaths.ChildActor1.Name);
            //
            // 메시지 처리할 타입을 Receive<T>에 명시한다
            //
            Receive<IImmutableMessage>(value => 
            {
                Handle(value);
                ChildActor.Tell(value);
            });
            
            
        }

        private void Handle(IImmutableMessage value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleRootActor1 <ImmutableMessage>",
                    value.GetMessage()));
        }
    }

    public class ExampleChildActor1 : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ExampleChildActor1());
        }

        public ExampleChildActor1()
        {
            //
            // 메시지 처리할 타입을 Receive<T>에 명시한다
            //
            Receive<IntMessage>(value => Handle(value));
            Receive<StringMessage>(value => Handle(value));
        }

        private void Handle(IntMessage value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleChildActor1 <IntMessage>",
                    value.message));
        }
        private void Handle(StringMessage value)
        {
            Console.WriteLine(
                string.Format("{0:000} {1,-30} : {2}",
                    Thread.CurrentThread.ManagedThreadId,
                    "ExampleChildActor1 <StringMessage>",
                    value.message));
        }
    }
    
}
