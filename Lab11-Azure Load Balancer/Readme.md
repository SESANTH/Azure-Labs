# Day 11 – Azure Load Balancer

## Objective

Learn how Azure distributes traffic across multiple Virtual Machines using Azure Load Balancer.

## Concepts Learned

- Azure Load Balancer
- Frontend IP
- Backend Pool
- Health Probe
- Load Balancing Rules
- NAT Rules

## Hands-on Tasks

- Created a Public Load Balancer
- Configured a Frontend IP
- Created a Backend Pool
- Configured a TCP Health Probe
- Created a Load Balancing Rule
- Explored NAT Rules

## Key Learnings

- Load Balancers distribute traffic across healthy VMs.
- Health Probes detect VM availability.
- Backend Pools contain target VMs.
- NAT Rules allow direct administration of backend VMs.
- Standard Load Balancer is recommended for production deployments.

## Interview Questions

Q1. What is Azure Load Balancer?

Azure Load Balancer is a Layer 4 service that distributes incoming TCP/UDP traffic across multiple healthy backend resources to improve availability and scalability.

Q2. What is a Backend Pool?

A Backend Pool is a collection of Azure resources (typically VMs or VM Scale Sets) that receive traffic from the Load Balancer.

Q3. What is a Health Probe?

A Health Probe periodically checks backend resources. If a VM fails the probe, Azure temporarily stops sending traffic to that VM until it becomes healthy again.

Q4. Public vs Internal Load Balancer
| Public           | Internal              |
| ---------------- | --------------------- |
| Internet-facing  | Private network only  |
| Uses Public IP   | Uses Private IP       |
| Web applications | Internal applications |

Q5. Load Balancer vs Application Gateway

| Load Balancer              | Application Gateway               |
| -------------------------- | --------------------------------- |
| Layer 4                    | Layer 7                           |
| TCP/UDP                    | HTTP/HTTPS                        |
| Basic traffic distribution | URL routing, SSL termination, WAF |



### Load Balancer vs Application Gateway

Azure Load Balancer operates at Layer 4 (TCP/UDP) and distributes network traffic.
Application Gateway operates at Layer 7 (HTTP/HTTPS) and supports URL-based routing, SSL termination, and Web Application Firewall (WAF).