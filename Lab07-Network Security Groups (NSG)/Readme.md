# Day 7 – Azure Network Security Groups (NSG)

## Objective

Learn how Azure secures network traffic using Network Security Groups.

## Concepts Learned

- Network Security Group
- Inbound Rules
- Outbound Rules
- Rule Priority
- Common Ports
- Allow vs Deny

## Hands-on Tasks

- Created an NSG
- Associated the NSG with a subnet
- Created HTTP, HTTPS, and SSH (or RDP) rules
- Reviewed Azure default rules
- Explored outbound rules

## Key Learnings

- NSGs filter network traffic.
- Rules are evaluated by priority.
- Inbound rules control incoming traffic.
- Outbound rules control outgoing traffic.
- NSGs can be applied to subnets or network interfaces.

## Screenshots

## Interview Questions

### NSG vs Azure Firewall

An NSG filters traffic at the subnet or NIC level using Layer 3/4 rules (IP, port, protocol). Azure Firewall is a managed, centralized firewall that supports advanced filtering, application rules, threat intelligence, logging, and network address translation.