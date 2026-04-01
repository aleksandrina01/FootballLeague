# FootballLeague

FootballLeague is a C# **Web API** application designed to help you manage football teams and matches, and display league ranking via RESTful endpoints. The project provides a foundation for building, extending, and integrating a football league management system with database support.

---

## Features

- RESTful Web API for managing teams and matches
- Endpoints for CRUD operations on teams, matches and ranking
- Calculate and display league ranking
- Interactive API documentation with **Swagger UI**
- Designed for extensibility and integration with web or mobile frontends

---

## Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (or SQL Server Express)
- Git

### Clone the Repository

```bash
git clone https://github.com/aleksandrina01/FootballLeague.git
cd FootballLeague
```

---

## Database Setup

You’ll need a SQL Server database for the API. Here’s an example connection string for local development:

```plaintext
Server=(localdb)\mssqllocaldb;Database=FootballLeagueDb;Trusted_Connection=True;
```

**Instructions:**

1. Update the connection string above in your `appsettings.json` (or as required by your project).
2. If using Entity Framework, run migrations to create the database schema:

    ```bash
    dotnet ef database update
    ```

---

## Configuration

Edit your `appsettings.json` file as follows:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FootballLeagueDb;Trusted_Connection=True;"
  }
}
```

---

## Running the Application

Restore dependencies and start the Web API:

```bash
dotnet restore
dotnet build
dotnet run
```

The API will be available at [http://localhost:5000](http://localhost:5000) (default port).

---

## API Documentation – Swagger

This project comes with integrated **Swagger** for interactive API documentation and testing.

- After running the Web API, navigate to:

  ```
  http://localhost:5000/swagger
  ```
  or
  ```
  http://localhost:<your_port>/swagger
  ```

- Browse available endpoints, view request/response formats, and test API calls directly from your browser.

---

## License

Distributed under the MIT License. See `LICENSE` for more information.

---

## Contact

Project maintained by [aleksandrina01](https://github.com/aleksandrina01)
