What is a Managed Disk?
Every Virtual Machine needs storage.
Azure stores the VM's operating system and data in Managed Disks.

Virtual Machine
↓
Managed Disk
↓
Ubuntu
Applications
Files

Why Managed Disks?

Before Managed Disks, you had to manage storage accounts yourself.

Now Azure manages everything.

VM
↓
Managed Disk

Benefits:

Automatic management
Better scalability
High availability
Easier backup
Simpler administration


Types of Managed Disks


1. OS Disk

Contains:

Operating System
Boot files
System configuration

Example:
Ubuntu
↓
OS Disk

Every VM has exactly one OS Disk.


2. Data Disk

Used for:

Databases
Application files
User uploads
Logs

Example:

VM
↓
OS Disk
↓
Data Disk

A VM can have multiple Data Disks.

Disk Performance Tiers
Azure provides different disk types.

| Disk Type      | Best For                      |
| -------------- | ----------------------------- |
| Standard HDD   | Backup, Archive               |
| Standard SSD   | Development                   |
| Premium SSD    | Production                    |
| Premium SSD v2 | High-performance applications |
| Ultra Disk     | Mission-critical databases    |

OS Disk vs Data Disk

| OS Disk          | Data Disk              |
| ---------------- | ---------------------- |
| Operating system | Application data       |
| Required         | Optional               |
| One per VM       | Multiple allowed       |
| Boots the VM     | Stores additional data |

What is a Snapshot?
A Snapshot is a point-in-time copy of a managed disk

VM Disk
↓
Snapshot
↓
Restore Later

If something goes wrong:

Delete files
Corrupt OS
Bad software update

You can restore from the snapshot.

Snapshot Workflow

Managed Disk
↓
Snapshot
↓
Restore Disk
↓
Create New VM

Snapshots are commonly used before:
OS upgrades
Major application deployments
Configuration changes

Disk Resize

Suppose your disk is:
30 GB
Later your application grows.

Azure lets you resize it:

30 GB
↓
64 GB
↓
128 GB
No need to recreate the VM.

Azure Backup vs Snapshot

| Snapshot                           | Azure Backup                  |
| ---------------------------------- | ----------------------------- |
| Manual or scheduled copy of a disk | Full backup service           |
| Fast recovery                      | Long-term retention           |
| Disk-level                         | VM/Application-level policies |
| Short-term protection              | Disaster recovery strategy    |



Best Practices
✅ Take a snapshot before major system changes.
✅ Use Standard SSD for development and learning environments.
✅ Separate operating system files and application data using Data Disks.
✅ Delete unused snapshots to reduce storage costs.
✅ Use Azure Backup for production workloads instead of relying only on snapshots.