{
  "log_level": "INFO",
  "server": true,
  "ui": true,
  "leave_on_terminate": true,
  "data_dir": "/home/kumarmo2/csharp/consul-grpc-practice/consul_data",
  "datacenter": "my-dc-1",
  "client_addr": "0.0.0.0", 
  "bind_addr": "0.0.0.0",
  "ports": {
    "grpc": 8502
  },
  "connect": {
    "enabled": true
  }
  "rpc": {
    "enable_streaming": true
  }
  "bootstrap_expect": 1,
   "performance": {
    "raft_multiplier": 1
  }
}
