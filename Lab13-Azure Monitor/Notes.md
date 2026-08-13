Azure Monitor

Why Monitoring?

Imagine your website suddenly becomes slow.

Users report:

"The application isn't working."

How do you know:

Is the VM overloaded?
Is CPU at 100%?
Is memory full?
Is the web app down?
Did someone delete a resource?

Azure Monitor answers these questions.

What is Azure Monitor?

Azure Monitor is Azure's centralized monitoring service.

It collects:

Metrics
Logs
Events
Alerts
Diagnostics

Think of it as the dashboard for your Azure environment.

Azure Monitor Architecture

Azure Resources

↓

Azure Monitor

├── Metrics
├── Logs
├── Alerts
├── Activity Log
└── Insights

Metrics

Metrics are numerical values collected over time.

Examples:

CPU %
Memory Usage
Disk Read
Disk Write
Network In
Network Out
HTTP Requests

CPU
20%
↓
60%
↓
90%
↓
15%

Logs

Logs contain detailed event information.

VM Started
↓
User Logged In
↓
Application Error
↓
Disk Attached

Logs answer:  "What happened?"

Metrics vs Logs

| Metrics                | Logs              |
| ---------------------- | ----------------- |
| Numeric data           | Detailed records  |
| Near real-time         | Historical events |
| Performance monitoring | Troubleshooting   |
| Graphs                 | Search & analysis |

Activity Log

Examples:

VM Created
VM Deleted
NSG Modified
Role Assigned
Storage Created

It does not contain application logs.

Diagnosic Settings

Diagnostic Settings send logs and metrics to destinations like:

Log Analytics Workspace
Storage Account
Event Hub

Without Diagnostic Settings, some detailed resource logs are not retained long-term.

Alerts

Instead of watching Azure Portal all day:

CPU > 80%
↓
Send Notification

Alert Components

Condition
↓
Action Group
↓
Notification


Action Group

An Action Group defines what happens after an alert.

Examples:

Email
SMS
Push Notification
Webhook
Azure Function
Logic App


Best Practices
✅ Monitor both infrastructure (VMs) and platform services (App Service).
✅ Create alerts for CPU, memory (where available), and availability.
✅ Review Activity Logs regularly for auditing.
✅ Enable Diagnostic Settings for production workloads.
✅ Use meaningful alert names and appropriate thresholds to reduce false alarms.

