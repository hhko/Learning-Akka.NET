# MORE FUN with Akka.NET Labs 

### Cluster Lab
1. **Overview**
   - Getting Started : **akka.cluster.seed-nodes**
   - Integrated with Petabridge.Cmd  
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
   
<br/>
<br/>

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
  - [ ] Console.WriteLine -> Log
  - [ ] 프로그램 종료 방법 변경
  - [ ] ActorSystem 이름을 conf 파일에서 지정하기 + SeedNode 이름에도 같이 사용하기?
  - [ ] Discovery Actors -> Actors Discovery
- [ ] Lab 추가
  - [ ] Overview > SeedNode가 종료된 후 재 시작할 때?
  - [ ] Overview > SeedNode가 N개 일 때
  - [ ] Warm-up, Router > Group
  - [ ] Warm-up, Deploy, Deploy 예외 처리 정보 부족 -> Akka.NET 이슈 제기
  - [ ] Cluster Routing Pools
  - [ ] Cluster Routing Groups
  - [ ] Cluster Singleton
  - [ ] Cluster Pub/Sub
  - [ ] ClusterClient
  - [ ] Split Brain Resolver
  - [ ] Consul Actor Discovery
  - [ ] Cluster Sharding
  - [ ] Cluster Distributed Data
  - [ ] Hocon > akka.cluster.failure-detector/akka.remote.transport-failure-detector/ ...
  - [ ] 장기간 Busy Actor일 때 Heartbeat 처리?
  - [ ] 매뉴얼 무중단 Rolling Update(메시지 버전 Up, ...)
  - [ ] Transactions
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
