# New Zealand Walking Trails API Documentation

This document provides an overview of the API's functionality, endpoints, usage instructions, and important considerations. This API serves as a backend resource for retrieving and managing information about various walking trails across New Zealand.

## Table of Contents

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Endpoints](#endpoints)
  - [Regions](#regions)
  - [Walks](#walks)
- [Authentication](#authentication)
- [Examples](#examples)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The New Zealand Walking Trails API is designed to provide users with information about various walking trails located in New Zealand. It allows users to retrieve details about different regions and the walks available within those regions.

## Technologies Used

- Programming Languages: C#
- Web Framework: ASP.NET Core
- Database: SQL
- ORM: Entity Framework Core
- Authentication: JWT (JSON Web Tokens)

## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 7.0) or 

### Installation

1. Clone this repository to your local machine.
2. Navigate to the project directory using the terminal.
3. Run the following command to build and run the API:

   ```shell
   dotnet run
   ```

4. The API will be accessible at `http://localhost:portNumber`.

## Endpoints

### Regions

- `GET /api/regions`: Get a list of all regions.
- `GET /api/regions/{id}`: Get information about a specific region.
- `POST /api/regions`: Create a new region (authentication required).
- `PUT /api/regions/{id}`: Update information about a region (authentication required).
- `DELETE /api/regions/{id}`: Delete a region (authentication required).

### Walks

- `POST /api/walks`: Create a new walk (authentication required).
- `GET /api/walks`: Get a list of walks with optional filtering, sorting, and pagination.
- `GET /api/walks/{id}`: Get information about a specific walk.
- `PUT /api/walks/{id}`: Update information about a walk (authentication required).
- `DELETE /api/walks/{id}`: Delete a walk (authentication required).

## Authentication

The API uses JWT (JSON Web Tokens) for authentication. Certain endpoints require specific roles for access. Roles are defined as "Reader" and "Writer."

To authenticate, include the JWT token in the authorization header of your requests:

```
Authorization: Bearer <your_token_here>
```

## Examples

### Get All Regions

```
GET /api/regions
```

Response:

```json
[
  {
    "id": "region_id",
    "name": "Region Name",
    ...
  },
  ...
]
```
