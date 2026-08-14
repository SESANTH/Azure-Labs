# Day 15 – Azure Virtual Machine Scale Sets

## Objective

Learn how Azure Virtual Machine Scale Sets manage groups of identical VMs and automatically scale them based on workload.

## Concepts Learned

- Virtual Machine Scale Sets
- VMSS Instances
- Scale-Out
- Scale-In
- Manual Scaling
- Autoscaling
- Load Balancer Integration
- Upgrade Policy
- Instance Protection

## Hands-on Tasks

- Created a VM Scale Set
- Deployed an Ubuntu VMSS instance
- Increased the instance count
- Reduced the instance count
- Explored autoscaling
- Reviewed VMSS networking
- Explored upgrade policies
- Reviewed Load Balancer integration

## Architecture

Internet
    |
    v
Azure Load Balancer
    |
    v
Learning-VMSS
    |
    +-- Instance 0
    +-- Instance 1
    +-- Instance 2

## Key Learnings

- VMSS manages multiple VMs as a group.
- Scale-out adds instances.
- Scale-in removes instances.
- Autoscaling adjusts capacity based on demand.
- VMSS works well with Load Balancers.
- VMSS is best suited for identical or stateless workloads.

## Screenshots

See the `Images` directory.

## Interview Questions
Q1. What is VMSS?

Azure Virtual Machine Scale Sets allow you to deploy and manage a group of load-balanced VMs with centralized configuration and autoscaling capabilities.

Q2. What is scale-out?

Increasing the number of VM instances.
2 → 4 VMs

Q3. What is scale-in?

Decreasing the number of VM instances.
4 → 2 VMs

Q4. VMSS vs Availability Set?
| VMSS                            | Availability Set                    |
| ------------------------------- | ----------------------------------- |
| Manages groups of VMs           | Improves availability of VMs        |
| Supports autoscaling            | Doesn't provide autoscaling         |
| Designed for scalable workloads | Designed primarily for availability |
| Instances share a common model  | Individual VMs                      |
| Works well with Load Balancer   | Often used with Load Balancer       |

They solve different problems.

Availability Set
        ↓
"Keep my VMs available."

VMSS
        ↓
"Keep the right number of VMs running."

Q5. Why combine VMSS and Load Balancer?

Because they solve complementary problems.

VMSS:

How many servers should exist?

Load Balancer:

Which healthy server should receive the request?

Together:

             Load Balancer
                   │
                   ▼
                 VMSS
          ┌────────┼────────┐
          ▼        ▼        ▼
         VM1      VM2      VM3


Q6. What happens when a VMSS instance fails?

Azure can detect unhealthy instances and replace or repair instances depending on the configured health monitoring and VMSS settings.

The important concept:

You manage the scale set, not individual servers.

Q7. What is elasticity?

Elasticity is the ability to automatically increase or decrease resources according to workload.

Example:

Low traffic
   ↓
2 VMs

High traffic
   ↓
5 VMs

Low traffic
   ↓
2 VMs

This can improve both availability and cost efficiency.


### VMSS vs Individual VMs

Individual VMs are managed independently, while VM Scale Sets provide centralized management and automated scaling for groups of similar VMs.