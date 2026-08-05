Microsoft Entra ID

│

├── Tenant
│
├── Users
│
├── Groups
│
├── Roles
│
├── Applications
│
└── Devices



What is a Tenant?

A Tenant is your organization's dedicated identity space in Microsoft Entra ID.

Example:

Company

↓

Microsoft Entra Tenant

↓

Users
Groups
Applications
Roles
Policies

One Tenant contains
Users
Groups
Applications
Service Principals
Security Policies
Devices

Azure Subscription vs Tenant

Many beginners confuse these.

Tenant

Identity Management

Contains

Users
Groups
Authentication
Roles
Subscription

Billing & Resources

Contains

Virtual Machines
Storage
Networks
Databases


Types of Users
Member

Internal employee.

Example:
developer@company.com

Guest
External user.
Example:
Vendor
Client
Consultant

Groups
Groups are collections of users.

Interview Questions

Q1. What is Microsoft Entra ID?

Microsoft Entra ID is Azure's cloud-based Identity and Access Management (IAM) service. It authenticates users, manages identities, and controls access to Azure resources and Microsoft cloud services.

Q2. User vs Group
User	Group
Individual identity	Collection of users
Has its own login	Does not sign in
Assigned permissions directly	Permissions assigned once and inherited by members
Q3. Tenant vs Subscription
Tenant	Subscription
Identity management	Resource management and billing
Stores users, groups, apps	Stores Azure resources
One tenant can have multiple subscriptions	Each subscription is linked to one tenant at a time
Q4. What is a Guest User?

A Guest User is an external identity invited into your Microsoft Entra tenant to collaborate without becoming a full internal member.

Best Practices
Use Security Groups instead of assigning permissions to individual users whenever possible.
Follow the least privilege principle—grant only the permissions users need.
Use Guest Users for external collaborators instead of sharing internal accounts.
Use consistent naming, such as Dev-Team, HR-Team, and Finance-Team, to make administration easier.


