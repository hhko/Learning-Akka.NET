1. SQLite
   - Install "SQLite/SQL Server Compact Toolbox" Extension in Visual Studio
   - Create Database

2. App.Akka.conf
   ```
   akka.persistence.journal {
	  plugin = "akka.persistence.journal.sqlite"

	  sqlite {
		connection-string = "Data Source=C:\\ ... \\SqliteData.db"
		class = "Akka.Persistence.Sqlite.Journal.SqliteJournal, Akka.Persistence.Sqlite"
		
		# Create journal table be initialized automatically
		auto-initialize = on 
	  }
   }
   ```

1. Actor
   - ReceivePersistentActor 
   
1. Message Handlers
   - Command: Command<T>(Action<T> handler, ...) : 
      - Persist<TEvent>(TEvent @event, Action<TEvent> handler);
   - Recover: Recover<T>(Action<T> handler, ...)