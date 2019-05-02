using Akka.Actor;
using Akka.Configuration;
using Petabridge.Cmd.Host;
using System;
using System.IO;

namespace DeployerTarget
{
    class Program
    {
        static void Main(string[] args)
        {
            // 
            // Deploy될 Actor 클래스를 참조해야 한다.
            // 참조하지 않은 Actor는 Deployed되어 생성할 수 없다.
            //

            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            ActorSystem system = ActorSystem.Create("DeployerTarget", config);

            var cmd = PetabridgeCmd.Get(system);
            cmd.Start();

            Console.ReadLine();
        }
    }
}
