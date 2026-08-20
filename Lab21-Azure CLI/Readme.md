
# Day 21 — Azure CLI

## Objective

Learn how Azure CLI is used to create, manage, and automate Microsoft Azure resources through a terminal

## Concepts Learned

- Azure CLI
- az login
- Subscription context
- Command groups
- create
- show
- list
- update
- delete
- Output formats
- --query
- JMESPath
- CLI troubleshooting
- CLI automation
- CLI vs Portal
- CLI vs IaC

## Hands-on Tasks

- Logged into Azure
- Listed subscriptions
- Selected subscription
- Listed resource groups
- Listed resources
- Inspected VM
- Queried VM names
- Created temporary Resource Group
- Verified Resource Group
- Deleted Resource Group
- Verified deletion

## Key Learnings

- Azure CLI is primarily a command-based management and automation tool.
- --query uses JMESPath expressions to filter or extract specific information from Azure CLI output.
- CLI It allows engineers to manage, inspect, troubleshoot, automate, and script Azure resources efficiently without relying entirely on the Portal.


## Screenshots

See the `Images` directory.

## Interview Questions
Q1. What is Azure CLI?

Azure CLI is Microsoft's cross-platform command-line interface for managing and querying Azure resources.

Q2. Is Azure CLI Infrastructure as Code?

No, not by itself.

Azure CLI is primarily a command-based management and automation tool. It can be used inside automation scripts and pipelines, but CLI commands themselves are not the same thing as declarative IaC such as Bicep or Terraform.

Q3. What does az account set do?

It selects the subscription that Azure CLI uses as the default context for subsequent commands.

show vs list?
show
 ↓
Specific resource


list
 ↓
Multiple resources

Example:

az vm show --resource-group RG-Learning --name Learning-VM

versus:

az vm list

Q5. What does --output table do?

It formats the command output as a human-readable table.

Q6. What is --query?

--query uses JMESPath expressions to filter or extract specific information from Azure CLI output.

Q7. Find all VMs?
az vm list --output table
Q8. Troubleshoot ResourceNotFound?

I would verify:

Resource name
 ↓
Resource group
 ↓
Active subscription
 ↓
Resource existence

using commands such as:

az account show
az resource list
az vm list

Q9. Current subscription?
az account show --output table
Q10. Why is CLI useful?

It allows engineers to manage, inspect, troubleshoot, automate, and script Azure resources efficiently without relying entirely on the Portal.

