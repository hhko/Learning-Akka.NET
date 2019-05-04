using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.Singleton;
using Akka.Event;
using System;

namespace NonSeedNodeSingletonActors
{
    public class MySingletonActor : ReceiveActor
    {
        #region Messages
        public sealed class Hello
        {
            public string Text { get; }

            public Hello(string text)
            {
                Text = text;
            }
        }
        #endregion

        private readonly ILoggingAdapter _log = Context.GetLogger();
        private readonly Cluster _cluster = Cluster.Get(Context.System);

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new MySingletonActor());
        }

        public MySingletonActor()
        {
            _log.Info($">>> {_cluster.SelfAddress}, {Self.Path.ToStringWithoutAddress()}");

            Receive<Hello>(_ => Handle(_));
            Receive<string>(_ => Handle(_));
        }

        private void Handle(Hello msg)
        {
            _log.Info($">>> Recevied message : {msg.Text}, Sender: {Sender}");
        }

        private void Handle(string msg)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Message received \"{msg}\"");
            Console.ForegroundColor = originalColor;
        }
    }
}
