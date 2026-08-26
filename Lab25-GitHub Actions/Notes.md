GitHub Actions

Git vs GitHub vs GitHub Actions

Git
 ↓
Version control system

GitHub
 ↓
Platform that hosts Git repositories

GitHub Actions
 ↓
Automation / CI/CD platform

Git
 ↓
tracks code changes

GitHub
 ↓
stores/collaborates around the repository

GitHub Actions
 ↓
automates what happens when repository events occur


GitHub

GitHub is a hosted platform built around Git repositories.

For example:

Your PC
   ↓
Git
   ↓
GitHub repository

What is a Workflow?

In GitHub Actions, the main automation definition is a workflow.

A workflow is defined using YAML.

It normally lives under:

.github/workflows/

For example:

repository
│
├── app/
│
└── .github/
    └── workflows/
        └── ci.yml

name: Hello CI

on:
  push:
    branches:
      - main

jobs:
  hello:
    runs-on: ubuntu-latest

    steps:
      - name: Say hello
        run: echo "Hello from GitHub Actions"


Events

GitHub Actions can react to events such as:

push
pull_request
workflow_dispatch
schedule


Manual trigger

You can also allow a workflow to be manually started:

on:
  workflow_dispatch:

Scheduled workflows

GitHub Actions can also run on schedules.

Conceptually:

on:
  schedule:
    - cron: "0 18 * * *"

This uses cron syntax.

Jobs

Now:

jobs:
  hello:

A workflow contains jobs.

Runner

A GitHub Actions runner is the machine that executes your job.

GitHub-hosted runners

GitHub provides hosted runners.

For example:

runs-on: ubuntu-latest

Self-hosted runners

You can also run your own machine as a GitHub Actions runner.

Actions

This is one of the most important GitHub Actions concepts.

Example:

- uses: actions/checkout@v4

This uses a reusable action.

actions/checkout

You'll see this constantly:

- uses: actions/checkout@v4

Why?

The runner needs your repository's source code.

Secrets

Never do:

env:
  PASSWORD: "MyPassword123"

Instead use GitHub Secrets.

For production, prefer short-lived identity-based authentication such as OpenID Connect (OIDC) rather than long-lived client secrets where practical.

IDC — important concept

This is an important modern CI/CD security concept.

Instead of storing:

Client secret

in GitHub:

GitHub Actions
      ↓
OIDC token
      ↓
Microsoft Entra ID
      ↓
Federated identity trust
      ↓
Azure

The workflow obtains a short-lived token and Azure trusts the configured GitHub identity under defined conditions.

Artifacts

Suppose the build creates:

app.zip

You can upload it as an artifact.

Conceptually:

Build
 ↓
app.zip
 ↓
Artifact
 ↓
Deploy

GitHub Actions provides artifact capabilities for storing workflow outputs.

Why artifacts?

Again:

Build once
 ↓
Artifact
 ↓
Deploy Dev
 ↓
Deploy Test
 ↓
Deploy Production

Environments

GitHub Actions supports environments such as:

development
staging
production

GitHub Actions security

Use:

least privilege
protected branches
environment protections
OIDC where possible
secret management
limited permissions

Don't give a CI/CD workflow unnecessary Owner permissions.

GitHub Actions troubleshooting

Did the workflow trigger?
Did the runner start?
Did checkout succeed?
Did dependency installation succeed?
Did tests fail?
Did authentication fail?
Did Azure CLI fail?
Did RBAC deny the action?
Did the Azure deployment fail?

Common failures
Workflow doesn't start

Check:

trigger
branch
event
YAML syntax
Checkout fails

Check:

repository
permissions
workflow token
Python installation fails

Check:

Python version
dependency versions
requirements.txt
Azure login fails

Check:

OIDC configuration
federated identity
tenant
subscription
identity
permissions
Azure deployment fails

Check:

Bicep
API version
resource name
SKU
quota
region
RBAC
dependencies

