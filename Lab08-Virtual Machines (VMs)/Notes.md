Virtual Machines (VMs)

What is a Virtual Machine?
A Virtual Machine is a computer running inside Azure.

Microsoft manages the physical hardware.
You only manage the operating system.

Azure VM Architecture

Resource Group
↓
Virtual Machine

├── Operating System
├── Managed Disk
├── Network Interface
├── Public IP
└── Virtual Network

A VM isn't just one resource—it depends on several Azure resources.

VM Images
A VM starts from an Image.

Examples:
Windows
Windows Server 2022

Linux
Ubuntu Server 24.04 LTS

Others:
Debian
Red Hat Enterprise Linux
SUSE
Oracle Linux

For learning, we'll use Ubuntu because it's lightweight and cheaper.

VM Sizes
Azure offers many VM sizes.

Examples:
| Size   | vCPU | RAM   | Best For        |
| ------ | ---- | ----- | --------------- |
| B1s    | 1    | 1 GB  | Learning        |
| B2s    | 2    | 4 GB  | Development     |
| D2s_v5 | 2    | 8 GB  | Production Apps |
| E4s_v5 | 4    | 32 GB | Databases       |


Managed Disks
Every VM needs storage.

Azure automatically creates:
Operating System Disk
↓
Managed Disk

Advantages:

High availability
Automatic management
Snapshots
Encryption support

Public IP
Internet
↓
Public IP
↓
VM


Network Interface (NIC)
Every VM receives a Network Interface.

VM
↓
NIC
↓
Private IP
↓
VNet

Availability Options

Azure offers:

No Infrastructure Redundancy
Good for learning.

Availability Zone
Protects against datacenter failures.

Availability Set
Distributes VMs across fault and update domains.

Boot Diagnostics

Azure automatically captures:

Boot logs
Console screenshots
Startup errors

Useful if the VM fails to boot.


VM Networking

Internet
↓
Public IP
↓
NSG
↓
NIC
↓
VM
↓
Private IP
↓
VNet

Interview Questions

Q1. What is an Azure Virtual Machine?

An Azure Virtual Machine is an Infrastructure as a Service (IaaS) offering that provides an on-demand virtual computer running Windows or Linux in Azure.

Q2. What resources are created with a VM?

A typical VM deployment includes:

Virtual Machine
Managed Disk
Network Interface (NIC)
Public IP (optional)
Virtual Network/Subnet (existing or new)
Network Security Group (existing or new)

Q3. What is a Managed Disk?
A Managed Disk is Azure-managed block storage for VMs. Azure automatically handles storage account management, scalability, availability, and maintenance.

Q4. SSH vs RDP
SSH	RDP
Linux	Windows
Port 22	Port 3389
Command-line access	Graphical desktop access

Q5. Why should you stop (deallocate) a VM?
Stopping (deallocating) a VM releases the compute resources so you stop paying for compute. Storage resources, such as managed disks, continue to incur storage charges.

Best Practices

✅ Use the smallest VM size suitable for learning.
✅ Stop (deallocate) VMs when not in use.
✅ Prefer SSH key authentication over passwords.
✅ Use existing VNets and NSGs instead of creating duplicates.
✅ Enable Boot Diagnostics for troubleshooting.




