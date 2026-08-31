Advanced Azure Networking

a VNet is an isolated network

You've already learned:

VNet
10.10.0.0/16

Inside:

10.10.1.0/24
10.10.2.0/24
10.10.3.0/24

By default, this is one logical Azure network.

But companies rarely have only one VNet.

They may have:

Development VNet
Production VNet
Security VNet
Data VNet
Shared Services VNet

Now we need connectivity between them.

VNet Peering

VNet Peering connects two Azure VNets so resources can communicate using private IP addresses over Microsoft's network.

Conceptually:

VNet A
10.10.0.0/16
     │
     │ Peering
     │
VNet B
10.20.0.0/16

Now:

VM A
10.10.1.4
   │
   ▼
VNet Peering
   │
   ▼
VM B
10.20.1.4

Why not just use one VNet?

Because different environments often need isolation.

For example:

Production
   ≠
Development

You may want:

Dev VNet
10.10.0.0/16

Prod VNet
10.20.0.0/16

Data VNet
10.30.0.0/16

Then selectively connect them.

Address spaces must not overlap

This is extremely important.

Good:

VNet-A
10.10.0.0/16

VNet-B
10.20.0.0/16

VNet peering is not transitive

VNet A
  │
  │ Peering
  ▼
VNet B
  │
  │ Peering
  ▼
VNet C

A → B → C

means A can automatically reach C.

That's not how basic VNet peering works.

You need appropriate connectivity/routing between the networks.

Remember:

Peering is not automatically transitive.

This is one reason hub-and-spoke architecture is useful.

Hub-and-Spoke

Instead of:

A ↔ B
A ↔ C
B ↔ C

we can create:

             HUB
              │
       ┌──────┼──────┐
       │      │      │
       ▼      ▼      ▼
     Dev     App    Data

The hub becomes the central networking point.

Why Hub-and-Spoke?

Centralize shared network services:

Hub
│
├── Azure Firewall
├── VPN Gateway
├── DNS
├── Bastion
└── Shared services

Spokes:

Spoke-App
Spoke-Dev
Spoke-Data
Spoke-Prod

This creates a more manageable enterprise architecture.

                         Internet
                            │
                            ▼
                     Azure Firewall
                            │
                            ▼
                      ┌───────────┐
                      │ HUB VNET  │
                      │           │
                      │ Firewall  │
                      │ VPN GW    │
                      │ DNS       │
                      └─────┬─────┘
                            │
                ┌───────────┼───────────┐
                │           │           │
                ▼           ▼           ▼
             Dev VNet    App VNet    Data VNet

Routing — the heart of networking

Now we need to understand:

When a packet has multiple possible destinations, how does Azure decide where to send it?

That's routing.

Suppose:

Source:
10.10.1.4

wants:

Destination:
10.30.1.5

The network needs to determine:

Where should this packet go?

Routing table

A route contains information like:

Destination
Next hop

Conceptually:

Destination       Next Hop
10.20.0.0/16      Peering
10.30.0.0/16      Firewall
0.0.0.0/0         Internet

The router evaluates these routes.


Routing table

A route contains information like:

Destination
Next hop

Conceptually:

Destination       Next Hop
10.20.0.0/16      Peering
10.30.0.0/16      Firewall
0.0.0.0/0         Internet

The router evaluates these routes.

Default route

You will frequently see:

0.0.0.0/0

This means:

Any IPv4 destination not covered by a more specific route.

For example:

10.20.0.0/16

is more specific than:

0.0.0.0/0

Longest prefix match

This is an important networking concept.

Suppose routing has:

10.0.0.0/8
10.10.0.0/16
10.10.1.0/24

Destination:

10.10.1.5

Which route wins?

10.10.1.0/24

because it is the most specific matching route.

System routes

Azure automatically creates system routes for VNets.

You don't manually configure every basic VNet route.

Conceptually:

VNet
 ↓
Azure system routes
 ↓
Local VNet communication

There are also routes for relevant Azure infrastructure and connectivity scenarios.

The exact effective routing table depends on the resource, subnet, peerings, gateways, and custom routes.

User Defined Routes — UDR

Sometimes the default Azure routing behavior isn't what you want.

You can create a:

User Defined Route

and associate it with a subnet through a route table.

Architecture:

Subnet
   │
   ▼
Route Table
   │
   ▼
UDR

Why use UDR?

Suppose you want all outbound traffic from your application subnet to go through:

Azure Firewall

Instead of:

App
 ↓
Internet

you want:

App
 ↓
Firewall
 ↓
Internet

You can use routing to force that traffic through the desired network appliance.

Typical UDR

Conceptually:

Destination:
0.0.0.0/0

Next hop:
Virtual appliance

Next hop IP:
10.0.0.4

Azure Firewall

Now we introduce the security appliance.

Azure Firewall is a managed, stateful network security service.

Conceptually:

Internet
   │
   ▼
Azure Firewall
   │
   ▼
Azure VNet

It can inspect/control network traffic according to configured rules and supported capabilities.

Firewall vs NSG

You've already learned NSGs.

This distinction is extremely important.

NSG
Subnet / NIC
 ↓
Traffic filtering
Azure Firewall
Central network security appliance
 ↓
Traffic inspection/control

NSG vs Azure Firewall

| NSG                             | Azure Firewall                                  |
| ------------------------------- | ----------------------------------------------- |
| Subnet/NIC-level filtering      | Centralized managed firewall                    |
| L3/L4-focused filtering         | Broader traffic inspection/control capabilities |
| Distributed                     | Centralized                                     |
| Lightweight                     | More feature-rich                               |
| Commonly attached to subnet/NIC | Deployed as Azure Firewall resource             |
| Great for local segmentation    | Great for centralized network security          |


Defense in depth

Example:

Internet
   ↓
Firewall
   ↓
NSG
   ↓
Application

Azure Firewall architecture

In a hub-and-spoke design:

                    Internet
                       │
                       ▼
                Azure Firewall
                       │
                  HUB VNet
                       │
           ┌───────────┼───────────┐
           ▼           ▼           ▼
         Dev          App         Data

Spoke traffic can be routed through the hub firewall.

Why centralize the firewall?

Imagine 20 VNets.

Without centralization:

VNet A → Firewall A
VNet B → Firewall B
VNet C → Firewall C
...

This can become operationally difficult.

With a hub:

              Firewall
                  │
       ┌──────────┼──────────┐
       ▼          ▼          ▼
     VNet A     VNet B     VNet C

Centralized control becomes easier.

VPN Gateway

On-Premises
     │
     │
     ▼
   Internet
     │
     ▼
Azure VPN Gateway
     │
     ▼
Azure VNet

Site-to-Site VPN

A Site-to-Site VPN connects an on-premises network to an Azure VNet over an encrypted VPN connection.

ON-PREM
10.0.0.0/16
    │
    │ Encrypted VPN
    │
    ▼
Azure VPN Gateway
    │
    ▼
Azure VNet
10.20.0.0/16

Point-to-Site VPN

Site-to-Site is:

Network
 ↔
Network

Point-to-Site is:

Individual device
 ↔
Azure VNet

Site-to-Site vs Point-to-Site

| Site-to-Site                   | Point-to-Site                |
| ------------------------------ | ---------------------------- |
| Network ↔ Network              | Device ↔ Network             |
| On-premises ↔ Azure            | Laptop ↔ Azure               |
| Gateway-to-gateway             | Client VPN                   |
| Common for hybrid connectivity | Common for individual access |


ExpressRoute

Now we reach enterprise connectivity.

VPN:

On-prem
 ↓
Internet
 ↓
Encrypted tunnel
 ↓
Azure

ExpressRoute provides a private connectivity option through a supported connectivity provider.

VPN vs ExpressRoute

| VPN                                 | ExpressRoute                                               |
| ----------------------------------- | ---------------------------------------------------------- |
| Uses encrypted tunnel over internet | Private connectivity via provider                          |
| Generally simpler                   | More enterprise-oriented                                   |
| Usually quicker to establish        | More planning/provisioning                                 |
| Internet-dependent path             | Doesn't use public internet as the primary connection path |
| Lower initial complexity            | Higher infrastructure/provider complexity                  |

Network Watcher

Azure provides Network Watcher capabilities for network monitoring and troubleshooting.

Useful tools/features include:

Connection troubleshoot
IP flow verify
Next hop
Effective security rules
Effective routes
Packet capture

az network watcher

You can inspect Network Watcher resources:

az network watcher list `
  --output table


NSG is stateful

If a connection is allowed appropriately, return traffic is handled as part of the established flow.

This is why you generally don't need to create a reverse rule simply to allow the response traffic of an allowed connection.

Firewall vs NSG vs Route Table

| Component        | Main question                                       |
| ---------------- | --------------------------------------------------- |
| Route table      | Where should traffic go?                            |
| NSG              | Is traffic allowed?                                 |
| Azure Firewall   | Should centralized traffic be inspected/controlled? |
| Private Endpoint | How do I privately reach a supported PaaS service?  |
| VPN Gateway      | How do I connect networks through VPN?              |
| DNS              | What IP does this hostname resolve to?              |


Hub-and-Spoke traffic flow

App Spoke
   │
   ▼
UDR
   │
   ▼
Hub Firewall
   │
   ▼
VPN Gateway
   │
   ▼
On-Prem


Production architecture

                              INTERNET
                                  │
                                  ▼
                         Azure Front Door
                              + WAF
                                  │
                                  ▼
                            App Service
                                  │
                           VNet Integration
                                  │
                                  ▼
                         ┌─────────────────┐
                         │   APP SPOKE     │
                         │                 │
                         │ App Subnet      │
                         │ Private EP      │
                         └────────┬────────┘
                                  │
                              Peering
                                  │
                                  ▼
                         ┌─────────────────┐
                         │    HUB VNET     │
                         │                 │
                         │ Azure Firewall  │
                         │ VPN Gateway     │
                         │ DNS             │
                         └────────┬────────┘
                                  │
                         ┌────────┴─────────┐
                         │                  │
                         ▼                  ▼
                    On-Premises        Other Spokes


