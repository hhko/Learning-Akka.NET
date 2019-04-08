using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace SeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var hocon = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.hocon"));
            ActorSystem system = ActorSystem.Create("ClusterLab", hocon);

            Console.WriteLine();
            Console.WriteLine("SeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
