Day 29 — Azure Production-Style .NET Application

Portfolio Project | Azure Cloud / Platform Engineering

1. Project Overview

This project demonstrates a production-style deployment of a simple ASP.NET Core .NET application on Microsoft Azure.
The application exposes an employee API and retrieves employee data from Azure SQL Database through a private network path.

The project combines:

Azure Virtual Network
Subnet segmentation
App Service
App Service VNet Integration
Azure SQL Database
Private Endpoint
Private DNS
Microsoft Entra ID / Managed Identity
Application Insights
Azure Monitor concepts
Bicep
GitHub Actions
Git/GitHub
Cloud troubleshooting and layered security

The goal was not to build a complex application. The goal was to understand how a real application is connected, secured, deployed, monitored, and documented on Azure.

2. Architecture

                         INTERNET
                            |
                            v
                    +---------------+
                    |  Azure App    |
                    |    Service    |
                    |  ASP.NET Core |
                    +-------+-------+
                            |
                     VNet Integration
                            |
                            v
                  +--------------------+
                  |     AppSubnet      |
                  |   10.50.1.0/24     |
                  +---------+----------+
                            |
                            | Private DNS
                            v
                  +--------------------+
                  |  Private Endpoint  |
                  |    10.50.2.x       |
                  +---------+----------+
                            |
                       Private Link
                            |
                            v
                    +---------------+
                    |   Azure SQL   |
                    |   EmployeeDB  |
                    +-------+-------+
                            |
                            v
                       Employees
                         Table

Identity path:

App Service
     |
     v
System-Assigned Managed Identity
     |
     v
Microsoft Entra ID
     |
     v
Azure SQL authorization

Monitoring path:

App Service
     |
     +------> Application Insights
                    |
                    v
               Azure Monitor

CI path:

Developer
    |
    v
Git
    |
    v
GitHub
    |
    v
GitHub Actions
    |
    +--> Checkout
    +--> Build / validation
    +--> Publish / deployment workflow

3. Azure Resource Inventory

| Resource                          | Name |                    |  Purpose |

| Resource Group                | `RG-Day29-Production`         | Contains Day 29 resources |
| Virtual Network               | `VNet-Production`             | Private network boundary |
| VNet CIDR                     | `10.50.0.0/16`                | VNet address space |
| Application Subnet            | `AppSubnet`                   | App Service VNet integration |
| Application subnet CIDR       | `10.50.1.0/24`                | Application network segment |
| Private Endpoint Subnet       | `PrivateEndpointSubnet`       | Private Endpoint placement |
| Private endpoint subnet CIDR  | `10.50.2.0/24`                | Private endpoint network segment |
| SQL Server                    | `sql-day29-prod-chandru`      | Azure SQL logical server |
| SQL Database                  | `EmployeeDB`                  | Application database |
| Private Endpoint              | `PE-SQL-Production`           | Private connectivity to SQL |
| Private DNS Zone              | `privatelink.database.windows.net` | Private SQL DNS resolution |
| App Service Plan              | `ASP-Day29-Production`         | App Service compute |
| Web App                       | `day29-prod-employee-dotnetapp`| Hosts the .NET API |
| Application Insights          | `appi-day29-production`        | Application telemetry |


4. Application

The application is a small ASP.NET Core API.

Endpoints:

GET /
GET /health
GET /employees


## `/`

Returns basic application information.

## `/health`

Returns application health.

Expected response:

```json
{
  "status": "Healthy"
}
```

## `/employees`

Returns employee records from Azure SQL.

Example:

```json
[
  {
    "id": 1,
    "name": "Arun",
    "department": "Cloud",
    "email": "arun@example.com"
  },
  {
    "id": 2,
    "name": "Priya",
    "department": "Development",
    "email": "priya@example.com"
  },
  {
    "id": 3,
    "name": "Karthik",
    "department": "DevOps",
    "email": "karthik@example.com"
  },
  {
    "id": 4,
    "name": "Sentry",
    "department": "DevOps",
    "email": "karthik@example.com"
  }
]
```

The API initially used hardcoded data during development.

It was then changed to retrieve the records from the `Employees` table in `EmployeeDB`.

The deployed `/employees` endpoint was verified successfully.

5. Database

Database:

EmployeeDB

Table:

Employees

Columns:

EmployeeId
Name
Department
Email

Example records were inserted for testing.

The application successfully returned the SQL records through the deployed App Service API.

6. Networking

VNet

VNet-Production
10.50.0.0/16

Subnets

AppSubnet
10.50.1.0/24

Used for App Service VNet Integration.

PrivateEndpointSubnet
10.50.2.0/24

Used for the SQL Private Endpoint.

The subnets are intentionally separated.

VNet-Production
|
+-- AppSubnet
|   10.50.1.0/24
|
+-- PrivateEndpointSubnet
    10.50.2.0/24

7. Private SQL Connectivity

Azure SQL is accessed through a Private Endpoint.

App Service
    |
    v
VNet Integration
    |
    v
AppSubnet
    |
    v
Private Endpoint
    |
    v
Azure SQL

The Private Endpoint provides a private IP representation of the Azure SQL service inside the VNet.

This avoids designing the application around direct public SQL connectivity.

8. Private DNS

Private DNS zone:
privatelink.database.windows.net

The zone is linked to:
VNet-Production

The Private Endpoint is associated with the private DNS zone.

Conceptually:

SQL hostname
     |
     v
Private DNS
     |
     v
Private Endpoint private IP
     |
     v
Azure SQL

This allows the application to use the normal Azure SQL hostname while the connection resolves through the private endpoint.

9. Identity and Security

The App Service uses a system-assigned managed identity.

App Service
     |
     v
System Assigned Managed Identity
     |
     v
Microsoft Entra ID

The application does not need a hardcoded SQL password for the passwordless authentication path.
The database authorization model follows the least-privilege principle.

For a read-only employee endpoint, read permissions are sufficient.

Important distinction:

Authentication
Who are you?

Authorization
What are you allowed to do?

Managed Identity helps with authentication.

SQL database roles/permissions control authorization.

10. Monitoring

Application Insights resource:

appi-day29-production
Application traffic was generated against the application endpoints.

Observed telemetry included:
Server requests
Failed requests
Server response time
Availability metrics
Application Insights was enabled for the App Service.

Monitoring flow:

Application
     |
     v
Application Insights
     |
     v
Azure Monitor

Monitoring is used to investigate:

Request failures
Application exceptions
Response time
Dependencies
Application behavior

11. Bicep

Bicep was added to the project under:

infra/

The Bicep file was successfully built/validated using Azure CLI.

Example validation command:
az bicep build --file main.bicep

12. GitHub Actions

A GitHub Actions workflow was added under:

.github/
└── workflows/
The workflow was executed successfully.

The CI pipeline demonstrates the basic automation flow:

GitHub
   |
   v
Checkout repository
   |
   v
Build / validation
   |
   v
Workflow result

The successful workflow run was captured as project evidence.

For a future production version, the workflow can be extended with:

Checkout
   |
   v
.NET setup
   |
   v
Restore
   |
   v
Build
   |
   v
Test
   |
   v
Bicep validation
   |
   v
Azure authentication using OIDC
   |
   v
Infrastructure deployment
   |
   v
Application deployment

13. Project Structure

Day29-Production-Project/
|
+-- app/
|   |
|   +-- EmployeeApi/
|       |
|       +-- Program.cs
|       +-- EmployeeApi.csproj
|       +-- appsettings.json
|
+-- infra/
|   |
|   +-- main.bicep
|   +-- parameters.bicepparam
|
+-- database/
|   |
|   +-- schema.sql
|   +-- seed.sql
|
+-- .github/
|   |
|   +-- workflows/
|       +-- day29-deploy.yml
|
+-- docs/
|   |
|   +-- screenshots/
|
+-- README.md

14. Important Commands

Check Azure resources

az resource list `
  --resource-group "RG-Day29-Production" `
  --output table

Check App Service

az webapp show `
  --resource-group "RG-Day29-Production" `
  --name "day29-prod-employee-dotnetapp" `
  --output table

Check VNet integration

az webapp vnet-integration list `
  --resource-group "RG-Day29-Production" `
  --name "day29-prod-employee-dotnetapp" `
  --output table

Check Managed Identity

az webapp identity show `
  --resource-group "RG-Day29-Production" `
  --name "day29-prod-employee-dotnetapp"

Check Private Endpoint

az network private-endpoint show `
  --resource-group "RG-Day29-Production" `
  --name "PE-SQL-Production"

Check Private DNS

az network private-dns zone show `
  --resource-group "RG-Day29-Production" `
  --name "privatelink.database.windows.net"

Build Bicep

az bicep build --file .\infra\main.bicep

15. Troubleshooting Model

If the application cannot access SQL, do not randomly change resources.

Troubleshoot layer by layer:

Application
    |
    v
Configuration
    |
    v
DNS resolution
    |
    v
VNet Integration
    |
    v
AppSubnet
    |
    v
Private Endpoint
    |
    v
Private DNS
    |
    v
Network connectivity
    |
    v
Managed Identity
    |
    v
SQL authentication
    |
    v
SQL authorization
    |
    v
Application logs

Example failure analysis

DNS resolves incorrectly

Investigate:

Private DNS Zone
VNet link
DNS zone group

Connection timeout

Investigate:

VNet Integration
Private Endpoint
routing
network controls
SQL networking

Login/authentication failure

Investigate:

Managed Identity
Microsoft Entra authentication
SQL identity configuration

Permission denied

Investigate:

SQL database user
database role
object permissions
least-privilege configuration

HTTP 500

Investigate:

Application logs
Application Insights
SQL dependency
connection errors
configuration

16. Key Cloud Concepts Learned

VNet

A logical private network boundary in Azure.

Subnet

A smaller network segment inside a VNet.

Private Endpoint

Provides private connectivity from a VNet to a supported Azure PaaS service.

Private DNS

Provides private name resolution for private endpoints.

VNet Integration

Allows App Service applications to make outbound connections into a VNet.

Managed Identity

Provides an Azure-managed identity for authentication without requiring application-managed credentials.

RBAC vs SQL permissions

Azure RBAC controls access to Azure resources.

SQL permissions control what an identity can do inside the database.

Application Insights

Provides application telemetry and observability.

Bicep

Declarative Infrastructure as Code for Azure resources.

GitHub Actions

Automates build, validation, testing and deployment workflows.

19. What This Project Demonstrates

This project demonstrates the ability to:

Create Azure networking infrastructure
Segment VNets using subnets
Deploy a .NET application to App Service
Integrate App Service with a VNet
Connect to Azure SQL through a Private Endpoint
Configure Private DNS
Use Managed Identity
Apply least-privilege database access
Monitor applications with Application Insights
Validate Azure infrastructure using Bicep
Use GitHub Actions for automation
Troubleshoot cloud connectivity by layers
Document an Azure architecture for repeatability

20. Production Improvements

The current project is intentionally small and credit-conscious.

A larger production architecture could add:

                    Internet
                       |
                       v
                Azure Front Door
                       |
                       v
                     WAF
                       |
                       v
                 App Service
                       |
                       v
                VNet Integration
                       |
                       v
                Private SQL

Other improvements:

Azure Key Vault where secrets are unavoidable
Deployment slots
Staging environment
Azure Front Door
Web Application Firewall
More comprehensive health checks
Automated tests
Complete Bicep modules
Bicep deployment through CI/CD
GitHub Actions OIDC federation
Azure Monitor alerts
Log Analytics
Backup and disaster recovery
Separate development/staging/production environments

These are future production enhancements, not claims about resources currently deployed in this credit-limited lab.


22. Portfolio Summary

Project

Azure Production-Style .NET Application with Private SQL Connectivity

Stack

Azure App Service
ASP.NET Core / .NET
Azure SQL Database
Azure VNet
Azure Subnets
Private Endpoint
Private DNS
Microsoft Entra ID
Managed Identity
Application Insights
Azure Monitor
Bicep
Azure CLI
Git
GitHub
GitHub Actions

Core architecture

.NET App Service
       |
       v
VNet Integration
       |
       v
Private Endpoint
       |
       v
Azure SQL
       |
       v
EmployeeDB

Security

Managed Identity
Private Endpoint
Private DNS
Least privilege

Automation

GitHub
   |
   v
GitHub Actions
   |
   v
Build / Validation / Deployment workflow


 Final  Status

NETWORKING
    VNet                         ✅
    Subnets                      ✅
    Private Endpoint             ✅
    Private DNS                  ✅

COMPUTE
    App Service Plan             ✅
    App Service                  ✅
    .NET API                     ✅

DATABASE
    Azure SQL                    ✅
    EmployeeDB                   ✅
    Employees table              ✅
    Application → SQL            ✅

IDENTITY
    Managed Identity             ✅
    SQL authorization            ✅

OBSERVABILITY
    Application Insights        ✅
    Request telemetry            ✅

INFRASTRUCTURE AS CODE
    Bicep                        ✅
    Bicep build/validation       ✅

CI/CD
    GitHub                       ✅
    GitHub Actions               ✅
    Successful workflow          ✅

DOCUMENTATION
    README                       ✅
    Architecture                 ✅


## Screenshots

See the `Images` directory.

See the `Architecture-images` directory.
