# Lab 9 – Azure Managed Disks & Snapshots

## Objective

Learn how Azure stores Virtual Machine data using Managed Disks and protects it using Snapshots.

## Concepts Learned

- Managed Disks
- OS Disk
- Data Disk
- Disk Types
- Snapshots
- Disk Resize
- Backup Concepts

## Hands-on Tasks

- Explored VM disks
- Attached a new Data Disk
- Verified the disk from Azure (and optionally Linux)
- Created a Snapshot
- Explored Snapshot properties
- Reviewed disk resize options

## Key Learnings

- Every VM has one OS Disk.
- Data Disks store application and user data.
- Snapshots provide point-in-time recovery.
- Azure supports online disk expansion.
- Managed Disks simplify storage management.

## Interview Questions

Q1. What is a Managed Disk?

A Managed Disk is Azure-managed block storage for Virtual Machines. Azure handles storage accounts, scalability, availability, and maintenance automatically.

Q2. OS Disk vs Data Disk
OS Disk	Data Disk
Contains operating system	Stores application and user data
One per VM	Multiple supported
Required	Optional
Q3. What is a Snapshot?

A Snapshot is a point-in-time copy of a managed disk. It can be used to restore data or create new disks if the original disk becomes corrupted or data is accidentally deleted.

Q4. Why create a Snapshot before updates?

Creating a snapshot before operating system upgrades or application deployments allows you to recover quickly if the update fails or causes unexpected issues.

Q5. Snapshot vs Azure Backup
Snapshot	Azure Backup
Point-in-time disk copy	Managed backup service
Quick restore	Long-term retention
Manual or automated through scripts	Policy-based backups
Disk-level protection	VM and workload protection

### Snapshot vs Azure Backup

Snapshots are point-in-time copies of managed disks used for quick recovery.
Azure Backup is a managed backup service with scheduling, retention policies, and disaster recovery capabilities.