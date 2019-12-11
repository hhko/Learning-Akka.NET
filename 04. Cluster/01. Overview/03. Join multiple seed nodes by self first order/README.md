## 데모 순서

1. SeedNode2을 실행한다.
   - 클러스터를 생성한다.
   - SeedNode2(자신)는 Leader가 된다.
   ```
   [INFO][2019-12-11 ?? 1:51:05][Thread 0005][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8082] - Node [akka.tcp://Cluster-Lab@127.0.0.1:8082] is JOINING itself (with roles []) and forming a new cluster
   [INFO][2019-12-11 ?? 1:51:05][Thread 0005][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8082] - Leader is moving node [akka.tcp://Cluster-Lab@127.0.0.1:8082] to [Up]
   ```
1. SeedNode1을 실행한다.
   - SeedNode1에 합류한다.
   ```
   [INFO][2019-12-11 ?? 1:51:43][Thread 0004][Cluster] Cluster Node [akka.tcp://Cluster-Lab@127.0.0.1:8081] - Welcome from [akka.tcp://Cluster-Lab@127.0.0.1:8082]
   ```

## 확인 사항
- 장점
  - 실행 순서와 상관 없이 바로 클러스터를 구성한다.
- 단점
  - akka.cluster.seed-nodes 정보가 다르다(개별로 관리해야 한다. 관리 포인트가 많아 진다).