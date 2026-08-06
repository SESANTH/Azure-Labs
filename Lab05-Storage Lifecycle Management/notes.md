What is Lifecycle Management?

Lifecycle Management automatically moves or deletes blobs based on rules you define.
Instead of manually managing thousands of files, Azure does it for you.

Azure automatically moves older files to cheaper storage.

Why Lifecycle Management?
Imagine a company stores CCTV footage.

Every day:
500 GB
After one year:
180+ TB
Keeping all of it in the Hot tier would be very expensive.

Instead:
0-30 Days
↓
Hot
31-90 Days
↓
Cool
90+ Days
↓
Archive
Huge cost savings.

Blob Access Tier Review

Yesterday we learned:

1.Hot
Frequently accessed
Examples:
Website images
Current project files
Active reports
Highest storage cost
Lowest access cost

2.Cool
Occasionally accessed
Examples:
Monthly reports
Older backups
Archived project documents
Lower storage cost
Higher access cost

3.Cold
4.Archive
Rarely accessed
Examples:
Legal records
Compliance documents
Five-year-old backups
Lowest storage cost
Highest retrieval cost
Retrieval takes hours.

How Lifecycle Rules Work ?
Azure checks blobs every day.
If
Blob Age > 30 Days
↓
Move to Cool

Rule Components

A Lifecycle Rule contains:
Scope
Filters
Conditions
Actions
Filters

Lifecycle Rules can filter by:
Container
Blob Prefix
Blob Type
Example:
Only
documents/

Actions
Azure can:
Move to Cool
Move to Archive
Delete Blob
Delete Blob Versions
Delete Snapshots

Why Not Archive Everything?
Archive storage is cheap.

So why not put everything there?
Because:

Archive retrieval:
Hours

Additional retrieval charges
Not suitable for frequently accessed files.

Difference Between Manual Tier Change and Lifecycle

Manual
You change tier yourself.

Lifecycle
Azure changes it automatically.


Interview Questions
Q1. What is Azure Lifecycle Management?

Azure Lifecycle Management automatically transitions blobs between access tiers or deletes them based on rules you define, helping reduce storage costs.

Q2. Why use Archive Storage?

Archive Storage is intended for long-term retention of data that is rarely accessed. It offers the lowest storage cost but has higher retrieval costs and requires time to rehydrate data before access.

Q3. When should you use Hot, Cool, and Archive tiers?
Tier	Best Use Case
Hot	Frequently accessed files
Cool	Occasionally accessed files
Archive	Rarely accessed, long-term retention
Q4. Can Lifecycle Management delete blobs?

Yes. Lifecycle Management can move blobs between tiers and delete blobs, snapshots, or versions when they meet the configured conditions.

Q5. Does Lifecycle Management move files immediately?

No. Azure evaluates lifecycle rules periodically (typically once per day). New files won't move immediately just because a rule exists.

Best Practices
✅ Keep active application data in the Hot tier.
✅ Move infrequently accessed data to Cool.
✅ Archive only data that can tolerate retrieval delays.
✅ Test lifecycle rules on a non-production storage account first.
✅ Use SAS Tokens instead of making containers public.