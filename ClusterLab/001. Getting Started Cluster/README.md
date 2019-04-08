## 클러스터 구성하기(필수 조건)

- **클러스터를 구성하기 위한 모든 ActorSystem은 반드시 이름이 같아야 한다.**
```cs
ActorSystem.Create("ClusterLab", ...);
```

<br/>

- **클러스터 시작점(Seed Node) 정보를 모두 공유한다.**
```
	cluster {
		seed-nodes = [
			"akka.tcp://ClusterLab@localhost:8081"
		]
	}
```
