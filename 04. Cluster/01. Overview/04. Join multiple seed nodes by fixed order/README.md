## 데모 순서

   ```
   [WARNING][2019-12-11 ?? 2:20:05][Thread 0004][[akka://Cluster-Lab/system/cluster/core/daemon/joinSeedNodeProcess-1#929318280]] Couldn't join seed nodes after [16] attempts, will try again. seed-nodes=[akka.tcp://Cluster-Lab@127.0.0.1:8081,akka.tcp://Cluster-Lab@127.0.0.1:8082,akka.tcp://Cluster-Lab@127.0.0.1:9999]
   ...(SeedNode1 실행 후)
   [INFO][2019-12-11 ?? 2:20:15][Thread 0004][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8083] - Welcome from [akka.tcp://Cluster-Lab@127.0.0.1:8081]
   ```
1. SeedNode2을 실행한다.
   - 클러스터 합류를 위해 재시도 지속한다(대기한다).
   ```
   [WARNING][2019-12-11 ?? 2:20:15][Thread 0021][[akka://Cluster-Lab/system/cluster/core/daemon/joinSeedNodeProcess-1#2064222105]] Couldn't join seed nodes after [12] attempts, will try again. seed-nodes=[akka.tcp://Cluster-Lab@127.0.0.1:8081,akka.tcp://Cluster-Lab@127.0.0.1:9999]
   ...(SeedNode1 실행 후)
   [INFO][2019-12-11 ?? 2:20:15][Thread 0021][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8082] - Welcome from [akka.tcp://Cluster-Lab@127.0.0.1:8081]
   ```

1. SeedNode1을 실행한다.
   - 클러스터를 구성한다.
   - SeedNode1(자신)는 Leader가 된다.
   - SeedNode2을 합류 시킨다.
   - SeedNode1에서 SeedNode2로 Leader을 변경한다.
   ```
   [INFO][2019-12-11 ?? 2:20:15][Thread 0006][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] is JOINING itself (with roles []) and forming a new cluster
   [INFO][2019-12-11 ?? 2:20:15][Thread 0006][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8081] to [Up]
   [INFO][2019-12-11 ?? 2:20:15][Thread 0006][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Node [akka.tcp://Cluster-Lab@127.0.0.1:8082] is JOINING, roles []
   [INFO][2019-12-11 ?? 2:20:15][Thread 0007][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Node [akka.tcp://Cluster-Lab@127.0.0.1:8083] is JOINING, roles []
   [INFO][2019-12-11 ?? 2:20:16][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8082] to [Up]
   [INFO][2019-12-11 ?? 2:20:16][Thread 0018][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8083] to [Up]
   ```

## 확인 사항
- 장점
  - 클러스터 생성 의존성을 명시할 수 있다.
    - 왜?  
	  클러스터는 akka.cluster.seed-nodes 첫번째 Seed Node가 합류할 때 생성된다.
  - akka.cluster.seed-nodes 정보가 같다(개별로 관리할 필요가 없다).
- 단점
  - akka.cluster.seed-nodes 첫번째 Seed Node가 합류할 수 없을 때는 클러스터를 생성할 수 없다.

## TODO
- Petabridge.cmd을 이용하여 Joining 상태와 Leader 식별을 추가한다.