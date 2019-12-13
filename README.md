# MORE FUN with Akka.NET 
**It is you. Actor Model for Event-Driven Microservice Architecture.**  

<br/>

**문서**  
- [홈(Current)](./README.md)
- [유용한 URLs](./URLs.md)

<br/>

**목차**   
1. Local
1. Remote
1. Persistence
1. Cluster
   - Overview
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
   - 클러스터 - 노드 역할과 최소 크기
     - Define Roles : **akka.cluster.roles**
     - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
     - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
     - Mix Minimum Size  
   - 클러스터 - 상태 동기화(Gossip)
   - 액터 - Routing
   - 액터 - Singleton
   - 액터 - Sharding
   - 메시지 - Pub/Sub
   - 메시지 - ClusterClient
   - 데이터 - Distributed Data
	
	
   
**TODO**
1. '확인 사항' -> 
1. akka.cluster.allow-weakly-up-members = on 예제(랜덤 폴트일 때), https://github.com/akkadotnet/akka.net/issues/3887
1. 04. Join the nodes to the cluster -> 05. Join the nodes to the cluster
1. '04. Cluster > 0. Warming up' 폴더 추가
   - Log
   - Petabridge.Cmd
   - Console Title
1. Leader 데모를 추가한다.
1. Best Practices을 README.md 파일에 정리한다.
1. Gossip을 정리한다.
   - Lifecycle: Leader Action, 명시적 Action
1. 클러스터 합류
   - seed-nodes 목록
   - akka.cluster.seed-node-timeout
   - 같은 프로세스 2번 실행
1. 클러스터 이탈
   - Graceful Leave
   - Petabridge.Cmd
   - akka.cluster.auto-down-unreachable-after
   - Split Brain
1. 클러스터 합류/이탈
   - 합류 > 합류?
   - 합류 > 이탈(정상), 인지 전 > 합류
   - 합류 > 이탈(정상), 인지 후 > 합류
   - 합류 > 이탈(비정상), 인지 전 > 합류
   - 합류 > 이탈(비정상), 인지 후 > 합류
1. Gossip
   - 알고리즘 이해
   - 옵션 이해
   - First Heatbeat ?

<br/>

- **Gossip**은 **Membership 상태**와 **Reachability 상태**를 동기화 시켜는 역할을 수행한다.
  - **Membership 상태: 이 노드가 클러스터의 현재 멤버인가?**  
    Joining, WeaklyUp, Up, Leaving, Exiting, Down, Removed
  - **Reachability 상태: 이 노드를 지금 연결할 수 있는가?**  
    Reachable, Unreachable  
    - Unreachable일 때 해당 Node을 클러스터에서 탈퇴(Removed) 시키지 않는 이유는 일시적인 네트워크 장애(Network Partition Tolerance)와 같이 자동으로 해결될 수 있는 일시적인 문제들이기 때문이다.
    - 노드와 연결할 수 없다고 해서 프로세스가 종료었다고 가정하면 안된다. 연결안된 노드가 여전히 활발하게 작업을 수행하다가 일시적 네트워크 장애가 해결되면 다시 정상적으로 작업을 수행할 수 있다.

- Reachability 상태는 akka.cluster.failure-detector가 결정합니다.

- Unreachable 현상
  1. Unreachable 노드가 1개 이상 존재하면 새 Node가 합류하지 못한다.
     - 새 Process로 처음 진입하는 Node일 때(새 Port)는 "Joining" 상태로 대기한다.
     - 새 Process로 재진입하는 Node일 때는(기존 Port) 기존 Node 상태를 "Up Unreachable"에서 "Down Unreachable"로 변경한다.
       새 Process가 이전 Port을 사용하고 있기 때문에 기존 Node는 종료되었다고 명확히 판단할 수 있기 때문에 "Up Unreachable"을 
```
akka.tcp://ClusterLab@localhost:8081 | [] | up |
akka.tcp://ClusterLab@localhost:8082 | [] | up | unreachable
akka.tcp://ClusterLab@localhost:8083 | [] | up | unreachable
```
```
akka.tcp://ClusterLab@localhost:8081 | [] | up |
akka.tcp://ClusterLab@localhost:8082 | [] | down | unreachable
akka.tcp://ClusterLab@localhost:8083 | [] | up | unreachable
```
```
akka.tcp://ClusterLab@localhost:8081 | [] | up |
akka.tcp://ClusterLab@localhost:8082 | [] | down | unreachable
akka.tcp://ClusterLab@localhost:8083 | [] | up | unreachable
akka.tcp://ClusterLab@localhost:8084 | [] | joining | 
```
  
- Unreachable 상태를 제거하는 방법
  - 수동 > Petabridge.Cmd 도구: cluster down -a 주소, clsuter down-unreachable
  - 자동 > akka.cluster.auto-down-unreachable-after
  - 자동 > Split Brain Resolver
  - 자동 > IDowningProvider
  - 자동 > IReachability 이벤트 처리: ClusterEvent.ReachableMember, ClusterEvent.UnreachableMember


## 04. Cluster 
1. **Overview**
   - Create a new cluster(Joining itself)
     ```cs
     akka.cluster.seed-nodes = [ 
        "akka.tcp://..."   // The seed-node's URL must be itself.  
     ]
     ```
   - Shut down a new cluster(Exiting itself gracefully)
     ```cs
     var cluster = Akka.Cluster.Cluster.Get(system);
     cluster.RegisterOnMemberRemoved(() => system.Terminate());
     cluster.Leave(cluster.SelfAddress);
	 
     // Waits for the Terminate to complete execution within a specified time interval.
     system.WhenTerminated.Wait();
     ```
   - Join multiple seed nodes  
     ```cs
     akka.cluster.seed-nodes = [ 
        "akka.tcp://...",  // The first seed-node's URL must be itself.
        "akka.tcp://..." 
     ]
     ```
   - ***Integrate with Petabridge.Cmd***
      - Leader?
      - Join
      - Exit
      - Petabridge.Cmd Exit/Join/...
   - ***Join the nodes to the cluster***
      - 새 Join(성공 됨), 기존 Join(실패 됨)
      - Exit 정상, Exit 비정상
      - Exit(정상)일 때 Rejoin, Exit(비정상)일 때 Rejoin
   - ***Retry Joining Time Interval : **akka.cluster.seed-node-timeout*****
   - ***Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after*****
## Cluster Lab-old
1. **Overview**
   - ~Create Cluster : **akka.cluster.seed-nodes**~
   - ~Integrate with Petabridge.Cmd~  
   - ~Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after**~
   - ~Retry Joining Time Interval : **akka.cluster.seed-node-timeout**~
   - ~Seed Node N개 일 때~
   - > Petabridge.Cmd Join, Leave, ...
1. **Roles and Minimum Size**
   - Define Roles : **akka.cluster.roles**
   - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
   - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
   - Mix Minimum Size  
1. **Gossip Events**
   - Subscribe to Gossip Events : **Cluster _cluster = Cluster.Get(Context.System); _cluster.Subscribe(Self, ...);**
   - Discover Actor by Tag
   - Discover Actor by Role and Path : **cluster.State.Members.Where(member => ...);**
1. **Warm-up for Cluster Routing**
   - Pool - Create Routees Automatically : **round-robin-pool**
   - Pool - Hanlde Exceptions from Routees
   - Group - Create routees yourself : **round-robin-group**, **path**
   - Group - Hanlde Exceptions from Routees
   - Deploy - Create Actor Remotely : **akka.actor.deployment.<actor-path>.remote**
   - Deploy - Hanlde Exceptions from Deployed Actors
   - > Remote Deploy - 자동으로 Deathwatch 되는지? 
1. **Cluster Routing**
   - Pool - Deploy Routees Remotely
   - Pool - Hanlde Exceptions from Routees
   - > Pool - Handle Routees Lifecycle
   - Group - Create Routees Yourself
   - Group - Handle Exceptions from Routees
   - > Group - Handle Routees Lifecycle
1. **Cluster Singleton**
   - Create Singleton
   - > Send Message To Singleton
   - > Manage Singleton???
   - > Buffer Size
1. **Distributed PubSub**
   - Publish - Communicate by Topic : **DistributedPubSub.Get**, **Subscribe/SubscribeAck**, **Publish**
   - Publish - Communicate by Topic with Same GroupId : **sendOneMessageToEachGroup: true**, (Send)
   - Publish - Communicate by Topic with Different GroupId : **sendOneMessageToEachGroup: true**, (Publish)
   - Publish - Get Topics
   - Send - Communicate by Path : **Put**, **Remove**, **Send**
   - Send - Communicate by Path with localAffinity : **localAffinity: true**
   - SendToAll - Communicate by Path 
   - SendToAll - Communicate by Path with ExcludeSelf : **excludeSelf: true**
   - > 사용자 정의 메시지(모든 Node에 참고해야 하나?, 메시지를 받지 않는 Node도?)
1. **Cluster Client**
   - Communicate with Cluster by Path 
   - Communicate with Cluster by Topic
   - Subscribe to SubscribeContactPoints Events
   - Subscribe to SubscribeClusterClients Events
   - Send Custom Messages
   - Sample - PingPong
   - > akka.cluster.client.receptionist, akka.cluster.client HOCON
1. **Sharding**
1. **Distributed Data**
1. **Split Brain Resolver**

<br/>

## Persistence Lab
1. **ReceivePersistentActor vs. UntypedPersistentActor**
   - PersistenceId 
   - OnCommand 
   - OnRecover 
   - IsRecovering   
   - Persist vs. PersistAsync 
   - DeferAsync 
   - DeleteMessages 
1. **AtLeastOnceDeliveryReceiveActor vs. AtLeastOnceDeliveryActor**
1. **PersistentView**
1. **AsyncWriteJournal**
1. **SnapshotStore**
   - LoadSnapshot vs. SaveSnapshot 
   - DeleteSnapshot vs. DeleteSnapshots
   - OnReplaySuccess vs. OnReplayFailure 
   - SnapshotSequenceNr 
1. SQLite
   - 뷰어
   - 데이터 확인하기

<br/>
<br/>

## DOING
1. Labs
   - **2019-05-W1** IoT Tutorials
   - **2019-05-W1** Persistence
   - **2019-05-W1** [Akka.Persistence.Extras](https://github.com/petabridge/Akka.Persistence.Extras)
   - **2019-05-W1** Cluster + Persistence
   - Cluster Singleton
   - Coordinated Shutdown
   - Split Brain Resolver
   - Cluster Sharding
   - Cluster Distributed Data
   - Hocon 
1. Cluster Extensions
   - Messages version-up
   - Node/Role version-up
   - Consul Actor Discovery
   - Transactions: Saga Pattern
1. FAQ
   - Local
   - Remote
   - 프로젝트 이해
1. Message Queue 연동
   - RabbitMQ
   - RabbitMQ Docker
   - HAProxy
1. OSS
   - .NET Core
   - [Tracing, Petabridge.Tracing.Zipkin](https://github.com/petabridge/Petabridge.Tracing.Zipkin)
   - [Metric, Akka with Prometheus](https://github.com/syncromatics/Akka.Monitoring.Prometheus)
   - [HealthCheck](https://github.com/petabridge/akkadotnet-healthcheck)
   - Cluster Actor Monitoring(Node, Actor Hierarchy, Hocon 뷰어/편집/배포, ...)
1. Docker
   - [Akka.NET-Bootstrap](https://github.com/petabridge/akkadotnet-bootstrap)
   - Docker Swarm 
   - Rolling Update
1. Books
   - Akka 쿡북
   - 아카 코딩 공작소
   - 러닝 아카
   - 아카를 이용한 마이크로서비스 개발
   - Reactive Application with Akka.NET
1. 추가 Lab
   - Docker Container Lab
   - Visual Studio Extension Lab

<br/>

## DOING - Labs
- [ ] Cluster Singleton
- [ ] [스플릿 브레인(Split Brain) 현상](https://aspell.tistory.com/75)
- [ ] At Least Once
- [ ] [Role 기반으로 Actor에게 메시지 보내기](https://github.com/akkadotnet/akka.net/issues/3757#issuecomment-483522034)
   - [ ] async IEnumerable 적용
- [ ] ClusterClient 
   - [ ] Sender.Tell???? 
   - [ ] 반드시 Seed Node이어야만 하나?
   - [ ] akka.cluster.client.receptionist.role ???
- [ ] Warm-up
   - [ ] 예외만 예제를 분리한다.
- [ ] NLog
   - [ ] [3] -> [0003] ThreadId 고정 영역으로 출력하기
   - [ ] Log 출력 소스 정보 [ ...? ] -> [akka://ClusterLab/deadletters]
   - [ ] 01. Overview -> 04. Retry Joining Time Interval -> NonSeedNode1 적용
- [ ] Console App
   - [ ] 콘솔 타이틀을 추가한다.
   - [ ] Console.WriteLine -> Log
   - [ ] 프로그램 종료 방법 변경
   - [ ] ActorSystem 이름을 conf 파일에서 지정하기 + SeedNode 이름에도 같이 사용하기?
   - [ ] [ClusterClient Router 연동할 때 버그 이슈](https://github.com/akkadotnet/akka.net/issues/3315)
- [ ] Split Brain
   - [ ] [Split Brain이란? cluster partition시 발생하는 split brain](https://heni.tistory.com/9)
   - [ ] [How to avoid the split-brain problem in elasticsearch](https://blog.trifork.com/2013/10/24/how-to-avoid-the-split-brain-problem-in-elasticsearch/)
   - [ ] [Running a Lagom microservice on Akka Cluster with split-brain resolver](https://medium.com/stashaway-engineering/running-a-lagom-microservice-on-akka-cluster-with-split-brain-resolver-2a1c301659bd)
   - [ ] [Monitoring 1.0, Commercial features and more](https://www.slideshare.net/Typesafe_Inc/typesafe-reactive-platform-monitoring-10-commercial-features-and-more)
- [ ] README.md 
   - [ ] 요약 추가
   - [ ] Diagram 이미지 추가
   - [ ] Youtube 데모 동영상 추가
   - [ ] Blog 적용
- [ ] 경로 만들기: new RootActorPath(Address) / ... ;
- [ ] Routee: _workerRouter.Ask<Routees>(new GetRoutees()) ...
- [ ] Visual Studio Code 기반으로 코드 작성
- [ ] 바쁜 Actor는 Heartbeat을 정상적으로 처리하나? Dispatcher와 연동.
- [ ] [Cluster.Tools.Client.ClusterReceptionist: An entry with the same key already exists](https://github.com/akkadotnet/akka.net/issues/2535)
<br/>

## TODO - Akka.NET Issues
- [ ] Hocon 다음 행 시작에 ","일 때 예외 발생
- [ ] RegisterOnMemberUp 람다 함수 Self Path가 akka://ClusterLab/system/cluster/$a 이다.
- [ ] .NET Core에서는 "akka.actor.deployment.<actor-path>.cluster.allow-local-routess = off"가 동작하지 않는다.
- [ ] Cluster Pool Routing 예외 처리가 Remote Deploy와 다르다(부족하다).
- [ ] ClusterClient "/user/xyz/" ActorPath 마지막에 "/"가 있으면 전달되지 않는다. 
- [ ] ClusterClient에세 정의된 사용자 정의 메시지가 Seed Node에도 참조되어 있어야 전송할 수 있다.
- [ ] Hocon 검증 방법?
- [ ] Rolling Update
- [ ] 부하 사전 분산(HAProxy)

<br/>

## TODO - Akka.NET FAQ
- [ ] Long running + Cancel: FSM
- [ ] Ask -> FSM
- [ ] Logging Message Info(including Generic).
- [ ] Log 분리(App, Akka, ...)
- [ ] Large Message
- [ ] 환경 설정 이해(akka.cluster.failure-detector/akka.remote.transport-failure-detector/ ...
- [ ] Serialization 
   - [ ] readonly
   - [ ] WPF
- [ ] WPF
   - [ ] main-thread Router x
   - [ ] Akka Coding Rules
   - [ ] MVVM + Akka Router
- [ ] 장애 처리 재시도?
- [ ] DeadLetter 재시도?
- [ ] 장기간 Busy Actor일 때 Heartbeat 처리?
- [ ] 매뉴얼 무중단 Rolling Update(메시지 버전 Up, ...)
- [ ] Actor Hierarchy 출력(/user/..., /system/...)
- [ ] 메시지 전송을 위해서는 메시지 Dll을 Cluster 모듈 모두가 참조해야 하나? SeedNode까지 포함해서?
- [ ] C#에서 C/C++ 예외 처리하기를 기본으로 제공한다.

<br/>
 