# Day 15 – Azure Managed Identity

## Objective

Learn How can an Azure resource securely access Key Vault without us putting a username, password, API key, or client secret into the application.

## Concepts Learned

- Managed Identity
- System-assigned vs User-assigned identity
- RBAC controls
- Service Principal
- Scope
- Role Assignment
- Secret Access Path
- DefaultAzureCredential

## Hands-on Tasks

- Enabled Managed Identity
- Provided Identity Permission to Key Vault
- Added role assignment
- Assigned permission at Key Vault
- Verified the Role Assignment


## Architecture

App Service
     │
     │ Managed Identity
     ▼
Microsoft Entra ID
     │
     │ identity/token
     ▼
Key Vault
     │
     │ RBAC says:
     │ "This identity can read secrets"
     ▼
Secret

## Key Learnings

- Managed Identity allows an Azure resource to authenticate to services that support Microsoft Entra authentication
- The identity lifecycle is tied to the resource.
- A User-Assigned Managed Identity is a separate Azure resource.
- Multiple Azure resources can use the same identity.
- The principal ID identifies the managed identity when you assign permissions.
- Give the minimum permissions at the smallest reasonable scope.

## Screenshots

See the `Images` directory.

## Interview Questions

What is Managed Identity?
Managed Identity is a feature of Microsoft Entra ID (formerly Azure AD) that provides Azure services with an automatically managed identity. It acts as an invisible service account that your application uses to securely connect to other Azure resources (like Key Vault, SQL databases, or Storage) without you having to manage any credentials.

Why do we use Managed Identity?

Zero Credential Management: You never have to handle, store, or rotate passwords, connection strings, or client secrets.

Enhanced Security: Credentials are automatically managed and rotated by Azure.

Reduced Risk: Developers cannot accidentally leak secrets in source control (like GitHub) because there are no secrets in the code.

Cost: It is completely free.


What are the two types?

System-assigned: Tied directly to a specific Azure resource (e.g., a single App Service).

User-assigned: Created as a standalone Azure resource and can be assigned to one or multiple Azure resources.

What happens to a system-assigned identity when its Azure resource is deleted?
The system-assigned identity is automatically permanently deleted from Microsoft Entra ID along with the resource.

Your App Service needs to read a Key Vault secret. What steps are required?

Enable Identity
Turn on System-assigned Managed Identity in the App Service settings. Azure generates an Object ID for the app in Entra ID.
↓

Grant Access
Go to the Key Vault. Assign the App Service's identity a role (e.g., Key Vault Secrets User via Azure RBAC) or add an Access Policy granting Get permissions for Secrets.
↓

Update Code
Use Azure SDKs (like DefaultAzureCredential in .NET) in your application code. The SDK automatically detects the environment and uses the Managed Identity to authenticate and fetch the secret.

Managed Identity is enabled but Key Vault returns 403. What do you check?

Missing Permissions: The identity was created, but it was never explicitly granted access (RBAC role or Access Policy) to the Key Vault.

Wrong Scope: You assigned a role for Keys or Certificates, but the app is trying to read a Secret.

Network Firewalls: Key Vault has a firewall enabled that is blocking the App Service's outbound IP addresses.

Propagation Delay: You just granted access, and Entra ID needs a minute or two to propagate the permissions.

Comparisons
System-assigned vs. User-assigned

Feature,System-assigned,User-assigned
Lifecycle,Shares the lifecycle of the Azure resource. Deleted when the resource is deleted.,Independent lifecycle. Must be explicitly deleted.
Relationship,1:1 (One resource has one identity).,1:N (One identity can be shared across multiple resources).
Best For,Workloads contained within a single Azure resource.,"Workloads that span multiple resources (e.g., a cluster of VMs that all need the same access)."

Managed Identity vs. Service Principal

Feature,Managed Identity,Service Principal
Secret Management,Fully automatic. Azure handles creation and rotation.,"Manual. You must generate, store, and rotate secrets/certificates safely."
Host Environment,Only works for applications hosted inside Azure.,"Works anywhere (on-premises servers, local dev machines, GitHub Actions, AWS)."

Managed Identity vs. App Settings (Client Secret)

Feature,Managed Identity,Storing Secret in App Settings
Security Risk,Very Low. No secrets exist to be stolen or leaked.,High. Anyone with read access to the App Service configuration can steal the secret and impersonate the app.
Maintenance,None.,High. Secrets expire and cause production outages if you forget to rotate and update the App Setting.

