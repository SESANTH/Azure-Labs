Azure Policy
A rule system that controls and evaluates the configuration of Azure resources.

For example:

RULE:
Only allow resources in Central India


Developer
    ↓
Creates VM
    ↓
Location = Central India
    ↓
✅ Allowed

RBAC vs Policy

RBAC asks:
WHO can perform an action?

Example:
Developer
   ↓
Contributor role
   ↓
Can create VM


Azure Policy asks:

WHAT configuration is allowed or required?

Example:

Developer
   ↓
Contributor
   ↓
Can create VM
        ↓
BUT
        ↓
Policy says:
Only Central India
        ↓
VM in West US
        ↓
❌ DENIED

RBAC
=
Who can do something?

Azure Policy
=
What is allowed/required?

RBAC and Policy are not alternatives.

They work together.

User
 ↓
RBAC
 ↓
"Are you allowed to attempt this?"
 ↓
YES
 ↓
Azure Policy
 ↓
"Does this resource comply with organizational rules?"
 ↓
YES → deployment continues
NO  → policy effect applies


Azure Policy architecture

There are four concepts you need to understand first:

Policy Definition
       ↓
Policy Assignment
       ↓
Scope
       ↓
Resource evaluation
       ↓
Compliance

And:

Multiple Policy Definitions
          ↓
       Initiative
          ↓
   Policy Assignment


Policy Definition

A policy definition is the actual rule.

Example:

Only allow resources in Central India.

Conceptually:

IF
resource.location != "centralindia"


THEN
deny

A policy definition describes the compliance condition and the effect to take when the rule matches.


A simplified policy structure looks like:

{
  "properties": {
    "displayName": "Allowed locations",
    "mode": "Indexed",
    "parameters": {},
    "policyRule": {
      "if": {
        "field": "location",
        "notIn": [
          "centralindia"
        ]
      },
      "then": {
        "effect": "deny"
      }
    }
  }
}

Azure:

Policy Definition
       ↓
"The resource must have an Environment tag"
       ↓
Policy Assignment
       ↓
Apply it to RG-Production

The definition describes the rule; the assignment applies that definition to a scope.


Scope

Azure Policy can be assigned at different levels.

Conceptually:

Management Group
       ↓
Subscription
       ↓
Resource Group
       ↓
Resource

Initiative

Imagine you have:

Policy 1
Require tags


Policy 2
Allowed locations


Policy 3
Allowed VM SKUs


Policy 4
HTTPS required


Policy 5
Storage public access disabled

Assigning five policies individually can become annoying.

Instead:

                 Initiative
                    │
       ┌────────────┼────────────┐
       ↓            ↓            ↓
    Policy 1     Policy 2     Policy 3
    Tags         Location     VM SKU
       │            │            │
       └────────────┼────────────┘
                    ↓
              One Assignment

An initiative, also called a policy set definition, is a collection of policy definitions managed together.

Real-world example

"Production Security Baseline"

    ├── Require HTTPS
    ├── Require secure transfer
    ├── Audit public IPs
    ├── Require tags
    ├── Allowed locations
    └── Audit diagnostic settings


Parameters

Policy:
Allowed locations

Parameter:
allowedLocations

Assignment:
allowedLocations =
[
  "centralindia",
  "southindia"
]

Now the same policy can be assigned differently in different environments.

Development
→ Central India + South India

Production
→ Central India only

Effects — VERY IMPORTANT

The effect determines what Azure does when a policy evaluates a resource.

Microsoft currently documents effects including audit, deny, modify, deployIfNotExists, auditIfNotExists, append, and others.

Audit

Audit means:

Allow it, but report it as non-compliant.

Example:

Developer
   ↓
Creates VM
   ↓
Missing Environment tag
   ↓
Azure allows creation
   ↓
Policy evaluates
   ↓
❌ Non-compliant

This is useful when introducing governance.

Instead of immediately breaking deployments:

Phase 1
Audit


Phase 2
Fix violations


Phase 3
Deny

This is a very realistic production strategy.

Deny

Deny means:

Stop the operation if it violates the policy.

Example:

Policy:
Only allow Central India


Developer
    ↓
Deploy VM
    ↓
Location = East US
    ↓
Policy
    ↓
DENY
    ↓
Deployment fails

This is enforcement.

A built-in example is an allowed-locations policy that can deny resources outside permitted locations.

Audit vs Deny

| Effect | Resource creation | Compliance           |
| ------ | ----------------- | -------------------- |
| Audit  | Allowed           | Non-compliant        |
| Deny   | Blocked           | Cannot create/update |


Remember:
Audit tells you. Deny stops you.

Modify

Modify means Azure Policy can modify certain resource properties during evaluation.

Conceptually:

Developer creates resource
        ↓
Missing required configuration
        ↓
Policy Modify
        ↓
Azure adds/changes property

A common governance scenario is modifying tags.

Example:

Resource
Environment = missing
        ↓
Modify policy
        ↓

Environment = Production

Don't think of Modify as:

"Policy can magically fix anything."

It has specific supported operations and requirements.

DeployIfNotExists

This one is more advanced.

VM
 ↓
Policy checks
 ↓
Monitoring configuration missing
 ↓
DeployIfNotExists
 ↓
Deploy required configuration

DeployIfNotExists can trigger deployment/remediation behavior for related resources, and remediation of existing resources requires a remediation task.


The four effects

AUDIT
↓
"You're violating the rule."
But I'll allow it.

DENY
↓
"No."
Deployment blocked.

MODIFY
↓
"I'll modify the supported property."

DEPLOYIFNOTEXISTS
↓
"The required related configuration
is missing; deploy it."

Compliance

Now suppose you have:

100 resources

Policy says:

Every resource needs an Environment tag.

Azure evaluates them.

80 compliant
20 non-compliant

You can see this in Azure Policy's Compliance view.

Exemption

Sometimes a policy is correct globally but one resource has a legitimate exception.

:

Security testing team
   ↓
Temporary resource
   ↓
East US

You don't necessarily want to remove the policy.

Instead:

Policy Assignment
       ↓
Exemption
       ↓
Specific resource / scope

An exemption identifies a resource hierarchy or resource that should not be evaluated under that assignment.

Good governance means:

Don't delete the policy just because one legitimate exception exists.

Example company governance

Imagine your company says:

Rule 1

Only these regions:

centralindia
southindia
Rule 2

Every resource must have:

Environment
Owner
CostCenter
Rule 3

Certain expensive VM SKUs are prohibited.

Rule 4

Storage accounts must use secure transfer.

Rule 5

Production resources require monitoring.

You could organize this as:

Production Governance Initiative
│
├── Allowed locations
├── Required tags
├── Allowed VM SKUs
├── Secure storage configuration
└── Monitoring configuration

Then:

Initiative
    ↓
Assignment
    ↓
Production Subscription

That's how Azure Policy becomes an enterprise governance mechanism.


Troubleshooting — Policy

This is one of the most important parts of today.

Imagine:

Developer tries to create VM and receives an authorization/deployment error.

Don't immediately assume RBAC.

Use:

Deployment failed
       ↓
Was it RBAC?
       ↓
Was it Policy?
       ↓
Was it quota?
       ↓
Was it SKU?
       ↓
Was it region?




Deployment failed
       ↓
Read exact error
       ↓
Policy mentioned?
       ↓
YES
       ↓
Find assignment
       ↓
Find definition
       ↓
Check scope
       ↓
Check parameters
       ↓
Check effect
       ↓
Check resource property
       ↓
Fix configuration


Remediation

Suppose:

100 VMs

Policy says:

Required monitoring configuration must exist.

You discover:

70 compliant
30 non-compliant

You don't necessarily want to manually fix 30 VMs.

For policies using effects such as:

Modify
DeployIfNotExists

you can use remediation.


Azure documents remediation as the mechanism for correcting resources violating modify or deployIfNotExists; existing resources need a remediation task.

