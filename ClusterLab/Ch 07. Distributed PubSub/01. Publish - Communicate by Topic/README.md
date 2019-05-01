## Topic으로 메시지 보내기/받기
1. Topic을 등록한다.
   - Subscribe
   - **Topic 해제를 위한 Unsubscribe/UnsubscribeAck는 액터가 파괴될 때 자동으로 호출된다.**
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Subscribe("NamedTopic", Self));
   ```
1. Topic 등록을 확인한다.
   - SubscribeAck
   ```
   Receive<SubscribeAck>(_ => Handle(_));
   
   private void Handle(SubscribeAck msg)
   {
      if (msg.Subscribe.Topic.Equals("NamedTopic")
   	     && msg.Subscribe.Ref.Equals(Self)
   	     && msg.Subscribe.Group == null)
      {
   	     _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
      }
   }
   ```
1. Topic으로 메시지를 보낸다.
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Publish(
      "NamedTopic",               // Topic
      "Hello1",                       // 메시지
      sendOneMessageToEachGroup: false));
   ```

<br/>
<br/>

## 데모
1. NonSeedNode1, NonSeedNode2에서 Topic을 등록한다.
2. NonSeedNode3에서 Topic으로 메시지를 보낸다.
- **SendToAll (excludeSelf: true)와 같다.**

   | NonSeedNode1 | NonSeedNode2 |
   |:--:|:--:|
   | NamedTopic | NamedTopic |

![](./Images/Demo.png)
