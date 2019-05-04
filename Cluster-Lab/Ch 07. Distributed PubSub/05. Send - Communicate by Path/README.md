## 액터 경로로 메시지 보내기
1. 액터를 등록한다.
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Put(Self)); 
   ```

1. 액터 경로를 이용하여 메시지를 보낸다.
   ```
   var mediator = DistributedPubSub.Get(Context.System).Mediator;
   mediator.Tell(new Send("/user/SubscriberActor", 
	"Hello1",
	localAffinity: false));
   ```

<br/>
<br/>

## 데모
1. 액터 경로(`/user/SubscriberActor`)가 같은 모든 액터에게 메시지를 랜덤 순서로 보낸다.

![](./Images/Demo.png)
