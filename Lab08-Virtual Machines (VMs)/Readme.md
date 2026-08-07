# Day 8 – Azure Virtual Machines

## Objective

Deploy and manage an Azure Virtual Machine.

## Concepts Learned

- Virtual Machines
- VM Sizes
- Images
- Managed Disks
- Network Interface
- Public IP
- SSH
- Boot Diagnostics

## Hands-on Tasks

- Created an Ubuntu Virtual Machine
- Attached it to an existing VNet and Subnet
- Used an existing NSG
- Connected using SSH
- Practiced Linux commands
- Reviewed VM resources
- Stopped the VM to reduce costs

## Key Learnings

- Azure VMs rely on multiple Azure resources.
- Managed Disks simplify storage management.
- SSH is the standard way to manage Linux VMs.
- Boot Diagnostics help troubleshoot startup issues.
- Stopping (deallocating) a VM reduces compute costs.

## Screenshots

## Interview Questions

### Managed Disk vs Unmanaged Disk

Managed Disks are Azure-managed storage resources that automatically handle scalability, availability, and storage accounts. Unmanaged Disks require the user to manage storage accounts manually and are generally not recommended for new deployments.