## 자동으로 액터를 생성하여 라우팅하기

TODO 1. /user/MyRouter/$a
			--> MyRouter로 장의 처리 전략을 수립하자.
	


1. Petabridge.Cmd.Host NuGet을 설치한다.

2. Routing 계층 구조 이해
	// compile-time 계층 구조
	Parent
	Son			// WithRouter(new ...)

	vs.

	// run-time 계층 구조
	Parent
	Son			// Router, 장애 처리 전략: Escalate
	$a			// Routee

3. Routee 장애 처리 전략은 기본적으로 "Router 부모"에서 정의한다.
	Parent		// WithSupervisorStrategy: Routee 장애 처리 전략이다.
	Son			// WithRouter

4. WithSupervisorStrategy vs. override SupervisorStrategy 함수 호출 우선 순위
	WithSupervisorStrategy > override SupervisorStrategy 함수

5. Router의 기존 장애 처리 전략 "Escalate"을 변경하고 싶다면,
   Router 전략 객체에 지정하면 된다.
	Parent
	Son			// WithRounter(new ...()
				//		.WithSupervisorStrategy( ... )
				//	)

6. 환경 설정 파일에 있는 Router 전략 사용하기
	using Akka.Routing;

	.WithRouter(FromConfig.Instance)



FAQ 1. Config 설정에서 Actor Path가 일치하지 않을 때 발생되는 run-time 예외
	A. 발생되는 경우
		a. akka.actor.deployment 계층 구조 불일치
	    b. Actor Path 불일치

	B. 예외
	Akka.Configuration.ConfigurationException: 
		'Configuration problem while creating [akka://LocalRoutingPool/user/GrandFatherActor] 
		with router dispatcher [akka.actor.default-dispatcher] and mailbox [] 
		and routee dispatcher [akka.actor.default-dispatcher] and mailbox [].'
	ConfigurationException: 
		Configuration missing for router [akka://LocalRoutingPool/user/GrandFatherActor] 
		in 'akka.actor.deployment' section.

	C. 예제 - akka.actor.deployment 계층 구조 불일치
		akka {
			deployment {
				/GrandFatherActor { ... }
			}
		}

		vs. 

		akka {
			actor {
				/GrandFatherActor { ... }
			}
		}


