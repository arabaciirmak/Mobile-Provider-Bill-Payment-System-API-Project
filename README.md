# Mobile Provider Bill Payment System API
This is a RESTful API project developed for the **SE 4458** course. It simulates a mobile provider system (e.g., Turkcell) where users can query bills via mobile/banking apps and pay via a website. Administrators can manage billing records through secure endpoints.

## Features Overview
| Platform | Role / API | Auth Required | Paging | Description |
| :--- | :--- | :---: | :---: | :--- |
| **Mobile App** | Query Bill | ✅ Yes | ❌ No | Query total bill & paid status. **Limit:** 3 calls per subscriber/day. |
| **Mobile App** | Query Bill Detailed | ✅ Yes | ✅ Yes | Get detailed bill info for a subscriber. |
| **Banking App** | Query Bill | ✅ Yes | ❌ No | List unpaid bills by month. |
| **Website** | Pay Bill | ❌ No | ❌ No | Mark bill as paid. Partial payments are saved. **No real credit card processing.** |
| **Admin** | Add Bill | ✅ Yes | ❌ No | Add a single bill for a subscriber for a given month. |
| **Admin** | Add Bill – Batch | ✅ Yes | ❌ No | Add multiple bills via **.csv file upload**. |

## Authentication & Security
- **JWT-based Authentication:** Secure access for Admin and User roles.
- **Role-Based Access Control (RBAC):** Ensures users can only access authorized endpoints.
- **Public Access:** The Website Payment endpoint (`/pay`) is publicly accessible to simulate real-world scenarios where login isn't required for payments.

## Rate Limiting
To prevent abuse and ensure fair usage:
- **Limit:** Maximum **3 requests per subscriber per day** for query endpoints.
- **Reset:** Rate limits reset daily based on **UTC time**.

## Logging
The system implements a custom Middleware to log detailed information for monitoring and debugging:
- **Source IP Address**
- **Request Latency (ms)**
- **HTTP Status Codes**
- **Request Path & Method**

## Technologies
- **Backend:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** Entity Framework Core (In-Memory DB)
- **Documentation:** Swagger / OpenAPI
- **Security:** JWT Authentication (Bearer Token)

## Data Model & Assumptions
### Bill Entity
Represents a monthly invoice for a subscriber containing total amount, paid amount, and payment status.

### Assumptions
1. **Uniqueness:** `SubscriberNo` is unique per billing period (Month).
2. **Payments:** Website payments do not require login; partial payments are tracked until the total is covered.
3. **Timezone:** All system times and rate limit resets are calculated in UTC.

## How to Run
1. Clone the repository:
   ```bash
   git clone [https://github.com/arabaciirmak/Mobile-Provider-Bill-Payment-System-API-Project.git](https://github.com/arabaciirmak/Mobile-Provider-Bill-Payment-System-API-Project.git)
