Production Web Application Platform

Target architecture:
                           USERS
                             │
                             ▼
                    Azure Front Door
                         + WAF
                             │
                             ▼
                      Azure App Service
                             │
                       Managed Identity
                             │
                       VNet Integration
                             │
                             ▼
                    ┌─────────────────┐
                    │    APP VNET     │
                    │                 │
                    │ App connectivity│
                    │ Private DNS     │
                    └────────┬────────┘
                             │
                             ▼
                     Private Endpoint
                             │
                        Private Link
                             │
                             ▼
                        Azure SQL
                             │
                             ▼
                      Employee Data

Supporting services:

              ┌─────────────────────┐
              │     Key Vault        │
              └──────────┬──────────┘
                         │
                  Managed Identity
                         │
                         ▼
                    App Service


              ┌─────────────────────┐
              │   Azure Monitor     │
              │ Log Analytics       │
              │ Alerts              │
              └─────────────────────┘

