# Lab 25 — Github Actions

## Objective

Learn how Github Actions CI/CD — taking the infrastructure/application code we've been creating and starting to automate its delivery.

## Concepts Learned

- GitHub Actions
- Workflows
- Events
- Triggers
- Jobs
- Steps
- Actions
- Runners
- GitHub-hosted Runners
- Self-hosted Runners
- YAML
- Variables
- Secrets
- Artifacts
- Job Dependencies
- Environments
- Azure Authentication
- OIDC
- Azure CLI Integration
- Bicep Integration
- CI/CD

## Screenshots

See the `Images` directory.

## Interview Questions

Q1. GitHub Actions?

GitHub Actions is GitHub's automation and CI/CD platform that allows workflows to run in response to repository and other events.

Q2. Git vs GitHub vs GitHub Actions
Git
=
Version control


GitHub
=
Git hosting/collaboration platform


GitHub Actions
=
Automation/CI/CD platform
Q3. Workflow?

A workflow is a YAML-defined automation process that contains jobs and is triggered by events.

Q4. Event?

An event is something that causes a workflow to run, such as a push, pull request, manual dispatch, or schedule.

Q5. Job?

A job is a group of steps executed on a runner.

Q6. Runner?

A runner is the machine/environment that executes GitHub Actions jobs.

Q7. Hosted vs self-hosted?

Hosted runners are provided and maintained by GitHub. Self-hosted runners are machines managed by the organization.

Q8. Action?

An Action is a reusable automation component that can perform a specific task.

Example:

uses: actions/checkout@v4
Q9. actions/checkout?

It checks out the repository's source code into the runner so subsequent steps can operate on it.

Q10. needs?

It establishes a dependency between jobs.

deploy:
  needs: test

means Deploy waits for Test.

Q11. Secrets?

Use GitHub's secure secrets/environment mechanisms rather than hardcoding secrets in YAML.

Q12. Azure authentication?

Use a secure identity mechanism such as GitHub Actions OIDC with Microsoft Entra ID, or another securely managed credential mechanism when required.

Q13. OIDC?

OpenID Connect allows GitHub Actions to obtain an identity token that Azure can trust through a configured federated identity relationship, reducing the need for long-lived secrets.

Q14. Deploy Bicep?

Conceptually:

GitHub
 ↓
Workflow
 ↓
Azure authentication
 ↓
Azure CLI
 ↓
Bicep validate
 ↓
What-if
 ↓
Deploy
Q15. GitHub Actions vs Azure DevOps?

Both are CI/CD platforms.

GitHub Actions is integrated directly into GitHub, while Azure Pipelines is part of Azure DevOps.

Their concepts are similar but their terminology and integrations differ.

Q16. Troubleshoot workflow?

Follow:

Workflow
 ↓
Job
 ↓
Failed step
 ↓
Logs
 ↓
Error
 ↓
Root cause
 ↓
Fix
 ↓
Rerun