Bicep / Infrastructure as Code (IaC)

The key idea:
Instead of telling Azure what to do step-by-step, we describe the infrastructure we want Azure to have.

Why do we need IaC?

Imagine your production environment contains:

VNet
 ├── Subnet
 ├── NSG
 └── Public IP


VM
 ├── NIC
 └── Managed Disk


Key Vault
Storage Account
App Service

You create everything manually in the Portal.

It works.

Now six months later:

"Create the exact same environment for testing."

You have to remember:

Which settings?
Which SKU?
Which region?
Which networking rules?
Which tags?
Which dependencies?
Which permissions?

That's a problem.


What is Infrastructure as Code?

A simple definition:

Infrastructure as Code is the practice of defining and managing infrastructure through machine-readable configuration files instead of relying on manual configuration.

Bicep is different

With Bicep, conceptually:

I want:
Resource Group
VNet
Subnet
NSG
VM

You describe the desired infrastructure.

Azure then determines how to deploy it.

That's:

Declarative Infrastructure as Code.

Four approaches

                    Azure Infrastructure
                           ▲
                           │
        ┌──────────────────┼──────────────────┐
        │                  │                  │
      Portal              CLI             PowerShell
        │                  │                  │
     Manual           Imperative         Imperative
     actions           commands           commands
        │                  │                  │
        └──────────────────┼──────────────────┘
                           │
                           │
                         Bicep
                           │
                     Declarative IaC

What is Bicep?

Bicep is Microsoft's declarative language for deploying Azure resources.

It is designed specifically for Azure.

Think:

Bicep
  ↓
Azure Resource Manager
  ↓
Azure resources

Bicep is much easier to read than raw ARM JSON.

What is ARM?

ARM means:

Azure Resource Manager

It is the Azure management layer through which resources are deployed and managed.

Conceptually:

Bicep
   ↓
ARM
   ↓
Azure Resource Providers
   ↓
Resources

Bicep is not replacing ARM.

Instead:

Bicep
 ↓
Compiled/translated
 ↓
ARM template
 ↓
ARM deployment


Bicep vs ARM Templates

ARM template

JSON:

{
  "type": "Microsoft.Storage/storageAccounts",
  ...
}

Can become very large and difficult to read.

Bicep

Much cleaner:

resource storageAccount 'Microsoft.Storage/storageAccounts@...' = {
    ...
}

ARM
=
JSON-based Azure IaC

Bicep
=
Simpler declarative language for Azure IaC


Bicep architecture

Think:

Bicep file
    │
    ▼
Bicep compiler
    │
    ▼
ARM template
    │
    ▼
Azure Resource Manager
    │
    ▼
Azure


resource storageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: 'day23storage12345'
  location: resourceGroup().location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}


resource
resource storageAccount

means:
I'm declaring an Azure resource.

Resource type
'Microsoft.Storage/storageAccounts@2023-05-01'

Break it down:

Microsoft.Storage
        ↓
Resource provider


storageAccounts
        ↓
Resource type


@2023-05-01
        ↓
API version

So:

Microsoft.Storage/storageAccounts


Resource name
name: 'day23storage12345'

Location
location: resourceGroup().location

This is interesting.

Instead of hardcoding:

location: 'centralindia'

we say:

Use the Resource Group's location.

That makes the template more reusable.

SKU
sku: {
  name: 'Standard_LRS'
}

We're defining the storage account SKU.

So Bicep isn't simply saying:

Create storage.

It's defining configuration.

Kind
kind: 'StorageV2'

This specifies the storage account kind.

Now the complete architecture is:

main.bicep
    ↓
Storage Account declaration
    ↓
ARM
    ↓
Azure Storage

Parameters

Hardcoding values isn't ideal.

Instead:

param storageAccountName string

Now the template expects a value.

param storageAccountName string

resource storageAccount 'Microsoft.Storage/storageAccounts@2023-05-01' = {
  name: storageAccountName
  location: resourceGroup().location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
}


Variables

You can also define variables.

Example:

param environment string


var storageName = 'app${environment}storage'

Now:

environment = dev

could produce a name based on:

appdevstorage

Variables help avoid repeating values.


Parameters vs Variables

Remember:

Parameter
=
Value supplied from outside


Variable
=
Value calculated/defined inside template

Outputs

Bicep can return useful information after deployment.

Example:

output storageAccountId string = storageAccount.id

Meaning:

After deployment, output the resource ID.


Resource dependencies

VNet
 ↓
Subnet
 ↓
NIC
 ↓
VM



Bicep can understand dependencies when resources reference each other.

For example:

resource vnet 'Microsoft.Network/virtualNetworks@...' = {
    ...
}


resource subnet 'Microsoft.Network/virtualNetworks/subnets@...' = {
    parent: vnet
    ...
}

Explicit vs implicit dependencies
Implicit dependency

If resource B references resource A:

A
 ↓
B

Bicep can infer the dependency.

Explicit dependency

You can use:

dependsOn

when you need to explicitly define a dependency.

But don't use dependsOn everywhere unnecessarily.

Bicep scope

Bicep deployments can operate at different scopes.

For example:

Management Group
Subscription
Resource Group
Tenant

You can deploy Bicep using Azure CLI:

az deployment group create `
  --resource-group "RG-Day23-Bicep" `
  --template-file "main.bicep"


Bicep validation

Before deploying, you can validate.

Conceptually:

Bicep
 ↓
Validate
 ↓
Deployment

Azure CLI supports:

az deployment group validate `
  --resource-group "RG-Day23-Bicep" `
  --template-file "main.bicep"

  What-if deployment

One of the most valuable features for production safety:

az deployment group what-if `
  --resource-group "RG-Day23-Bicep" `
  --template-file "main.bicep"

Think:

Bicep
 ↓
What-if
 ↓
"What would change?"
 ↓
Review
 ↓
Deploy

This is extremely useful before modifying production infrastructure.

Why what-if matters

Imagine production already contains:

VNet
VM
Storage
Key Vault

You modify your Bicep file.

Before deploying:

what-if

can help you see the expected changes.


Bicep vs Terraform

Bicep
Microsoft
 ↓
Azure-focused
 ↓
Declarative IaC
Terraform
HashiCorp
 ↓
Provider-based
 ↓
Multi-cloud
 ↓
Declarative IaC


Terraform
 ↓
Azure
AWS
GCP
etc.


