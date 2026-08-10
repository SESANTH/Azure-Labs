Why High Availability?

Imagine you're running an e-commerce website.

Customer
↓
Website
↓
Azure VM

Now imagine the physical server hosting the VM crashes.

Business stops.

Azure provides mechanisms to reduce this risk.

What Causes Downtime?

Some common causes:

Hardware failure
Power failure
Network failure
Planned maintenance
Operating system updates

Azure is designed to minimize these outages.


Availability Set
An Availability Set protects against failures within the same datacenter.

Azure spreads your VMs across:

Fault Domains
Update Domains

Example:
Availability Set

├── VM1
└── VM2

If one physical server fails, the other VM continues running.

Fault Domains

A Fault Domain represents a group of hardware that shares a common power source and network switch.

Example:

Fault Domain 1
↓
Server A
Server B

Fault Domain 2
↓
Server C
Server D

If Fault Domain 1 loses power:
VM1 ❌
VM2 ✅

The application can still continue.

Update Domains

Azure periodically performs maintenance.
Instead of restarting every VM together:

Update Domain 0
↓
Restart
Then later:
Update Domain 1
↓
Restart

Only part of the application is affected at a time.


Availability Zone

Availability Zones protect against entire datacenter failures.

Example:
India South
Zone 1
Zone 2
Zone 3

Each zone has:

Independent power
Independent cooling
Independent networking

If Zone 1 fails:

Zone 1 ❌
Zone 2 ✅
Zone 3 ✅

Applications remain available.

| Availability Set               | Availability Zone                                       |
| ------------------------------ | ------------------------------------------------------- |
| Protects within one datacenter | Protects across multiple datacenters in the same region |
| Uses Fault & Update Domains    | Uses physically separate zones                          |
| Best for legacy deployments    | Recommended for new deployments                         |

Availability Set
Datacenter
↓
Availability Set
├── VM1
└── VM2

Region
├── Zone 1 → VM1
├── Zone 2 → VM2
└── Zone 3 → VM3

SLA (Service Level Agreement)
Microsoft provides different SLAs depending on deployment.

| Deployment                                | Typical SLA* |
| ----------------------------------------- | ------------ |
| Single VM (Premium SSD)                   | 99.9%        |
| Two or more VMs in Availability Set       | 99.95%       |
| Two or more VMs across Availability Zones | 99.99%       |

*Always verify current SLA values in Microsoft's official documentation, as they can change over time.

Higher availability means less downtime.

Azure Recommendation

For new production workloads:
✅ Availability Zones

For older or compatibility-based deployments:
✅ Availability Sets


Architecture Comparison

Single VM

Internet
↓
VM
Single point of failure.

Availability Set

Internet
↓
Load Balancer
├── VM1
└── VM2

Protected from hardware and maintenance failures within a datacenter.

Availability Zone
Internet
↓
Load Balancer
├── Zone 1 → VM1
├── Zone 2 → VM2
└── Zone 3 → VM3

Protected even if an entire datacenter becomes unavailable.


Best Practices
✅ Use Availability Zones for new production deployments whenever the region supports them.
✅ Deploy at least two VMs behind a Load Balancer for high availability.
✅ Use Availability Sets when Availability Zones are unavailable or when required by legacy architectures.
✅ Monitor VM health with Azure Monitor and Azure Advisor.
✅ Consider backups and disaster recovery in addition to high availability.