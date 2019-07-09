### This chapter covers
- How to create ActorSystem.

### How 
1. namespace
```
using Akka.Actor;
```

2. ActorSystem Creation
```cs
ActorSystem system = ActorSystem.Create("...");
```

### What
1. ActorSystem class
- public abstract class ActorSystem : ...
   - public static ActorSystem Create(string name);
   - public static ActorSystem Create(string name, Config config);