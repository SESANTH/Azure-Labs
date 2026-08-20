# Day 20 — Azure Cost Management

## Objective

Learn how Microsoft describes Cost Management as a set of FinOps tools for analyzing, monitoring, and optimizing Microsoft Cloud costs.


## Concepts Learned

- Cost Management
- Cost Analysis
- Budgets
- Budget Alerts
- Actual Cost
- Forecasted Cost
- Cost Allocation
- Tags
- Pricing Calculator
- Azure Advisor
- Cost Optimization

## Hands-on Tasks

- Explored Cost Management
- Explored Cost Analysis
- Analyzed resource/service costs
- Created budget
- Configured budget alert
- Explored Azure Advisor
- Reviewed cost optimization recommendations

## Key Learnings

- Cost management should actually start before deployment, not after receiving the bill.
- Pricing Calculator gives an estimate, not necessarily your final invoice.
- Microsoft specifically recommends tagging as part of cost visibility and allocation.
- A budget does not automatically stop your resources.
- Budget alerts are for notification and tracking
- Budgets can be configured with actual-cost and forecasted-cost alerts.
- Azure offers reservations that can reduce costs compared with pay-as-you-go pricing.
- Savings Plans are another commitment-based optimization option for eligible compute usage.
- Advisor can identify opportunities such as underutilized resources. 


## Screenshots

See the `Images` directory.


Q1. What is Azure Cost Management?

Answer:

Azure Cost Management is a set of tools used to analyze, monitor, forecast, and optimize Microsoft Cloud spending.

Q2. What is Cost Analysis?

Answer:

Cost Analysis allows us to examine Azure spending and break it down by dimensions such as service, resource, resource group, subscription, location, and tags.

Q3. What is an Azure budget?

Answer:

A budget defines a spending threshold for a selected scope and can generate alerts when actual or forecasted costs reach configured thresholds.

Q4. Does exceeding a budget stop resources?

Answer:

No. A budget is primarily a monitoring and notification mechanism. Exceeding the threshold does not automatically stop resource consumption.

Q5. Actual cost vs forecasted cost?

| Actual                 | Forecast                      |
| ---------------------- | ----------------------------- |
| Cost already incurred  | Estimated future cost         |
| Based on current usage | Predicts future spending      |
| Shows what happened    | Helps predict what may happen |


Q6. How do tags help?

Answer:

Tags provide metadata such as environment, owner, application, or department that can help organize, analyze, and allocate costs.

Q7. How would you investigate an unexpected Azure bill?

Answer:

I would start with Cost Analysis, compare the current period with previous periods, identify the service with the largest increase, drill down to the resource, investigate usage and configuration changes, determine the cause, optimize the resource if appropriate, and then verify the cost trend.

Q8. What is Azure Pricing Calculator?

Answer:

It is a tool used to estimate the expected cost of Azure services before deploying a solution.

Q9. Azure Advisor vs Cost Management?

Answer:

Cost Management provides visibility and control over cloud spending through capabilities such as Cost Analysis and budgets. Azure Advisor provides recommendations that can help optimize resources, including cost optimization opportunities.

Q10. Give three ways to reduce Azure costs.

Answer:

Examples include:

Right-size overprovisioned resources.
Shut down or remove unused resources.
Use appropriate pricing/commitment options for predictable workloads.

Other possibilities include optimizing storage tiers and improving resource utilization.
