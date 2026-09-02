
## Azure 30-Day Cloud Engineering Challenge

A hands-on Azure Cloud Engineering portfolio covering 30 days of
practical labs, troubleshooting, Infrastructure as Code, networking,
security, monitoring, CI/CD, and a production-style enterprise capstone.

The goal of this repository is to demonstrate practical Azure skills
through repeatable labs and real project work rather than certification
theory alone.

## What This Repository Covers

Over 30 days, this repository progresses from Azure fundamentals to a
production-style application environment.

Azure Fundamentals
        ↓
Identity & Governance
        ↓
Storage & Compute
        ↓
Networking & Security
        ↓
Monitoring & Backup
        ↓
CLI / PowerShell
        ↓
Bicep Infrastructure as Code
        ↓
Azure DevOps / GitHub Actions
        ↓
Azure SQL + Private Connectivity
        ↓
Production Application
        ↓
Enterprise Capstone

## 📅 30-Day Progress

Day      Topic                                    Status

Day 01   Azure Resource Groups                    - [✔]
Day 02   Microsoft Entra ID                       - [✔]
Day 03   Azure RBAC                               - [✔]
Day 04   Azure Storage                            - [✔]
Day 05   Storage Lifecycle Management             - [✔]
Day 06   Virtual Network (VNet)                   - [✔]
Day 07   Network Security Group (NSG)             - [✔]
Day 08   Virtual Machines                         - [✔]
Day 09   Managed Disks & Snapshots                - [✔]
Day 10   Availability Sets & Availability Zones   - [✔]
Day 11   Azure Load Balancer                      - [✔]
Day 12   Azure App Service                        - [✔]
Day 13   Azure Monitor                            - [✔]
Day 14   Azure Backup & Recovery                  - [✔]
Day 15   Virtual Machine Scale Sets (VMSS)        - [✔]
Day 16   Azure Key Vault                          - [✔]
Day 17   Azure Managed Identity                   - [✔]
Day 18   Azure Policy                             - [✔]
Day 19   Azure Log Analytics                      - [✔]
Day 20   Azure Cost Management                    - [✔]
Day 21   Azure CLI                                - [✔]
Day 22   PowerShell                               - [✔]
Day 23   Bicep Infrastructure as Code             - [✔]
Day 24   Azure DevOps CI/CD                       - [✔]
Day 25   GitHub Actions                           - [✔]
Day 26   Azure SQL Database                       - [✔]
Day 27   Private Connectivity                     - [✔]
Day 28   Advanced Azure Networking                - [✔]
Day 29   Production .NET Project                  - [✔]
Day 30   Enterprise Environment Capstone          - [✔]

## 🏗️ Major Projects

Project 01 --- Azure Infrastructure & Administration Labs

The first part of the challenge focuses on Azure administration and
cloud fundamentals.

Topics covered

Resource Groups
Microsoft Entra ID
Users and Groups
RBAC
Storage Accounts
Blob Storage
Azure Files
SAS
Storage Lifecycle Management
VNets
Subnets
NSGs
Virtual Machines
Managed Disks
Snapshots
Availability Sets
Availability Zones
Load Balancer
VM Scale Sets

These labs established the core knowledge required to manage Azure
infrastructure.

## 🔐 Project 02 --- Azure Operations, Security & DevOps

The middle section focuses on operating Azure environments securely and
automating infrastructure/application workflows.

Security

Azure Key Vault
Managed Identity
Microsoft Entra ID
Azure RBAC
Azure Policy
Network Security Groups
Private connectivity
Monitoring
Azure Monitor
Log Analytics
Application monitoring
Alerts and troubleshooting
Backup & Recovery
Azure Backup
Recovery Services
Backup concepts
Recovery planning
Cost Management
Azure Cost Management
Resource cleanup
Understanding Azure consumption
Designing labs with limited cloud credits
Automation
Azure CLI
PowerShell
Bicep
Azure DevOps CI/CD

GitHub Actions

## ☁️ Project 03 --- Production-Style Employee Management Platform

The Day 29 project moved the learning from individual Azure labs into a
real application environment.

Application

An ASP.NET Core .NET 8 Employee Management API was deployed to:
Azure App Service
The application integrates with:

Azure SQL Database
for persistent employee data.

Core API

GET /health
GET /employees
GET /employees/{id}
POST /employees
PUT /employees/{id}
DELETE /employees/{id}

Technology
C#
ASP.NET Core
.NET 8
Minimal API
Azure App Service
Azure SQL Database
Microsoft.Data.SqlClient
Microsoft Entra authentication
Managed Identity
Application Insights
Bicep
Azure CLI
GitHub Actions

## Networking Architecture

The application environment uses:

VNet-Production
10.50.0.0/16

with:

AppSubnet
10.50.1.0/24

and:

PrivateEndpointSubnet
10.50.2.0/24

The App Service uses VNet Integration.

Azure SQL is accessed through:

Private Endpoint
        ↓
Private DNS
        ↓
Azure SQL

Private DNS zone:

privatelink.database.windows.net

This provided hands-on experience with private application-to-database
connectivity.

##  Identity & Security

The App Service uses a system-assigned Managed Identity.

The application authenticates to Azure SQL using:

Authentication=Active Directory Default

This avoids storing a SQL password in the application source code.

Access is controlled using:

Microsoft Entra ID
        ↓
Managed Identity
        ↓
Azure RBAC
        ↓
Azure Resources

## Day 29 Monitoring

Application Insights was integrated with the application.

Monitoring included:

HTTP requests

Request failures

Response time

Application health

Application performance

This was used both for observability and for troubleshooting
deployment/application issues.

## Day 30 --- Enterprise Environment Capstone

Day 30 extended the Day 29 application into a broader enterprise-style
Azure environment.

The capstone combines:

ASP.NET Core
       +
Azure App Service
       +
Azure SQL
       +
Private Networking
       +
Managed Identity
       +
Blob Storage
       +
Application Insights
       +
Bicep
       +
GitHub Actions

## Day 30 Employee CRUD

The API was extended to support complete employee CRUD operations.

GET     /employees
GET     /employees/{id}
POST    /employees
PUT     /employees/{id}
DELETE  /employees/{id}

The application was tested locally and then deployed to Azure.

CRUD operations were verified against Azure SQL.

## Day 30 --- Azure Blob Storage

Azure Blob Storage was added for employee document management.

Existing storage account:

stday30employee31568

Private container:

employee-documents

Documents are organized using employee-specific paths such as:

employee-5/<unique-file-name>

API endpoints

POST /employees/{id}/documents
GET  /employees/{id}/documents

The application uses:

DefaultAzureCredential
        ↓
App Service Managed Identity
        ↓
Azure RBAC
        ↓
Azure Blob Storage

No storage account key is embedded in the application.

## Day 30 --- Infrastructure as Code

Bicep is used to represent Azure infrastructure.

The capstone infrastructure includes resources such as:

Virtual Network

Subnets

Storage Account

Blob Container

Bicep was validated locally with:

az bicep build --file main.bicep

Azure resource-group validation was also completed successfully:

az deployment group validate `
  --resource-group "RG-Day29-Production" `
  --template-file ".\main.bicep"

Validation result:

provisioningState: Succeeded

## Day 30 --- GitHub Actions CI

The repository contains a dedicated capstone workflow:

.github/workflows/capstone.yml

The workflow automatically validates:

GitHub Push / Pull Request
          |
          +-------------------+
          |                   |
          v                   v
     Build .NET API      Validate Bicep
          |                   |
          +---------+---------+
                    |
                    v
                 PASS

The workflow performs:

.NET API

Checkout
   ↓
Setup .NET 8
   ↓
Restore
   ↓
Build

Bicep

Checkout
   ↓
Install Bicep
   ↓
Build / Validate Bicep

Latest capstone CI validation:

Build .NET API    ✅
Validate Bicep    ✅

This workflow is currently CI/validation. It should not be described
as fully automated Azure CD because it does not currently deploy Azure
resources.

## 🗂️ Repository Structure

Azure-Labs/
│
├── .github/
│   └── workflows/
│       └── capstone.yml
│
├── Lab01-ResourceGroups/
├── Lab02-Microsoft-Entra-ID/
├── Lab03-RBAC/
├── ...
├── Lab23-Bicep-...
├── Lab24-Azure-DevOps-CI-CD/
├── Lab25-GitHub-Actions/
├── Lab26-Azure-SQL-Database/
├── Lab27-Private-Connectivity/
├── Lab28-Advanced-Azure-Networking/
│
├── Lab29-Production Project/
│
├── Project03-EnterpriseEnvironment/
│   ├── app/
│   │   └── EmployeeApi/
│   │       ├── EmployeeApi.csproj
│   │       ├── Program.cs
│   │       └── ...
│   │
│   ├── infra/
│   │   └── Infra/
│   │       └── main.bicep
│   │
│   ├── database/
│   │
│   └── docs/
│       └── screenshots/
│
├── .gitignore
└── README.md

🧪 Troubleshooting Experience

A major part of the challenge was learning to troubleshoot real Azure
problems rather than only following deployment tutorials.

Hands-on troubleshooting included:

Azure VM SKU/region availability.

VM Scale Set quota and regional capacity issues.

Azure App Service deployment issues.

.NET application build and publish issues.

Azure SQL authentication.

Managed Identity permissions.

Private Endpoint connectivity.

Private DNS resolution.

VNet Integration.

Blob Storage authorization.

GitHub Actions workflow path issues.

Bicep validation.

Azure CLI command execution.

Git repository cleanup and workflow management.

This troubleshooting experience is an important part of the project
because cloud engineering involves diagnosing configuration, networking,
identity, deployment, and application issues.

🛠️ Tools Used

Azure Portal
Azure CLI
PowerShell
Git
GitHub
GitHub Actions
Azure DevOps
Bicep
.NET 8
C#
ASP.NET Core
SQL
Application Insights
Log Analytics

## Security Practices Demonstrated

The projects demonstrate several Azure security patterns:

Microsoft Entra ID
Azure RBAC
Managed Identity
Private Endpoint
Private DNS
Network Security Groups
Azure Key Vault concepts
Azure Policy
Private database connectivity
Private Blob Storage
Passwordless Azure service authentication
Parameterized SQL queries
No hard-coded Azure credentials

## Cloud Engineering Skills Demonstrated

Azure Administration
Resource management
Resource Groups
Tags
Resource Locks
Azure CLI
PowerShell
Cost Management
Compute
Virtual Machines
Managed Disks
Snapshots
Availability Sets
Availability Zones
VM Scale Sets
App Service
Networking
VNets
Subnets
NSGs
Public IP
Load Balancer
VNet Integration
Private Endpoint
Private DNS
Advanced networking
Storage
Storage Accounts
Blob Storage
Azure Files
SAS
Lifecycle Management
Identity & Security
Microsoft Entra ID
Users and Groups
RBAC
Managed Identity
Key Vault
Azure Policy
Monitoring & Recovery
Azure Monitor
Application Insights
Log Analytics
Azure Backup
Recovery Services
Databases
Azure SQL Database
SQL connectivity
Passwordless authentication
Private SQL connectivity
Infrastructure as Code
Bicep
Repeatable infrastructure
Template validation
DevOps
Git
GitHub
Azure DevOps
GitHub Actions
CI pipelines
Application build validation
Infrastructure validation

## 🎯 Learning Outcome

The 30-day challenge progressed from individual Azure services to a
connected cloud environment.

The final architecture demonstrates:

Identity
   ↓
Security / RBAC
   ↓
Application
   ↓
Networking
   ↓
Private Connectivity
   ↓
Database + Storage
   ↓
Monitoring
   ↓
Infrastructure as Code
   ↓
CI

The most important outcome was not simply learning individual Azure
services, but understanding how those services work together in a
realistic cloud environment.

## 📌 Portfolio Projects

` Project 01 --- Production-Style Employee Management API `

Azure App Service | ASP.NET Core | .NET 8 | Azure SQL | VNet |
Private Endpoint | Managed Identity | Application Insights | Bicep

Key achievement:

Designed and deployed a production-style ASP.NET Core Employee
Management API on Azure App Service with Azure SQL, implementing
private networking, Private Endpoint, Private DNS, Managed Identity,
RBAC, monitoring, and Infrastructure as Code.

` Project 02 --- Enterprise Employee Management Platform `

Azure App Service | ASP.NET Core | Azure SQL | Blob Storage |
Managed Identity | VNet | Bicep | GitHub Actions

Key achievement:

Extended the production application into an enterprise-style platform
with full CRUD operations, employee document management through Azure
Blob Storage, passwordless Managed Identity access, private
networking, monitoring, Bicep infrastructure validation, and GitHub
Actions CI.

💼 Resume Skills From This Repository

This repository provides hands-on evidence for:

Microsoft Azure
Azure Administration
Azure App Service
Azure SQL
Azure Storage
Azure Blob Storage
Azure Networking
VNet
Subnetting
NSG
Private Endpoint
Private DNS
Managed Identity
RBAC
Microsoft Entra ID
Application Insights
Log Analytics
Azure Monitor
Azure Backup
Azure CLI
PowerShell
Bicep
Infrastructure as Code
Git
GitHub
GitHub Actions
CI/CD Fundamentals
ASP.NET Core
C#
.NET 8
SQL
Cloud Troubleshooting

🚀 Future Improvements

Potential next steps for the platform include:

Azure Front Door + WAF

Azure Key Vault integration

API authentication and authorization

GitHub Actions Azure deployment using OIDC

Automated database migrations

Unit and integration testing

Docker/container deployment

Azure Monitor dashboards and alerts

Centralized Log Analytics

API versioning

OpenAPI/Swagger documentation

Production deployment environments such as Dev/Test/Prod

## 🏁 Final Status

Azure 30-Day Challenge
        |
        +-- Azure Fundamentals       ✅
        +-- Identity & Security      ✅
        +-- Storage                  ✅
        +-- Compute                  ✅
        +-- Networking               ✅
        +-- Monitoring               ✅
        +-- Backup & Recovery        ✅
        +-- CLI / PowerShell         ✅
        +-- Bicep IaC                ✅
        +-- Azure DevOps              ✅
        +-- GitHub Actions            ✅
        +-- Azure SQL                 ✅
        +-- Private Connectivity      ✅
        +-- Production Application    ✅
        +-- Enterprise Capstone       ✅

30 days of Azure hands-on learning completed.

Author

Sesanth Chandru

Azure / Cloud Engineering Portfolio

Focus: Azure Cloud Engineering → Cloud/Platform Engineering → DevOps