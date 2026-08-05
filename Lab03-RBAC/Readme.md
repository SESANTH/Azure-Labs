# Day 3 – Azure RBAC

## Objective
Learn how Azure controls access using Role-Based Access Control (RBAC).

## Concepts Learned
- Authentication vs Authorization
- RBAC
- Built-in Roles
- Scope
- Inheritance
- Least Privilege

## Hands-on Tasks
- Explored Access Control (IAM)
- Assigned Reader role to HR
- Assigned Contributor role to Developers group
- Verified role assignments
- Reviewed built-in roles

## Key Learnings
- RBAC controls authorization.
- Reader can only view resources.
- Contributor can manage resources but cannot assign permissions.
- Owner has full control, including assigning roles.
- Permissions are inherited based on scope.

## Screenshots

## Interview Questions

### Authentication vs Authorization
Authentication verifies identity. Authorization determines what actions the identity is allowed to perform.

### Reader vs Contributor vs Owner
Reader can only view resources.
Contributor can manage resources but cannot assign roles.
Owner has full resource management and can assign RBAC roles.