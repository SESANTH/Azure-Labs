So far, you have learned to deploy and secure one VM. But production applications rarely run on a single server.

Today's question is:

"If one VM becomes overloaded or fails, how do users continue accessing the application?"

The answer is Azure Load Balancer.

Azure Load Balancer

Why Do We Need a Load Balancer?
Imagine 10,000 users visit your website.

Without a Load Balancer:
Internet
↓
VM1

Problems:
VM becomes overloaded
Slow response
Single point of failure

With a Load Balancer:
Internet
↓
Azure Load Balancer
↓
VM1
VM2
VM3

Traffic is distributed automatically.

Azure Load Balancer Architecture

Internet
↓
Frontend IP
↓
Load Balancer
↓
Backend Pool
↓
VM1
VM2
VM3

Types of Azure Load Balancer

Public Load Balancer
Receives traffic from:

Internet
↓
Load Balancer

Used for:
Websites
APIs
Public applications


Internal Load Balancer
Receives traffic only from inside Azure.

Application Servers
↓
Internal Load Balancer
↓
Database Servers

Used for:

Internal applications
Database clusters
Private services

Components of Load Balancer

1. Frontend IP

The IP users connect to.
Example:
20.x.x.x

This IP never changes (if static).

2. Backend Pool

Contains the servers.
Example:
Backend Pool
↓
VM1
VM2
VM3


3. Health Probe

Azure continuously checks:
"Is this VM healthy?"

Example:
Health Probe
↓
VM1 ✅
VM2 ❌
VM3 ✅

VM2 stops receiving traffic.

4. Load Balancing Rule

Defines:
Receive traffic on:
Port 80

Forward to:
Backend Pool
↓
Port 80

Traffic Flow

User
↓
Public IP
↓
Frontend
↓
Load Balancer
↓
Backend Pool
↓
Healthy VM


Health Probe Example

Azure checks every few seconds.
GET /
↓
VM Responds
↓
Healthy

If not:
VM
↓
No Response
↓
Marked Unhealthy
↓
Traffic Stops


Load Balancing Algorithms

Azure supports different distribution modes.
Common one:

5-Tuple Hash
Uses:
Source IP
Source Port
Destination IP
Destination Port
Protocol

This helps keep connections consistent.

For AZ-104, you mainly need to know that Azure distributes traffic automatically.

Azure Load Balancer vs Application Gateway

| Load Balancer           | Application Gateway       |
| ----------------------- | ------------------------- |
| Layer 4 (TCP/UDP)       | Layer 7 (HTTP/HTTPS)      |
| Port-based routing      | URL-based routing         |
| Very fast               | Advanced web routing      |
| No SSL termination      | SSL termination supported |
| General network traffic | Web applications          |


Best Practices
✅ Use Standard SKU for production workloads.
✅ Always configure Health Probes.
✅ Deploy backend VMs across Availability Zones when possible.
✅ Restrict management access (SSH/RDP) with NSGs.
✅ Use Application Gateway when advanced HTTP routing or WAF is required.

