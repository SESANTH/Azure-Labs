# Production-Style Employee Management Platform

## Overview

A production-style employee management API built with ASP.NET Core
and deployed on Microsoft Azure.

The project demonstrates cloud networking, identity, security,
database integration, object storage, monitoring, Infrastructure
as Code, and CI/CD.

---

## Architecture

Internet
   |
   v
Azure App Service
   |
   | VNet Integration
   v
AppSubnet
10.50.1.0/24
   |
   v
Private Endpoint
   |
   v
Azure SQL
EmployeeDB

App Service
   |
   v
Managed Identity
   |
   +----> Azure SQL
   |
   +----> Azure Blob Storage

Blob Storage
   |
   v
employee-documents

Application
   |
   v
Application Insights
   |
   v
Azure Monitor

Infrastructure
   |
   v
Bicep

Source / CI
   |
   v
GitHub Actions


Technologies
ASP.NET Core / .NET 8
Azure App Service
Azure SQL Database
Azure Blob Storage
Azure Virtual Network
VNet Integration
Private Endpoint
Private DNS
Microsoft Entra ID
Managed Identity
RBAC
Application Insights
Azure Monitor
Bicep
GitHub Actions
Azure CLI

| Method | Endpoint                    | Purpose            |
| ------ | --------------------------- | ------------------ |
| GET    | `/`                         | Application status |
| GET    | `/health`                   | Health check       |
| GET    | `/employees`                | List employees     |
| GET    | `/employees/{id}`           | Get employee       |
| POST   | `/employees`                | Create employee    |
| PUT    | `/employees/{id}`           | Update employee    |
| DELETE | `/employees/{id}`           | Delete employee    |
| POST   | `/employees/{id}/documents` | Upload document    |
| GET    | `/employees/{id}/documents` | List documents     |


Security
Azure SQL is accessed using Microsoft Entra authentication.
App Service uses Managed Identity.
Blob Storage access uses RBAC.
Blob container is private.
SQL is accessed through Private Endpoint.
Private DNS provides private name resolution.
No Azure storage keys are stored in source code.

Networking

VNet:

10.50.0.0/16

Subnets:

AppSubnet - 10.50.1.0/24

PrivateEndpointSubnet - 10.50.2.0/24

Monitoring

Application Insights is used to monitor:

Requests
Response time
Failed requests
Application exceptions
Application availability

Infrastructure as Code

Azure infrastructure is represented using Bicep.

infra/
├── main.bicep
└── modules/

CI/CD

GitHub Actions performs:

Repository checkout
.NET setup
Dependency restore
Application build
Bicep validation

Testing

The following operations were tested successfully:

Health endpoint
Employee listing
Employee lookup
Employee creation
Employee update
Employee deletion
Blob document upload
Blob document listing

Lessons Learned
Private Endpoint provides private access to Azure services.
VNet Integration provides App Service outbound connectivity into a VNet.
Managed Identity removes the need for application-managed Azure credentials.
RBAC controls access to Azure resources.
Azure SQL is appropriate for structured transactional data.
Blob Storage is appropriate for unstructured documents.
Application Insights provides application-level observability.
Bicep allows infrastructure to be represented as code.
GitHub Actions automates application validation and delivery.

Project Evidence

Screenshots are available under:

 ` docs/screenshots/ `

 Troubleshooting

Common issues encountered during the project:

Azure SQL authorization
Verified Microsoft Entra authentication and Managed Identity permissions.

Private Endpoint connectivity
Verified VNet, subnet, Private Endpoint and Private DNS configuration.

Blob authorization
Assigned:
Storage Blob Data Contributor to the App Service Managed Identity.

Deployment
Built and published the .NET application before deploying to App Service.

Project Status

Completed.

The Azure environment was used as the deployment environment while
the complete source code, infrastructure definitions, CI/CD workflow,
documentation and screenshots are preserved in GitHub.

