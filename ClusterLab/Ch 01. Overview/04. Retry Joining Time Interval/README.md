## Seed Node 최초 재접속 시도 간격을 지정하기
- **seed-node-timeout = 5s** 기본값이다
  - 5초 간격으로 무한 재시도를 한다.
  - Seed Node 최초 접속(Join)을 위한 시간 간격이다.
```
how long to wait for one of the seed nodes to reply to initial join request.
```
 
<br/>
<br/>

## 데모 시나리오
- seed-node-timeout 정보
  - NonSeedNode1 : 10s
  - NonSeedNode2 : 15s
  - NonSeedNode3 : 20s
  
- NonSeedNode1을 실행 시킨다.
  - [INFO][ ... 8:29:47] ... 3 dead letters encountered.
  - 10초 후(seed-node-timeout = 10s)
  - [INFO][ ... 8:29:57] ... 4 dead letters encountered.
  ![](./Images/seed-node-timeout_10s.png)
  
- seed-node-timeout 기본값일 때는 5초 간격으로 재접속을 시도한다.
  - [INFO][ ... 8:10:13] ... 3 dead letters encountered.
  - 5초 후(seed-node-timeout = 5s)
  - [INFO][ ... 8:10:17] ... 4 dead letters encountered.
  ![](./Images/seed-node-timeout_default_5s.png)
  
<br/>
<br/>
 
## TODO
- [ ] NonSeedNode가 SeedNode 없을 때(먼저 실행, ... 등) 무한으로 재접속 시도를 방지하기
```
    # If a join request fails it will be retried after this period.
    # Disable join retry by specifying "off".
    retry-unsuccessful-join-after = 10s
```
- [ ] 9번 DeadLetter 이후 부터는 Warning으로 처리한다.
![](./Images/Warning_Attempts.png)