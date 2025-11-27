# Mobile Provider Bill Payment System API

This is a RESTful API project developed for the SE 4458 course. It simulates a mobile provider system (e.g., Turkcell) where users can query bills via mobile/banking apps and pay via a website. Administrators can manage billing records through secure endpoints.

## Features Overview

| Platform | Role / API | Auth Required | Paging | Description |
| :--- | :--- | :---: | :---: | :--- |
| Mobile App | Query Bill | Yes | No | Query total bill & paid status. Limit: 3 calls per subscriber/day. |
| Mobile App | Query Bill Detailed | Yes | Yes | Get detailed bill info for a subscriber. |
| Banking App | Query Bill | Yes | No | List unpaid bills by month. |
| Website | Pay Bill | No | No | Mark bill as paid. Partial payments are saved. No real credit card processing. |
| Admin | Add Bill | Yes | No | Add a single bill for a subscriber for a given month. |
| Admin | Add Bill - Batch | Yes | No | Add multiple bills via .csv file upload. |

## Authentication & Security

- **JWT-based Authentication:** Secure access for Admin and User roles.
- **Role-Based Access Control:** Ensures users can only access authorized endpoints.
- **Public Access:** The Website Payment endpoint is publicly accessible to simulate real-world scenarios where login isn't required for payments.

## Rate Limiting

To prevent abuse and ensure fair usage:
- **Limit:** Maximum 3 requests per subscriber per day for query endpoints.
- **Reset:** Rate limits reset daily based on UTC time.

## Logging

The system implements a custom Middleware to log key information for monitoring:
- **Request Path & Method** (e.g., GET /api/v1/bills/query)
- **Request Latency** (in milliseconds)
- **HTTP Status Codes** (e.g., 200, 401, 404)

## Data Model

<img width="358" height="667" alt="ER Diagram" src="https://github.com/user-attachments/assets/2e9d0303-3fc2-411f-a4cc-a5be7110c26f" />

### Project Assumptions
1. **Uniqueness:** SubscriberNo is considered unique for a specific billing period (Month).
2. **Partial Payments:** The system accepts partial payments via the Website API. The bill is marked as Paid only when PaidAmount >= TotalAmount.
3. **Timezone:** All system times and rate limit resets are calculated in UTC to ensure consistency across regions.

## Technologies

- **Backend:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** Entity Framework Core (In-Memory DB)
- **Documentation:** Swagger / OpenAPI
- **Security:** JWT Authentication (Bearer Token)
- **Deployment:** Azure App Service

## Project Video
[Buraya YouTube video linkini yapıştır]

## Live Demo (Swagger)
[Click here to view Swagger UI](https://mobile-provider-irmak-fwgpgsaqesdpcwh8.germanywestcentral-01.azurewebsites.net/swagger/index.html)
