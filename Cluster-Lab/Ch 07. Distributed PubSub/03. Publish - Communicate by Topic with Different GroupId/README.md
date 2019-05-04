## Topic과 Group으로 메시지 보내기/받기
1. Topic을 등록한다.
   - public Subscribe(string topic, IActorRef @ref, `string group = null`)
   - **Topic 해제를 위한 Unsubscribe/UnsubscribeAck는 액터가 파괴될 때 자동으로 호출된다.**
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Subscribe("NamedTopic", Self, "DifferentGroupId"));
   ```
1. Topic 등록을 확인한다.
   - SubscribeAck
   - msg.Subscribe.Group.Equals("DifferentGroupId")
   ```
   Receive<SubscribeAck>(_ => Handle(_));
   
   private void Handle(SubscribeAck msg)
   {
      if (msg.Subscribe.Topic.Equals("NamedTopic")
   	     && msg.Subscribe.Ref.Equals(Self)
   	     && msg.Subscribe.Group.Equals("DifferentGroupId"))
      {
   	     _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
      }
   }
   ```
1. Topic으로 메시지를 보낸다.
   - Group 이름이 다를 때는 모든 액터에게 메시지가 발송된다(Publish 기본 기능과 같다).
   - If all the subscribed actors have different group names, then this works like normal Publish and each message is broadcasted to all subscribers.
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Publish(
      "NamedTopic",               // Topic
      "Hello1",                       // 메시지
      sendOneMessageToEachGroup: true));   // 그룹 사용
   ```

<br/>
<br/>

## 데모
1. NonSeedNode1, NonSeedNode2에서 같은 Group과 Topic을 등록한다.
2. NonSeedNode3에서 `sendOneMessageToEachGroup: true`로 Topic으로 메시지를 보낸다.
- **SendToAll (excludeSelf: true)와 같다.**

   | NonSeedNode1 | NonSeedNode2 |
   |:--:|:--:|
   | NamedTopic | NamedTopic |
   | DifferentGroupId1 | DifferentGroupId2 |

![](./Images/Demo.png)
