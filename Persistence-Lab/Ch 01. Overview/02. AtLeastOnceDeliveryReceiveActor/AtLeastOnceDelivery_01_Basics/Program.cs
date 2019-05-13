using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace AtLeastOnceDelivery_01_Basics
{
    class Case1
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("PersistenceLab", config);

            IActorRef sender = system.ActorOf(AtLeastOnceDeliverySenderActor.Props(), nameof(AtLeastOnceDeliverySenderActor));
            sender.Tell("1");

            system.WhenTerminated.Wait();
        }
    }

    class Case2_Recovery
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("PersistenceLab", config);

            IActorRef sender = system.ActorOf(AtLeastOnceDeliverySenderActor.Props(), nameof(AtLeastOnceDeliverySenderActor));

            system.WhenTerminated.Wait();
        }
    }
}
