# Million Luxury

Luxury real estate management system.

## Prerequisites

- .NET 9.0 SDK
- Node.js and npm
- MongoDB
- NoSQLBooster (or similar MongoDB tool)

## Running Instructions

### 1. Database

To import sample data:

1. Open NoSQLBooster
2. Connect to MongoDB (mongodb://localhost:27017)
3. Execute the `MillionLuxuryDB.js` script

### 2. Backend

```bash
cd backend/MillionLuxury.API
dotnet run
```

The API will be available at: http://localhost:5292

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

The application will be available at: http://localhost:5173

## Project Overview

Million Luxury is a web application for luxury property management that enables:

- Property owner management
- Property administration
- Property image management
- Real estate transaction tracking

### Technology Stack

- Backend: .NET 9.0, MongoDB
- Frontend: React 19, TypeScript, Material-UI


[demo-million-luxury.webm](https://github.com/user-attachments/assets/658986dd-d836-49cd-9bfe-c506d3f54b4d)

