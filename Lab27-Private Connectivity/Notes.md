What is a Private Endpoint?

A Private Endpoint is a network interface in your VNet that is assigned a private IP address and connects privately to an Azure service through Azure Private Link.

Conceptually:

VNet
 │
 └── Subnet
       │
       └── Private Endpoint
              │
              │ Private Link
              ▼
          Azure SQL

The key word:

Private IP

Private Endpoint is not the database

The Private Endpoint is the network interface/access point.

Think:

Azure SQL
     ▲
     │
Private Link
     │
Private Endpoint
     │
     ▼
Your VNet

What is Azure Private Link?

Private Link is the Azure technology that enables private connectivity from your VNet to supported Azure services.

xamples of supported services include various Azure PaaS services such as:

Azure SQL
Storage
Key Vault
Azure Database services
Azure App Services

Private Endpoint vs Private Link

Don't confuse them.

Private Link

The Azure technology/service architecture that provides private connectivity.

Private Endpoint

The network interface you create inside your VNet to consume that private connectivity.

Think:

Private Link
=
Technology


Private Endpoint
=
Your private network interface

What does the architecture actually look like?

                    Azure VNet
                        │
                        ▼
                 Private Subnet
                        │
                        ▼
                Private Endpoint
                  Private IP
                        │
                        │
                   Private Link
                        │
                        ▼
                   Azure SQL



                    App Service
                         │
                         │ VNet Integration
                         ▼
                       VNet
                         │
                         ▼
                 Private Endpoint
                         │
                         ▼
                    Azure SQL


Important: App Service is not placed inside your VNet

With App Service VNet Integration:

App Service
     │
     │ VNet Integration
     ▼
    VNet

The App Service itself doesn't simply become a VM sitting inside your subnet.

Instead, VNet Integration provides outbound connectivity from the App Service to resources accessible through the VNet.

This distinction matters.

VNet Integration


Suppose:

App Service

needs to reach:

Azure SQL Private Endpoint

We can use:

App Service
      │
      ▼
VNet Integration
      │
      ▼
VNet
      │
      ▼
Private Endpoint
      │
      ▼
Azure SQL

VNet Integration is the App Service side of the connection.

Private Endpoint is the Azure SQL side

Think of the two sides:

App Service
    │
    ▼
VNet Integration
    │
    ▼
VNet
    │
    ▼
Private Endpoint
    │
    ▼
Azure SQL

What about DNS?

Suppose your application connects to:

myserver.database.windows.net

The application doesn't normally say:

Connect to 10.10.2.5

It uses a hostname.

So:

Application
     ↓
myserver.database.windows.net
     ↓
DNS
     ↓
IP address



Public DNS scenario

Without private connectivity:

myserver.database.windows.net
             ↓
          DNS
             ↓
      Public IP
             ↓
        Azure SQL

That's public connectivity.

Private DNS scenario

With Private Endpoint:

myserver.database.windows.net
             ↓
          DNS
             ↓
      Private IP
             ↓
    Private Endpoint
             ↓
        Azure SQL

This is why DNS is critical.


Private DNS Zone

Azure provides private DNS zones for private connectivity scenarios.

For Azure SQL, a commonly used private DNS zone is:

privatelink.database.windows.net

The general pattern becomes:

database.windows.net
        ↓
Private Link DNS
        ↓
privatelink.database.windows.net
        ↓
Private IP

DNS flow

Let's say:

Application

asks:

What IP address belongs to myserver.database.windows.net?

With private connectivity:

Application
      ↓
DNS query
      ↓
Private DNS resolution
      ↓
Private IP
      ↓
Private Endpoint
      ↓
Azure SQL

That's the complete concept.



Why DNS can break private connectivity

Imagine everything is correctly configured:

VNet          ✅
Private EP    ✅
Azure SQL     ✅
Routing       ✅

But DNS returns the public endpoint.

Then:

Application
     ↓
DNS
     ↓
Public IP
     ↓
❌

The application may fail to reach the intended private path.

This is why:

Private networking is not only about IP addresses. DNS is part of the architecture.

Private DNS zone architecture

                 Azure VNet
                     │
                     │ linked
                     ▼
              Private DNS Zone
                     │
                     ▼
        privatelink.database.windows.net
                     │
                     ▼
              Private Endpoint
                     │
                     ▼
                 Azure SQL


Public endpoint vs Private Endpoint

| Public Endpoint                    | Private Endpoint                               |
| ---------------------------------- | ---------------------------------------------- |
| Public IP path                     | Private IP in VNet                             |
| Internet-facing endpoint           | Private network access                         |
| Firewall commonly used             | VNet/private connectivity controls             |
| Easier setup                       | More architecture                              |
| Suitable for some public workloads | Strong choice for private enterprise workloads |


Service Endpoint

A service endpoint extends your VNet identity to supported Azure services over Azure's backbone.

Private Endpoint

Private Endpoint gives the service a private IP interface in your VNet.

Service Endpoint vs Private Endpoint

| Service Endpoint                      | Private Endpoint                |
| ------------------------------------- | ------------------------------- |
| VNet identity extended to service     | Private IP in VNet              |
| Service public endpoint remains       | Private access path             |
| Doesn't create private IP for service | Creates private IP              |
| Simpler                               | More private architecture       |
| Service-specific support              | Private Link-supported services |


Private Endpoint subnet

When creating a Private Endpoint, you select:

VNet
 ↓
Subnet

The Private Endpoint receives a private IP from that subnet.

Example:

VNet: 10.10.0.0/16

Subnets:

AppSubnet
10.10.1.0/24

PrivateEndpointSubnet
10.10.2.0/24

Then:

Private Endpoint
10.10.2.4

might represent the private interface.

Don't overlap networks

This becomes increasingly important when VNets connect through:

Peering
VPN
ExpressRoute
Hub-and-spoke

For example:

VNet A
10.10.0.0/16

VNet B
10.10.0.0/16

This creates address overlap.

Later connectivity becomes problematic.

So:

Plan address spaces before building production VNets.

Hub-and-spoke architecture

Private connectivity becomes even more powerful in a hub-and-spoke architecture.

                    HUB VNET
                       │
          ┌────────────┼────────────┐
          │            │            │
          ▼            ▼            ▼
      Spoke A       Spoke B       Spoke C
      App           Data          Dev


Private DNS in hub-and-spoke

DNS becomes a shared service.

Conceptually:

                  Hub VNet
                     │
              Private DNS
                     │
        ┌────────────┼────────────┐
        ▼            ▼            ▼
     Spoke A      Spoke B      Spoke C

This avoids duplicating DNS configuration everywhere.

