# Food Voting App
Realtime Application using .Net Core, SignalR, Redis Cache and Docker.  

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
