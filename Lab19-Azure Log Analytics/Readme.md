# Day 19 - Azure Log Analytics

## Objective

Learn how how Azure engineers collect, query, and investigate logs.

## Concepts Learned

- Log Analytics
- Log Analytics Workspace
- Azure Monitor
- Metrics vs Logs
- KQL
- Tables
- AzureActivity
- Log querying
- Centralized logging


## Hands-on Tasks

- Created Log Analytics Workspace
- Explored workspace
- Explored tables
- Queried AzureActivity
- Used KQL filters
- Investigated activity records

## Architecture

                         Internet
                            │
                            ↓
                         App Service
                            │
                    ┌───────┴────────┐
                    ↓                ↓
              Application        Key Vault
                  Logs                │
                    │                 │
                    └────────┬────────┘
                             ↓
                    Log Analytics
                       Workspace
                             │
                             ↓
                           KQL
                             │
                             ↓
                     Cloud Engineer


## Key Learnings

- Log Analytics is a service used to collect, store, search, and analyze log data using KQL.
- Azure Monitor is the broader monitoring platform.
- KQL is used to query log data in Azure Monitor / Log Analytics.


## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is Azure Log Analytics?

Answer:

Log Analytics is the log analysis capability of Azure Monitor that uses Log Analytics workspaces to collect and query log data using KQL.

Q2. What is a Log Analytics Workspace?

Answer:

A Log Analytics Workspace is a centralized Azure resource used to store and query log data collected from supported Azure resources and services.

Q3. What is KQL?

Answer:

Kusto Query Language is the query language used to search, filter, analyze, and investigate log data in Azure Monitor Log Analytics.

Q3. What is KQL?

Answer:

Kusto Query Language is the query language used to search, filter, analyze, and investigate log data in Azure Monitor Log Analytics.

Q4. Metrics vs Logs?

Answer:

Metrics are numerical time-series measurements such as CPU utilization, while logs contain detailed records and events that are useful for investigation and troubleshooting.

Q5. Is Log Analytics the same as Azure Monitor?

Answer:

No. Azure Monitor is the broader monitoring platform. Log Analytics provides the workspace and log-querying capabilities used to analyze log data within Azure Monitor.

Q6. Why might a Log Analytics query return no data?

Answer:

Possible causes include:

No data is being sent to the workspace
Incorrect table
Incorrect time range
Incorrect filtering
Diagnostic/data collection configuration is missing
The relevant event hasn't occurred

