# Mobile Provider Bill Payment System API
This is a RESTful API project developed for the SE 4458 course. It simulates a system where users can query bills, make payments and admins can manage billing records.

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
- **Role-Based Access Control:** Ensures users can only access authorized endpoints.
- **Public Access:** The Website Payment endpoint is publicly accessible to simulate real-world scenarios where login isn't required for payments.

## Rate Limiting
To prevent abuse and ensure fair usage:
- **Limit:** Maximum **3 requests per subscriber per day** for query endpoints.
- **Reset:** Rate limits reset daily based on **UTC time**.

## Logging
The system implements a custom Middleware to log key information for monitoring:
- **Request Path & Method** (e.g., GET /api/v1/bills/query)
- **Request Latency** (in milliseconds)
- **HTTP Status Codes** (e.g., 200, 401, 404)

## Technologies
- **Backend:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** Entity Framework Core (In-Memory DB)
- **Documentation:** Swagger / OpenAPI
- **Security:** JWT Authentication (Bearer Token)
