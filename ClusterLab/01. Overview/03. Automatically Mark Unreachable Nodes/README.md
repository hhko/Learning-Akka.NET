## Unreachable Node을 자동으로 클러스터에서 제거하기
- **auto-down-unreachable-after = off** 기본값이다
  - Unreachable Node에 대해 무한으로 재시도를 한다.
- **auto-down-unreachable-after = 5s** 특정 기간이 지속되면 자동으로 클러스터에서 해당 노드를 제거한다.
  - auto-down-unreachable-after는 Seed Node에서 설정한다.
  - 'unreachable'로 인식(deadletter 9번째)한 후 10초 동안 'unreachable'이 연속되면 Cluster에서 해당 Node을 자동으로 제거 시킨다(더 이상 재시도를 하지 않는다).
```
Should the 'leader' in the cluster be allowed to automatically mark
unreachable nodes as DOWN after a configured time of unreachability?
Using auto-down implies that two separate clusters will automatically be
formed in case of network partition.
Disable with "off" or specify a duration to enable auto-down.
If a downing-provider-class is configured this setting is ignored.
```
 
<br/>
<br/>

## 데모 시나리오
- SeedNode1을 실행 시킨다.
- NonSeedNode1을 실행 시킨다.
- NonSeedNode1을 종료 시킨다(콘솔 창 닫기).
  - SeedNode1에서 deadletter 9번 호출된다, 7:40:59 
  - 5초 후(auto-down-unreachable-after = 5s)
  - 7:41:04, SeedNode1에서 NonSeedNode1을 클러스터에서 제거 시킨다(더 이상 재시도 하지 않는다).
```
[INFO][ ... 7:40:59] ... 9 dead letters encountered. 

...

[INFO][ ... 7:41:04] ... Loader is removing unreachable node [akka.tcp://ClusterLab@localhost:8091]
```
  ![](./Images/Auo-down.png)
  
<br/>
<br/>
 
## TODO
