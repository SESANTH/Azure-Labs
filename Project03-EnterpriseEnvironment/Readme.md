Azure Enterprise Employee Management Platform

Day 30 Capstone

A production-style employee management platform built with ASP.NET
Core .NET 8 and deployed on Microsoft Azure. The project combines
application development, Azure SQL, private networking, Managed
Identity, Blob Storage, monitoring, Bicep Infrastructure as Code, and
GitHub Actions CI.

Azure resources were used for hands-on deployment and validation. This
repository preserves the application, infrastructure code, CI
workflow, and documentation as the portfolio record.

Architecture

Users
  |
  v
Azure App Service (.NET 8 API)
  |
  +---- VNet Integration ----> VNet-Production (10.50.0.0/16)
  |                              |-- AppSubnet (10.50.1.0/24)
  |                              `-- PrivateEndpointSubnet (10.50.2.0/24)
  |                                      |
  |                                      v
  |                               Private Endpoint
  |                                      |
  |                                      v
  |                               Azure SQL / EmployeeDB
  |
  +---- Managed Identity ----> Azure SQL
  |
  `---- Managed Identity ----> Azure Blob Storage
                                  `-- employee-documents

Application Insights <---- App Service

Bicep --------------------> Azure Infrastructure
GitHub Actions ------------> .NET Build + Bicep Validation

Objectives

Deploy an ASP.NET Core application on Azure App Service.

Integrate the API with Azure SQL Database.

Implement private database connectivity with VNet Integration,
Private Endpoint and Private DNS.

Use Managed Identity and RBAC instead of application credentials.

Store employee documents in private Azure Blob Storage.

Monitor application requests and failures with Application Insights.

Represent infrastructure using Bicep.

Validate application and infrastructure changes with GitHub Actions.

Technology Stack

Application: C#, ASP.NET Core, .NET 8, Minimal APIs,
Microsoft.Data.SqlClient, Azure.Storage.Blobs, Azure.Identity.

Azure: App Service, Azure SQL Database, Storage Account/Blob
Storage, Virtual Network, Subnets, Private Endpoint, Private DNS,
Microsoft Entra ID, RBAC, Managed Identity, Application Insights.

DevOps/IaC: Azure CLI, Bicep, Git, GitHub, GitHub Actions.

API Endpoints

Method   Endpoint                      Purpose

GET      /health                     Health check
GET      /employees                  List employees
GET      /employees/{id}             Get employee
POST     /employees                  Create employee
PUT      /employees/{id}             Update employee
DELETE   /employees/{id}             Delete employee
POST     /employees/{id}/documents   Upload employee document
GET      /employees/{id}/documents   List employee documents

Example employee payload:

{
  "employeeId": 5,
  "name": "Chandru",
  "department": "Platform Engineering",
  "email": "chandru@example.com"
}

Azure SQL

Database: EmployeeDB

Table: Employees

The table stores EmployeeId, Name, Department, and Email.

The application uses parameterized SQL queries and passwordless
authentication with:

Authentication=Active Directory Default

Blob Storage

Employee documents are stored in the private container:

employee-documents

Objects use an employee-specific prefix such as:

employee-5/<unique-file-name>

Blob public access is disabled. The App Service Managed Identity is used
to access the storage account.

Networking

Virtual network:

VNet-Production
10.50.0.0/16

Subnets:

AppSubnet
10.50.1.0/24

PrivateEndpointSubnet
10.50.2.0/24

Azure SQL is reached through a Private Endpoint and the Private DNS
zone:

privatelink.database.windows.net

This avoids relying on public SQL connectivity for the
application-to-database path.

Identity and Security

The App Service uses a system-assigned Managed Identity. Azure RBAC
grants the identity access to required resources.

Security practices demonstrated:

Managed Identity instead of embedded Azure credentials.

RBAC for resource access.

Private Endpoint for Azure SQL.

Private DNS for private SQL name resolution.

Blob public access disabled.

HTTPS-enabled Azure services.

Parameterized SQL queries.

No SQL password stored in application source code.

Monitoring

Application Insights is integrated with the application to provide
visibility into:

HTTP requests

Response times

Failed requests

Application performance

Application health

Infrastructure as Code

Bicep defines the Azure infrastructure used by the capstone.

Local validation:

az bicep build --file main.bicep

Azure resource-group validation:

az deployment group validate `
  --resource-group "RG-Day29-Production" `
  --template-file ".\main.bicep"

The resource-group validation completed successfully.

GitHub Actions CI

Workflow:

.github/workflows/capstone.yml

The pipeline runs on pushes and pull requests targeting main.

It performs:

Checkout
   |
   +--> Setup .NET 8
   |      |
   |      +--> Restore
   |      `--> Build .NET API
   |
   `--> Install Bicep
          |
          `--> Build/validate Bicep

Latest validated result:

Build .NET API    PASS
Validate Bicep    PASS

Repository Structure

Azure-Labs/
├── .github/
│   └── workflows/
│       └── capstone.yml
│
└── Project03-EnterpriseEnvironment/
    ├── app/
    │   └── EmployeeApi/
    │       ├── EmployeeApi.csproj
    │       ├── Program.cs
    │       └── ...
    ├── infra/
    │   └── Infra/
    │       └── main.bicep
    ├── database/
    └── docs/
        └── screenshots/

Testing

The application was tested locally and in Azure.

Verified application operations:

Health check

Employee retrieval

Employee creation

Employee update

Employee deletion

SQL data verification

Blob document upload

Blob document listing

Day 29 to Day 30 Progression

Day 29 --- Production .NET Application

Day 29 established the production-style Azure application foundation:

ASP.NET Core API

Azure App Service

Azure SQL

VNet Integration

Private Endpoint

Private DNS

Managed Identity

Application Insights

Bicep

GitHub Actions

Day 30 --- Enterprise Capstone

Day 30 extended the environment with:

Full Employee CRUD API

Azure Blob Storage integration

Employee document upload/listing

Managed Identity access to Blob Storage

Expanded Bicep validation

Dedicated capstone CI workflow

Portfolio architecture and documentation

Key Cloud Engineering Skills Demonstrated

Azure App Service deployment

Azure SQL integration

VNet and subnet design

Private Endpoint and Private DNS

Managed Identity and RBAC

Azure Blob Storage

Application Insights

Azure CLI

Bicep Infrastructure as Code

Git/GitHub

GitHub Actions CI

Cloud troubleshooting

Secure passwordless Azure service access

Future Improvements

Azure Key Vault for additional secret/configuration management.

Azure Front Door with WAF.

API authentication and authorization.

Automated Azure deployment from GitHub Actions using OIDC.

Automated database migrations.

Unit and integration tests.

Docker/container deployment.

Azure Monitor dashboards and alerts.

Centralized Log Analytics.

API versioning and OpenAPI documentation.

Project Outcome

The capstone demonstrates an enterprise-oriented Azure application
platform combining:

ASP.NET Core
+ Azure App Service
+ Azure SQL
+ Private Networking
+ Managed Identity
+ Azure Blob Storage
+ Application Insights
+ Bicep
+ GitHub Actions

This project demonstrates practical Azure Cloud Engineering skills
across compute, networking, identity, security, storage, monitoring,
Infrastructure as Code, CI, and application integration.

Author

Sesanth Chandru

Azure / Cloud Engineering Portfolio