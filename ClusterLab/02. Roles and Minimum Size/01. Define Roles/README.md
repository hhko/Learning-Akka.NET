## Node에 Role(역할)을 정의한다.
1. Role 기본값은 없다. 
```
    # The roles of this member. List of strings, e.g. roles = ["A", "B"].
    # The roles are part of the membership information and can be used by
    # routers or other services to distribute work to certain member types,
    # e.g. front-end and back-end nodes.
    roles = []
```

2. N개의 Role을 정의할 수 있다.
```
	akka.cluster.roles = [
		"front-end",
		"worker"
	]
```	
 
<br/>
<br/>

## 데모 시나리오
- Role 정보
 구분 | Role 
-----|-----
 SeedNode1 | Master, Lighthouse 
 NonSeedNode1 | Provider 
 NonSeedNode2 | Worker, Scheduler 
 NonSeedNode3 | Worker, Resource Manager 

  ![](./Images/Roles.png)
