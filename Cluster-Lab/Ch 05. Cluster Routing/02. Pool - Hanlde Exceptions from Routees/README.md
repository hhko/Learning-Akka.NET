## Cluster Pool Routing 예외 로그 출력하기(정보가 부족하다)
1. 배포된 액터에서 예외가 발생하면 배포한 액터에게 전달된다.
2. **예외가 발생한 소스 라인 정보까지 제공하지 않는다 vs. Remote Deploy일 때는 제공한다.**

<br/>
<br/>

## 데모
1. 시나리오
   - NonSeedNode1에서 FooActor를 NonSeedNode2에게 배포한다.

2. NonSeedNode1 환경 설정
```
akka {
	actor {
		provider = "cluster"

		deployment {
			/FooActor {
				router = round-robin-pool
				nr-of-instances = 10

				cluster {
					enabled = on
					allow-local-routess = off
					max-nr-of-instances-per-node = 3
					
					#
					# Scheduler Role에게 Cluster Routee를 배포한다.
					#
					use-role = Scheduler
				}
			}
		}
	}
	...
}
```
3. NonSeedNode2 환경 설정
```
akka {
	actor {
		provider = "cluster"
	}

    ...

	cluster {
		...

		roles = [
			"Scheduler"
		]
	}
}
```

![](./Images/NonSeedNode1.png)
![](./Images/NonSeedNode2.png)

<br/>
<br/>

## TODO?
1. Cluster Pool Routing 되기 전에 Tell을 하면 메시지가 전송되지 않는다.
   - Dead Letter에게도 전달되지 않는다.

<br/>
<br/>

## TODO
1. Akka.NET 팀에게 해당 이슈를 제기한다.
