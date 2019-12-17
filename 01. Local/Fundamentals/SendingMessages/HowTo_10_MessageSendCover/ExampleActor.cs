using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HowTo_10_MessageSendCover
{
    class ParentActor : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new ParentActor());
        }
        
        public ParentActor()
        {
            Receive<string>(value => Console.WriteLine("ParentActor : " + value));

            IActorRef myActor = Context.ActorOf(MyActor.Props(), ActorPaths.MyActor.Name);

            myActor.Tell("To Child");
        }
    }
    class MyActor : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new MyActor());
        }

        public MyActor()
        {
            Receive<string>(value => Console.WriteLine("MyActor : " + value));

            Self.Tell("To Self");

            Context.Parent.Tell("To Parent");

            Context.ActorSelection(ActorPaths.SelectActor.Path).Tell("To Select");
        }
    }
    class SelectionActor : ReceiveActor
    {
        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new SelectionActor());
        }

        public SelectionActor()
        {
            Receive<string>(value => {
                Console.WriteLine("SelectionActor : " + value);
                Sender.Tell("To Sender");
            });


        }
    }
}
