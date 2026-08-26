# Lab 26 — Azure SQL

## Concepts Learned
- Azure SQL Concepts
- SQL Logical Server
- Database
- Compute Models
- DTU vs vCore
- Networking
- Firewall
- Authentication
- Microsoft Entra ID
- Managed Identity
- Backup
- High Availability
- Geo-Replication

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is Azure SQL Database?

Azure SQL Database is a managed relational database service based on Microsoft SQL Server that removes much of the infrastructure management associated with running SQL Server yourself.

Q2. Azure SQL vs SQL Server on VM?

Azure SQL Database is managed by Azure and reduces infrastructure administration, while SQL Server on a VM gives more control but requires more responsibility for the OS and SQL Server environment.

Q3. Logical server?

A logical SQL server is a management and security boundary for Azure SQL databases. It isn't equivalent to a traditional physical SQL Server machine.

Q4. DTU?

DTU is a bundled performance metric combining CPU, memory, and I/O capacity.

Q5. vCore?

vCore is a compute-based purchasing model that gives more explicit control over compute capacity.

Q6. Serverless?

A compute option designed for variable workloads where compute can scale based on demand and potentially reduce cost during periods of low utilization, subject to the service's configuration and availability.

Q7. Firewall rule?

A firewall rule controls which client IP addresses can access the Azure SQL public endpoint.

Q8. Public vs private endpoint?

Public connectivity uses the service's public endpoint and network/firewall controls.

Private Endpoint provides a private IP address in an Azure VNet for accessing the Azure service privately.

Q9. SQL vs Entra authentication?

SQL authentication uses SQL-managed credentials.

Microsoft Entra authentication uses Azure identity and tokens.

Q10. Secure App Service connection?

A strong production approach is:

App Service
 ↓
Managed Identity
 ↓
Microsoft Entra ID
 ↓
Azure SQL

combined with appropriate network controls and database permissions.

Q11. Azure RBAC vs SQL permissions?

Azure RBAC controls management access to Azure resources, while SQL permissions control access to databases and data operations.

Q12. Backup vs replication?

Backup provides recoverable copies for restoration.

Replication maintains another copy of the database for availability/disaster-recovery scenarios.

Q13. SQL timeout?

Investigate:

DNS
 ↓
Network
 ↓
Firewall / Private Endpoint
 ↓
Routing
 ↓
Authentication
 ↓
Authorization
 ↓
Database
Q14. Bicep?

Define:

SQL Server
 ↓
Database
 ↓
Networking
 ↓
Security

in Bicep and deploy through Azure Resource Manager.

Q15. Database migrations?

Because application code and database schema must remain compatible. CI/CD should apply controlled schema changes without unnecessarily destroying production data.

