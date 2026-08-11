# Day 12 – Azure App Service

## Objective

Learn how to deploy and manage applications using Azure App Service (PaaS).

## Concepts Learned

- App Service
- App Service Plan
- Web Apps
- Deployment Center
- Deployment Slots
- Scaling
- Custom Domains

## Hands-on Tasks

- Created an App Service Plan
- Created a Web App
- Explored Deployment Center
- Reviewed Scaling options
- Explored Deployment Slots

## Key Learnings

- App Service is a Platform as a Service (PaaS).
- Azure manages the underlying infrastructure.
- Multiple Web Apps can share one App Service Plan.
- Deployment Slots reduce deployment downtime.
- Scaling is simpler than managing Virtual Machines.


## Interview Questions

Q1. What is Azure App Service?

Azure App Service is a fully managed Platform as a Service (PaaS) for hosting web applications, APIs, and backend services without managing the underlying servers.


Q2. App Service vs Virtual Machine
| App Service                | Virtual Machine                  |
| -------------------------- | -------------------------------- |
| PaaS                       | IaaS                             |
| Azure manages OS           | You manage OS                    |
| Faster deployment          | More flexibility                 |
| Built-in scaling           | Manual infrastructure management |
| Best for web apps and APIs | Best for full server control     |

Q3. What is an App Service Plan?

An App Service Plan defines the compute resources, pricing tier, region, and scaling capabilities used by one or more App Services.

Q4. What are Deployment Slots?

Deployment Slots allow you to deploy and test new versions of an application in a staging environment before swapping them into production, reducing downtime.

Q5. Can multiple Web Apps share an App Service Plan?

Yes. Multiple Web Apps can run on the same App Service Plan and share its underlying compute resources.

### VM vs App Service

Virtual Machines provide full control over the operating system and infrastructure (IaaS). App Service abstracts the infrastructure, allowing developers to focus on deploying and managing applications (PaaS).