# MORE FUN with Akka.NET Labs 

### Cluster Lab
1. **Overview**
   - Create Cluster : **akka.cluster.seed-nodes**
   - Integrate with Petabridge.Cmd  
   - Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after**
   - Retry Joining Time Interval : **akka.cluster.seed-node-timeout**
1. **Roles and Minimum Size**
   - Define Roles : **akka.cluster.roles**
   - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
   - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
   - Mix Minimum Size  
1. **Gossip Events**
   - Subscribe to Gossip Events : **Cluster _cluster = Cluster.Get(Context.System); _cluster.Subscribe(Self, ...);**
   - Discovery Actors
1. **Warm-up For Cluster Routing**
   - Local Routing Pools : Create routees automatically. **round-robin-pool**
   - Local Routing Groups : Create routees yourself. **round-robin-group**, path
   - Remote Deploy
   - Remote Deploy Exceptions Info 
1. **Cluster Routing**
1. **Cluster Singleton**
1. **Distributed PubSub**
1. **Cluster Client**
   - Communicate with Cluster by Path 
   - Communicate with Cluster by Topic
   - Subscribe to SubscribeContactPoints Events
   - Subscribe to SubscribeClusterClients Events

      
   
<br/>
<br/>

## TODO - 2019-04-W3

- [ ] ClusterClient > Subscribe to SubscribeContactPoints Events README.md 추가
- [ ] ClusterClient > Subscribe to SubscribeClusterClients Events 예제 정리
- [ ] ClusterClient > Sender.Tell???? 
- [ ] ClusterClient > Send Custom Messages 예제 추가
- [ ] ClusterClient > Understand Cluster Client Config 예제 추가
- [ ] ClusterClient > Tutorials PingPong 예제 추가
- [ ] ClusterClient > 반드시 Seed Node이어야만 하나?
- [ ] Cluster > Pool Routing, Group Routing
- [ ] Cluster > Pub/Sub
- [ ] At least once
- [ ] 콘솔 타이틀
- [ ] NLog 
- [ ] [Role 기반으로 Actor에게 메시지 보내기](https://github.com/akkadotnet/akka.net/issues/3757#issuecomment-483522034)
- [ ] akka.cluster.client.receptionist.role ???

## TODO
- [ ] README.md
  - [ ] 주요 함수 Summary 추가
  - [ ] Diagram 이미지 추가
  - [ ] Youtube 데모 동영상 추가
- [ ] NLog
  - [x] NLog.config -> App.NLog.conf 파일명 변경하기
  - [ ] [3] -> [0003] ThreadId 고정 영역으로 출력하기
  - [ ] Log 출력 소스 정보 [ ...? ] -> [akka://ClusterLab/deadletters]
  - [ ] 01. Overview -> 04. Retry Joining Time Interval -> NonSeedNode1 적용
- [ ] Akka
  - [ ] 콘솔 타이틀을 추가한다.
  - [ ] Console.WriteLine -> Log
  - [ ] 프로그램 종료 방법 변경
  - [ ] ActorSystem 이름을 conf 파일에서 지정하기 + SeedNode 이름에도 같이 사용하기?
  - [ ] [ClusterClient Router 연동할 때 버그 이슈](https://github.com/akkadotnet/akka.net/issues/3315)
- [ ] Lab 추가
  - [ ] Warm-up Routting 예외 처리, 생성 분리
  - [ ] Warm-up, Deploy, Deploy 예외 처리 정보 부족 -> **Akka.NET 이슈 제거**
  - [ ] ClusterClient "/user/xyz/" ActorPath 마지막에 "/"가 있으면 전달되지 않는다. -> **Akka.NET 이슈 제거**
  - [ ] ClusterClient에세 정의된 사용자 정의 메시지가 Seed Node에도 참조되어 있어야 전송할 수 있다 -> **Akka.NET 이슈 제거**
  - [ ] ClusterClient Event 처리 예제 추가
  - [ ] Overview > SeedNode가 종료된 후 재 시작할 때?
  - [ ] Overview > SeedNode가 N개 일 때
  - [ ] Gossip Events > RegisterOnMemberRemoved, Leave, Join, ... 
  - [ ] Cluster Routing Pools
  - [ ] Cluster Routing Groups
  - [ ] Cluster Singleton
  - [ ] Cluster Pub/Sub
  - [ ] Split Brain Resolver
  - [ ] Consul Actor Discovery
  - [ ] Cluster Sharding
  - [ ] Cluster Distributed Data
  - [ ] Hocon > akka.cluster.failure-detector/akka.remote.transport-failure-detector/ ...
  - [ ] 장기간 Busy Actor일 때 Heartbeat 처리?
  - [ ] 매뉴얼 무중단 Rolling Update(메시지 버전 Up, ...)
  - [ ] Transactions
  - [ ] Actor Hierarchy 출력(/user/..., /system/...)
- [ ] 예제 추가
  - [ ] [Cluster.WebCrawler](https://github.com/petabridge/akkadotnet-code-samples/tree/master/Cluster.WebCrawler)
  - [ ] [Cluster.Monitoring](https://github.com/cgstevens/Akka.Cluster.Monitor)
  - [ ] [Akka.NET Repo Cluster Examples](https://github.com/akkadotnet/akka.net/tree/dev/src/examples/Cluster)
  - [ ] [Sharding](https://github.com/uatec/akka.net-clustersharding-example)
  - [ ] [Pluralsight Akka.NET Cluster](https://github.com/thelegendofando/Pluralsight)
  - [ ] [Pub/Sub Problem](https://github.com/thelegendofando/ProducerConsumerProblem)
  - [ ] [Singleton Example](https://github.com/cgstevens/MyClusterServices)
  - [ ] [FileProcessor](https://github.com/cgstevens/FileProcessor)
  - [ ] [Akka.NET Workshop](https://github.com/profesor79/LDNAAkkaWorkshop)
  - [ ] [Akka.NET Patterns](https://github.com/profesor79/akka.net.Patterns)
  - [ ] [Riccardo Terrell, ActorModel](https://github.com/rikace/AkkaActorModel)
  - [ ] [Riccardo Terrell, Workshop](https://github.com/rikace/akkaworkshop)
  - [ ] [Riccardo Terrell, devConf2017](https://github.com/rikace/devConf2017)
  - [ ] [Proto Saga](http://proto.actor/blog/2017/06/24/money-transfer-saga.html)
  - [ ] [Proto Examples](https://github.com/rogeralsing/protoactor-dotnet/tree/dev/examples)
  - [ ] [Akka.Cluster.Sharding demo](https://github.com/Horusiath/AkkaDemos)
  - [ ] [CQRS](https://github.com/Horusiath/AkkaCQRS)
  - [ ] [HealthCheck](https://github.com/petabridge/akkadotnet-healthcheck)
  - [ ] [Cluster Sharding](https://github.com/ctrlaltdan/akka.net-cluster-sharding)
  - [ ] [한글 예제](https://github.com/psmon/AkkaNetTest/blob/master/README.md)
  - [ ] [akka-net-traffic-control](https://github.com/EdwinVW/akka-net-traffic-control)
  - [ ] [akka.net-warehouse-sample](https://github.com/EdwinVW/akka.net-warehouse-sample)
- 한글 문서
  - [ ] [Bbotcamp 한글화](https://blog.rajephon.dev/2018/12/02/akka-01/)
  - [ ] [블로그](https://m.blog.naver.com/PostView.nhn?blogId=kbh3983&logNo=221182343063&categoryNo=119&proxyReferer=https%3A%2F%2Fwww.google.co.kr%2F)
  - [ ] [한글 Wiki](http://wiki.webnori.com/m/mobile.action#page/1048605)
- FAQ
  - [ ] Long running + Cancel: FSM
  - [ ] Ask -> FSM
  - [ ] Logging Message Info(including Generic).
  - [ ] Log 분리(App, Akka, ...)
  - [ ] Large Message
  - [ ] 환경 설정(Remote, Cluster, ...)
  - [ ] Serialization > readonly
  - [ ] Serialization > WPF
  - [ ] WPF main-thread Router x
  - [ ] WPF MVVM + Akka 표준
  - [ ] WPF MVVM + Akka Router
  - [ ] 장애 처리 재시도?
  - [ ] DeadLetter 재시도?
