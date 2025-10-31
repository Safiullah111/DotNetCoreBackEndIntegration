# DotNetCoreBackEndIntegration
# API.Work

**API.Work** is a modular, scalable, and maintainable .NET Web API project that implements **CQRS**, centralized **logging**, and **AutoMapper** to build a clean architecture with separation of concerns and high testability.

---

## Table of Contents

- [Project Overview](#project-overview)  
- [Architecture](#architecture)  
- [Features](#features)  
- [Tech Stack](#tech-stack)  
- [Getting Started](#getting-started)  
- [Configuration](#configuration)  
- [Usage](#usage)  
- [Logging](#logging)  
- [Object Mapping (AutoMapper)](#object-mapping-automapper)  
- [CQRS Pattern](#cqrs-pattern)  
- [Testing](#testing)  
- [Contributing](#contributing)  
- [License](#license)  

---

## Project Overview

API.Work is designed to provide a robust framework for enterprise-grade Web APIs following best practices such as **CQRS** (Command Query Responsibility Segregation), **centralized structured logging** with Serilog, and **object mapping** using AutoMapper. This separation promotes maintainability, scalability, and cleaner code.

---

## Architecture

The solution is divided into layered projects, each with a clear responsibility:

| Layer                        | Responsibility                                     |
|------------------------------|--------------------------------------------------	|
| **API.Work.Presentation**    | REST API endpoints, request validation, controllers|
| **API.Work.Application**     | Business logic, command and query handlers			|
| **API.Work.Application.Contracts** | DTOs and interface contracts                 |
| **API.Work.Domain**          | Core business entities and domain logic			|
| **API.Work.Domain.Shared**   | Shared kernel: common utilities and base classes	|
| **API.Work.EntityFramework** | Data access implementation with Entity Framework	|

---

## Features

- CQRS pattern separating reads and writes via MediatR  
- Centralized logging using Serilog with file sinks and console output  
- AutoMapper for mapping between domain entities and DTOs  
- Clean architecture ensuring testability and maintainability  
- Integration with Entity Framework Core for data persistence  
- Global error handling and structured API responses  
- Unit and integration tests covering business logic and data access  

---

## Tech Stack

- **.NET 9** (or your .NET version)  
- **MediatR** for CQRS and mediator pattern  
- **Serilog** for structured logging  
- **AutoMapper** for object-to-object mapping  
- **Entity Framework Core** for ORM and database interaction  
- **Swagger / OpenAPI** for API documentation  
- **xUnit / NUnit** for testing  

---

## Security

### Authentication

This project uses **JWT (JSON Web Token)** for authentication, ensuring secure and stateless user sessions. Tokens are issued after successful login and must be included in the `Authorization` header of API requests as a Bearer token.

### Authorization

Two levels of authorization are implemented:

- **Role-Based Authorization**: Access to API endpoints is controlled based on user roles (e.g., Admin, User, Manager). Roles are embedded within the JWT claims and enforced via `[Authorize(Roles = "Admin")]` attributes or policies.

- **Permission-Based Authorization**: For more granular control, permissions (such as `CanEditUsers`, `CanViewReports`) are managed and checked against user claims or a custom permission handler, enabling fine-grained access control beyond roles.

### How to Use

- Assign roles and permissions when creating users.
- Protect controllers or actions with appropriate `[Authorize]` attributes specifying roles or policies.
- Ensure clients include the JWT token in the `Authorization` header:


## Getting Started

### Prerequisites

- [.NET 9.0.0 SDK](https://dotnet.microsoft.com/en-us/download)  
- SQL Server / other supported DB  
- IDE like Visual Studio or VS Code  

### Clone & Run

```bash
git clone https://github.com/your-repo/api.work.git
cd api.work
dotnet restore
dotnet build
dotnet run --project src/API.Work.Presentation
