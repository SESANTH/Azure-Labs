Azure CLI

Why Azure CLI matters

Imagine you're managing:

50 VMs
10 Storage Accounts
20 App Services
5 VNets
15 Key Vaults

Doing everything manually through Portal becomes slow.

Instead:

Engineer
   ↓
Azure CLI
   ↓
Azure Resource Manager
   ↓
Azure Resources

You can automate repetitive operations.

For example:

az vm list
can inspect VMs.

az vm start
can start a VM.

az vm stop
can stop a VM.

Azure CLI
Engineer
   ↓
Command
   ↓
Azure CLI
   ↓
Azure

CLI commands can be:

interactive
scripted
automated
used in pipelines

Bicep
Bicep file
    ↓
Desired state
    ↓
Azure

Declarative Infrastructure as Code.

Terraform
Terraform configuration
       ↓
Desired infrastructure
       ↓
Terraform
       ↓
Azure

Declarative IaC.



az
 ↓
SERVICE
 ↓
COMMAND
 ↓
ARGUMENTS
 ↓
RESOURCE

Example:

az vm list --output table

az
 ↓
vm
 ↓
list
 ↓
--output table


Azure CLI command families

Examples:

az vm list
az storage account list
az group list
az webapp list
az network vnet list

az
├── group
├── vm
├── storage
├── webapp
├── network
├── keyvault
├── account
└── monitor


Azure CLI installation/check

On your Windows machine, open PowerShell:

az --version

Login

The most fundamental command:

az login

PowerShell
    ↓
az login
    ↓
Microsoft Entra ID
    ↓
Authentication
    ↓
Azure CLI session

This is authentication, not authorization.

Remember your Key Vault lesson:

Authentication
=
Who are you?


Authorization
=
What are you allowed to do?

What happens after az login?

You may have access to multiple subscriptions.

Check them:

az account list --output table

Output formats

This is very important for CLI work.

Azure CLI commonly supports output formats such as:

json
jsonc
table
tsv
yaml
yamlc

The most useful initially:

JSON
az vm list --output json

Good for:

scripts
automation
detailed inspection

Table
az vm list --output table

Good for humans.

Example:

Name           ResourceGroup    Location
-------------  ---------------  ----------
Learning-VM    RG-Learning      centralindia

TSV
az vm list --output tsv

Useful when feeding values into shell scripts.

az resource list `
  --resource-group "RG-Learning" `
  --output table

  What is this command doing?
az resource list --resource-group "RG-Learning" --output table

Break it down:

az
 ↓
resource
 ↓
list
 ↓
--resource-group
 ↓
RG-Learning
 ↓
--output
 ↓
table

Meaning:

List all Azure resources inside RG-Learning and display them as a table.


Query individual resource types
VMs
az vm list --output table
Storage Accounts
az storage account list --output table
Web Apps
az webapp list --output table
VNets
az network vnet list --output table
Public IPs
az network public-ip list --output table
NSGs
az network nsg list --output table


Resource Group commands

az
 ↓
group
 ├── create
 ├── list
 ├── show
 ├── update
 └── delete

 Create
az group create `
  --name "RG-CLI-Lab" `
  --location "centralindia"

  Verify
az group show `
  --name "RG-CLI-Lab" `
  --output table


  Create vs Show vs List

  create
=
Create something

show
=
Show one specific resource

list
=
Show multiple resources

update
=
Modify

delete
=
Remove

az group create
az group show
az group list
az group update
az group delete

Delete

When you're finished:

az group delete `
  --name "RG-CLI-Lab"


WARNING

Be extremely careful with:

delete

Especially:

resource group delete

because deleting a resource group can delete its resources.

VM operational commands

az vm
 ├── create
 ├── show
 ├── list
 ├── start
 ├── stop
 ├── restart
 ├── deallocate
 ├── delete
 └── resize

 Start
az vm start `
  --resource-group "RG-Learning" `
  --name "Learning-VM"
Stop
az vm stop `
  --resource-group "RG-Learning" `
  --name "Learning-VM"
Deallocate
az vm deallocate `
  --resource-group "RG-Learning" `
  --name "Learning-VM"


  A VM can be:

Running

or:

Stopped

or:

Deallocated

For Azure VM cost management, deallocation matters because compute billing can stop when the VM is deallocated, although attached resources such as disks can still incur charges.

Querying resource properties

Suppose:

az vm show `
  --resource-group "RG-Learning" `
  --name "Learning-VM"

You may receive a huge JSON response.

Instead, use queries.

Azure CLI supports JMESPath queries through:

--query

This is one of the most powerful CLI features.

Example --query

az vm list `
  --query "[].name" `
  --output table

Query Resource Groups
az group list `
  --query "[].name" `
  --output table

This returns only resource group names.

Query locations
az group list `
  --query "[].location" `
  --output table


Query + filtering

Example:

az vm list `
  --query "[?location=='centralindia'].name" `
  --output table


Azure CLI troubleshooting

Suppose:

"My VM isn't reachable."

Don't immediately restart it.

Use CLI.

Step 1 — Does VM exist?
az vm show `
  --resource-group "RG-Learning" `
  --name "Learning-VM" `
  --output table


Step 2 — Is it running?
az vm get-instance-view `
  --resource-group "RG-Learning" `
  --name "Learning-VM" `
  --output table

Step 3 — Check networking
VM
 ↓
NIC
 ↓
Private IP
 ↓
NSG
 ↓
Public IP

Now inspect the associated resources.


CLI + Networking

For example:

az network public-ip list --output table
az network nsg list --output table
az network vnet list --output table
az network nic list --output table

Now you can investigate Azure networking without opening five different Portal blades.


CLI + App Service

az webapp list --output table

Show one:

az webapp show `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp" `
  --output table

List deployment-related information:

az webapp deployment list-publishing-profiles `
  --resource-group "learning-webapp_group" `
  --name "learning-webapp"


CLI + Key Vault

az keyvault list --output table

Show a vault:

az keyvault show `
  --name "<vault-name>" `
  --output table

Then investigate:

Key Vault
   ↓
Identity
   ↓
RBAC
   ↓
Role
   ↓
Scope

Azure CLI help system

One of the most important skills:

az --help

You can also use:

az vm --help

and:

az vm create --help

az find

Azure CLI also provides:

az find "az vm"

or searches around commands.

Practical CLI workflow

1. az login
        ↓
2. az account list
        ↓
3. az account set
        ↓
4. az group list
        ↓
5. az resource list
        ↓
6. Inspect resource
        ↓
7. Query required property
        ↓
8. Modify if necessary
        ↓
9. Verify

Suppose you execute:

az vm show ...

and receive:

ResourceNotFound

Don't just retry.

Ask:

ResourceNotFound
      ↓
Is resource name correct?
      ↓
Is resource group correct?
      ↓
Is subscription correct?
      ↓
Is resource actually created?

This is why we learned:

az account show

and:

az resource list


Automation

Conceptually:

List VMs
   ↓
Filter Development
   ↓
Loop
   ↓
Stop each VM

This is automation.

Still:

Automation using CLI ≠ declarative IaC.


Commands Learned Today


az --version

az login

az account list

az account show

az account set

az group list

az group create

az group show

az group delete

az resource list

az vm list

az vm show

az vm get-instance-view

az vm start

az vm stop

az vm deallocate

az webapp list

az webapp show

az network vnet list

az network nsg list

az network public-ip list

az --help

az vm --help

az vm create --help

az find

--output
--query



