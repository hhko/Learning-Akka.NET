# MORE FUN with Akka.NET Labs 

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

## Cluster Lab
1. **Overview**
   - Create Cluster : **akka.cluster.seed-nodes**
   - Integrate with Petabridge.Cmd  
   - Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after**
   - Retry Joining Time Interval : **akka.cluster.seed-node-timeout**
   - > Seed Node N개 일 때
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
 
## TODO - Akka.NET Samples
- [ ] [Cluster.WebCrawler](https://github.com/petabridge/akkadotnet-code-samples/tree/master/Cluster.WebCrawler)
- [ ] [Cluster.Monitoring](https://github.com/cgstevens/Akka.Cluster.Monitor)
- [ ] [Akka.NET Repo Cluster Examples](https://github.com/akkadotnet/akka.net/tree/dev/src/examples/Cluster)
- [ ] [Sharding](https://github.com/uatec/akka.net-clustersharding-example)
- [ ] [Sharding & Persistence](https://github.com/jurek333/AkkaNETShardingPersistenceTest)
- [ ] [Pluralsight Akka.NET Cluster](https://github.com/thelegendofando/Pluralsight)
- [ ] [Pub/Sub Problem](https://github.com/thelegendofando/ProducerConsumerProblem)
- [ ] [Singleton Example](https://github.com/cgstevens/MyClusterServices)
- [ ] [akka-cluster-singleton, Scala](https://github.com/viswanath7/akka-cluster-singleton)
- [ ] [Akka Singleton Supervision](https://doc.akka.io/docs/akka/current/cluster-singleton.html#supervision)
- [ ] [x-akka-cluster-singleton](https://github.com/jpierson/x-akka-cluster-singleton)
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
- [ ] [Akka 공식 예제](https://github.com/akka/akka-samples)
- [ ] [Akka.NET 공식 예제](https://github.com/akkadotnet/akka.net/tree/dev/src/examples)
- [ ] [Akka.NET Bootcamp](https://github.com/petabridge/akka-bootcamp)
- [ ] [Actor MapReduce WordCounter](https://github.com/DavidHoerster/ActorMapReduceWordCounter)
- [ ] [Saga Pattern](https://github.com/heynickc/akka_process_manager)
- [ ] [Saga UnitTests](https://github.com/VaughnVernon/Dotsero/blob/master/DotseroTest/ProcessManagerTests.cs), [동영상](https://vimeo.com/104021785)
- [ ] [Akka.NET Reference Architecture - CRQS + Sharding + In-Memory Replication](https://github.com/Aaronontheweb/InMemoryCQRSReplication)
- [ ] [Debugger application for viewing Akka.NET Cluster.Sharding Data](https://github.com/Aaronontheweb/Cluster.Sharding.Viewer)
- [ ] [Akka.AtLeastOnceDelivery-Snapshot-Manipulation](https://github.com/Aaronontheweb/Akka.AtLeastOnceDelivery-Snapshot-Manipulation)
- [ ] [Generic persistent gateway for Akka.NET at-least-once-delivery semantics](https://gist.github.com/Horusiath/64d829720b66df16bc59dbf106a8008e)
- [ ] [Play with Akka Cluster](https://github.com/carsten-j/playWithAkkaCluster)
- [ ] [AkkaAnalyzer](https://github.com/AkkaNetContrib/AkkaAnalyzer)
- [ ] [Akka.NET-Throughput](https://github.com/Aaronontheweb/Akka.NET-Throughput)
- [ ] [Detect Socket Leaks](https://github.com/petabridge/SocketLeakDetection)
- [ ] [.netzuid-akka Talk](https://github.com/Danthar/.netzuid-akka)
- [ ] [PersistentActor with a backoff supervisor](https://github.com/Danthar/Akka.PersistentBackoff)
- [ ] [Process Manager](https://github.com/justeat/ProcessManager), [Blog](https://tech.just-eat.com/2015/05/26/process-managers/)
- [ ] [Auto-Updating Cache using Actors](https://github.com/profesor79/akka.net.Patterns)
- [ ] [a simple cluster with Akka.NET on .NET Core](https://github.com/BigDaddy1337/dotnet-core-akka-cluster-example)
- [ ] [Akka retrying backoffsupervisor](https://gist.github.com/Danthar/82e89eff01ba1eb1d4c3)
- [ ] [Akka.Cluster.SplitBrainResolver](https://github.com/kennethito/Akka.Cluster.SplitBrainResolver)
<br/>

## Blog Korean
- [ ] [Botcamp 한글화](https://blog.rajephon.dev/2018/12/02/akka-01/)
- [ ] [Naver 블로그](https://m.blog.naver.com/PostView.nhn?blogId=kbh3983&logNo=221182343063&categoryNo=119&proxyReferer=https%3A%2F%2Fwww.google.co.kr%2F)
- [ ] [하마 블로그](https://hamait.tistory.com/category/Akka)
- [ ] [하마 Akka 클러스터 번역](https://hamait.tistory.com/m/670)
- [ ] [인호의 IT 잡동사니](https://helloino.tistory.com/category/Programing/Akka)
- [ ] [코드 로드](https://codeday.me/ko/?s=Akka), [실패한 메시지 다시 시도하기 ](https://codeday.me/ko/qa/20190421/370868.html) 
- [ ] [Akka.NET Kafka](http://wiki.webnori.com/display/AKKA)
- [ ] [Akka.NET + ASP.NET Core](https://hardyeats.github.io/2018/02/19/aspnetcore-akka/)
- [ ] [임백준 Akka 강의](https://www.youtube.com/watch?v=6AsrJdCSojs&list=PLVsNizTWUw7EH7R0gP6qyc4p-vOc6jvJq)
- [ ] [Akka Stream을 활용한 Reliable Message Stream 처리](https://www.popit.kr/tag/akka-stream)
- [ ] [김이선 블로그](http://veblush.github.io/ko/), [발표 동영상](https://www.youtube.com/watch?v=rcG0g5M42Tg) 
<br/>

## Blog - Akka.NET English
- [ ] [Akka.NET Official](https://petabridge.com/blog/)
- [ ] [Akka.NET CEO, Aaronstannard](http://www.aaronstannard.com/)
- [ ] [Akka.NET 팀 코어 멤버](https://bartoszsypytkowski.com/tag/akka-net/)
- [ ] [Gigi](http://gigi.nullneuron.net/gigilabs/tag/akka-net/)
- [ ] [Akka-Guide](https://connelhooley.uk/blog/2017/02/21/akka-guide)
- [ ] [Akka-Testing-Helper V1](https://connelhooley.uk/blog/2017/09/30/introducing-akka-testing-helpers-di)
- [ ] [Akka-Testing-Helper V2](https://connelhooley.uk/blog/2018/10/05/akka-testing-helpers-v2)
- [ ] [AkkaTestingHelpers](https://github.com/connelhooley/AkkaTestingHelpers)
- [ ] [Akka.NET with EF.Core](https://havret.io/akka-entity-framework-core)
- [ ] [Akka.NET + DI + Testing Considerations](https://sachabarbs.wordpress.com/2018/05/22/akka-net-di-testing-considerations/)
- [ ] [Supervisors in C# with Akka.NET](https://buildplease.com/pages/supervisors-csharp/)
- [ ] [Akka.NET 기술 정리 7개](https://hryniewski.net/tag/akka-net/)
- [ ] [Akka.NET Extension 만들기](https://havret.io/akka-entity-framework-core)

<br/>

## Blog - DDD and DesignPatterns English
- Primitive Obsession
   - [ ] [Primitive Obsession](https://refactoring.guru/smells/primitive-obsession)
   - [ ] [강박적 기본 타입](http://dj6316.torchpad.com/%EB%A6%AC%ED%8C%A9%ED%86%A0%EB%A7%81%28refactoring%29/CH.03+%EC%BD%94%EB%93%9C%EC%9D%98+%EA%B5%AC%EB%A6%B0%EB%82%B4/5.%EA%B0%95%EB%B0%95%EC%A0%81+%EA%B8%B0%EB%B3%B8+%ED%83%80%EC%9E%85+%EC%82%AC%EC%9A%A9+Primitive+Obsession)
   - [ ] [Strongly Typed Identifiers in .NET revealed](https://www.seeitsharp.pl/2018/11/strongly-typed-identifiers-dot-net/)
   - [ ] [From Primitive Obsession to Domain Modelling](https://blog.ploeh.dk/2015/01/19/from-primitive-obsession-to-domain-modelling/)
   - [ ] [Functional C# Primitive obsession](https://enterprisecraftsmanship.com/2015/03/07/functional-c-primitive-obsession/)
   - [ ] [Dealing with primitive obsession](https://lostechies.com/jimmybogard/2007/12/03/dealing-with-primitive-obsession/)
   - [ ] [Primitive Obsession, Custom String Types, and Self Referencing Generic Constraints](https://haacked.com/archive/2012/09/30/primitive-obsession-custom-string-types-and-self-referencing-generic-constraints.aspx/)
   - [ ] [Primitive Obsession by Example](https://medium.com/@gatm50/primitive-obsession-by-example-1f8c157f9900)
   - [ ] [Autofac Primitive Obsession](https://github.com/lucax88x/PrimitiveObsession)
   - [ ] [Designing Data Objects in C# and F#](https://www.dotnetcurry.com/patterns-practices/1429/data-objects-csharp-fsharp)
   - [ ] [Designing Data Objects in C#: More examples](https://www.dotnetcurry.com/patterns-practices/1475/data-objects-csharp-examples)
   - [ ] [NuGet, ValueOf](https://github.com/mcintyre321/ValueOf/)
   - [ ] [NuGet, Value](https://github.com/tpierrain/Value)
   - [ ] [NuGet, FluentTypes](https://github.com/Fyzxs/FluentTypes)
- Functional
   - [ ] [PPT, Functional patterns and techniques in C#](https://www.slideshare.net/PterTakcs2/functional-patterns-and-techniques-in-c)
   - [ ] [PurityAnalyzer](https://marketplace.visualstudio.com/items?itemName=yacoubmassad.PurityAnalyzer)
   - [ ] [PurityAnalyzer Source](https://github.com/ymassad/PurityAnalyzer)
   - [ ] [PurityAnalyzer Examples](https://github.com/ymassad/PureCodeInCSharpExample/tree/ExtractingWriteToConsole)
   - [ ] [Writing Pure Code in C#](https://www.dotnetcurry.com/csharp/1464/pure-code-csharp)
   - [ ] [Writing Honest Methods in C#](https://www.dotnetcurry.com/patterns-practices/1449/pure-impure-methods-csharp)
   - [ ] [Composing Honest Methods in C#](https://www.dotnetcurry.com/patterns-practices/honest-methods-csharp)
   - [ ] [Functional, Reactive, and Distributed Systems Bibliography](https://gist.github.com/brendanzab/a6073e73f751a6ca9750f960a92f2afe)
- Domain-Driven Design
   - [ ] [DDD, Hexagonal, Onion, Clean, CQRS, … How I put it all together](https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/)
- Repository Pattern
   - [ ] [4 Common Mistakes with the Repository Pattern](https://programmingwithmosh.com/net/common-mistakes-with-the-repository-pattern/)
   - [ ] [Using the Repository and Unit Of Work Pattern in .NET Core](https://garywoodfine.com/generic-repository-pattern-net-core/)
- CQRS
   - [ ] [BookARoom project](https://github.com/tpierrain/CQRS)
