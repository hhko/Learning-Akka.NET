
## 데모
1. 데모 시나리오
   - ClusterClientApp 실행: ContactPoints
   - SeedNode1 실행( 1개가 접속 가능할 때): ContactPointAdded(N개), ContactPointRemoved(N - 1개)
   - SeedNode2 실행
   - SeedNode1 종료: ContactPointAdded(1개), ContactPointRemoved(1개)
2. 메시지 정의
   - ContactPoints: 접속 대상 목록
   - ContactPointAdded: 접속 시도
   - ContactPointRemoved: 접속 시도 실패, 접속 해제
3. 데모 요약
   - ClusterClient가 실행되면 ContactPoints 받는다.
   - ContactPoints 정보 중에 접속 가능하면 전체를 대상(ContactPointAdded N번 호출)으로 접속 시도를 한다.
   - 접속 중일 때는 새 접속 가능한 대상이 추가되어도 ContactPointAdded는 호출되지 않는다.
   - 접속 중인 노드가 종료되면 새 접속 대상으로 접속을 시도한다. 
4. 데모 시나리오 
   - ClusterClientApp 실행
      - ContactPoints
         - akka.tcp://ClusterLab@localhost:8081
         - akka.tcp://ClusterLab@localhost:8082
   ![](./Images/01_Demo_ClusterClientApp_Running.png)
   
   - SeedNode1 실행
      - ContactPointAdded
         - akka.tcp://ClusterLab@localhost:8081
         - akka.tcp://ClusterLab@localhost:8082   
   ![](./Images/02_Demo_SeedNode1_Running.png)
   
      - ContactPointRemoved
         - akka.tcp://ClusterLab@localhost:8082   
   ![](./Images/03_Demo_SeedNode1_Running.png)
   
   - SeedNode2 실행
   
   - SeedNode1 종료
      - ContactPointAdded
         - akka.tcp://ClusterLab@localhost:8082   
      - ContactPointRemoved
         - akka.tcp://ClusterLab@localhost:8081
   
   ![](./Images/04_Demo_SeedNode1_Terminating.png)
   

