service {
    name = "hello-service-rest"
    id = "hello-service-rest-1"
    port = 5000


    connect {
        sidecar_service{}
    }
}