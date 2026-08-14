1. What is VM Scale Set?

A Virtual Machine Scale Set lets you deploy and manage a group of load-balanced VMs.

Instead of managing:

VM1
VM2
VM3
VM4

individually:

VM Scale Set
     │
     ├── Instance 1
     ├── Instance 2
     ├── Instance 3
     └── Instance 4

Azure manages them as a group.

2. Why Do We Need VMSS?

Imagine your application normally receives:

100 users

Two VMs are enough.

During a sale:

10,000 users

Two VMs may not be enough.

Without autoscaling:

VM1
VM2

❌ Overloaded

With VMSS:

Normal:

VM1
VM2

        ↓ Traffic increases

VM1
VM2
VM3
VM4
VM5

        ↓ Traffic decreases

VM1
VM2

This is elasticity.

3. Scale-Out vs Scale-In
Scale-Out

Add more VM instances.

2 VMs

↓

4 VMs

Used when demand increases.

Scale-In

Remove VM instances.

4 VMs

↓

2 VMs

Used when demand decreases.

4. Manual Scaling

You can manually specify:

Instance Count = 3

Azure creates:

Instance 1
Instance 2
Instance 3

5. Autoscaling

You can tell Azure:

If CPU > 70%
    ↓
Add VM

And:

If CPU < 30%
    ↓
Remove VM

Conceptually:
CPU
 │
 │ 80% ─────── Scale Out
 │
 │
 │ 30% ─────── Scale In
 │
 └────────────────── Time

 6. VMSS + Load Balancer

This is where today's lab connects directly with Day 11.

Architecture:

                 Internet
                    │
                    ▼
             Public IP
                    │
                    ▼
             Load Balancer
                    │
                    ▼
             VM Scale Set
          ┌─────────┼─────────┐
          ▼         ▼         ▼
        VM1       VM2       VM3


7. VMSS vs Individual VMs

| Individual VMs            | VM Scale Set                            |
| ------------------------- | --------------------------------------- |
| Manage each VM            | Manage as a group                       |
| Manual scaling            | Supports autoscaling                    |
| More administration       | Centralized management                  |
| Good for unique servers   | Good for identical workloads            |
| Each VM can be customized | Instances follow a common configuration |

8. VMSS Instance

An instance is an individual VM inside the scale set.

Example:
Learning-VMSS

├── Instance 0
├── Instance 1
└── Instance 2

You generally shouldn't think of these as permanent individual servers.

The scale set is the important resource.

9. Important Concept – Identical Instances

VMSS is ideal when your servers are stateless or similarly configured.

For example:
Web Server
Web Server
Web Server

Not:
Database Server
Web Server
Special Server

For unique workloads, individual VMs or other services may be more appropriate.



VMSS Architecture – Final

                         Internet
                            │
                            ▼
                     Public Load Balancer
                            │
                            ▼
                    Backend Pool
                            │
                            ▼
                     Learning-VMSS
                    ┌───────┼───────┐
                    ▼       ▼       ▼
                  VM1     VM2     VM3


Best Practices
✅ Use VMSS for horizontally scalable workloads.
✅ Combine VMSS with a Load Balancer for highly available web tiers.
✅ Configure minimum and maximum instance limits.
✅ Use autoscaling based on meaningful metrics.
✅ Keep applications as stateless as possible.
✅ Store persistent data outside individual VM instances.
✅ Use health monitoring so unhealthy instances can be detected.
✅ Don't manually modify individual instances unless you understand the impact on the scale-set model.
