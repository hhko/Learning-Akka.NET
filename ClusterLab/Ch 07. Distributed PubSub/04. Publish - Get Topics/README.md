## 클러스터의 모든 Topic 얻기
1. Topic 요청 메시지를 보낸다.
   - GetTopics.Instance
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(GetTopics.Instance);
   ```

1. Topic 요청 응답 메시지를 처리한다.
   - CurrentTopics
   ```
   Receive<CurrentTopics>(_ => Handle(_));
   private void Handle(CurrentTopics msg)
   {
   	   _log.Info($">>> Recevied message : {msg}, Sender: {Sender}");
       
   	   foreach (string currentTopic in msg.Topics)
   	   {
   	   	   _log.Info($">>> Current Topic : {currentTopic}");
   	   }
   }
   ```

<br/>
<br/>

## 데모
1. NonSeedNode3에서 NonSeedNode1과 NonSeedNode2에서 등록한 Topic 정보를 얻는다.

   | NonSeedNode1 | NonSeedNode2 |
   |:--:|:--:|
   | NamedTopic1 | NamedTopic2 |

![](./Images/Demo.png)
