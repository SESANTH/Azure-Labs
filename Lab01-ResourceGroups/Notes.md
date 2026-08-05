Azure Subscription

      │

Resource Group

      │

Resources

      │

VM
Storage
Database
App Service
Virtual Network





Q1: Why are Resource Groups used?

They provide a logical container for related Azure resources, making it easier to manage deployments, assign permissions (RBAC), organize costs with tags, apply locks, and delete or automate groups of resources together.

Q2: Can one resource belong to multiple Resource Groups?

No. An Azure resource can belong to only one Resource Group at a time. If you need it in another Resource Group, you must move the resource (if the resource type supports moving).

Q3: What is the difference between a Region and an Availability Zone?

A Region is a geographic location containing one or more datacenters. Availability Zones are physically separate datacenters within the same region, designed to provide high availability if one zone fails.