service {
    name = "hello-service"
    id = "hello-service-1"
    port = 7000


    connect {
    sidecar_service {}
  }
}