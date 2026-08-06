What is a Virtual Network (VNet)?
A Virtual Network is your private network inside Azure.

Azure Virtual Network
↓
VM
Database
Storage
Application

A VNet securely connects Azure resources.

Real-World Example

Office Building

↓
HR
Finance
IT
Management

In Azure:

Virtual Network
↓
Subnet-HR
Subnet-Finance
Subnet-App
Subnet-DB

Azure Networking Hierarchy

Subscription
↓
Resource Group
↓
Virtual Network
↓
Subnets
↓
Resources

What is Address Space?
Every network needs an IP range.

Example:
192.168.1.0/24

Azure usually uses private ranges like:
10.0.0.0/16

This means your VNet owns all IPs from:

10.0.0.1
↓
10.0.255.254

Think of Address Space as the land you own.


What is a Subnet?

A subnet divides a large network into smaller logical networks.

VNet

├── Web
├── Application
├── Database

Each subnet can have different security rules.

Example

Suppose your VNet is:
10.0.0.0/16

Create:
Subnet-A
10.0.1.0/24

Create:
Subnet-B
10.0.2.0/24

Azure automatically allocates IP addresses within those ranges.

Subnet-Web
↓
Public Servers
Subnet-App
↓
Business Logic

Subnet-Database
↓
SQL Servers


Private IP Address
Private IPs are used inside the VNet.

VM1
↓
10.0.1.4

Public IP Address
A Public IP allows access from the Internet.

Internet
↓
20.x.x.x
↓
Azure VM

| Private IP                      | Public IP                                           |
| ------------------------------- | --------------------------------------------------- |
| Internal communication          | Internet communication                              |
| Not reachable from the Internet | Reachable from anywhere (subject to security rules) |
| Example: `10.0.1.4`             | Example: `20.x.x.x`                                 |


Azure DNS

Computers prefer IP addresses.
Humans prefer names.

Example:
www.microsoft.com

DNS translates it into an IP address.

www.microsoft.com
↓
20.x.x.x

Azure automatically provides internal DNS for resources inside a VNet.

Communication example

VNet
│
├── Subnet-A
│      VM1
│
└── Subnet-B
       VM2

VM1 can communicate with VM2 over their private IP addresses as long as no Network Security Group (NSG) blocks the traffic.

Best Practice Architecture
Virtual Network

├── Web Subnet
├── App Subnet
├── Database Subnet
└── Management Subnet

This is the architecture used by many enterprise applications.


Interview Questions
Q1. What is a Virtual Network?

An Azure Virtual Network (VNet) is a logically isolated private network in Azure that enables secure communication between Azure resources, the Internet, and on-premises networks.

Q2. Why are Subnets used?

Subnets divide a VNet into smaller logical segments, allowing better organization, improved security, separate IP ranges, and different network policies for different workloads such as web servers, application servers, and databases.

Q3. Private IP vs Public IP

| Private IP            | Public IP                |
| --------------------- | ------------------------ |
| Used within the VNet  | Used for Internet access |
| Not Internet routable | Internet routable        |
| Example: `10.0.1.4`   | Example: `20.x.x.x`      |


Q4. What is Azure DNS?

Azure DNS provides name resolution for Azure resources. By default, resources in the same VNet can resolve each other's names using Azure-provided DNS.

Q5. Can two subnets communicate?

Yes. By default, subnets within the same VNet can communicate with each other unless traffic is restricted using Network Security Groups (NSGs) or other networking controls.

Best Practices
✅ Plan your address space before deployment to avoid overlaps.
✅ Separate application tiers (Web, App, Database) into different subnets.
✅ Use private IPs for internal communication whenever possible.
✅ Expose only resources that require Internet access with Public IPs.
✅ Leave Azure-provided DNS unless you have a specific need for custom DNS servers.