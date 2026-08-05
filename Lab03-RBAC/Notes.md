Azure RBAC (Role-Based Access Control)

What is RBAC?

RBAC (Role-Based Access Control) is Azure's authorization system.

It determines:

Who can access a resource?
What actions can they perform?
Where can they perform those actions?

Authentication vs Authorization

This is a very common interview question.

Authentication
Who are you?

Example:
Username
Password
MFA

Handled by:
Microsoft Entra ID

Authorization

What are you allowed to do?

Example:

Read VM
Create VM
Delete VM

Handled by:
Azure RBAC


How RBAC Works
User

↓

Role Assignment

↓

Scope

↓

Azure Resource



RBAC Components

RBAC has three main components.

1. Security Principal

The identity receiving permissions.

Examples:

User
Group
Service Principal
Managed Identity

Example:

Developer User


2. Role Definition

Defines the permissions.

Examples:

Reader
Contributor
Owner
Virtual Machine Contributor
Storage Blob Data Reader

3. Scope

Where the permissions apply.

Example:

Subscription

↓

Resource Group

↓

Virtual Machine


| Permission       | Reader | Contributor | Owner |
| ---------------- | ------ | ----------- | ----- |
| View Resources   | ✅      | ✅           | ✅     |
| Create Resources | ❌      | ✅           | ✅     |
| Modify Resources | ❌      | ✅           | ✅     |
| Delete Resources | ❌      | ✅           | ✅     |
| Assign Roles     | ❌      | ❌           | ✅     |


Inheritance

Permissions flow downward.

Subscription

↓

Contributor

↓

RG-Learning

↓

Storage

↓

VM

↓

Network


Interview Questions
Q1. What is RBAC?

Azure Role-Based Access Control (RBAC) is Azure's authorization system. It controls who can access Azure resources, what actions they can perform, and at what scope those permissions apply.

Q2. Authentication vs Authorization
Authentication	Authorization
Verifies identity	Determines permissions
Managed by Microsoft Entra ID	Managed by Azure RBAC
Login process	Access control after login
Q3. Reader vs Contributor vs Owner
Reader	Contributor	Owner
View only	Manage resources	Full control
Cannot modify	Cannot assign roles	Can assign roles
Q4. What is Scope in RBAC?

Scope defines where a role assignment applies. Azure supports four levels:

Management Group
Subscription
Resource Group
Individual Resource

Permissions assigned at a higher scope are inherited by lower scopes.

Q5. Why use Groups instead of assigning roles to individual users?

Assigning roles to groups simplifies administration. New users only need to be added to the appropriate group to inherit the correct permissions, reducing manual effort and errors.