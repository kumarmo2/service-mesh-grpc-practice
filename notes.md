## Registrator

- This automatically registers and deregisters the container with a `service registry` like `consul`.  
  very very useful(and almost necessary for deploying on ecs). Read documentation [here](https://gliderlabs.github.io/registrator/latest/)

- This is a very useful because our service don't even have to register the service itself. Hence making the serviceit totally
  unaware of consul.
