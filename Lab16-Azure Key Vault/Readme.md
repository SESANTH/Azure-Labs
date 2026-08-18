# Day 16 - Azure Key Vault 🔐

## Objective

Learn why applications should never store secrets directly in code or configuration files, and how Azure Key Vault solves that problem.

## Concepts Learned

- Secrets
- Keys
- Certificates
- Secret versions
- RBAC
- Soft delete
- Purge protection

## Hands-on Tasks


- Created Keyvault
- Created keys
- Created Secrets
- Assigned role assignments permissions using IAM
- Created API key secret
- Rotated database password
- Created encryption key

## Architecture
                    Azure
                      │
             ┌────────▼────────┐
             │   Key Vault     │
             │ Learning-KeyVault│
             └────────┬────────┘
                      │
          ┌───────────┼───────────┐
          ▼           ▼           ▼
       Secrets       Keys      Certificates
          │
          ▼
   Password / API Key /
   Connection String


## Key Learnings

- A cryptographic key is used for cryptographic operations.
- A certificate is used for identity and secure communications, commonly TLS/HTTPS.
- Microsoft describes soft delete similarly to a recovery/recycle-bin mechanism for Key Vault resources.
- Purge protection prevents permanent deletion during the retention period.


## Screenshots

See the `Images` directory.

## Interview Questions


1. What is Azure Key Vault?

Microsoft Azure Key Vault is a managed service for securely storing and controlling access to sensitive information used by applications and Azure resources.

Think of it as:

A secure digital locker for secrets, encryption keys, and certificates.

Instead of putting sensitive information inside your application code, you store it in Key Vault and let authorized applications retrieve it when needed.

Key Vault can store
🔐 Secrets — passwords, connection strings, API keys, tokens
🔑 Keys — cryptographic keys used for encryption/signing
📜 Certificates — TLS/SSL certificates and their associated secrets/keys

Example:

App Service
    │
    │ "Give me DB password"
    ↓
Azure Key Vault
    │
    │ DB_PASSWORD
    ↓
Application
    │
    ↓
Azure SQL Database

The application doesn't need to have the actual password hardcoded in its source code.

2. Why shouldn't you store passwords directly in application source code?

Suppose you write:

DB_PASSWORD = "MySuperSecretPassword123"

This is dangerous.

Problem 1 — Git

If you commit it:

git add .
git commit
git push

the password may end up in Git history.

Even if you later delete it from the current file, the old commit may still contain it.

Problem 2 — Developers can see it

Anyone with access to the repository could potentially see the credential.

Problem 3 — Rotation becomes painful

Suppose the password changes:

Old password → New password

You now have to modify code/configuration and potentially redeploy the application.

Problem 4 — Secret duplication

The same password might end up in:

Source code
Git
CI/CD pipeline
App configuration
Developer machines
Docker image
Logs

That's a security nightmare.

Better architecture
                 ┌───────────────┐
                 │   Key Vault   │
                 │               │
                 │ DB_PASSWORD   │
                 │ API_KEY       │
                 │ Certificates  │
                 └───────┬───────┘
                         │
                         │ secure access
                         ↓
                 ┌───────────────┐
                 │   App Service │
                 └───────┬───────┘
                         │
                         ↓
                    Database
3. What can Key Vault store?

There are three important object types:

Object	Purpose	Example
Secret	Store sensitive values	Password, API token
Key	Cryptographic operations	Encryption key
Certificate	TLS/SSL certificates	myapp.com certificate
Secret

A secret is basically a sensitive piece of data.

Examples:

DB_PASSWORD
API_KEY
StorageConnectionString
JWT_SECRET
Key

A key is a cryptographic key.

It can be used for operations such as:

Encryption
Decryption
Signing
Verification

For example:

Data
  ↓
Encryption Key
  ↓
Encrypted Data

The key isn't simply a password stored for your application to read.

Certificate

Certificates are commonly used for:

HTTPS
TLS
Authentication

For example:

https://myapp.com
        ↓
TLS Certificate
        ↓
Secure HTTPS connection
4. Difference between a secret and a key?

This is a very important interview distinction.

Secret

A secret is primarily data that you want to protect from unauthorized access.

Example:

DB_PASSWORD = "abc123"

Your application might retrieve the value.

Key

A cryptographic key is used to perform cryptographic operations.

Example:

Encryption Key
       ↓
Encrypt data
       ↓
Ciphertext
Simple memory trick

Secret = sensitive value

Key = cryptographic tool

Don't think:

"A key is just another password."

It isn't.

Practical
5. Your App Service needs a database password. Where would you store it?

Azure Key Vault.

For example:

Azure SQL
   ↑
   │ password
   │
Key Vault
   ↑
   │ secure retrieval
   │
App Service

A good production architecture would be:

App Service
     │
     │ Managed Identity
     ↓
Azure Key Vault
     │
     │ DB password
     ↓
Application
     │
     ↓
Azure SQL
Important concept: Managed Identity

Ideally, you don't want to store a Key Vault username/password inside the App Service either.

Instead:

App Service
     │
     │ Managed Identity
     ↓
Key Vault

Azure identifies the App Service automatically.

Then you grant that identity permission to read the required secret.

This is a major cloud-security pattern:

Don't store credentials to access your credential store. Use identity.

6. A developer receives Forbidden while reading a Key Vault secret. What would you troubleshoot?

This is where your recent RBAC learning connects directly to Key Vault.

403 Forbidden generally means:

The request reached Key Vault, but the caller isn't authorized to perform that operation.

I would troubleshoot in this order.

Step 1 — Who is making the request?

Is it:

Developer user?
App Service managed identity?
VM managed identity?
Service principal?
Pipeline identity?

This is extremely important.

You must identify the actual security principal.

Step 2 — Does that identity have the correct Key Vault role?

For example, if the application needs to read secrets, it needs an appropriate data-plane role such as:

Key Vault Secrets User

The role must be assigned to the actual identity making the request.

Example:

App Service
     │
     │ Managed Identity
     ↓
Principal ABC
     │
     │ Key Vault Secrets User
     ↓
Key Vault
Step 3 — Check the scope

The role could be assigned at:

Key Vault
Resource Group
Subscription

depending on the assignment.

Make sure the assignment actually covers the Key Vault.

Step 4 — Check whether the Key Vault uses Azure RBAC

Key Vault supports:

Azure RBAC

and historically:

Access Policies

If the vault is configured for RBAC, don't expect a legacy access-policy assignment to solve the problem.

Step 5 — Check network restrictions

Even with the correct RBAC permission, networking can block access.

Check:

Key Vault → Networking

Potential restrictions include:

Public network access
Firewall rules
Private endpoint
Virtual network restrictions

So your troubleshooting model is:

                403
                 │
        ┌────────┴─────────┐
        ↓                  ↓
   Authorization        Network
        │                  │
   Who are you?       Can you reach KV?
   What role?         Firewall?
   Correct scope?     Private endpoint?
   RBAC or policy?
7. Why would you enable soft delete?

Soft delete protects accidentally deleted Key Vault objects.

Suppose you have:

DB_PASSWORD

Someone accidentally deletes it.

Without recovery protection, that can become a serious problem.

With soft delete:

Delete secret
      ↓
Secret becomes deleted/recoverable
      ↓
Can recover it during retention period

So:

Soft delete = protection against accidental deletion.

It's particularly important for production environments.

8. What does purge protection do?

This is slightly different.

Soft delete

Protects against:

Accidental deletion

Purge protection

Protects against:

Permanent destruction of a deleted object during its retention period

Think:

Normal delete
     ↓
Soft deleted
     ↓
Recoverable

But someone might try:

Soft deleted
     ↓
PURGE
     ↓
Gone permanently

Purge protection prevents that permanent purge during the retention period.

Memory trick

Soft delete = "I can recover it."

Purge protection = "You can't permanently destroy it yet."

Comparison
9. Key Vault vs Azure Storage — when would you use each?

These services solve completely different problems.

	Key Vault	Azure Storage
Main purpose	Secrets/security	Data storage
Passwords	✅	❌ Not appropriate
API keys	✅	❌
Encryption keys	✅	❌
Certificates	✅	❌
Images	❌	✅
Videos	❌	✅
Documents	❌	✅
Backups/files	❌	✅
Blob data	❌	✅
Example

Your application is a restaurant application.

You have:

Restaurant images
Menu PDFs
User-uploaded files

→ Azure Storage

But:

Database password
Payment API key
JWT signing secret
TLS certificate

→ Azure Key Vault

Think:

Storage = application data

Key Vault = application secrets/cryptographic material

10. Key Vault secret vs environment variable?

This is another important distinction.

Environment variable

You could configure:

DB_PASSWORD=abc123

and the application reads:

os.getenv("DB_PASSWORD")

That's convenient, but the environment variable itself is not a dedicated secret-management system.

It may potentially be exposed through:

Application configuration
Deployment systems
Process inspection
Logs/debugging
Misconfigured environments
Key Vault

The secret is stored centrally:

Key Vault
   │
   │ secret
   ↓
Application

with:

Identity
RBAC
Auditing
Versioning
Rotation
Security controls
Best practice

Environment variables aren't automatically "bad."

For example:

APP_ENV=production
PORT=8080
LOG_LEVEL=info

are perfectly reasonable environment variables.

For sensitive credentials:

DB_PASSWORD
API_SECRET
PRIVATE_KEY

prefer a proper secret-management system such as Key Vault.

11. Key Vault RBAC vs legacy access policies?

This is particularly relevant to the permission problem you've been working through.

Legacy Access Policies

Older Key Vault authorization model.

You explicitly configure something like:

User / Service Principal
        ↓
Access Policy
        ↓
Secret permissions

Example:

Get
List
Set
Delete
Azure RBAC

Modern Azure authorization approach.

You assign Azure roles:

Identity
   ↓
Role Assignment
   ↓
Key Vault

For example:

Key Vault Secrets User

The big advantage is consistency with the broader Azure RBAC model you've already learned.

Simple comparison
	Access Policies	Azure RBAC
Model	Key Vault-specific	Azure-wide authorization model
Role assignments	❌	✅
Centralized IAM approach	Limited	✅
Modern recommendation	Legacy	Generally preferred
Granular permissions	Yes	Yes, through roles
Important

You shouldn't blindly mix the mental models.

When troubleshooting:

First determine which Key Vault permission model is enabled.

Then troubleshoot authorization using that model.

12. Soft delete vs purge protection?

This is worth memorizing exactly.

	Soft Delete	Purge Protection
Protects against	Accidental deletion	Permanent destruction
Object after deletion	Recoverable	Remains protected from purge
Can recover?	✅	✅
Prevents purge?	Not necessarily	✅
Main purpose	Recovery	Stronger deletion protection
Example

Imagine:

Key Vault
   │
   └── DB_PASSWORD

Someone deletes it.

Soft delete
DB_PASSWORD
     ↓
DELETE
     ↓
Soft Deleted
     ↓
RECOVER
Purge protection
DB_PASSWORD
     ↓
DELETE
     ↓
Soft Deleted
     ↓
PURGE attempt
     ↓
❌ Blocked during retention period

So remember the hierarchy:

                Key Vault
                    │
          ┌─────────┴─────────┐
          ↓                   ↓
     Soft Delete        Purge Protection
          │                   │
     Recover deleted      Prevent permanent
        objects             destruction
