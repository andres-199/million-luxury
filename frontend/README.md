# Frontend - Million Luxury

## Clean Architecture

The frontend implements Clean Architecture principles, separating responsibilities into concentric layers where dependencies flow inward:

### 1. Domain Layer (src/domain)

Innermost layer, with no external dependencies:

- Business entity and model definitions
- Core business rules
- Repository interfaces

### 2. Application Layer (src/application)

Orchestrates application use cases:

- Business logic implementation
- Specific use cases
- Application services

### 3. Infrastructure Layer (src/infrastructure)

Technical implementations and frameworks:

- State management with Redux Toolkit
- API services
- Repository implementations

### 4. Presentation Layer (src/presentation)

Outermost layer, user interface:

```
presentation/
├── components/ # Reusable components
├── pages/      # Main views
├── router/     # Route configuration
├── theme/      # Material-UI theme
└── utils/      # Utilities
```

## Technologies

- React 19.1.1
- TypeScript 5.9
- Material-UI 7.3
- Redux Toolkit 2.9
- React Router 7.9
- i18next 25.6

## Execution

```bash
npm install
npm run dev
```
