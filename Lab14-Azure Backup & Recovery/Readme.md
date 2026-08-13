# Day 14 – Azure Backup & Recovery

## Objective

Learn how Azure protects Virtual Machines using Azure Backup and Recovery Services Vault.

## Concepts Learned

- Azure Backup
- Recovery Services Vault
- Backup Policy
- Recovery Points
- Retention
- Restore
- Soft Delete
- Backup vs Snapshot

## Hands-on Tasks

- Created a Recovery Services Vault
- Configured VM backup
- Created a backup policy
- Enabled backup for Learning-VM
- Triggered an on-demand backup
- Monitored the backup job
- Verified the recovery point
- Explored restore options

## Architecture

Learning-VM
    |
    v
Azure Backup
    |
    v
Recovery Services Vault
    |
    v
Recovery Point

## Key Learnings

- Azure Backup provides policy-based protection for Azure VMs.
- Recovery Services Vault stores recovery information.
- Backup policies control backup schedules and retention.
- Recovery points are used for restoration.
- Snapshots and backups serve different purposes.
- Soft delete helps protect backup data from accidental deletion.

## Screenshots

See the `Images` directory.

## Interview Questions
Q1. What is Azure Backup?

Azure Backup is a managed Azure service that provides backup and recovery capabilities for supported workloads such as Azure Virtual Machines.

Q2. What is a Recovery Services Vault?

A Recovery Services Vault is an Azure resource used to store and manage backup data and recovery points.

Q3. What is a Backup Policy?

A backup policy defines:

Backup frequency
Backup schedule
Retention period

Daily
 ↓
Keep 30 days

Q4. What is a Recovery Point?

A recovery point represents a point-in-time state from which protected data can be restored.

Q5. Snapshot vs Backup?

The key distinction:

Snapshot
   ↓
Point-in-time disk copy

versus

Azure Backup
   ↓
Scheduled protection
   ↓
Retention
   ↓
Recovery

Q6. Why do we need backups if we already have Availability Zones?

This is a very important interview question.

Availability Zones protect against infrastructure failure.

Backup protects against data loss or corruption.

Example:

Zone 1 ❌
Zone 2 ✅

Your application can continue running.

But if someone executes:

rm -rf important-data

across your application environment, Availability Zones don't magically restore that data.

Backup can.

So:

High Availability ≠ Backup

You generally need both.


### Snapshot vs Azure Backup

A snapshot is a point-in-time copy of a managed disk, while Azure Backup provides scheduled, policy-based protection with retention and recovery capabilities.

### What is a Recovery Services Vault?

A Recovery Services Vault is an Azure resource used to store and manage backup data and recovery points for supported workloads.