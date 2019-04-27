## PingPong 예제
1. 시나리오
   - ClusterClient에서 PingActor는 1초마다 클러스터 /user/PongActor 액터에게 Ping 메시지를 보낸다.
       - Timeout 처리르 한다.
   - PongActor는 메시지는 재전송한다.

2. 데모   
![](./Images/Demo.png)   

3. 타임아웃 처리 데모
![](./Images/Demo_Timeout.png)   