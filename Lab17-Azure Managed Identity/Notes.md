How can an Azure resource securely access Key Vault without us putting a username, password, API key, or client secret into the application?

The answer is Managed Identity.

Azure Managed Identity 🔐

What Problem Does Managed Identity Solve?

Suppose your Flask application needs:

Database password
API key
Storage credential
Bad approach

You could create:

username = "appuser"
password = "SuperSecret123"

Then the application authenticates to Key Vault using those credentials.

But now you have another problem:

Where do you store the credentials used to access Key Vault?

You haven't solved the security problem.

You've just moved it.

Application
     │
     │ username/password
     ▼
   Key Vault

Where does the username/password live?

GitHub?
Environment variable?
Config file?
App Settings?

All of these create additional credential-management problems.


💡 Managed Identity Changes the Model

Instead:

App Service
     │
     │ "I am this Azure identity"
     ▼
Microsoft Entra ID
     │
     │ token
     ▼
Key Vault

There is no password that you have to create and manually manage for the application.

Azure manages the identity's credentials.

That's the key idea.

Managed Identity allows an Azure resource to authenticate to services that support Microsoft Entra authentication without you managing credentials in your application.

Day 2

We learned:

Microsoft Entra ID
       ↓
Identity

It answers:

Who are you?

Day 3

We learned:

RBAC
       ↓
Permissions

It answers:

What are you allowed to do?

Day 16

We learned:

Key Vault
       ↓
Secure secrets

Now Day 17 combines them:

App Service
    │
    │ has
    ▼
Managed Identity
    │
    │ authenticated by
    ▼
Microsoft Entra ID
    │
    │ RBAC permission
    ▼
Key Vault
    │
    ▼
Secret

This is the mental model


🧩 What Exactly Is a Managed Identity?

A Managed Identity is an identity in Microsoft Entra ID that is associated with an Azure resource.

Learning-AppService
       │
       ▼
Managed Identity
       │
       ▼
Microsoft Entra ID

Azure manages the identity's credentials.

Two Types of Managed Identity

There are two types you need to know.

Managed Identity
│
├── System-assigned
│
└── User-assigned

🟢 System-Assigned Managed Identity

This identity belongs to one Azure resource.

Example:

Learning-AppService
       │
       └── System Assigned Identity

The identity lifecycle is tied to the resource.

If the resource is deleted:

App Service
    ↓
Deleted
    ↓
System-assigned identity
    ↓
Deleted

Good for

When one resource needs its own identity.

For our lab:

Learning App Service
       ↓
System Identity
       ↓
Key Vault

🔵 User-Assigned Managed Identity

A User-Assigned Managed Identity is a separate Azure resource.

Think:

             User-Assigned Identity
                    │
             ┌──────┴──────┐
             ▼             ▼
        App Service       VM

Multiple Azure resources can use the same identity.

This is useful when several resources need the same identity.


For example:

Application
   ├── App Service
   ├── VM
   └── Function App
          ↓
Shared Managed Identity
          ↓
       Key Vault


System vs User Assigned

|                                | System-assigned               | User-assigned   |
| ------------------------------ | ----------------------------- | --------------- |
| Identity lifecycle             | Tied to resource              | Independent     |
| Can multiple resources use it? | No                            | Yes             |
| Created separately?            | No                            | Yes             |
| Deleted with resource?         | Yes                           | No              |
| Good for                       | Simple one-resource scenarios | Shared identity |
| Our lab                        | ✅                             | Later           |

Interview answer

System-assigned identity is tied to the lifecycle of an Azure resource, while user-assigned identity is an independent Azure resource that can be associated with multiple resources.



🐍 Flask Application Architecture

Day 12 application was:

Flask
  ↓
App Service

Now we're moving toward:

Flask
  ↓
DefaultAzureCredential
  ↓
Managed Identity
  ↓
Key Vault
  ↓
Secret

A typical Python application can use Azure's identity libraries to obtain credentials and the Key Vault SDK to access secrets.

Conceptually:

credential = DefaultAzureCredential()

client = SecretClient(
    vault_url=KEY_VAULT_URL,
    credential=credential
)

secret = client.get_secret("Lab-DatabasePassword")

The important thing is:

There is no password in this code.

The identity comes from the Azure environment.

🧠 What Is DefaultAzureCredential?

You don't need to memorize every authentication mechanism yet.

Understand the purpose.

DefaultAzureCredential provides a standard way for an application to obtain an Azure identity credential.

During local development, it can use developer credentials.

When running in Azure with Managed Identity enabled, it can use the Managed Identity.

Conceptually:

LOCAL
VS Code / Azure CLI
       ↓
Developer identity


AZURE
App Service
       ↓
Managed Identity

Same application code.

Different authentication environment.

This is extremely useful for real projects.



🔥 Why This Is Better

Without Managed Identity:

Application
   ↓
Client ID
   +
Client Secret
   ↓
Azure

Now you have to protect:

Client Secret

With Managed Identity:

Application
   ↓
Managed Identity
   ↓
Microsoft Entra ID

No manually managed application credential.

Managed Identity vs Service Principal

Service Principal
Think:

Application
   ↓
Service Principal
   ↓
Client ID
   +
Secret/Certificate
   ↓
Entra ID

You generally have credentials that you must manage.

Managed Identity

Azure Resource
      ↓
Managed Identity
      ↓
Entra ID

Simple interview answer

A Service Principal is an application identity whose credentials are typically managed by us, while Managed Identity provides an Azure-managed identity for supported Azure resources, reducing the need to store and rotate credentials ourselves.


🧠 Scenario Question

Suppose we have:

VM
App Service
Function App

and all three need access to:
Key Vault

Would you use:
Option A
Three system-assigned identities?

or
Option B
One user-assigned identity?

There isn't always one universal answer.

You need to consider:

Should they share the same permissions?
Should identity lifecycle be independent?
Do we want separate auditability?
Do they represent the same application?

If they represent one logical application and genuinely need identical permissions, a user-assigned identity can make sense.

If they should have independent identities/permissions, separate system-assigned identities are often preferable.

Security design matters more than memorizing "always use X."




🔍 Troubleshooting Lab

Now let's think like a Cloud Engineer.
Imagine your application returns:

403 Forbidden

when trying to access:
Lab-DatabasePassword

Don't immediately change random settings.

Follow the chain.

Step 1 — Is Managed Identity enabled?
App Service
 ↓
Identity
 ↓
System assigned = ON?
Step 2 — Is the correct identity being used?

Check:
Principal ID

Step 3 — Does the identity have the correct RBAC role?

Check Key Vault:
IAM
 ↓
Key Vault Secrets User
Step 4 — Is the role assigned to the correct identity?

Very common mistake:
Wrong App Service

or:

Wrong Managed Identity
Step 5 — Is the application requesting the correct secret?

Check:
Lab-DatabasePassword

vs:

Lab-DB-Password
Step 6 — Networking

Later we will also check:

Key Vault
 ↓
Networking
 ↓
Public/Private access

This becomes especially important when we introduce Private Endpoints.



Troubleshooting Mental Model


                 Request
                    │
                    ▼
             Is identity enabled?
                    │
                    ▼
             Who is the identity?
                    │
                    ▼
             Does it have RBAC?
                    │
                    ▼
             Correct scope?
                    │
                    ▼
          Correct Key Vault object?
                    │
                    ▼
             Network accessible?
                    │
                    ▼
                  Success


🖥️ CLI Exploration

Let's start making your CLI skills more practical.

Check your App Service:

az webapp identity show \
  --resource-group RG-Learning \
  --name <YOUR-APP-NAME>

This should expose identity information.

You can also inspect the Key Vault:

az keyvault show \
  --resource-group RG-Learning \
  --name <YOUR-KEY-VAULT-NAME> \
  --output table

And inspect role assignments for the identity.

First obtain the principal ID from:

az webapp identity show \
  --resource-group RG-Learning \
  --name <YOUR-APP-NAME> \
  --query principalId \
  --output tsv

Then:

az role assignment list \
  --assignee <PRINCIPAL-ID> \
  --all \
  --output table

Now you're doing something different from Day 8/15.

You're not just using CLI to create resources.

You're using it to inspect and troubleshoot state.

🔥 Important: CLI ≠ IaC

This:
az webapp identity show ...
is:
CLI-based resource management / inspection

It is not automatically Infrastructure as Code.

Compare:

CLI
 ↓
Imperative commands
"Do this"

versus:

Bicep
 ↓
Declarative definition
"This is the desired state"

and:

Terraform
 ↓
Declarative infrastructure configuration
