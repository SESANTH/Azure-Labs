PowerShell

PowerShell is a command-line shell and scripting language designed for system administration and automation.

You can use it to manage:

Windows
Linux
files
processes
services
networking
Azure
Microsoft 365
automation workflows

PowerShell vs Azure CLI

This distinction is important.

Azure CLI

az vm list
The CLI is command-oriented.

PowerShell

PowerShell uses cmdlets.

Example:
Get-AzVM

Mental model:

Verb
 ↓
Noun
Get
 ↓
AzVM

Meaning:

Get Azure Virtual Machines.

The PowerShell Verb-Noun model

PowerShell commands generally follow:

Verb-Noun

Examples:

Get-Process
Get-Service
Get-ChildItem
Get-Date
Set-Location
New-Item
Remove-Item

The verb tells you what operation you're performing.

The noun tells you what you're operating on.

For example:

Get-Process
 ↓    ↓
Verb  Noun

means:

Get processes.

Common PowerShell verbs

You'll frequently encounter:

Get
Set
New
Remove
Start
Stop
Restart
Enable
Disable
Update
Test

Get
 ↓
Read information

New
 ↓
Create

Set
 ↓
Configure/change

Remove
 ↓
Delete

Start
 ↓
Start something

Stop
 ↓
Stop something

First PowerShell commands

Run:

Get-Date

This returns the current date/time.

Then:

Get-Location

This tells you your current working directory.

Then:

Get-ChildItem

This lists files/directories.

Why aliases can be confusing

You might write:

ls

and think:

"I'm using Linux."

Not necessarily.

Inside PowerShell:

ls
 ↓
alias
 ↓
Get-ChildItem

PowerShell's biggest difference: Objects

Traditional shell commands often work heavily with text.
PowerShell works heavily with objects.

Imagine:

VM
├── Name
├── Location
├── ResourceGroup
├── HardwareProfile
├── StorageProfile
└── NetworkProfile

PowerShell can manipulate these properties directly.

Conceptually:

Azure resource
      ↓
PowerShell
      ↓
Object
      ↓
Properties
      ↓
Filter/select/sort

This is one reason PowerShell is powerful for administration.

Inspect an object's properties

Run:

Get-Process | Get-Member

The pipeline:

Get-Process
     ↓
   |
     ↓
Get-Member

means:

Get processes, then inspect what kind of objects they are and what properties/methods they expose.

The PowerShell pipeline

The pipeline is fundamental.

Symbol:

|

Example:

Get-Process | Where-Object CPU -gt 100

Think:

Get processes
     ↓
    |
     ↓
Filter them
     ↓
CPU > 100

So:

COMMAND
   |
COMMAND
   |
COMMAND

allows you to build operations step by step.

Pipeline example

Suppose:

Get-Process

returns 200 processes.

You want only processes whose name is:

chrome

You can filter:

Get-Process | Where-Object {$_.Name -eq "chrome"}

The important concept is:

$_

means:

The current object flowing through the pipeline.

So:

$_.Name

means:

The Name property of the current object.

Filtering

PowerShell comparison operators include:

-eq    Equal
-ne    Not equal
-gt    Greater than
-ge    Greater/equal
-lt    Less than
-le    Less/equal
-like  Pattern matching

Example:

Get-Process | Where-Object {$_.CPU -gt 100}

means:

Give me processes where CPU is greater than 100.

Select-Object

Suppose you have a huge object but only need:

Name
Id
CPU

Use:

Get-Process |
Select-Object Name, Id, CPU

This is similar to the Azure CLI:

--query

but the underlying model is different.

Sort-Object

You can sort results:

Get-Process |
Sort-Object CPU -Descending

Meaning:

Get processes
      ↓
Sort by CPU
      ↓
Highest first

Format-Table

For display:

Get-Process |
Format-Table Name, Id, CPU

This is primarily about presentation.

Don't confuse:

Select-Object

with:

Format-Table
Select-Object

Changes/selects the properties you want in the resulting objects.

Format-Table

Controls how objects are displayed.

This distinction becomes important in scripts.

PowerShell variables

Variables begin with:

$

Example:

$name = "Learning-VM"

Then:

$name

returns:

Learning-VM

Azure automation frequently uses variables.

Example:

$resourceGroup = "RG-Learning"
$vmName = "Learning-VM"

Now you can use:

$resourceGroup
$vmName

instead of repeatedly typing the values.

Why variables matter for automation

Instead of:

Get-AzVM -ResourceGroupName "RG-Learning" -Name "Learning-VM"

you can write:

$rg = "RG-Learning"
$vm = "Learning-VM"


Get-AzVM -ResourceGroupName $rg -Name $vm

Now your script becomes reusable.

For example:

Development
Production
Testing

can use the same script with different variables.

Azure PowerShell

Azure PowerShell

Now we move from general PowerShell to Azure.

Microsoft provides Azure PowerShell through the Az PowerShell module.

The command structure becomes:

Get-Az...
New-Az...
Set-Az...
Remove-Az...

For example:

Get-AzVM

means:

Get Azure VMs.


Azure CLI vs Azure PowerShell

| Task                 | Azure CLI         | Azure PowerShell         |
| -------------------- | ----------------- | ------------------------ |
| List VMs             | `az vm list`      | `Get-AzVM`               |
| List resource groups | `az group list`   | `Get-AzResourceGroup`    |
| Create RG            | `az group create` | `New-AzResourceGroup`    |
| Delete RG            | `az group delete` | `Remove-AzResourceGroup` |
| Azure authentication | `az login`        | `Connect-AzAccount`      |


The syntax is different, but the underlying Azure resources are the same.

Azure PowerShell login

Azure CLI:

az login

Azure PowerShell:

Connect-AzAccount

Conceptually:

PowerShell
    ↓
Connect-AzAccount
    ↓
Microsoft Entra ID
    ↓
Authentication
    ↓
Azure PowerShell session

Again:

Authentication
=
Who are you?

Check Azure context

After login:

Get-AzContext

This is extremely important.

It tells you the current Azure context.

Think of it as similar to:

az account show

Azure context

Your context contains information such as:

Account
Subscription
Tenant
Environment

Change subscription

You can list subscriptions:

Get-AzSubscription

Then select one:

Set-AzContext -Subscription "<subscription-name>"

Verify:

Get-AzContext

The equivalent CLI flow from yesterday was:

az account list
az account set
az account show

PowerShell:

Get-AzSubscription
Set-AzContext
Get-AzContext

Resource Groups

List resource groups:

Get-AzResourceGroup

Specific resource group:

Get-AzResourceGroup -Name "RG-Learning"

Create:

New-AzResourceGroup `
    -Name "RG-Day22-PS" `
    -Location "centralindia"

Verify:

Get-AzResourceGroup -Name "RG-Day22-PS"

CLI vs PowerShell — same operation
Azure CLI
az group create `
  --name "RG-Day22-PS" `
  --location "centralindia"
Azure PowerShell
New-AzResourceGroup `
    -Name "RG-Day22-PS" `
    -Location "centralindia"

Both eventually interact with Azure.

But the interfaces are different.

The important difference

CLI:

Command + arguments
       ↓
Output

PowerShell:

Cmdlet
  ↓
Object
  ↓
Pipeline
  ↓
Object manipulation

This is one of the biggest conceptual differences.

List Azure resources
Get-AzResource

Filter resources

Suppose you only want VMs.

Get-AzResource |
Where-Object {$_.ResourceType -eq "Microsoft.Compute/virtualMachines"}


Get-AzVM `
    -ResourceGroupName "RG-Learning" `
    -Name "Learning-VM"


Get-AzVM `
    -ResourceGroupName "RG-Learning" `
    -Name "Learning-VM" `
    -Status


Start / Stop VM

Start:

Start-AzVM `
    -ResourceGroupName "RG-Learning" `
    -Name "Learning-VM"

Stop:

Stop-AzVM `
    -ResourceGroupName "RG-Learning" `
    -Name "Learning-VM"

For cost optimization, you may use:

Stop-AzVM `
    -ResourceGroupName "RG-Learning" `
    -Name "Learning-VM" `
    -Force


List VNets:

Get-AzVirtualNetwork

Specific VNet:

Get-AzVirtualNetwork `
    -ResourceGroupName "RG-Learning"

List NSGs:

Get-AzNetworkSecurityGroup

Public IPs:

Get-AzPublicIpAddress

Network interfaces:

Get-AzNetworkInterface

Pipeline + Azure

Now combine Azure PowerShell with the pipeline.

For example:

Get-AzVM |
Select-Object Name, ResourceGroupName, Location

This gives a clean view.

Or:

Get-AzVM |
Where-Object {$_.Location -eq "centralindia"} |
Select-Object Name, ResourceGroupName, Location


Exporting results

You can export objects to CSV:

Get-AzVM |
Select-Object Name, ResourceGroupName, Location |
Export-Csv "vm-inventory.csv" -NoTypeInformation

Automation example

Imagine your company has development VMs tagged:

Environment = Development

You could build a script that:

Find VMs
 ↓
Identify Development
 ↓
Check state
 ↓
Stop/deallocate after working hours

Conceptually:

$developmentVMs = Get-AzVM |
    Where-Object {$_.Tags["Environment"] -eq "Development"}


foreach ($vm in $developmentVMs) {
    Stop-AzVM `
        -ResourceGroupName $vm.ResourceGroupName `
        -Name $vm.Name `
        -Force
}

Understand the script
Variable
$developmentVMs

stores the results.

Get
Get-AzVM

gets VMs.

Filter
Where-Object

selects only Development VMs.

Loop
foreach

processes each VM.

Action
Stop-AzVM

stops each VM.

So:

Get
 ↓
Filter
 ↓
Loop
 ↓
Action

This is the foundation of PowerShell automation.

PowerShell error handling

Production scripts need error handling.

Basic structure:

try {
    Get-AzVM `
        -ResourceGroupName "RG-Learning" `
        -Name "Learning-VM" `
        -ErrorAction Stop
}
catch {
    Write-Host "VM lookup failed"
}

Conceptually:

Try
 ↓
Execute
 ↓
Error?
 ↓
Catch
 ↓
Handle

This becomes important when automation is running without a human watching the terminal.

-ErrorAction

You'll frequently see:

-ErrorAction Stop

This tells PowerShell to treat a terminating error in the operation as a stopping error so it can be caught by catch.



Important interview distinction

Azure CLI

Best described as:

A cross-platform command-line tool for managing Azure.

Azure PowerShell

Best described as:

A PowerShell-based management and automation framework using Azure-specific cmdlets from the Az module.

Bicep

Declarative Azure Infrastructure as Code.

Terraform

Declarative Infrastructure as Code that can manage Azure and other providers.

Commands to remember

Get-Date

Get-Location

Get-ChildItem

Get-Process

Get-Command

Get-Member

Where-Object

Select-Object

Sort-Object

$variable

Connect-AzAccount

Get-AzContext

Get-AzSubscription

Set-AzContext

Get-AzResourceGroup

New-AzResourceGroup

Get-AzResource

Get-AzVM

Get-AzNetworkInterface

Get-AzNetworkSecurityGroup

Get-AzPublicIpAddress

Get-AzWebApp

Get-AzStorageAccount

Get-AzKeyVault

