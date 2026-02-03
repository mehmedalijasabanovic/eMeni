## eMeni Backend

Backend API for the eMeni digital menu and service platform

The eMeni backend is an ASP.NET-based Web API that exposes all core capabilities of the platform: menu and service management, reservations, reviews, analytics, AI chat logging, and administration.  
It follows **Clean Architecture** with **CQRS**, **JWT-based authentication**, and **rate limiting** to provide a clear separation of concerns, scalability, and security.

---

### Project Structure

The backend solution (`eMeni.Backend.sln`) is organized into the following projects:

- **`eMeni.API`**
  - ASP.NET Core Web API entry point
  - Configures middleware, authentication/authorization, rate limiting, Swagger, logging, and DI
  - Hosts controllers for authentication, businesses, menus, locations, users, etc.

- **`eMeni.Application`**
  - **Application layer** implementing **CQRS** (commands and queries)
  - Contains the core business use cases and application logic
  - Defines interfaces for persistence and infrastructure dependencies (`IAppDbContext`, `IJwtTokenService`, etc.)
  - Contains validation, behaviors, and common application models

- **`eMeni.Domain`**
  - **Domain layer** with business entities and domain rules
  - Contains entities for menus, businesses, locations, reservations, reviews, statistics, payments, orders, and AI chat logs
  - Defines base entity types and aggregates

- **`eMeni.Infrastructure`**
  - **Infrastructure layer** implementing persistence and external services
  - Entity Framework Core DbContext and configurations
  - Migrations, seeding, and database initializers
  - JWT token generation, current user resolution, and central exception handling

- **`eMeni.Shared`**
  - Shared DTOs, options, constants, and helpers that are used across layers

- **`eMeni.Tests`**
  - Automated tests (e.g., integration tests) for critical behaviors and endpoints

---

### Clean Architecture Layers

- **Domain Layer (`eMeni.Domain`)**
  - Contains core business entities such as:
    - Businesses, menus, menu items
    - Locations (cities, addresses)
    - Reservations and orders
    - Reviews and statistics
    - Payments and AI chat logs
  - Contains base entity definitions (`BaseEntity`) and aggregates
  - Has **no dependency** on other layers; it is the innermost core of the system

- **Application Layer (`eMeni.Application`)**
  - Implements **CQRS** using a request/handler pattern (`IRequest`, `IRequestHandler<,>`)
  - Organizes logic into modules (e.g., Auth, Business, Identity, Location, Menu)
  - Uses **commands** for write operations (create/update/delete) and **queries** for read operations (list/get)
  - Defines **interfaces** for persistence and cross-cutting concerns:
    - `IAppDbContext` – abstraction over EF Core DbContext
    - `IAppCurrentUser` – current authenticated user abstraction
    - `IAuthorizationHelper` – high-level authorization checks
    - `IJwtTokenService` – token generation and management
  - Includes **pipeline behaviors** (e.g., validation behavior) to apply cross-cutting concerns to all requests
  - Returns strongly typed results and pagination models (`PageRequest`, `PageResult<T>`)

- **Infrastructure Layer (`eMeni.Infrastructure`)**
  - Implements `IAppDbContext` with an EF Core `eMeniContext`
  - Provides database configurations and mappings for domain entities
  - Contains migrations and seeders (including data useful for Swagger/testing)
  - Implements `IJwtTokenService` using `JwtSecurityTokenHandler` and application `JwtOptions`
  - Exposes implementations for current user context (`AppCurrentUser`) and centralized exception handling
  - Depends on `eMeni.Domain` and `eMeni.Application`, but **those layers never depend on Infrastructure**

- **API Layer (`eMeni.API`)**
  - Hosts the ASP.NET Core application and composes:
    - Routing and controller endpoints
    - Authentication and authorization
    - Rate limiting
    - Request/response logging and exception handling
    - Swagger/OpenAPI documentation
  - Controllers are thin: they receive HTTP requests, translate them into **Application commands/queries**, and return standardized responses

---

### Cross-Cutting Concerns

- **Authentication & Authorization**
  - Access is secured with **JWT Bearer authentication**
  - Tokens are issued via Auth module handlers in `eMeni.Application`, using `IJwtTokenService`
  - Token configuration is read from `Jwt` settings in `appsettings.json` and bound to `JwtOptions`
  - Authorization helpers in the Application layer enforce business-specific rules (e.g., ownership of resources)

- **Rate Limiting**
  - ASP.NET Core **rate limiting middleware** is configured in `DependencyInjection` and enabled in `Program.cs`
  - Global rate limits protect the API from abuse
  - Specific policies (for example, a stricter policy on authentication endpoints) are applied using `[EnableRateLimiting("AuthPolicy")]` on sensitive controllers

- **Validation**
  - A **validation behavior** in the Application layer runs before handlers, ensuring commands and queries are valid
  - Reduces duplication and keeps controllers thin

- **Logging & Monitoring**
  - Request/response logging middleware captures incoming and outgoing traffic for diagnostics
  - Logs are written to files (see the `Logs` folder in `eMeni.API`)

- **Error Handling**
  - A centralized exception handling approach ensures consistent error responses
  - Domain/application exceptions can be translated into meaningful HTTP status codes

- **AI Chat Logging**
  - AI chat interactions are stored via `AiChatLogEntity` and corresponding infrastructure configuration
  - This enables audits, analytics, and potential future improvements to AI-based support

---

### Functional Overview (Backend Perspective)

- **End Users (Potential Customers)**
  - Browse menus and services by city and category
  - Create reviews and ratings for services they have consumed
  - Use AI-powered chat support to explore services and get guidance
  - View business locations on Google Maps (via stored geo/location data)
  - Reserve a seat or schedule a meeting/appointment where supported

- **Business Owners**
  - Manage their business profile and digital menu/service catalog
  - Choose between **featured** and **premium featured** status (promotion ranks that affect sorting)
  - Select packages that control how many images and other assets they can attach to their menu
  - Request **designed templates** or custom templates for their menus
  - Order **QR-code decorations** that link directly to their eMeni menu
  - Access **statistics and analytics** on menu visits, including PDF exports
  - View and manage reservations and bookings

- **Administrators**
  - Manage subscription **packages** and their parameters
  - Oversee and manage **orders** (e.g., menu setup, designs, custom services)
  - Manage **QR-code decoration** orders and statuses

---

### Configuration

Backend configuration is primarily managed via **`appsettings.json`** and **`appsettings.Development.json`** in the `eMeni.API` project.

- **Database**
  - Connection string is defined under the `ConnectionStrings` section
  - Ensure it points to your local or production database instance

- **JWT**
  - `Jwt` section typically contains:
    - Issuer
    - Audience
    - Signing key / secret
    - Token lifetime configuration

- **Rate Limiting**
  - `RateLimiting` section governs:
    - Global window limits
    - Named policies (e.g., `AuthPolicy`) with different limits for sensitive endpoints

Adjust these values according to your environment (development, staging, production).

---

### Running the Backend Locally

#### Prerequisites

- Installed **.NET SDK** compatible with the solution
- A running **database server** (typically SQL Server or equivalent, depending on your configuration)

#### 1. Restore Dependencies

From the repository root:

```bash
dotnet restore eMeni.Backend/eMeni.Backend.sln
```

#### 2. Configure Connection String and JWT

Open `eMeni.Backend/eMeni.API/appsettings.Development.json` and:

- Update the **database connection string** to point to your local database.
- Configure the **Jwt** section (issuer, audience, signing key, expiry).

#### 3. Apply Database Migrations

From the `eMeni.Backend/eMeni.API` directory:

```bash
dotnet ef database update
```

This will create or update the database schema according to the latest migrations.

#### 4. Run the API

From the repository root:

```bash
dotnet run --project eMeni.Backend/eMeni.API
```

The API will start listening on the URLs defined in `Properties/launchSettings.json` (typically something like `https://localhost:<port>` and `http://localhost:<port>`).

#### 5. Explore the API (Swagger)

Once the API is running, open your browser and navigate to:

- `https://localhost:<port>/swagger`

This exposes the Swagger UI where you can:

- Inspect available endpoints
- Try requests directly from the browser
- View request and response schemas

---

### Testing

#### Unit / Integration Tests

From the repository root:

```bash
dotnet test eMeni.Backend/eMeni.Backend.sln
```

The `eMeni.Tests` project uses a custom web application factory to spin up an in-memory version of the API for high-fidelity tests.

---

### Extending the Backend

- **Add new business capabilities**
  - Create new commands/queries in the `eMeni.Application` modules
  - Add or update domain entities in `eMeni.Domain`
  - Map new entities in `eMeni.Infrastructure` configurations and migrations

- **Adjust rate limiting or security**
  - Update the `RateLimiting` configuration and policies
  - Extend JWT claims and authorization rules in the Application layer

- **Enhance analytics and AI**
  - Add new endpoints and handlers for analytics
  - Extend AI chat logging and analysis features

If you provide more details about your deployment and environment (database type, hosting provider, CI/CD), this README can be adapted with production-specific guidance as well.
