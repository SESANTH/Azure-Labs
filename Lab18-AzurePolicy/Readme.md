# Day 18 - Azure Policy

## Objective

Learn how Azure Policy lets you define rules that evaluate Azure resources and apply effects such as auditing or denying non-compliant configurations.

## Concepts Learned

- RBAC vs Policy
- Policy Definition
- Policy Definition vs Assignment
- Scope
- Initiative
- Parameters
- Effects
- Audit vs Deny
- Modify
- DeployIfNotExists
- Compliance
- Exemption
- Remediation


## Hands-on Tasks

- Created Policy
- Assigned the policy
- Assigned Parameters
- Created the assignment
- Tested with a new resource

## Architecture

                 AZURE POLICY
                      │
          ┌───────────┴───────────┐
          ↓                       ↓
   Policy Definition          Initiative
          │                       │
          └───────────┬───────────┘
                      ↓
                  Assignment
                      ↓
                    Scope
                      ↓
                 Resource
                      ↓
                 Evaluation
                      ↓
        ┌─────────────┼─────────────┐
        ↓             ↓             ↓
      Audit          Deny        Modify /
                                  Deploy
                      ↓
                  Compliance
                      ↓
               Remediation /
                Exemption

RBAC
↓
WHO can do it?

POLICY
↓
WHAT configuration is allowed?

SCOPE
↓
WHERE does the rule apply?

EFFECT
↓
WHAT happens when it matches?

COMPLIANCE
↓
ARE resources following the rule?

## CLI commands

# List policy definitions
az policy definition list --output table

# Show a policy definition
az policy definition show --name "<policy-name>"

# List policy assignments
az policy assignment list --output table

# Show an assignment
az policy assignment show --name "<assignment-name>"

# Check policy states
az policy state list --output table

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is Azure Policy?

Answer:

Azure Policy is an Azure governance service used to enforce organizational rules and evaluate whether Azure resources comply with required configurations.

Q2. What is the difference between RBAC and Azure Policy?

Answer:

RBAC controls who can perform actions on Azure resources.

Azure Policy controls what resource configurations are allowed or required.

Example:

RBAC
Developer can create VM.


Policy
VM must be in Central India.


Result:
Developer can create a VM,
but not an incorrectly configured VM.
Q3. What is a policy definition?

Answer:

A policy definition contains the rule that determines whether a resource is compliant and the effect that should occur when the rule matches.

Conceptually:

IF condition
THEN effect
Q4. What is a policy assignment?

Answer:

A policy assignment applies a policy definition to a particular scope, such as a management group, subscription, resource group, or resource.

Q5. What is an initiative?

Answer:

An initiative, or policy set definition, is a collection of related policy definitions that can be managed and assigned together.

Q6. What is the difference between Audit and Deny?

Answer:

Audit
→ Allows the operation
→ Reports non-compliance


Deny
→ Blocks the operation
Q7. What does Modify do?

Answer:

Modify allows Azure Policy to change supported resource properties when a policy condition is met, such as adding or modifying tags.

Q8. What is DeployIfNotExists?

Answer:

It allows Azure Policy to deploy a related resource or configuration when the required resource/configuration doesn't exist and the policy conditions are met.

Q9. What is compliance?

Answer:

Compliance represents whether resources evaluated by an Azure Policy assignment satisfy the policy requirements.

Q10. Why would you use an exemption?

Answer:

When a specific resource or scope has a legitimate reason to be excluded from a policy requirement without removing or weakening the policy for everyone else.