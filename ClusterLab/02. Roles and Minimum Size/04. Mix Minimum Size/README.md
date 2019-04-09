## Cluster을 구성하기 위한 최소 Node와 Role의 Node 개수 지정하기.

 Node | Port | Role 
-----|-----|-----
 SeedNode1 | 8081 | Master, Lighthouse 
 NonSeedNode1 | 8091 | Provider 
 NonSeedNode2 | 8092 | Worker, Scheduler 
 NonSeedNode3 | 0893 | Worker, Resource Manager 

- Per-Role Minimum Size 설정
   - AND 조건을 만족하기 전까지는 "Joining" 상태가 된다.
   - 클러스터가 구성된 이후 최소 개수는 무시된다(8091, 8092 포트 Node가 클러스터에서 제거된 후에도 클러스터는 유지된다).
   - 클러스터 구성 조건은 다음과 같다.
      - 최소 3개 Node가 있어야 한다.
	  - 8091(Provider)와 8093(Resource Manager) Node는 반드시 있어야 한다.
```
		min-nr-of-members = 3

		role {
			Provider.min-nr-of-members = 1
			"Resource Manager".min-nr-of-members = 1
		}
```
  ![](./Images/Mix_Minimum_Size.png)
  
<br/>
<br/>

## TODO
- [ ] "Joining" 상태일 때 그 이유를 확인할 수 있는 방법인 없나?
   - App.Akka.conf(Hocon) 설정을 런타임에 확인할 수 있는 방법은?
