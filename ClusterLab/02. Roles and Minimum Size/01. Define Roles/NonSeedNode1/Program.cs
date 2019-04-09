using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace NonSeedNode1
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            Console.WriteLine();
            Console.WriteLine("NonSeedNode1 is running...");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
