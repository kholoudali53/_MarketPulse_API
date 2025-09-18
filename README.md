# MarketPulse API

## Description

The MarketPulse API provides a comprehensive set of endpoints for managing products, user accounts, customer baskets, and orders. It leverages ASP.NET Core, Entity Framework Core, and a variety of other technologies to deliver a robust and scalable e-commerce backend.

## Features and Functionality

*   **Product Management:**
    *   Retrieve paginated lists of products with filtering, sorting, and searching capabilities.
    *   Get product details by ID.
    *   Retrieve product brands and types.
*   **User Authentication and Authorization:**
    *   User registration and login.
    *   JWT-based authentication.
    *   Retrieval of current user information and address.
*   **Customer Basket Management:**
    *   Create, retrieve, update, and delete customer baskets.
*   **Order Management:**
    *   Create orders.
    *   Retrieve orders for a specific user.
    *   Retrieve order details by ID for a specific user.
*   **Caching:**
    *   Caching of product lists using Redis for improved performance.
*   **Error Handling:**
    *   Centralized exception handling middleware.
    *   Custom API error responses.

## Technology Stack

*   **ASP.NET Core:** Web API framework
*   **Entity Framework Core:** ORM for database interaction
*   **SQL Server:** Relational database
*   **Redis:** In-memory data store for caching
*   **AutoMapper:** Object-object mapping
*   **Microsoft.AspNetCore.Identity:**  Authentication and authorization
*   **JWT (JSON Web Tokens):** Authentication mechanism
*   **StackExchange.Redis:** Redis client library
*   **.NET 9.0:** Development platform

## Prerequisites

*   .NET SDK 9.0 or higher
*   SQL Server instance
*   Redis server instance

## Installation Instructions

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/kholoudali53/_MarketPulse_API.git
    cd _MarketPulse_API
    ```

2.  **Configure Database Connections:**

    *   Update the connection strings in `Store.G01.APIs/appsettings.json` for both the main `StoreDbContext` and the `StoreIdentityDbContext`.  Example:

        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=your_server;Database=MarketPulseDB;Trusted_Connection=True;TrustServerCertificate=True",
            "IdentityConnection": "Server=your_server;Database=MarketPulseIdentityDB;Trusted_Connection=True;TrustServerCertificate=True",
            "Redis": "your_redis_connection_string"
          },
          "Jwt": {
            "Key": "Your_Secret_Key_Here",
            "Issuer": "https://localhost:7241/",
            "Audience": "https://localhost:7241/",
            "DurationInDays": "7"
          },
          "BASEURL": "https://localhost:7241/"
        }
        ```

    *Replace `your_server`, `MarketPulseDB`, `MarketPulseIdentityDB`, `your_redis_connection_string`, and `"Your_Secret_Key_Here"` with your actual SQL Server, Redis connection details and secret Key.*

3.  **Apply Database Migrations:**

    ```bash
    # Navigate to the repository layer
    cd Store.G01.Repository

    # Apply migrations for the main database
    dotnet ef database update -p Store.G01.Repository -s ../Store.G01.APIs

    # Apply migrations for the Identity database
    dotnet ef database update -p Store.G01.Repository.Identity -s ../Store.G01.APIs
    ```

4.  **Seed the Databases:**

    The application seeds the database with initial data on startup. This is handled in `Store.G01.APIs/Helper/ConfigureMiddleware.cs`.  However, ensure the `StoreDbContextSeed.SeedAsync` and `StoreIdentityDbContextSeed.SeedAppUserAsync` methods are executed.

5.  **Build and Run the API:**

    ```bash
    # Navigate back to the API layer
    cd ../Store.G01.APIs

    # Build and run the API
    dotnet run
    ```

    The API will be accessible at `https://localhost:7241` (or the port configured in `launchSettings.json`).

## Usage Guide

### Authentication

*   **Register:** `POST /api/Accounts/register`
    *   Requires: `FirstName`, `LastName`, `Email`, `Password`, `PhoneNo` in the request body.
    *   Returns: `UserDto` containing `Email`, `DisplayName`, and `Token`.
*   **Login:** `POST /api/Accounts/login`
    *   Requires: `Email`, `Password` in the request body.
    *   Returns: `UserDto` containing `Email`, `DisplayName`, and `Token`.
*   **Get Current User:** `GET /api/Accounts/GetCurrentUser`
    *   Requires: Valid JWT token in the `Authorization` header.
    *   Returns: `UserDto` containing `Email`, `DisplayName`, and `Token`.
*   **Get User Address:** `GET /api/Accounts/Address`
    *   Requires: Valid JWT token in the `Authorization` header.
    *   Returns: `AddressDto` containing address information.

### Products

*   **Get All Products:** `GET /api/Products`
    *   Optional Query Parameters:
        *   `sort`: Sort order (`name`, `pricAsc`, `priceDesc`).
        *   `brandId`: Filter by brand ID.
        *   `typeId`: Filter by type ID.
        *   `pageSize`: Number of items per page (default: 5).
        *   `pageIndex`: Page number (default: 1).
        *   Requires: Valid JWT token in the `Authorization` header.
    *   Returns: `PaginationResponse<ProductDto>` containing a list of products and pagination information.
*   **Get Product by ID:** `GET /api/Products/{id}`
    *   Requires: Valid JWT token in the `Authorization` header.
    *   Returns: `ProductDto` containing product details.
*   **Get All Brands:** `GET /api/Products/brands`
    *   Returns: `IEnumerable<TypeBrandDto>` containing a list of product brands.
*   **Get All Types:** `GET /api/Products/types`
    *   Returns: `IEnumerable<TypeBrandDto>` containing a list of product types.

### Basket

*   **Get Basket:** `GET /api/Basket?id={id}`
    *   Returns: `CustomerBasket` basket details by Id.
*   **Create or update Basket:** `POST /api/Basket`
    *   Requires: `CustomerBasketDto` in the request body.
    *   Returns: `CustomerBasket` containing the updated basket details.
*   **Delete Basket:** `DELETE /api/Basket?id={id}`

### Orders

*   **Create Order:** `POST /api/Orders`
    *   Requires: `BasketId`, `DeliveryMethodId`, and `shipToAddress` (AddressDto) in the request body.
    *   Requires: Valid JWT token in the `Authorization` header.
    *   Returns: `OrderToReturnDto` containing order details.

## API Documentation

Swagger is enabled for this project.  After running the API, access the Swagger UI at `https://localhost:7241/swagger/index.html`. This provides interactive documentation for all available endpoints.

## Contributing Guidelines

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix.
3.  Implement your changes.
4.  Write unit tests for your changes.
5.  Ensure all tests pass.
6.  Submit a pull request.

