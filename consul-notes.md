# Consul

## Connect envoy

- `consul connect envoy`: cli command for configuring envoy as proxy. [Documentation here](https://www.consul.io/commands/connect/envoy).

- By Default, to route the traffic through `ingress-gateway` to correct service, the request must set header, `Host` as `<service>.ingress.<datacenter>.<domain>`.  
  eg: `hello-service-rest.ingress.my-dc-1.consul`. For `path based routing`, [See here](https://www.consul.io/docs/connect/config-entries/ingress-gateway#http-listener-with-path-based-routing).

- Options:

  - -bootstrap: If present, the command will simply output the generated bootstrap config to stdout in JSON protobuf form. This can be directed to a file and used to start Envoy with envoy -c bootstrap.json.

  - -sidecar-for: The ID (not name if they differ) of the service instance this proxy will represent. The target service doesn't need to exist on the local agent yet but a sidecar proxy registration with proxy.destination_service_id equal to the passed value must be present. If multiple proxy registrations targeting the same local service instance are present the command will error and -proxy-id should be used instead.

  - -gateway: Flag to indicate that Envoy should be configured as a Gateway. Must be one of: `terminating`, `ingress`, or `mesh`. If multiple gateways are managed by the same local agent then -proxy-id should be used as well to specify the instance this represents.

  - -admin-bind: The host:port to bind Envoy's admin HTTP API. Default is localhost:19000. Envoy requires that this be enabled. The host part must be resolvable DNS name or IP address.

  - -register: Indicates that the gateway service should be registered with the local agent instead of expecting it to already exist. This flag is unused for traditional sidecar proxies.

  - -address: The address to advertise for services within the local datacenter to use to reach the gateway instance. This flag is used in combination with -register. This takes the form of <ip address>:<port> but also supports go-sockaddr templates.

  - -service: The name of the gateway service to register. This flag is used in combination with -register.

  - -wan-address: The address to advertise for services within remote datacenters to use to reach the gateway instance. This flag is used in combination with -register. This takes the form of <ip address>:<port> but also supports go-sockaddr templates.

## Steps to create an apiGateway

- By apiGateway, I mean a gateway which external services could connect to(envoy proxy) and get routed to appropriate service.

- Steps
  1. Have the `connect` enabled in consul server.
  2. Make sure the `grpc port` is open. This is used for configuring envoy proxy through `xds` apis.
  3. Launch the consul server.
  4. Launch the service.
  5. Launch the sidecar for the service.
  6. create a gateway of type `ingress-gateway` and launch the proxy using `consul connect envoy`.

## consul config

- `consul config`: Used to integract with consul's central configuration. [Read Here](https://www.consul.io/commands/config)
