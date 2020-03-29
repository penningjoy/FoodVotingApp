# Food Voting App
A multi-container Realtime Application using .Net Core, SignalR, Redis Cache and Docker.  

The SignalR App has the following features:--

	1. App runs on Dotnet Core inside a Linux docker container.
	2. Handles connection management automatically.
	3. Sends reponses to all connected clients simultaneously. 
	4. Can be scaled to handle increasing traffic.
	5. App data is stored in In-Memory Redis Cache running in a container.

The client code is written in vote.js and the server code is the voterHub.cs.
Hubs call client-side code by sending responses that contain the name and 
parameters of the client-side method. The client tries to match the name
to a method inside the client-side code. When the client finds a match, it calls the
method with the parameter data.

## Usage
```
docker run --name local-redis-server -p 6379:6379 -d redis
docker build -t foodvotingapp -f Dockerfile .
docker run -p 32768:8080 -d foodvotingapp

Go to localhost:32768
```

## Notes
The Redis server and the Signal Core Application run in two separate containers. The IP referred to
in the signalR application is the host computer IP.

```
ipconfig
```
