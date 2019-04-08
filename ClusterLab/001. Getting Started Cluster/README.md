1. ActorSystem 이름은 모두 동일해야 클러스터를 구성할 수 있다.
```cs
ActorSystem.Create("ClusterLab", ...);
```
1. 클러스터를 구성하기 위해 Seed Node 정보를 알아야 한다.
```
	cluster {
		seed-nodes = [
			"akka.tcp://ClusterLab@localhost:8081"
		]
	}
```