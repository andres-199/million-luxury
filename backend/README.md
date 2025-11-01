# Backend - Million Luxury

## Clean Architecture

The backend implements Clean Architecture with four concentric layers, where dependencies point toward the center:

### 1. Domain Layer (MillionLuxury.Domain)

Core of the application, with no external dependencies:

```
Entities/           # Pure business entities
├── Owner.cs        # Property owners
├── Property.cs     # Properties
├── PropertyImage.cs# Images
└── PropertyTrace.cs# Transaction history
```

- Defines core business rules and entities
- No dependencies on other layers
- Contains only pure domain logic

### 2. Application Layer (MillionLuxury.Application)

Orchestrates data flow and use cases:

```
Features/           # CQRS Implementation
├── Owners/         # Owner use cases
├── Properties/     # Property use cases
├── PropertyImages/ # Image use cases
└── PropertyTraces/ # Transaction use cases
```

- Implements use cases using CQRS and MediatR
- Defines repository interfaces
- No infrastructure dependencies

### 3. Infrastructure Layer (MillionLuxury.Infrastructure)

Technical implementations:

- MongoDB repositories
- External services
- Configurations
- Concrete interface implementations

### 4. API Layer (MillionLuxury.API)

Application entry point:

```
Controllers/                 # REST Endpoints
├── OwnersController.cs
├── PropertiesController.cs
├── PropertyImagesController.cs
└── PropertyTracesController.cs
```

- REST Controllers
- Application configuration
- Dependency injection
- Middleware

## Technologies

- .NET 9.0
- MediatR 13.1.0
- MongoDB
- Swagger

## Execution

```bash
cd MillionLuxury.API
dotnet run
```
