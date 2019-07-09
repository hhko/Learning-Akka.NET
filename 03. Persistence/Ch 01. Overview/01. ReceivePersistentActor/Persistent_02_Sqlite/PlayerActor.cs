using Akka.Actor;
using Akka.Event;
using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistent_02_Sqlite
{
    public class PlayerActor : ReceivePersistentActor
    {
        #region Messages
        public sealed class Exercise
        {
            public int Up { get; }

            public Exercise(int up)
            {
                Up = up;
            }
        }

        public sealed class Show { }

        public sealed class ThrowException { }
        #endregion

        private ILoggingAdapter _log = Context.GetLogger();
        private string _name;
        private int _power = 0;

        public override string PersistenceId => $"player-{_name}";

        public static Props Props(string name)
        {
            return Akka.Actor.Props.Create(() => new PlayerActor(name));
        }

        public PlayerActor(string name)
        {
            _name = name;

            //
            // Command Message Handlers
            //
            // Command<T>(Action<T> handler, ...
            //
            // TODO: true와 false 차이점은?
            //      Command<T>(Func<T, bool> handler)
            Command<Exercise>(_ => Handle(_));
            Command<Show>(_ => Handle(_));
            Command<ThrowException>(_ => Handle(_));

            //
            // Recover Message Handlers
            //
            // Recover<T>(Action<T> handler, ...
            //
            // TODO: true와 false 차이점은?
            //      Recover<T>(Func<T, bool> handler)        
            Recover<Exercise>(_ => HandleRecover(_));
        }

        private void Handle(Exercise msg)
        {
            //
            // TODO?: @event와 handler가 같은데?
            //      Persist<TEvent>(TEvent @event, Action<TEvent> handler);
            //
            Persist(msg, @event =>
                {
                    _power += @event.Up;
                });
        }

        private void HandleRecover(Exercise msg)
        {
            _log.Info($">>> {_name} replaying {nameof(Exercise)} message of '{msg.Up}' from journal");
            _power += msg.Up;
        }

        private void Handle(Show msg)
        {
            _log.Info($">>> The power of '{_name}' is {_power}.");
        }

        private void Handle(ThrowException msg)
        {
            throw new ApplicationException($">>> Throw exception in player: {_name}");
        }
    }
}
