# Day 10 – Azure Availability Sets & Availability Zones

## Objective

Learn how Azure improves application availability using Availability Sets and Availability Zones.

## Concepts Learned

- High Availability
- Availability Sets
- Availability Zones
- Fault Domains
- Update Domains
- Service Level Agreements (SLAs)

## Hands-on Tasks

- Reviewed VM availability settings
- Explored Availability Set creation
- Compared Availability Zones
- Studied Fault Domains and Update Domains
- Reviewed Azure recommendations

## Key Learnings

- Availability Sets protect against hardware and maintenance failures within a datacenter.
- Availability Zones protect against datacenter-level failures.
- Fault Domains isolate hardware failures.
- Update Domains reduce planned maintenance impact.
- High availability improves application uptime.

## Interview Questions

Q1. What is an Availability Set?

An Availability Set is an Azure feature that distributes multiple VMs across Fault Domains and Update Domains within the same datacenter to improve availability.

Q2. What is an Availability Zone?

An Availability Zone is a physically separate datacenter within an Azure region, providing protection against datacenter-level failures.

Q3. Fault Domain vs Update Domain
| Fault Domain                       | Update Domain                                |
| ---------------------------------- | -------------------------------------------- |
| Protects against hardware failures | Protects against planned maintenance         |
| Separate power/network/rack        | Restarted one group at a time during updates |


Q4. Availability Set vs Availability Zone
| Availability Set                | Availability Zone                        |
| ------------------------------- | ---------------------------------------- |
| Same datacenter                 | Different datacenters                    |
| Fault & Update Domains          | Physical zone separation                 |
| Suitable for legacy deployments | Preferred for new production deployments |

Q5. Why use a Load Balancer with multiple VMs?

A Load Balancer distributes incoming traffic across healthy VMs. Combined with Availability Sets or Availability Zones, it helps maintain application availability even if one VM fails.

### Availability Set vs Availability Zone

Availability Sets distribute VMs across Fault Domains and Update Domains within one datacenter.
Availability Zones distribute VMs across physically separate datacenters in the same Azure region, providing higher resilience.