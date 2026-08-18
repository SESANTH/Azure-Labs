Azure Key Vault

Where should an application store passwords, API keys, connection strings, and certificates?
Not here:

DB_PASSWORD = "MyPassword123"
OPENAI_API_KEY = "sk-xxxxx"

And definitely not here:

GitHub
└── config.py
    └── password


Instead:

Application
     │
     ▼
Microsoft Entra ID
     │
     ▼
Azure Key Vault
     │
     ├── Secrets
     ├── Keys
     └── Certificates

Azure Key Vault is designed to securely store and manage these objects.

1. What is Azure Key Vault?

Azure Key Vault is a managed service for securely storing:

Passwords
API keys
Connection strings
Cryptographic keys
Certificates

Microsoft describes Keys, Secrets and Certificates as the main Key Vault object types.

Think of it as:
                 Azure Key Vault
                       │
        ┌──────────────┼──────────────┐
        ▼              ▼              ▼
     Secrets          Keys       Certificates
        │              │              │
    Passwords       Encryption       TLS/SSL
    API Keys         Keys           Certs
    DB Strings


2. Why Not Store Secrets in Code?

Bad:

DATABASE_PASSWORD = "SuperSecret123"

Then:

git add .
git commit
git push

Now your password may be sitting permanently in Git history.

Even if you later delete the line, the secret may still exist in previous commits.


Better Architecture

GitHub
   │
   │ Application Code
   ▼
Azure App Service
   │
   │ Authenticate using identity
   ▼
Azure Key Vault
   │
   ▼
Secret

3. Key Vault Objects
Secret

A Secret stores sensitive values.

Examples:

DB_PASSWORD
API_KEY
CONNECTION_STRING
JWT_SECRET

Example:

Name:
DatabasePassword

Value:
<secret value>

The application retrieves it when needed.


Key Vault
│
├── Secrets
│     ├── DatabasePassword
│     ├── APIKey
│     └── ConnectionString
│
├── Keys
│     ├── EncryptionKey
│     └── SigningKey
│
└── Certificates
      ├── TLS Certificate
      └── Application Certificate


4. Key

A Key is primarily used for cryptographic operations.

Examples:

Encryption
Decryption
Signing
Verification

Conceptually:

Application
     │
     ▼
Key Vault
     │
     ▼
Cryptographic Key
     │
     ▼
Encrypt / Decrypt

Don't confuse:

Secret ≠ Key

A password is normally a secret.

An encryption key is a key.

Certificate

A certificate is used for identity and secure communications, commonly TLS/HTTPS.

For example:

Client
   │
   │ HTTPS
   ▼
Application
   │
Certificate
   ↓
Prove server identity

A Key Vault certificate is actually a more complex object involving certificate metadata, a key and a secret.

5. Secret Versions

This is another important concept.

Suppose we have:

Lab-DatabasePassword

Version 1:
Password123

Later we rotate it:
Password456

Key Vault can maintain versions.

Lab-DatabasePassword
│
├── Version 1
│     Password123
│
└── Version 2
      Password456

The application can use the current version, while older versions remain available according to Key Vault's versioning/recovery behavior.

Microsoft documents Key Vault object identifiers as including an object version, and secret versioning is an important part of managing credential rotation.

Why is this useful?

Imagine production:

Old DB password
      ↓
Application

You rotate the password:

New DB password
      ↓
Key Vault

You can transition applications without treating the credential as an entirely new configuration object.

This becomes much more important when we learn Managed Identity and application integration on Day 17.

6. 🛡️ Key Vault Security

WHO can access it?
        ↓
Azure RBAC

WHAT happens if it gets deleted?
        ↓
Soft Delete + Purge Protection

7. 🔐 Azure RBAC

We learned:

Who
 ↓
Can do what
 ↓
At what scope

Key Vault follows the same principle.

For example:

Developer
   │
   └── Key Vault Secrets User
              │
              ▼
        Read secrets

Another person might have:

Key Vault Administrator

which is much more powerful.

Microsoft recommends Azure RBAC as the authorization model for Key Vault; the legacy access-policy model still exists in some contexts, but RBAC is the recommended approach

8. 🗑️ Soft Delete

Imagine someone accidentally deletes:

Learning-KeyVault

Without recovery protection, that could be disastrous.

Soft delete gives you a recovery window.

Think:

DELETE
  │
  ▼
Soft Deleted
  │
  ├── Recover
  │
  └── eventually permanently removed

Microsoft describes soft delete similarly to a recovery/recycle-bin mechanism for Key Vault resources.

9. ☢️ Purge Protection

Now imagine an attacker does:

Delete Key Vault
      ↓
Soft Deleted
      ↓
Purge

Purge means:

Permanently destroy it.

Purge protection prevents permanent deletion during the retention period.

DELETE
  ↓
Soft Delete
  ↓
       ┌──────────────┐
       │ Recoverable  │
       └──────────────┘
              │
       Purge protection
              │
              ▼
   Permanent deletion blocked


Important:

Purge protection is effectively irreversible once enabled.

For today's learning vault, that's okay, but understand the consequence before enabling it.

