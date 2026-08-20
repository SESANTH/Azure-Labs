# Lab 22 — PowerShell

## Objective

Learn how to use PowerShell to operate, inspect, troubleshoot, and automate Azure environments

## Concepts Learned

- PowerShell
- Cmdlets
- Verb-Noun
- Objects
- Pipeline
- Where-Object
- Select-Object
- Sort-Object
- Variables
- Azure PowerShell
- Az module
- Connect-AzAccount
- Get-AzContext
- Azure resource management
- PowerShell automation
- Error handling
- PowerShell vs Azure CLI
- PowerShell vs IaC

## Screenshots

See the `Images` directory.


## Interview Questions

Q1. What is PowerShell?

PowerShell is a command-line shell and scripting language designed for administration and automation.

Q2. What is a cmdlet?

A cmdlet is a lightweight PowerShell command designed to perform a specific operation.

Examples:

Get-Process
Get-AzVM
New-AzResourceGroup
Q3. Verb-Noun?

PowerShell commands generally use:

Verb-Noun

For example:

Get-AzVM

where:

Get = operation
AzVM = Azure VM resource

Q4. Pipeline?

The pipeline uses | to pass objects from one command to another.

Example:

Get-AzVM |
Select-Object Name, Location


Q5. Where-Object?

It filters objects based on a condition.

Example:

Get-AzVM |
Where-Object {$_.Location -eq "centralindia"}

Q6. Select-Object?

It selects specific properties from objects.

Example:

Get-AzVM |
Select-Object Name, Location

Q7. What is an object?

An object is a structured piece of data containing properties and potentially methods.

PowerShell can directly manipulate those properties rather than treating everything as plain text.

Q8. What is Azure PowerShell?

Azure PowerShell is a collection of PowerShell cmdlets provided through the Az module for managing Azure resources.

Q9. Authentication?
Connect-AzAccount

Q10. Current context?
Get-AzContext

Q11. CLI vs PowerShell?

Azure CLI uses command-style syntax such as:

az vm list

Azure PowerShell uses PowerShell cmdlets such as:

Get-AzVM

PowerShell additionally provides the PowerShell object pipeline and scripting capabilities.

Q12. Is PowerShell automation IaC?

No.

A PowerShell script that creates or modifies Azure resources is generally imperative automation.

IaC tools such as Bicep and Terraform describe desired infrastructure declaratively.


