# MORE FUN with Akka.NET Labs 

1. Cluster Lab
   1. Overview  
       - Getting Started : **akka.cluster.seed-nodes**
       - Integrated with Petabridge.Cmd  
       - Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after**
       - Retry Joining Time Interval : **akka.cluster.seed-node-timeout**
   1. Roles and Minimum Size  
       - Define Roles : **akka.cluster.roles**
       - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
       - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
       - Mix Minimum Size  
   1. Gossip Events
       - Subscribe to Gossip Events : **Cluster _cluster = Cluster.Get(Context.System); _cluster.Subscribe(Self, ...);**
   
<br/>
<br/>

## TODO
- [ ] NLog
  - [ ] NLog.config -> App.NLog.conf 파일명 변경하기
  - [ ] [3] -> [0003] ThreadId 고정 영역으로 출력하기
  - [ ] Log 출력 소스 정보 [ ...? ] -> [akka://ClusterLab/deadletters]
  - [ ] 01. Overview -> 04. Retry Joining Time Interval -> NonSeedNode1 적용
- [ ] Akka
  - [ ] Console.WriteLine -> Log
  - [ ] 프로그램 종료 방법 변경
  - [ ] ActorSystem 이름을 conf 파일에서 지정하기 + SeedNode 이름에도 같이 사용하기?
