using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;

namespace BasicConfiguration
{
    class Program
    {
        private Config _appConfig;
        private Config _appAkkaConfig;

        private const string CONFIG_FOLDERNAME = "Conf";
        private const string APP_CONFIG_FILENAME = "App.conf";
        private const string AKKA_CONFIG_FILENAME = "Akka.conf";

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Execute();
        }

        public void Execute()
        {
            ReadConfigFromFile();

            ActorSystem system = ActorSystem.Create(_appConfig.GetString("app.actorsystem"), _appAkkaConfig);

            Console.WriteLine("ing");
            Console.ReadLine();
        }

        private void ReadConfigFromFile()
        {
            _appConfig = ConfigurationFactory.ParseString(
                File.ReadAllText(
                Path.Combine(CONFIG_FOLDERNAME, APP_CONFIG_FILENAME)));

            _appAkkaConfig = ConfigurationFactory.ParseString(
                File.ReadAllText(
                Path.Combine(CONFIG_FOLDERNAME, AKKA_CONFIG_FILENAME)));
        }
    }
}
