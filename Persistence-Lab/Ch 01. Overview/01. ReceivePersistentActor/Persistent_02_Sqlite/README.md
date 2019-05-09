1. SQLite
   - Install "SQLite/SQL Server Compact Toolbox" Extension in Visual Studio
   - Create Database

2. App.Akka.conf 파일
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

1. 액터
   - ReceivePersistentActor 상속
   - Command<T>(Action<T> handler, 
      - Persist<TEvent>(TEvent @event, Action<TEvent> handler);
   - Recover<T>(Action<T> handler,