# Lab 26 — Azure SQL

## Concepts Learned

Public vs Private Connectivity
VNet
Subnets
Private Endpoint
Private Link
VNet Integration
Private DNS
DNS Resolution
Service Endpoint vs Private Endpoint
Azure SQL Private Connectivity
NSG
Routing

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is a Private Endpoint?

A Private Endpoint is a network interface with a private IP address in a VNet that provides private connectivity to a supported Azure service through Private Link.

Q2. What is Private Link?

Azure Private Link is the technology that enables private connectivity from VNets to supported Azure services.

Q3. Difference?
Private Link
=
Technology

Private Endpoint
=
Private network interface using that technology
Q4. VNet Integration?

VNet Integration allows supported services such as App Service to make outbound connections into resources accessible through an Azure VNet.

Q5. Private Endpoint vs Service Endpoint?

Service Endpoint extends VNet identity to supported Azure services while the service uses its public endpoint.

Private Endpoint provides a private IP in your VNet and private connectivity through Private Link.

Q6. Why DNS?

Applications use hostnames rather than directly using IP addresses.

Private DNS ensures the hostname resolves to the intended private IP/path.

Q7. Private DNS Zone?

A DNS zone used within private networking to resolve private service names to private IP addresses.

Q8. Why VNet link?

It allows resources in the VNet to use the private DNS zone for name resolution.

Q9. App Service → Azure SQL?
App Service
 ↓
VNet Integration
 ↓
VNet
 ↓
Private Endpoint
 ↓
Private Link
 ↓
Azure SQL

with appropriate Private DNS configuration.

Q10. Does VNet Integration place App Service inside VNet?

No. It provides outbound connectivity from the App Service to resources accessible through the VNet.

Q11. SQL timeout?

Investigate:

DNS
 ↓
IP
 ↓
Routing
 ↓
NSG/network controls
 ↓
Private Endpoint
 ↓
SQL

Then investigate authentication if the network connection succeeds.

Q12. DNS returns public IP?

Check:

Private DNS Zone
 ↓
VNet link
 ↓
DNS zone group
 ↓
DNS configuration
Q13. Why Private Endpoint?

To provide a private network path and reduce reliance on public endpoints for supported Azure services.

Q14. NSG?

NSGs filter network traffic at supported subnet/NIC boundaries. Their role depends on the architecture and traffic path; don't assume every PaaS connection behaves exactly like VM-to-VM traffic.

Q15. Bicep?

Define:

VNet
 ↓
Subnet
 ↓
Private Endpoint
 ↓
Private DNS
 ↓
VNet Link

and connect the Private Endpoint to the target Azure service.