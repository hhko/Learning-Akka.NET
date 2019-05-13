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