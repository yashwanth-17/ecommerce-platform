# Database Setup Instructions

## SQL Server Setup (Production)

1. Install SQL Server 2019 or later
2. Create a new database named `EcommerceDB`
3. Run the schema script: `database/schema.sql`
4. Update the connection string in `appsettings.json` to point to your SQL Server instance

Example connection string for SQL Server:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EcommerceDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

## SQLite Setup (Development)

For development purposes, the application is configured to use SQLite by default.

1. The SQLite database file will be created automatically when you run the application
2. Entity Framework migrations will create the necessary tables
3. Sample data will be seeded automatically

## Entity Framework Migrations

To create and apply migrations:

```bash
# Navigate to the backend directory
cd backend

# Add a new migration
dotnet ef migrations add InitialCreate

# Update the database
dotnet ef database update
```

## Database Features

- **User Management**: Handled by ASP.NET Identity
- **Product Catalog**: Products with categories, pricing, and inventory
- **Shopping Cart**: User-specific cart items
- **Order Management**: Order tracking with status updates
- **Data Relationships**: Proper foreign key constraints and indexes

## Sample Data

The database includes sample products in various categories:
- Electronics (Laptop, Headphones, Smartphone)
- Home & Kitchen (Coffee Mug)
- Sports & Outdoors (Running Shoes)

## Security Considerations

- All user passwords are hashed using ASP.NET Identity
- JWT tokens are used for authentication
- Database connections use parameterized queries to prevent SQL injection
- Proper foreign key constraints maintain data integrity

