## 클러스터 구성하기(필수 조건)

- **클러스터를 구성하기 위해 모든 ActorSystem 이름은 반드시 같아야 한다(Hocon 정보까지도).**
```cs
ActorSystem.Create("ClusterLab", ...);
```

- **클러스터 시작점(Seed Node) 정보를 모두 공유한다.**
```
	cluster {
		seed-nodes = [
			"akka.tcp://ClusterLab@localhost:8081"
		]
	}
```
<br/>
<br/>

## 데모 시나리오
- 포트 정보
  - SeedNode1 : 8081
  - NonSeedNode1 : 8091
  - NonSeedNode2 : 8092
  - NonSeedNode3 : 8093
  
- 실행 순서
  - SeedNode1 실행
  - NonSeedNode1 실행
  - NonSeedNode2 실행
  - NonSeedNode3 실행

- 실행 예.
  - SeedNode1(8081)만 실행할 때: [Up] 메시지를 확인할 수 있다.
![](./Images/Starting_SeedNode1.png)

  - SeedNode1(8081) 실행 후 NonSeedNode1(8091)가 실행될 때: [Up]과 Welcome 메시지를 확인할 수 있다.
![](./Images/Starting_NonSeedNode1.png)

<br/>
<br/>

## TODO
- [ ] NonSeedNode가 종료될 때 SeedNode가 무한으로 재시도하는 것을 방지하기
- [ ] NonSeedNode가 SeedNode 없을 때(먼저 실행, ... 등) 무한으로 재접속 시도를 방지하기