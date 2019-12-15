# MORE FUN with Akka.NET 
**It is you. Actor Model!**
> We believe that writing correct concurrent & distributed, resilient and elastic applications is too hard. 
> Most of the time it's because we are using the wrong tools and the wrong level of abstraction.
>
> **Actor Model is here to change that.**
>
> **Using the Actor Model we raise the abstraction level and provide a better platform to build correct concurrent and scalable applications.** 

<br/>

**Documents**  
- [**HOME(Current)**](./README.md)
- [Useful URLs](./URLs.md)
- [TODOs](./TODOs.md)

<br/>

**Contents**   
1. Local
   - 단위 테스트
1. Remote
   - 단위 테스트
1. Persistence
   - 단위 테스트
1. Cluster
   - 클러스터 - 개요
     - Petebridge.Cmd
	 - 로그, 타이틀, 종료 이벤트
	 - 액터시스템 클러스터 시각화(?)
   - 클러스터 - 생성
     - 시드 노드가 1개일 때, 클러스터 생성하기(Create a cluster)
     - 시드 노드가 1개일 때, 클러스터 파괴하기(Destory a cluster)
	 - 시드 노드가 N개일 때, 시드 노드가 고정 순서
	 - 시드 노드가 N개일 때, 시드 노드가 자신 우선
	 - 재시도?
	 - 타임아웃?
   - 클러스터 - 노트 합류와 이탈
     - 노드 합류
	 - 노드 이탈(정상)
	 - 노드 이탈(비정상)
	 - Reachable일 때 같은 노드 합류 안됨
	 - Unreachable일 때 노드 합류 안됨(같은 노드와 새 노드)
	 - Unreachable일 때 노드 합류(weekly member, 같은 노드와 새 노드)
	 - 노드 이탈 시키기
	   - 시간(단순)
	   - 노드 갯수(Split Brain Resolver)
	   - ...3개(Split Brain Resolver)
     - 이탈 감지
   - 클러스터 - 노드 역할과 최소 크기
     - Define Roles : **akka.cluster.roles**
     - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
     - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
     - Mix Minimum Size  
   - 클러스터 - 상태 동기화(Gossip)
   - 액터 - Routing
   - 액터 - Singleton
   - 액터 - Sharding
   - 액터 - 인지? 찾기
   - 액터 - 지표(Matrices)
   - 메시지 - Pub/Sub
   - 메시지 - ClusterClient
   - 메시지 - 동기화
   - 메시지 - 추적
   - 데이터 - Distributed Data
   - 컨테이너 - Docker Compose
   - 컨테이너 - Kubernetes
     - Health Checker
   - 예제 - Cluster WebCrawler
   - 예제 - End to End Akka.NET Distributed Programming
   - 단위 테스트
1. Streams
