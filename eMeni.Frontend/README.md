## eMeni Frontend

Angular SPA for the eMeni digital menu and service platform

The eMeni frontend is a **single-page application built with Angular** that provides user interfaces for potential customers, business owners, and administrators.  
It leverages **Angular Material**, **custom SCSS styling**, and **Angular animations** (including route transitions) to deliver a modern, responsive, and polished user experience.

---

### Key Responsibilities

- **Public experience for end users**
  - Browse menus and services by **city** and **category**
  - View pricing, descriptions, and images of services/menus
  - Read ratings and reviews from other customers
  - Use AI-powered chat support to explore businesses and offers
  - View business locations on integrated maps
  - Reserve a table or schedule an appointment/meeting where supported

- **Business owner dashboard**
  - Manage business profile and contact details
  - Create and edit **digital menus** or service catalogs
  - Choose between **templates** or custom-designed menus
  - Manage package selection and understand limits (e.g., number of images per menu)
  - Review reservations and bookings from customers
  - View statistics and analytics as exposed by the backend

- **Administrator interface**
  - Manage packages and their limitations
  - Oversee orders and requests (e.g., custom designs, menu setups)
  - Manage QR-code decoration requests and their statuses

The frontend communicates exclusively with the **eMeni backend API**, which handles all business rules, persistence, authentication, and authorization.

---

### Technology Stack

- **Angular 21** (generated with Angular CLI 21.0.2)
- **TypeScript**
- **Angular Material** component library
- **Angular Animations** for route and component transitions
- **SCSS** for global and component-level styling
- Angular Router with route-level guards and lazy-loaded modules (where applicable)

---

### UI/UX Foundations

- **Angular Material**
  - A centralized module (`material-modules.ts`) re-exports frequently used Material components such as:
    - Navigation (`MatToolbarModule`, `MatSidenavModule`, `MatMenuModule`)
    - Forms (`MatFormFieldModule`, `MatInputModule`, `MatSelectModule`, `MatCheckboxModule`, `MatDatepickerModule`)
    - Feedback (`MatSnackBarModule`, `MatDialogModule`, `MatProgressBarModule`, `MatProgressSpinnerModule`)
    - Data display (`MatTableModule`, `MatPaginatorModule`, `MatSortModule`, `MatCardModule`, `MatTabsModule`)
  - Ensures consistent look and feel across the platform

- **Animations and transitions**
  - Angular animation utilities (`@angular/animations`) are used to define:
    - **Route transitions** with fade and slide effects for navigation between pages
    - **Component-level transitions** like fade-in, slide-in, and scale-fade
  - The `core/animations` module centralizes common animations (e.g., `routeAnimations`, `fadeAnimation`, `slideInRight`, `slideInLeft`, `scaleFade`) for reuse

- **Styling**
  - Global SCSS theming integrates tightly with Angular Material (`@use '@angular/material' as mat;`)
  - Custom color palettes and typography can be configured for brand alignment
  - Layout is responsive, targeting mobile, tablet, and desktop breakpoints

---

### Project Structure (High Level)

Key directories in `src/app`:

- **`core/`**
  - Core application services (e.g., API clients, interceptors, guards)
  - Shell components like main layout, navigation, and top-level routing
  - Animation definitions for route and component transitions

- **`modules/`**
  - Feature modules for different sets of functionality, such as:
    - Public browsing (cities, categories, businesses, menus)
    - Business owner dashboard (menu management, packages, orders)
    - Reservations and bookings
    - Administration (packages, orders, QR decorations)
  - Shared Material module (`shared/material-modules.ts`) to aggregate Material imports

- **`shared/`**
  - Reusable UI components (buttons, cards, dialogs, etc.)
  - Shared pipes and directives
  - Shared models and utility functions

- **`environments/`**
  - Environment-specific configuration (`environment.ts`, `environment.development.ts`, etc.)
  - Contains the **API base URL** and environment flags

- **`assets/` and `public/`**
  - Static assets (images, icons, logos)
  - i18n JSON files under `public/i18n` for localization support

Exact module and component names may evolve, but this structure is designed to keep responsibilities well separated and the app scalable.

---

### Development Setup

#### Prerequisites

- **Node.js** (LTS version recommended)
- **npm**
- **Angular CLI** (matching the version in `package.json`)

#### 1. Install Dependencies

From the `eMeni.Frontend` directory:

```bash
npm install
```

#### 2. Configure Environment

Check `src/environments` for environment files (for example `environment.development.ts`) and verify:

- **API base URL** points to your running backend (e.g., `https://localhost:<port>/api`)
- Any other environment-specific settings are configured as needed

#### 3. Start the Development Server

From the `eMeni.Frontend` directory:

```bash
ng serve
```

By default, the app will be available at:

- `http://localhost:4200/`

The dev server supports live reload, so changes to source files automatically trigger rebuilds and refresh the browser.

---

### Building for Production

To create an optimized production build:

```bash
ng build --configuration production
```

This will:

- Compile the Angular application
- Optimize and minify JavaScript, HTML, and CSS
- Output static assets into the `dist/` directory (by default `dist/e-meni-frontend` or similar)

The resulting files can be served from any static file server (e.g., Nginx, IIS, a CDN) and configured to proxy API calls to the eMeni backend.

---

### Testing

#### Unit tests

To run unit tests:

```bash
ng test
```

This command runs the configured unit test runner (see `angular.json` and `package.json` for the exact setup) and reports test results.

#### End-to-end (e2e) tests

If you add an e2e framework (such as Cypress or Playwright), you can configure corresponding scripts in `package.json` and run, for example:

```bash
npm run e2e
```

Refer to your chosen e2e framework’s documentation for detailed setup and usage.

---

### Working With Angular CLI

You can still use Angular CLI to scaffold new building blocks:

- **Generate a component**

```bash
ng generate component feature-name/my-component
```

- **Generate other artifacts**

```bash
ng generate service my-service
ng generate module my-module
ng generate guard my-guard
```

For all options:

```bash
ng generate --help
```

---

### Extending the Frontend

- **New features and views**
  - Add new feature modules under `src/app/modules` for clear separation
  - Use shared components and Material design to remain visually consistent

- **Animations and UX polish**
  - Reuse and extend existing route and component animations from `core/animations`
  - Introduce micro-interactions (hover, focus, loading states) using Angular animations and Material states

- **Internationalization**
  - Extend translation JSON files in `public/i18n`
  - Wire translations into components and services using your chosen i18n strategy

If you share more about your deployment environment (e.g., how the frontend is hosted and how it reaches the backend), this README can be enhanced with deployment-specific instructions as well.
