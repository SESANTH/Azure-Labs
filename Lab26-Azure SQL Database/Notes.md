Azure SQL Database is Microsoft's managed relational database service based on Microsoft SQL Server.

Think:

Azure
 │
 └── Azure SQL Database
        │
        ├── Database
        ├── Tables
        ├── Views
        ├── Stored Procedures
        └── Data

Managed service concept

or example:

VM

requires you to manage:

OS
patching
disk
networking
software

But:

Azure SQL Database

abstracts away much of the underlying infrastructure.

The cloud provider handles many platform responsibilities.

IaaS
 ↓
VM
 ↓
More responsibility

PaaS / managed database
 ↓
Azure SQL Database
 ↓
Less infrastructure responsibility


Azure SQL architecture

                    Azure
                      │
                      ▼
              Logical SQL Server
                      │
          ┌───────────┼───────────┐
          ▼           ▼           ▼
       Database 1  Database 2  Database 3
          │
          ├── Tables
          ├── Views
          ├── Procedures
          └── Data

Logical server

When creating Azure SQL Database, you'll typically encounter:

SQL logical server
        +
Database

For example:

sqlserver-learning
      │
      ├── EmployeeDB
      ├── SalesDB
      └── AppDB

The logical server provides a management and security boundary for its databases.

Database

The database contains your actual relational data.

Azure SQL Database is relational

Azure SQL vs SQL Server on VM

| SQL Server on VM                  | Azure SQL Database                         |
| --------------------------------- | ------------------------------------------ |
| IaaS                              | Managed/PaaS                               |
| Manage OS                         | Azure handles platform infrastructure      |
| Install SQL Server                | Azure provides database service            |
| More control                      | Less infrastructure control                |
| More operational work             | Less operational work                      |
| You manage patching more directly | Azure handles much of platform maintenance |


Pricing / compute models

Azure SQL has different purchasing and compute models.

Two important concepts:

DTU
vCore

DTU model

DTU means:

Database Transaction Unit

It bundles:

CPU
Memory
I/O

into a simplified performance unit.

Conceptually:

DTU
├── CPU
├── Memory
└── I/O

It provides a simpler purchasing model.

vCore model

The vCore model gives you more explicit control over compute resources.

Think:

vCore
 ↓
Compute capacity
 ↓
Memory
 ↓
Storage/network characteristics

This model is generally more flexible for planning production workloads.

DTU vs vCore

Interview answer:

DTU is a bundled performance model combining CPU, memory, and I/O into a database transaction unit, while the vCore model provides more explicit control over compute resources and is generally better suited to detailed capacity planning.

You don't need to memorize pricing today.

Understand the model.

Serverless

Azure SQL can also use serverless compute in supported configurations.

Think:

Low workload
    ↓
Less compute
    ↓
Potential cost savings


Higher workload
    ↓
More compute

For workloads with variable or intermittent usage, serverless can be useful.

Public connectivity

Azure SQL can expose a public endpoint.

Conceptually:

Internet
   │
   ▼
Azure SQL Public Endpoint
   │
   ▼
Database

But access should be restricted.

That's where firewall rules come in.

SQL firewall

Azure SQL provides firewall rules that control which IP addresses can connect through the public endpoint.

Conceptually:

Client IP
    │
    ▼
SQL Firewall
    │
 ┌──┴───┐
 │      │
Allow  Deny
 │
 ▼
Database

Important production warning 🚨

Don't treat:

Allow all IPs

as a solution.

For example:

0.0.0.0 - 255.255.255.255

would effectively open the service broadly.

That's bad production security.


SQL authentication

There are two broad authentication approaches you should understand:

SQL authentication
        +
Microsoft Entra authentication

Microsoft Entra authentication

Instead:

Application / User
       ↓
Microsoft Entra ID
       ↓
Token
       ↓
Azure SQL

This integrates identity into Azure.

Advantages include:

centralized identity
RBAC/identity management integration
reduced password dependence
better integration with managed identities

Backup

Azure SQL Database provides automated backup capabilities.

Conceptually:

Database
   ↓
Automated backups
   ↓
Point-in-time recovery

This is one of the benefits of using a managed database service.

High availability

Production databases need to survive infrastructure failures.

Conceptually:

Application
     ↓
Azure SQL
     │
     ├── Compute/replication
     └── Availability mechanisms

The exact architecture depends on the Azure SQL service tier and configuration.

The important concept:

Database availability is part of application reliability.

Geo-replication

For larger production systems, databases may need copies in another region.

Backup vs replication
Backup
Database
 ↓
Backup
 ↓
Restore when necessary
Replication
Primary
 ↓
Secondary copy

Replication is intended to support availability/disaster-recovery scenarios, while backups support recovery from data loss/corruption and point-in-time restoration.

Monitoring

Azure SQL can be monitored through Azure monitoring capabilities.

Think:

Azure SQL
   ↓
Metrics
Logs
Alerts
   ↓
Azure Monitor

Useful things to watch include:

CPU
Storage
Connections
Query performance
DTU/vCore utilization
Deadlocks
Errors

Database migrations

Imagine you add:

Employee.Email

Your application code expects:

Email

But production database doesn't have it.

Then:

Application
    ↓
SELECT Email
    ↓
Database
    ↓
❌ Column doesn't exist

So production CI/CD must account for database migrations.

Migration flow

Conceptually:

Code change
   ↓
Database migration
   ↓
Application deployment

For example:

Migration 001
Create Employees


Migration 002
Add Email


Migration 003
Add DepartmentId

The exact tooling depends on your application stack.

Important deployment principle

Don't blindly do:

Delete database
 ↓
Create database

for every deployment.

Production databases contain valuable data.

Instead:

Schema migration
 ↓
Preserve data
 ↓
Deploy application

This is a critical production concept.

