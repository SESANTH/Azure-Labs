# Lab 24 — Azure DevOps CI/CD

## Objective

Learn how Azure DevOps CI/CD — taking the infrastructure/application code we've been creating and starting to automate its delivery.

## Concepts Learned

- CI
- Continuous Delivery
- Continuous Deployment
- Azure DevOps
- Azure Repos
- Azure Pipelines
- YAML
- Pipeline
- Stages
- Jobs
- Steps
- Tasks
- Agents
- Agent Pools
- Microsoft-hosted Agents
- Self-hosted Agents
- Triggers
- Variables
- Service Connections
- Artifacts
- Pipeline Security
- Bicep CI/CD

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. What is CI/CD?

CI/CD is an automated software delivery approach where code is continuously integrated, tested, packaged, and delivered or deployed through automated pipelines.

Q2. CI vs Delivery vs Deployment
CI
↓
Build + Test


Continuous Delivery
↓
Build + Test + Prepare for release


Continuous Deployment
↓
Build + Test + Automatically deploy
Q3. Azure DevOps?

Azure DevOps is Microsoft's suite of development and delivery services, including Azure Repos, Azure Pipelines, Boards, Test Plans, and Artifacts.

Q4. Azure Pipelines?

Azure Pipelines is the CI/CD service used to automate build, test, and deployment workflows.

Q5. Agent?

An agent is the compute environment that executes pipeline jobs and steps.

Q6. Microsoft-hosted vs self-hosted?

Microsoft-hosted agents are managed by Microsoft and provide fresh VMs for jobs. Self-hosted agents are managed by the organization and provide more control but require maintenance.

Q7. Agent pool?

An agent pool is a collection of agents from which Azure Pipelines can obtain an agent to execute jobs.

Q8. Stage?

A stage is a logical grouping of jobs representing a major phase such as Build, Test, or Deploy.

Q9. Job?

A job is a collection of steps executed together on an agent.

Q10. Task?

A task is a predefined unit of pipeline functionality, such as running Azure CLI or Azure PowerShell.

Q11. YAML?

YAML is a human-readable configuration format used to define the pipeline workflow as code.

Q12. Service connection?

A service connection provides Azure DevOps with an authenticated connection to an external service such as an Azure subscription, allowing pipeline tasks to interact with that service.

Q13. Why not store secrets in YAML?

Because YAML is source-controlled and potentially visible to developers or attackers. Secrets should be stored using secure mechanisms such as secret variables, Key Vault, or secure identity/authentication mechanisms.

Q14. Artifact?

An artifact is a build output that can be stored and consumed by later deployment stages or jobs.

Q15. Deploy Bicep?

A typical approach:

Bicep
 ↓
Pipeline
 ↓
Azure CLI task
 ↓
Service connection
 ↓
validate
 ↓
what-if
 ↓
deploy
Q16. Why use what-if?

To preview infrastructure changes before deployment and reduce the risk of unintended modifications.