## Registrator

- This automatically registers and deregisters the container with a `service registry` like `consul`.  
  very very useful(and almost necessary for deploying on ecs). Read documentation [here](https://gliderlabs.github.io/registrator/latest/)

- This is a very useful because our service don't even have to register the service itself. Hence making the service totally
  unaware of consul.

## Consul

- ### Ingress Gateway

  - This is kind of apiGateway which allows external services into the service mesh.
  - [Tutorial](https://learn.hashicorp.com/tutorials/consul/service-mesh-ingress-gateways?in=consul/developer-mesh)

- ### Configuration Entries

  - Configuration entries can be created to provide cluster-wide defaults for various aspects of Consul.

## Envoy-Control

- This is really cool. It is a control plane for envoy proxy which uses consul for service discovery
- Totally Platform agnostic and an alternative for `consul connect`.
