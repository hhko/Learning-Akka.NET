# MORE FUN with Akka.NET Labs 

## 04. Cluster 
1. **Overview**
   - Create a new cluster(Joining itself)
     ```cs
	 akka {
		remote {
			dot-netty.tcp {
				hostname = "127.0.0.1"   	// Seed Node - IP
				port = 8081			// Seed Node - Port 
			}
		}
		cluster {
			seed-nodes = [ "akka.tcp://Cluster-Lab@127.0.0.1:8081" ]
		}
	 }
     ```
   - Shut down a new cluster(Exiting itself gracefully)
     ```cs
     var cluster = Akka.Cluster.Cluster.Get(system);
     cluster.RegisterOnMemberRemoved(() => system.Terminate());
     cluster.Leave(cluster.SelfAddress);
	 
	 // Waits for the Terminate to complete execution within a specified time interval.
     system.WhenTerminated.Wait();
     ```
     - https://stackoverflow.com/questions/38309461/akka-net-cluster-node-graceful-shutdown/38325349
     - https://github.com/ZoolWay/akka-net-cluster-graceful-shutdown-samples
   - Join multiple seed nodes
     ```cs
	 akka {
		cluster {
			seed-nodes = [
				"akka.tcp://Cluster-Lab@127.0.0.1:8081",	// 
				"akka.tcp://Cluster-Lab@127.0.0.1:8082"		// My IP and Port
			]
		}
	 }
	 ```
   - ***Join the nodes to the cluster***
      - Join / 동일한 것은 한개만
      - Exit
      - Rejoin
   - ***Integrate with Petabridge.Cmd***
      - Leader?
      - Join
      - Exit
      - Petabridge.Cmd Exit/Join/...
   - ***Retry Joining Time Interval : **akka.cluster.seed-node-timeout*****
   - ***Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after*****
## Cluster Lab-old
1. **Overview**
   - Create Cluster : **akka.cluster.seed-nodes**
   - Integrate with Petabridge.Cmd  
   - Automatically Mark Unreachable Nodes : **akka.cluster.auto-down-unreachable-after**
   - Retry Joining Time Interval : **akka.cluster.seed-node-timeout**
   - > Seed Node N개 일 때
   - > Petabridge.Cmd Join, Leave, ...
1. **Roles and Minimum Size**
   - Define Roles : **akka.cluster.roles**
   - Cluster-Wide Minimum Size : **akka.cluster.min-nr-of-members**
   - Per-Role Minimum Size : **akka.cluster.role.<role-name>.min-nr-of-members**
   - Mix Minimum Size  
1. **Gossip Events**
   - Subscribe to Gossip Events : **Cluster _cluster = Cluster.Get(Context.System); _cluster.Subscribe(Self, ...);**
   - Discover Actor by Tag
   - Discover Actor by Role and Path : **cluster.State.Members.Where(member => ...);**
1. **Warm-up for Cluster Routing**
   - Pool - Create Routees Automatically : **round-robin-pool**
   - Pool - Hanlde Exceptions from Routees
   - Group - Create routees yourself : **round-robin-group**, **path**
   - Group - Hanlde Exceptions from Routees
   - Deploy - Create Actor Remotely : **akka.actor.deployment.<actor-path>.remote**
   - Deploy - Hanlde Exceptions from Deployed Actors
   - > Remote Deploy - 자동으로 Deathwatch 되는지? 
1. **Cluster Routing**
   - Pool - Deploy Routees Remotely
   - Pool - Hanlde Exceptions from Routees
   - > Pool - Handle Routees Lifecycle
   - Group - Create Routees Yourself
   - Group - Handle Exceptions from Routees
   - > Group - Handle Routees Lifecycle
1. **Cluster Singleton**
   - Create Singleton
   - > Send Message To Singleton
   - > Manage Singleton???
   - > Buffer Size
1. **Distributed PubSub**
   - Publish - Communicate by Topic : **DistributedPubSub.Get**, **Subscribe/SubscribeAck**, **Publish**
   - Publish - Communicate by Topic with Same GroupId : **sendOneMessageToEachGroup: true**, (Send)
   - Publish - Communicate by Topic with Different GroupId : **sendOneMessageToEachGroup: true**, (Publish)
   - Publish - Get Topics
   - Send - Communicate by Path : **Put**, **Remove**, **Send**
   - Send - Communicate by Path with localAffinity : **localAffinity: true**
   - SendToAll - Communicate by Path 
   - SendToAll - Communicate by Path with ExcludeSelf : **excludeSelf: true**
   - > 사용자 정의 메시지(모든 Node에 참고해야 하나?, 메시지를 받지 않는 Node도?)
1. **Cluster Client**
   - Communicate with Cluster by Path 
   - Communicate with Cluster by Topic
   - Subscribe to SubscribeContactPoints Events
   - Subscribe to SubscribeClusterClients Events
   - Send Custom Messages
   - Sample - PingPong
   - > akka.cluster.client.receptionist, akka.cluster.client HOCON
1. **Sharding**
1. **Distributed Data**
1. **Split Brain Resolver**

<br/>
<br/>
