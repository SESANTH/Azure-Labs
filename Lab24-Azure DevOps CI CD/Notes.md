Azure DevOps CI/CD

CI vs CD

Continuous Integration
Frequently integrate code changes into a shared repository and automatically build/test them.

Continuous Delivery
Code is automatically built, tested, and prepared for release, so it can be deployed reliably when approved.

Continuous Deployment

Continuous Deployment goes one step further:

Code
 ↓
Build
 ↓
Test
 ↓
Deploy automatically
 ↓
Production

No manual approval between the pipeline and production, assuming all configured checks pass.


CI/CD comparison

| Concept               | Meaning                                     |
| --------------------- | ------------------------------------------- |
| CI                    | Build + test code changes                   |
| Continuous Delivery   | Automatically prepare a releasable artifact |
| Continuous Deployment | Automatically deploy changes                |


What is Azure DevOps?

Azure DevOps is Microsoft's suite of development and delivery services.

It includes capabilities such as:

Azure DevOps
├── Azure Repos
├── Azure Pipelines
├── Azure Boards
├── Azure Test Plans
└── Azure Artifacts

Azure Repos

Azure Repos provides Git repositories.

Conceptually:

Developer
   ↓
Git
   ↓
Azure Repos

Azure Repos gives a team a centralized Git-based collaboration environment.

Azure Pipelines

Azure Pipelines is the CI/CD engine.


                  Developer
                      │
                      ▼
                  Git Push
                      │
                      ▼
               Azure Repos
                      │
                      ▼
               Azure Pipeline
                      │
          ┌───────────┴───────────┐
          ▼                       ▼
       Build                    Test
          │                       │
          └───────────┬───────────┘
                      ▼
                  Artifact
                      │
                      ▼
                 Deployment
                      │
                      ▼
                    Azure


What is a pipeline?

A pipeline is an automated workflow.

Pipeline
   │
   ├── Build
   ├── Test
   ├── Package
   └── Deploy


The pipeline defines:

What should happen, in what order, under what conditions.

YAML

Azure Pipelines commonly uses YAML.

Example:

trigger:
- main


pool:
  vmImage: 'ubuntu-latest'


steps:
- script: echo "Hello Azure DevOps"

What is an Agent?

An agent is the machine that actually executes pipeline jobs.


Pipeline
    ↓
Agent
    ↓
PowerShell
Bash
Python
Azure CLI
Bicep
Git
etc.

The pipeline itself is the workflow definition.

The agent is where the work happens.

Microsoft-hosted agents

Microsoft provides temporary machines for your pipeline jobs.

For example:

pool:
  vmImage: 'ubuntu-latest'

  Self-hosted agents

Instead of Microsoft providing the machine:

Your organization
      ↓
Own VM
      ↓
Azure DevOps Agent
      ↓
Pipeline

That's a self-hosted agent.

Microsoft-hosted vs self-hosted

| Microsoft-hosted           | Self-hosted                     |
| -------------------------- | ------------------------------- |
| Microsoft manages agent VM | You manage VM                   |
| Fresh environment          | Persistent environment possible |
| Easy setup                 | More setup                      |
| Standard tools             | Custom tools                    |
| Less maintenance           | More maintenance                |


Agent pools

An agent pool is a collection of agents.

Think:

Agent Pool
   │
   ├── Agent 1
   ├── Agent 2
   ├── Agent 3
   └── Agent 4

Azure Pipelines chooses an available agent according to the pipeline's configuration and available parallel capacity.

Stages

Large pipelines can be divided into stages.

For example:

Stage 1
Build
   ↓
Stage 2
Test
   ↓
Stage 3
Deploy

YAML:

stages:
- stage: Build
  jobs:
  - job: BuildJob
    steps:
    - script: echo "Building"

- stage: Deploy
  jobs:
  - job: DeployJob
    steps:
    - script: echo "Deploying"

Jobs

A stage contains jobs.

Example:

Stage
   │
   ├── Job A
   └── Job B

A job runs on an agent.

Steps

Steps are individual actions.

Example:

steps:
- script: echo "Build"
- script: echo "Test"
- script: echo "Package"

Tasks

Instead of writing shell commands yourself, Azure DevOps provides tasks.

Example conceptually:

- task: AzureCLI@2

This executes Azure CLI through a pipeline task.

Another example:

- task: AzurePowerShell@5

This executes Azure PowerShell.

Script vs Task
Script
- script: |
    echo "Hello"

You're directly executing commands.

Task
- task: AzureCLI@2

You're using a predefined Azure DevOps task.

Variables

Pipelines frequently use variables.

Example:

variables:
  environment: 'dev'

Then:

- script: echo $(environment)

Output:

dev

Variables help avoid hardcoding values throughout the pipeline.

Secrets

Never put:

password: "MyPassword123"

directly into Git.

Instead:

Secret
 ↓
Secure variable / Key Vault / service connection
 ↓
Pipeline

Service Connections

This is extremely important.

Your pipeline needs to authenticate to Azure.

You don't want:

Pipeline
 ↓
username
 ↓
password

Instead, Azure DevOps can use a service connection to establish an authenticated connection to Azure.

Why service connections matter

Imagine the pipeline needs to deploy:

Bicep
 ↓
Azure

The pipeline needs identity.

Without authentication:

Pipeline
 ↓
Azure
 ❌

With appropriate service connection:

Pipeline
 ↓
Service Connection
 ↓
Azure
 ✅

 But:

Authentication does not automatically mean permission.

The identity still needs appropriate Azure authorization.

This connects directly to your:

Authentication
      ↓
Authorization
      ↓
RBAC

model.


Production-style architecture

A simplified production pipeline:

Developer
   │
   ▼
Git Push
   │
   ▼
Azure Repos
   │
   ▼
CI Pipeline
   │
   ├── Restore dependencies
   ├── Build
   ├── Test
   └── Package
          │
          ▼
       Artifact
          │
          ▼
     Deployment Stage
          │
          ├── Bicep What-If
          │
          ▼
       Approval
          │
          ▼
     Azure Deployment


YAML pipeline example

Here's a very simple pipeline:

trigger:
- main

pool:
  vmImage: 'ubuntu-latest'

stages:

- stage: Build
  jobs:
  - job: Build
    steps:
    - script: |
        echo "Installing dependencies"
        echo "Running tests"
        echo "Creating artifact"

- stage: Deploy
  dependsOn: Build
  jobs:
  - job: Deploy
    steps:
    - script: |
        echo "Deploying application"



Triggers

Triggers determine:

When should the pipeline run?

Common patterns:

Push
 ↓
Pipeline

or:

Pull Request
 ↓
Validation pipeline

or:

Scheduled
 ↓
Pipeline

Artifact

An artifact is the output produced by a build that can be consumed later.

Example:

Source Code
   ↓
Build
   ↓
application.zip
   ↓
Artifact
   ↓
Deployment


Why artifacts matter

Imagine:

Build once
    ↓
Artifact
    ↓
Dev
    ↓
Test
    ↓
Production

You don't want to rebuild the application differently for every environment.

You want confidence that:

The exact artifact tested is the artifact deployed.

That's an important CI/CD principle.


Pipeline security

A pipeline has powerful permissions.

Imagine:

Pipeline
 ↓
Azure Subscription
 ↓
Production

If compromised, it could potentially make destructive changes.

Therefore:

Pipeline identity
       ↓
Least privilege
       ↓
Only required permissions


RBAC + Pipeline

Suppose the pipeline only needs to deploy to:

RG-Production

Don't automatically give:

Owner

at:

Subscription

Managed Identity connection

You also learned:

Application
 ↓
Managed Identity
 ↓
Entra ID
 ↓
Key Vault

CI/CD has a similar identity model:

Pipeline
 ↓
Service Connection / workload identity
 ↓
Entra ID
 ↓
Azure

The exact authentication mechanism can vary, but the principle remains:

Don't put long-lived credentials directly into source code.


Pipeline logs

A production engineer needs to answer:

Pipeline failed
      ↓
Which stage?
      ↓
Which job?
      ↓
Which task?
      ↓
What command?
      ↓
What error?
      ↓
Why?


Important: don't deploy production blindly

For our first pipeline:

Build
 ↓
Test
 ↓
What-if

is enough.

We'll later build proper:

Dev
 ↓
Test
 ↓
Approval
 ↓
Production

pipelines.


Microsoft-hosted agent details

A Microsoft-hosted agent is a temporary VM provided by Azure Pipelines for running a job. Microsoft states that each job receives a fresh VM and that the VM is discarded after the job.

So don't assume:

Job 1
creates file
 ↓
Job 2
will automatically see it

Separate jobs can run on separate fresh agents.

If data must move between jobs/stages, use appropriate artifacts or other pipeline mechanisms.


Why this matters

Imagine:

Build Job
 ↓
app.zip

Then:

Deploy Job
 ↓
Where is app.zip?

If you're using fresh hosted agents:

Build Agent
    ↓
discarded


Deploy Agent
    ↓
new VM

Therefore you need:

Build
 ↓
Publish artifact
 ↓
Deploy
 ↓
Download artifact

This is a fundamental CI/CD concept.


Bicep + Application deployment

Repository
│
├── app/
│   ├── application code
│   └── requirements.txt
│
├── infra/
│   ├── main.bicep
│   └── modules/
│
└── azure-pipelines.yml


Then:

git push
    ↓
Pipeline
    │
    ├── Build application
    ├── Test application
    ├── Validate Bicep
    ├── What-if infrastructure
    └── Deploy



Azure DevOps vs GitHub Actions

Azure DevOps Pipelines
Microsoft ecosystem
 ↓
Azure Pipelines
GitHub Actions
GitHub
 ↓
Actions

Both can:

build
test
package
deploy
run Azure CLI
deploy Bicep
run Terraform
interact with Azure


The important Azure DevOps concepts are:

Pipeline
Stage
Job
Step
Task
Agent
Agent Pool
Trigger
Variable
Service Connection
Artifact
YAML

