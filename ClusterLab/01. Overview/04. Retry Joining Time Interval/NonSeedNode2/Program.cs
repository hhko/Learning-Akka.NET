using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace NonSeedNode2
{
    class Program
    {
        static void Main(string[] args)
        {
            var hocon = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("ClusterLab", hocon);

            Console.WriteLine();
            Console.WriteLine("NonSeedNode2 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
