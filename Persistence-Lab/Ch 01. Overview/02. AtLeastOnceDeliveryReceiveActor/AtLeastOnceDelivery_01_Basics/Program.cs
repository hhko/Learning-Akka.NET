using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace AtLeastOnceDelivery_01_Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("PersistenceLab", config);

            IActorRef sender = system.ActorOf(AtLeastOnceDeliverySenderActor.Props(), nameof(AtLeastOnceDeliverySenderActor));
            sender.Tell("1");
            //sender.Tell("2");

            system.WhenTerminated.Wait();

            
        }
    }
}
