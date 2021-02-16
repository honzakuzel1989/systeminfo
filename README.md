# systeminfo
Small tool for get some system information

# Api

```
GET <url>/system/info/usage?fs=<fs> - returns processor, memory and disk usage (by fs parameter) in % calculated by heuristics
GET <url>/system/info/network?iface=<iface> - returns network info, like ip address and interface name
GET <url>/system/info/updates - returns processor, memory and disk usage in % calculated by heuristics and network info
GET <url>/system/info - returns all mentioned in one request
```
# Configuration

```
SYSTEM_INFO_FILESYSTEM - filesystem name for disk usage, default /dev/sda1 for `info` endpoint
SYSTEM_INFO_INTERFACE - web interface name for IP vizualization, default enp0s25 for `info` endpoint
```
