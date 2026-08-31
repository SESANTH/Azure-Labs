# Lab 28 — Advanced Azure Networking

## Concepts Learned

                         INTERNET
                             │
                             ▼
                       WAF / Front Door
                             │
                             ▼
                          APP
                             │
                      VNet Integration
                             │
                             ▼
                       APP SPOKE
                             │
                          Peering
                             │
                             ▼
                         HUB VNET
                     ┌───────┼────────┐
                     │       │        │
                     ▼       ▼        ▼
                  Firewall  VPN GW   DNS
                     │       │
                     │       ▼
                     │    On-Prem
                     │
                     ▼
                 DATA SPOKE
                     │
                     ▼
               Private Endpoint
                     │
                     ▼
                 Azure SQL
                 
## Screenshots

See the `Images` directory.

## Interview Questions


1. VNet Peering

A private connection between Azure VNets that enables communication using private IP addresses over Microsoft's network.

2. Why no overlapping address spaces?

Routing becomes ambiguous because the same IP range could represent multiple networks.

3. Is peering transitive?

No. Basic VNet peering isn't automatically transitive.

4. Hub-and-spoke?

A network architecture where a central hub VNet provides shared services/connectivity to multiple spoke VNets.

5. Route table?

A collection of routes that determines where network traffic should be sent.

6. UDR?

A User Defined Route is a custom route created by an administrator to influence traffic flow.

7. Longest-prefix match?

When multiple routes match a destination, the most specific route is preferred.

8. Next hop?

The next destination/network component to which a packet should be forwarded.

9. NSG vs Firewall?

NSGs provide distributed network traffic filtering at supported subnet/NIC boundaries. Azure Firewall is a centralized managed firewall service providing broader network traffic inspection and control capabilities.

10. Centralized firewall?

It provides a central point for enforcing network security policies across multiple networks/workloads.

11. Defense in depth?

Using multiple complementary security controls rather than relying on a single security mechanism.

12. VPN Gateway?

An Azure managed gateway that provides VPN connectivity between Azure and external networks or clients.

13. S2S vs P2S?
S2S:
Network ↔ Network

P2S:
Individual device ↔ Azure VNet
14. VPN vs ExpressRoute?

VPN uses encrypted connectivity over the internet.

ExpressRoute provides private connectivity to Azure through a supported connectivity provider.

15. Hybrid connectivity?

Connecting on-premises infrastructure and Azure so they can communicate as part of a combined environment.

16. Private Endpoint vs Service Endpoint?

Private Endpoint provides a private IP in your VNet for private connectivity to a supported service.

Service Endpoint extends VNet identity to a supported Azure service while the service continues to use its public endpoint.

17. Private DNS?

It allows applications to resolve service hostnames to the appropriate private addresses for private connectivity.

18. VNet Integration?

It allows supported services such as App Service to make outbound connections into resources accessible through a VNet.

19. Connection timeout?

Investigate:

DNS
 ↓
Routing
 ↓
NSG
 ↓
Firewall
 ↓
Private Endpoint/VPN/Peering
 ↓
Destination
20. Where will packet go?

Use route analysis / effective routes / next-hop analysis.

21. Effective routes?

They show the actual routing information applicable to a network interface after considering system routes, custom routes, peering, gateways, and other applicable connectivity.

22. Effective security rules?

They help determine the actual NSG rules affecting network traffic at the network interface/subnet level.