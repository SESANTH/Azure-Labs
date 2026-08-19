Azure Log Analytics

Why This Matters
Imagine your production application suddenly becomes slow.

You receive:

"Users are getting 500 errors."

You cannot simply look at the Azure Portal and guess.

You need evidence.

A cloud engineer might investigate:

User
 ↓
Application
 ↓
Azure Resource
 ↓
Logs
 ↓
Log Analytics Workspace
 ↓
KQL Query
 ↓
Find Error
 ↓
Identify Root Cause
 ↓
Fix
 ↓
Verify

This is why logging and querying are fundamental troubleshooting skills.

What Is Log Analytics?


What Is Log Analytics?
Simple explanation

Log Analytics is a service used to collect, store, search, and analyze log data using KQL.

The actual place where the logs are stored and queried is the:

Log Analytics Workspace

Think of it like a centralized investigation database.

Instead of checking logs independently on:

VM
App Service
NSG
Key Vault
Azure Activity Log
Container
Application

you can send relevant logs into a centralized workspace.

VM ───────────────┐
                  │
App Service ──────┤
                  │
Key Vault ────────┤
                  ↓
        Log Analytics Workspace
                  ↓
                 KQL
                  ↓
          Investigation


Log Analytics vs Azure Monitor

This distinction is very important for interviews.

Azure Monitor

Azure Monitor is the broader monitoring platform.

It deals with things such as:

Metrics
Logs
Alerts
Application monitoring
Resource monitoring


Log Analytics

Log Analytics is primarily the log analysis/querying capability, using a Log Analytics workspace and KQL.

Think:

Azure Monitor
│
├── Metrics
│
├── Alerts
│
├── Logs
│
└── Log Analytics
      │
      └── KQL

So don't say:

"Log Analytics and Azure Monitor are completely separate monitoring systems."

They work together.

Metrics vs Logs

| Metrics                               | Logs                     |
| ------------------------------------- | ------------------------ |
| Numerical data                        | Detailed records         |
| Time-series data                      | Individual/event records |
| CPU percentage                        | Error message            |
| Memory usage                          | Authentication event     |
| Request count                         | Configuration event      |
| Good for quick performance monitoring | Good for investigation   |

Metric
VM CPU = 92%

This tells you something is happening.

Log
Application failed to connect to database.
Connection timeout after 30 seconds.

This helps tell you why it happened.

Think:

Metric
 ↓
"Something is wrong"


Log
 ↓
"Why is it wrong?"

What Is a Log Analytics Workspace?

A Log Analytics Workspace is an Azure resource that provides a centralized environment for collecting and querying log data.

Example:

Subscription
│
├── VM
├── App Service
├── Key Vault
├── Storage
│
└── Log Analytics Workspace
          │
          ├── VM logs
          ├── Application logs
          ├── Activity logs
          └── Other diagnostic data

The workspace is therefore extremely useful for centralized troubleshooting.

KQL — Kusto Query Language

KQL is used to query log data in Azure Monitor / Log Analytics.

It looks somewhat like:

Table
| where condition
| project columns
| sort by column

For example:

AzureActivity
| where ActivityStatusValue == "Failed"

Read it as:

AzureActivity
      ↓
Find records
      ↓
where status = Failed

Important KQL Concepts

where

Filter records.

AzureActivity
| where ActivityStatusValue == "Failed"

Meaning:

Show only failed activity.

project

Choose columns.

AzureActivity
| project TimeGenerated, Caller, OperationNameValue

Meaning:

Show only these fields.

sort

Sort results.

AzureActivity
| sort by TimeGenerated desc

Meaning:

Show newest events first.

take

Limit results.

AzureActivity
| take 10

Meaning:

Show 10 records.

Your First KQL Query

Once we have logs available:

AzureActivity
| take 10

Then:

AzureActivity
| sort by TimeGenerated desc
| take 20

Then:

AzureActivity
| where ActivityStatusValue == "Failed"

Then:

AzureActivity
| where ActivityStatusValue == "Failed"
| project TimeGenerated, Caller, OperationNameValue
| sort by TimeGenerated desc

Notice the progression:

All records
   ↓
Latest records
   ↓
Failed records
   ↓
Relevant columns
   ↓
Newest first

This is how an engineer gradually narrows an investigation.

The engineer doesn't just ask:

"Is the application broken?"

They ask:

What resource?
     ↓
What event?
     ↓
When?
     ↓
Who?
     ↓
What operation?
     ↓
Success or failure?
     ↓
What error?
     ↓
What dependency?

That is the troubleshooting mindset we are building.

Symptom
   ↓
Identify resource
   ↓
Identify time
   ↓
Check metrics
   ↓
Check logs
   ↓
Filter relevant events
   ↓
Identify caller
   ↓
Identify operation
   ↓
Identify error
   ↓
Check dependencies
   ↓
Fix
   ↓
Verify



AzureActivity
| take 10
AzureActivity
| sort by TimeGenerated desc
| take 20
AzureActivity
| where ActivityStatusValue == "Failed"

