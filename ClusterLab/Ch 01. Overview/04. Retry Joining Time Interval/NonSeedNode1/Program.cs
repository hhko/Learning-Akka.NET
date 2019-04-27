using Akka.Actor;
using Akka.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace NonSeedNode1
{
    class Program
    {
        static void InitializeNLogConfig()
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var assemblyFolder = Path.GetDirectoryName(codeBasePath);
            string nlogConfigFilePath = Path.Combine(assemblyFolder, "App.NLog.conf");

            //
            // TODO: xUnit 테스트 필요
            //
            //string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(nlogConfigFilePath, ignoreErrors: true);
        }

        static void Main(string[] args)
        {
            InitializeNLogConfig();


            var config = ConfigurationFactory.ParseString(File.ReadAllText("App.Akka.conf"));
            var clusterName = config.GetString("akka.cluster.name");

            ActorSystem system = ActorSystem.Create("ClusterLab", config);

            system.Log.Info(">>> Non-SeedNode1 is running.");

            Console.ReadLine();
        }
    }
}
