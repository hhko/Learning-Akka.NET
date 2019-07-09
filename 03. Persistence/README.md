## Akka.Persistence
1. Event Sourcing
   - 성공적인 연산을 모두 저널에 이벤트로 남긴다.
   - 계산은 메모리에서 이뤄지며, 연산에 성공하면 이벤트를 저장한다.  
     store events ..
   - 저널 내부에 있는(저장된) 이벤트는 실질적으로 변경 불가능하다.
   - 장점 vs. 단점
      - 장점: 데이터베이스를 쓰고(이벤트 저장) 읽는(복구) 과정이 두 단계로 나뉜다는 것이다.
      - 단점: 필요한 저장 공간이 늘어난다(스냅샵을 만들면 최종 상태를 복구하는 데 걸리는 시간과 저장 공간 사용량을 줄일 수 있다).
   - 이벤트(Event): 액터가 그 로직을 올바르게(성공) 수행했다는 증거를 제공한다.
   - 명령(Command): 로직을 실행하기 위해 액터에게 보내지는 메시지를 의미한다.
   - 로직이 실패할 때는 Evnet가 Jurnal에 저장되나? 저장되지 않는다면, 실패는???
   - Persist(  <- 동기 
      첫 번째 인자: 영속화할 이벤트  
      두 번째 인자: 영속화된 이벤트를 처리할 함수로, 해당 이벤트가 저널에 성공적으로 영속되면 호출된다. <- 비동기  
         &nbsp;&nbsp;&nbsp;   // 두 번째 인자 함수가 완료되기 전에 다음 명령이 호출되지 않는다.  
         &nbsp;&nbsp;&nbsp;   // -> 두 번째 인자 함수안에서 Sender를 참조해도 안전한다.  
     )  
   - RecoveryCompleted: 복구가 완료되면 받는 메시지
   - 복구 중에 전달되는 메시지는 복구가 완료되면 도착한 순서대로 전달된다(Stash???)  
   - **??: 복구 중에 예외가 발생되면???**
   - **??: Event 저장 실패할 때는??**
  

## akka.persistence.at-least-once-delivery

- Interval between re-delivery attempts.
   - 재시도 간격이다.
   - redeliver-interval = 5s

- After this number of delivery attempts a `ReliableRedelivery.UnconfirmedWarning`, message will be sent to the actor.
   - n번 재시도 이후 일 때 `ReliableRedelivery.UnconfirmedWarning' 메시지를 받는다.
   - warn-after-number-of-unconfirmed-attempts = 5
     - 5일 때 6번째 재시도 후에 `ReliableRedelivery.UnconfirmedWarning' 메시지를 받는다.

- Maximum number of unconfirmed messages that will be sent in one re-delivery burst.
   - > redelivery-burst-limit = 10000

- Maximum number of unconfirmed messages that an actor with AtLeastOnceDelivery is allowed to hold in memory.
   - > max-unconfirmed-messages = 100000
