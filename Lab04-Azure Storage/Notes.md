Azure Storage

Azure Storage is Microsoft's cloud storage service.

| Scenario               | Azure Storage Service |
| ---------------------- | --------------------- |
| Website images         | Blob Storage          |
| Employee shared folder | Azure Files           |
| Background processing  | Queue Storage         |
| NoSQL key-value data   | Table Storage         |
| VM backups             | Blob Storage          |
| Log files              | Blob Storage          |
| Application uploads    | Blob Storage          |


Storage Account
A Storage Account is the top-level container for Azure Storage services.

Storage Account

↓

Blob
Files
Queues
Tables



Subscription

↓

Resource Group

↓

Storage Account

↓

Blob Containers

↓

Files
Queues
Tables

Naming Rules

Storage Account names must:

Be globally unique
Contain only lowercase letters and numbers
Be between 3 and 24 characters
Have no spaces or special characters

Example:

✅ Good

sesanthstorage01



Types of Storage
1. Blob Storage

Blob = Binary Large Object

Used for:

Images
PDFs
Videos
ZIP files
Backups
Logs

2. Azure Files

Azure Files provides a shared file system.

Think of it like:

Office Shared Drive

\\Server\Shared

Azure:

Azure Files

↓

Shared Folder

↓

Employee Documents

Multiple VMs and users can mount the same file share.

When to Use Azure Files?
Shared company folders
Lift-and-shift migrations
Application shared storage


3. Queue Storage

Queue Storage stores messages.

Example:
Website uploads a file.
Instead of processing immediately:

Upload Request

↓

Queue

↓

Worker

↓

Processes Later

Useful for asynchronous processing.

4. Table Storage

Table Storage is a NoSQL key-value database.

Not like SQL.

Example:

PartitionKey	RowKey	Name
Employee	1	John
Employee	2	Alice

Useful for:

Metadata
Logging
Configuration
IoT data



Access Tiers

Azure Blob Storage has three tiers.

Hot Tier

Frequently accessed data.

Examples:

Website images
Active documents

Cost:

Higher storage cost
Lower access cost
Cool Tier

Occasionally accessed data.

Examples:

Monthly reports
Older documents

Cost:

Lower storage cost
Higher access cost
Archive Tier

Rarely accessed data.

Examples:

Legal records
Old backups
Compliance data

Cost:

Cheapest storage
Expensive retrieval
Retrieval can take hours

| Tier    | Access Frequency |
| ------- | ---------------- |
| Hot     | Daily            |
| Cool    | Monthly          |
| Archive | Yearly or Rarely |



Redundancy (High-Level)

Azure stores multiple copies of your data.

Common options:

LRS (Locally Redundant Storage) – 3 copies within one datacenter (lowest cost).
ZRS (Zone-Redundant Storage) – Copies across Availability Zones.
GRS (Geo-Redundant Storage) – Copies to another Azure region.

For learning, LRS is sufficient and keeps costs low.

Shared Access Signature (SAS)

Normally:

Blob

↓

Private

Only authorized users can access it.

Need to share a single file without making it public?

Use a SAS Token.

Example:

Resume.pdf

↓

Generate SAS

↓

Temporary Secure URL

The URL expires automatically based on the settings you choose.


Interview Questions
Q1. What is a Storage Account?

A Storage Account is the top-level Azure resource that provides access to Blob Storage, Azure Files, Queue Storage, and Table Storage. It defines configuration such as performance, redundancy, and networking.

Q2. Blob Storage vs Azure Files
Blob Storage	Azure Files
Stores unstructured objects	Shared file system
Accessed via REST APIs (HTTP/HTTPS)	Accessed via SMB/NFS
Ideal for images, videos, backups	Ideal for shared folders and legacy applications
Organized into containers	Organized into file shares

Q3. What is a SAS Token?

A Shared Access Signature (SAS) is a secure URL containing a token that grants limited access to storage resources for a specific time and with specific permissions, without exposing the storage account keys.

Q4. Explain Hot, Cool, and Archive Tiers
Tier	Best For
Hot	Frequently accessed data
Cool	Infrequently accessed data
Archive	Rarely accessed, long-term data

Q5. What is the difference between Queue Storage and Table Storage?
Queue Storage stores messages for asynchronous processing between application components.
Table Storage stores structured NoSQL data using PartitionKey and RowKey.


Best Practices
✅ Use LRS for labs to minimize costs.
✅ Keep Blob Containers Private unless public access is explicitly required.
✅ Use SAS Tokens instead of sharing storage account keys.
✅ Choose the correct access tier based on how often data is accessed.
✅ Organize blobs into logical containers such as documents, images, and backups.
