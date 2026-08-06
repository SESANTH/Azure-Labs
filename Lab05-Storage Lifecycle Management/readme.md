# Day 5 – Azure Storage Lifecycle Management

## Objective
Learn how Azure automatically manages Blob Storage using Lifecycle Management rules.

## Concepts Learned
- Lifecycle Management
- Access Tiers
- Lifecycle Rules
- Blob Filters
- Automatic Tiering
- Shared Access Signature (SAS)

## Hands-on Tasks
- Reviewed Blob access tiers
- Explored Blob properties
- Created a Lifecycle Management rule
- Configured automatic movement from Hot → Cool → Archive
- Generated a SAS Token

## Key Learnings
- Lifecycle Management automates storage optimization.
- Hot tier is for frequently accessed data.
- Cool tier is for infrequently accessed data.
- Archive tier is for long-term retention with infrequent access.
- SAS Tokens provide temporary secure access to blobs.

## Screenshots

## Interview Questions

### Why use Archive Storage?
Archive Storage minimizes storage costs for data that is rarely accessed, such as compliance records, historical backups, or legal documents. It has the lowest storage cost but requires rehydration before data can be read.