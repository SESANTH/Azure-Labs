Azure Network Security Groups (NSG)

Think of it like this:

VNet = Apartment Building 🏢
Subnet = Individual Floors
NSG (Network Security Group) = Security Guards at each entrance

Without an NSG, every room on a floor could potentially communicate freely. An NSG lets you decide who can enter and what traffic is allowed.

What is an NSG?

A Network Security Group (NSG) is Azure's virtual firewall for controlling network traffic.

It filters traffic by:

Source
Destination
Port
Protocol
Action (Allow or Deny)

Think of it as a security checkpoint.

Internet
↓
NSG
↓
Virtual Machine

The NSG decides whether traffic is allowed through.

Why Do We Need NSGs?
Suppose you create a Virtual Machine.

Without an NSG:
Internet
↓
VM

With an NSG:
Internet
↓
NSG
↓
VM

Only approved traffic reaches the VM.

Where Can an NSG Be Applied?

Virtual Network
↓
Subnet
or
↓
Network Interface (NIC)

Most organizations attach NSGs to subnets, but they can also be applied directly to a VM's network interface for more granular control.

How NSGs Work

Every packet is evaluated against the NSG rules.

Internet              
↓
Port 80
↓
Allow
↓
VM

Another: 

Internet
↓
Port 22
↓
Deny
↓
Blocked


Inbound vs Outbound Rules

Inbound Rules
Traffic coming into Azure resources.

Example:
Internet
↓
Azure VM

Common inbound traffic:
SSH
RDP
HTTP
HTTPS

Outbound Rules

Traffic leaving Azure resources.

Example:
Azure VM
↓
Internet

Example use cases:

Download updates
Connect to APIs
Access Azure Storage

Rule Components

Each NSG rule has:

Priority
Source
Source Port
Destination
Destination Port
Protocol
Action


| Property         | Value    |
| ---------------- | -------- |
| Priority         | 100      |
| Source           | Internet |
| Protocol         | TCP      |
| Destination Port | 80       |
| Action           | Allow    |


Rule Priority

Azure processes rules from the lowest priority number to the highest.

Priority 100
↓
Allow HTTP

Priority 200
↓
Allow HTTPS

Priority 300
↓
Deny All

Once a rule matches, Azure stops checking further rules.


Common Ports

SSH
Linux Remote Login
Port:
22

RDP
Windows Remote Desktop
Port:
3389

HTTP
Website
Port:
80

HTTPS
Secure Website
Port:
443

Default NSG Rules

Azure automatically creates default rules.

| Priority | Rule                              |
| -------- | --------------------------------- |
| 65000    | Allow VNet traffic                |
| 65001    | Allow Azure Load Balancer         |
| 65500    | Deny all inbound Internet traffic |

Custom rules with lower priority numbers (like 100 or 200) override these defaults.

Example Architecture

Internet
↓
NSG
↓
Web Subnet
↓
Web Server

Rules:
Allow 80
Allow 443
Block Everything Else

Interview Questions
Q1. What is an NSG?

A Network Security Group (NSG) is an Azure networking resource that filters inbound and outbound traffic to Azure resources using security rules based on IP addresses, ports, protocols, and actions.

Q2. Why are NSGs used?

NSGs protect Azure resources by allowing only authorized network traffic and blocking unwanted access.

Q3. NSG vs Azure Firewall

NSG	Azure Firewall
Basic Layer 3/4 filtering	Fully managed stateful firewall
Applied to subnet or NIC	Centralized protection for multiple VNets
IP, port, protocol rules	Network rules, application rules, DNAT, threat intelligence
No application-level filtering	Supports FQDN and application filtering
Free resource (standard charges don't apply)	Paid managed service

Q4. What are Inbound and Outbound Rules?
Inbound rules control traffic entering Azure resources.
Outbound rules control traffic leaving Azure resources.

Q5. What happens if two rules match?

Azure evaluates rules in ascending priority order. The first matching rule is applied, and no further rules are evaluated.

Best Practices

✅ Open only the ports you actually need.
✅ Avoid allowing SSH (22) or RDP (3389) from Any in production; restrict to trusted IP ranges.
✅ Use subnet-level NSGs for consistent security across related resources.
✅ Assign meaningful rule names such as Allow-HTTP and Allow-HTTPS.
✅ Periodically review and remove unused rules.