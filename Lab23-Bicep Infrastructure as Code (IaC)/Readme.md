# Day 23 — Bicep / Infrastructure as Code

## Objective

Learn how Instead of telling Azure what to do step-by-step, we describe the infrastructure we want Azure to have.

## Concepts Learned

- Infrastructure as Code
- Imperative vs Declarative
- Azure Resource Manager
- ARM Templates
- Bicep
- Resources
- Resource Types
- API Versions
- Parameters
- Variables
- Outputs
- Dependencies
- Modules
- Deployment Scope
- Validation
- What-if
- Bicep Deployment

## Bicep Files

See the `bicep` directory.

## Key Learnings

- Declarative Infrastructure as Code.

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is IaC?

Infrastructure as Code is the practice of defining and managing infrastructure using machine-readable configuration files rather than relying on manual configuration.

Q2. Why use IaC?

IaC provides:

repeatability
consistency
version control
automation
reviewability
reproducibility
Q3. What is Bicep?

Bicep is Microsoft's declarative language for defining Azure infrastructure as code.

Q4. What is ARM?

Azure Resource Manager is the management layer through which Azure resources are deployed and managed.

Q5. Bicep vs ARM?

Bicep provides a cleaner, more concise language for Azure IaC, while ARM templates use JSON.

Bicep is translated into an ARM deployment representation for Azure Resource Manager.

Q6. Bicep vs Azure CLI?

Azure CLI is a command-line management and automation tool.

Bicep is a declarative Infrastructure as Code language.

Azure CLI can be used to deploy Bicep templates.

Q7. Declarative vs imperative?

Imperative:

Tell the system what actions to perform.

Example:

Create VNet
Create subnet
Create VM

Declarative:

Describe the desired state.

Example:

I want:
VNet
Subnet
VM

The deployment system determines the necessary operations.

Q8. Parameter?

A parameter is an input value supplied to a Bicep deployment.

Q9. Variable?

A variable is a value defined within the Bicep template, often to simplify or reuse expressions.

Q10. Output?

An output exposes a value from the deployment after it completes.

Q11. dependsOn?

dependsOn explicitly defines a deployment dependency between resources when it cannot or should not be inferred automatically.

Q12. Why use what-if?

To preview the expected changes before deploying them, helping engineers identify unintended modifications.

Q13. Deployment fails?

Investigate:

Error
 ↓
Failed resource
 ↓
Error code/message
 ↓
Configuration
 ↓
Dependency
 ↓
Permissions
 ↓
Region/SKU/quota
 ↓
Fix
 ↓
Validate
 ↓
What-if
 ↓
Redeploy
Q14. Bicep vs Terraform?

Bicep is Azure-specific and closely integrated with Azure Resource Manager. Terraform is a multi-provider IaC platform that can manage Azure and other cloud/service providers.

Q15. Why Git?

Git provides:

version history
change tracking
code review
collaboration
rollback/reference to previous infrastructure definitions