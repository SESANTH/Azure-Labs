Why App Service?

So far We've been deploying applications like this:

Internet
      │
      ▼
Load Balancer
      │
      ▼
Virtual Machine
      │
      ▼
Install Nginx
Install Runtime
Deploy Code
Patch OS
Maintain Server

We manage everything.

Now Azure offers another option:

Internet
      │
      ▼
Azure App Service
      │
      ▼
Deploy Application

Azure manages:

Operating System
Runtime
Web Server
Security patches
Infrastructure

You only deploy your code.


What is Azure App Service?

Azure App Service is a Platform as a Service (PaaS) offering for hosting web applications, REST APIs, and backend services.

Supported languages include:

.NET
Python
Node.js
Java
PHP


Virtual Machine (IaaS)

You manage:

Operating System
Updates
Runtime
IIS / Nginx
Security
Scaling


Azure
↓
VM
↓
You install everything

App Service (PaaS)

Azure manages:

Operating System
Runtime
Patching
Web Server

You only deploy the application.

Azure
↓
App Service
↓
Deploy Code

App Service Architecture

Internet
↓
App Service
↓
App Service Plan
↓
Web App


App Service Plan

An App Service Plan provides the compute resources for one or more App Services.

Think of it as the "machine" that runs your apps.

App Service Plan
↓
Web App 1
Web App 2
API App

Multiple applications can share the same plan.

App Service Pricing Tiers

| Tier          | Best For     |
| ------------- | ------------ |
| Free (F1)     | Learning     |
| Basic (B1)    | Small apps   |
| Standard (S1) | Production   |
| Premium       | High traffic |

Deployment Options

Azure supports deployment from:

GitHub
Azure DevOps
Local ZIP deployment
Visual Studio
VS Code
Azure CLI

Deployment Slots

Suppose your production app is live.

Instead of replacing it directly:
Production
↓
Running

Create another slot.

Production

Staging

Deploy the new version to Staging, test it, then Swap.
Users experience minimal downtime.


Scaling
App Service supports:

Vertical Scaling

Increase CPU/RAM.
B1
↓
S1

Horizontal Scaling

Increase instances.
Web App
↓
Instance 1
Instance 2
Instance 3

Azure distributes traffic automatically.

Custom Domains

Instead of:

https://myapp.azurewebsites.net

Use:

https://www.mycompany.com

App Service supports custom domains and SSL certificates.


Best Practices
✅ Use the Free (F1) tier for learning.
✅ Keep related Web Apps on the same App Service Plan when appropriate.
✅ Use Deployment Slots for production deployments.
✅ Enable diagnostics and Log Stream when troubleshooting.
✅ Use GitHub or Azure DevOps for automated deployments.
