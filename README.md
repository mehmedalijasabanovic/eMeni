## eMeni

Modern digital menu and service discovery platform

eMeni is a web platform for **digitalizing menus and service catalogs** for any type of business (hospitality, healthcare, freelancers, designers, car rentals, and more).  
End users can discover services, check prices, read reviews, and make reservations, while business owners manage their digital menus, promotional packages, and QR-code decorations.  
The system consists of an **Angular frontend** and an **ASP.NET-based backend** built with **Clean Architecture**, **CQRS**, **JWT authentication**, and **rate limiting**.

---

### Core Product Idea

- **Digital menus for any business**  
  End users can browse menus/services for restaurants, cafes, cinemas, healthcare providers, freelancers, and other service businesses.

- **Access via QR code or website**  
  Each business can receive a decorative QR code that points to their public eMeni page, or be discovered via the main website by city, category, or search.

- **Transparent pricing & self-service**  
  Users see prices and offers without needing to call or message businesses, reducing repetitive price/offer inquiries for owners.

- **Reservations and appointments**  
  For eligible businesses (e.g., restaurants, cinemas, offices), users can **reserve a table or book an appointment/meeting** directly from the platform.

- **Visibility and promotion for businesses**  
  Businesses can pay to highlight their profile and choose between **featured** and **premium featured** placements, increasing exposure on listings.

- **Business analytics**  
  Owners see **visit statistics** for their menus and can **export reports to PDF** to analyze performance over time.

- **AI chat support**  
  Integrated AI chat helps users quickly navigate services and menus, and can assist in answering common questions or guiding discovery.

---

### User Roles & Capabilities

- **Potential customers (end users)**  
  - **Browse menus by city and category**  
  - **Rate and review services** they have used  
  - **Use AI chat support** to find businesses or understand offerings  
  - **View locations on Google Maps** to plan visits  
  - **Reserve seats or schedule appointments/meetings** where supported

- **Business owners / service providers**  
  - **Create and manage a digital menu** or service catalog  
  - **Select from templates** or request a **fully custom design** for their menu  
  - **Order QR-code decorations** (e.g., table stands or other decorative forms) that link to their eMeni page  
  - **Choose eMeni packages** that define how many images and assets they can upload per menu  
  - **Highlight their business** with **featured** or **premium featured** placement (sorting based on promotion rank)  
  - **View statistics and export them as PDF** (menu visits, trends, etc.)  
  - **Review reservations/appointments** booked by customers

- **Administrators**  
  - **Manage packages** (limits, pricing, and capabilities)  
  - **Manage orders** (e.g., menu creation services, template/custom design requests)  
  - **Manage QR-code decorations** (orders, statuses, fulfillment)

---

### High-Level Architecture

- **Frontend (eMeni.Frontend)**
  - Angular-based SPA
  - Angular Material component library and responsive layout
  - Rich **route and component transitions** using Angular animations
  - Feature modules for public browsing, business dashboards, reservations, and admin views
  - Internationalization-ready structure (i18n JSON in `public/i18n`)

- **Backend (eMeni.Backend)**
  - ASP.NET-based API following **Clean Architecture**
  - Projects: `eMeni.API`, `eMeni.Application`, `eMeni.Domain`, `eMeni.Infrastructure`, `eMeni.Shared`, `eMeni.Tests`
  - **CQRS pattern** implemented with request/handler pattern and pipeline behaviors  
  - **JWT authentication** and refresh tokens for secure stateless auth  
  - **Rate limiting** (global and per-policy) to protect critical endpoints (especially auth)  
  - **Entity Framework Core** for data access and migrations  
  - Centralized logging, exception handling, and request/response logging middleware  
  - **Swagger** UI for interactive API exploration

---

### Technology Stack

- **Frontend**
  - Angular 21 (Angular CLI 21)
  - Angular Material
  - Angular Animations and custom route transitions
  - SCSS-based design system

- **Backend**
  - ASP.NET Core Web API
  - Clean Architecture (Domain, Application, Infrastructure, API, Shared, Tests)
  - MediatR-style CQRS (commands, queries, pipeline behaviors)
  - Entity Framework Core, migrations, seeding
  - JWT authentication & authorization
  - ASP.NET rate limiting middleware
  - Swagger / OpenAPI

- **Data & Infrastructure**
  - Relational database (e.g., SQL Server or PostgreSQL; see backend README for exact configuration)
  - Logging to files and/or external sinks

---

### Repository Layout

- **`eMeni.Backend/`** – Backend solution (`eMeni.Backend.sln`) with API, Application, Domain, Infrastructure, Shared, and Tests projects.
- **`eMeni.Frontend/`** – Angular SPA client for end users, business owners, and administrators.
- **`eMeni.Documents/`** – Diagrams, logo, and other documentation assets.
- **`README.md`** – Global overview (this file).

More detailed information is available in:

- **`eMeni.Backend/README.md`** – Backend architecture and backend-specific setup.
- **`eMeni.Frontend/README.md`** – Frontend architecture and frontend-specific setup.

---

### Getting Started

#### Prerequisites

- **Node.js** (LTS version recommended) and **npm**
- **Angular CLI** (matching the version in `eMeni.Frontend/package.json`)
- **.NET SDK** compatible with the backend solution
- A supported **relational database** (check backend config; typically SQL Server)

---

### Running the Backend

See the detailed guide in `eMeni.Backend/README.md`. In short:

1. **Restore dependencies**
   - **Backend**: `dotnet restore eMeni.Backend/eMeni.Backend.sln`
2. **Configure environment**
   - Update connection string and JWT settings in `eMeni.Backend/eMeni.API/appsettings.Development.json`.
3. **Apply database migrations**
   - Run EF Core migrations (e.g., `dotnet ef database update` from the API project directory).
4. **Run the API**
   - `dotnet run --project eMeni.Backend/eMeni.API`
   - Check `Properties/launchSettings.json` for the exact URLs (typically `https://localhost:<port>`).

---

### Running the Frontend

See the detailed guide in `eMeni.Frontend/README.md`. In short:

1. Navigate to the frontend project:
   - `cd eMeni.Frontend`
2. Install dependencies:
   - `npm install`
3. Start the development server:
   - `npm start` or `ng serve`
4. Open the app in your browser:
   - Navigate to `http://localhost:4200/` by default.

Make sure the backend is running and that the environment configuration in the frontend (`src/environments/*.ts`) points to the correct API base URL.

---

### Running the Full System (Local Development)

1. **Start the backend API**  
   - From the repository root:  
     `dotnet run --project eMeni.Backend/eMeni.API`

2. **Start the Angular frontend**  
   - From the repository root:  
     `cd eMeni.Frontend`  
     `npm install` (first run only)  
     `ng serve`

3. **Open the application**  
   - Visit the frontend URL (e.g., `http://localhost:4200/`) and interact with the platform.

4. **Explore the API**  
   - Open the backend Swagger UI (e.g., `https://localhost:<port>/swagger`) to inspect and test endpoints.

---

### Contributing & Next Steps

- Extend business categories and cities to cover more use cases.
- Enhance AI chat support to offer more personalized recommendations.
- Add more dashboard analytics and filters for business owners.
- Improve QR-code decoration configuration (styles, materials, and branding options).

If you need this README tailored further (for example, to match exact database technology, deployment setup, or CI/CD workflow), please provide those details and it can be extended accordingly.