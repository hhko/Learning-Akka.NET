# Akka.NET Labs
MORE FUN with Akka.NET 

1. Cluster
   1. Overview
      1. Getting Started
      2. Integrated with Petabridge.Cmd
      3. Automatically Mark Unreachable Nodes
      4. Retry Joining Time Interval
   2. Roles and Minimum Size
   
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
  - [ ] App.Akka.hocon -> App.Akka.conf 파일명 변경하기
  - [ ] var hocon = ...; -> var config = ...;
  - [ ] ActorSystem 이름을 conf 파일에서 지정하기 + SeedNode 이름에도 같이 사용하기?
