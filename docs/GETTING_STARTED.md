# Getting Started Guide

## Prerequisites Installation

### 1. Install .NET 6.0 SDK
- Download from: https://dotnet.microsoft.com/download/dotnet/6.0
- Verify installation: `dotnet --version`

### 2. Install Node.js and npm
- Download from: https://nodejs.org/ (LTS version recommended)
- Verify installation: `node --version` and `npm --version`

### 3. Install Angular CLI
```bash
npm install -g @angular/cli
ng version
```

### 4. Install Docker (Optional)
- Download from: https://www.docker.com/get-started
- Verify installation: `docker --version`

## Step-by-Step Setup

### Backend Setup

1. **Navigate to backend directory**
   ```bash
   cd backend
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Create database and apply migrations**
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the backend**
   ```bash
   dotnet run
   ```
   
   The API will be available at: http://localhost:5000
   Swagger documentation: http://localhost:5000/swagger

### Frontend Setup

1. **Navigate to frontend directory**
   ```bash
   cd ecommerce-frontend
   ```

2. **Install npm packages**
   ```bash
   npm install
   ```

3. **Start the development server**
   ```bash
   ng serve
   ```
   
   The application will be available at: http://localhost:4200

## First Time Usage

### 1. Register a New User
- Navigate to http://localhost:4200
- Click "Register" in the top navigation
- Fill in the registration form
- You'll be automatically logged in after registration

### 2. Browse Products
- The home page shows all available products
- Use the search bar to find specific products
- Filter by category using the dropdown

### 3. Add Items to Cart
- Click "Add to Cart" on any product
- View your cart by clicking the cart icon in the navigation
- Adjust quantities or remove items as needed

### 4. Place an Order
- In your cart, click "Proceed to Checkout"
- Fill in shipping and payment information
- Complete your order

## Development Workflow

### Making Changes to the Backend

1. **Add new models**: Create in `Models/` folder
2. **Create migrations**: `dotnet ef migrations add <MigrationName>`
3. **Update database**: `dotnet ef database update`
4. **Add services**: Implement in `Services/` folder
5. **Create controllers**: Add to `Controllers/` folder

### Making Changes to the Frontend

1. **Generate components**: `ng generate component <component-name>`
2. **Generate services**: `ng generate service <service-name>`
3. **Update routing**: Modify `app.routes.ts`
4. **Add styles**: Update component SCSS files

### Testing Your Changes

1. **Backend tests**: `dotnet test` (from backend directory)
2. **Frontend tests**: `npm test` (from frontend directory)
3. **Manual testing**: Use both the web interface and Swagger UI

## Common Issues and Solutions

### Backend Issues

**Issue**: Database connection errors
**Solution**: Ensure the SQLite database file is created and migrations are applied

**Issue**: CORS errors
**Solution**: Verify CORS is configured in `Program.cs`

**Issue**: JWT authentication not working
**Solution**: Check JWT settings in `appsettings.json`

### Frontend Issues

**Issue**: API calls failing
**Solution**: Verify the backend is running on port 5000

**Issue**: Routing not working
**Solution**: Check `app.routes.ts` configuration

**Issue**: Styling issues
**Solution**: Verify SCSS compilation and imports

### Docker Issues

**Issue**: Container build failures
**Solution**: Check Dockerfile syntax and base image availability

**Issue**: Container networking
**Solution**: Verify docker-compose network configuration

## Next Steps

1. **Explore the codebase**: Familiarize yourself with the project structure
2. **Read the API documentation**: Visit the Swagger UI
3. **Try the Docker setup**: Use docker-compose for full-stack deployment
4. **Customize the application**: Add new features or modify existing ones
5. **Deploy to production**: Follow the deployment guides in the docker/ folder

## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Angular Documentation](https://angular.io/docs)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Docker Documentation](https://docs.docker.com/)
- [Kubernetes Documentation](https://kubernetes.io/docs/)

## Support

If you encounter any issues:
1. Check this getting started guide
2. Review the main README.md
3. Check the specific documentation in the docker/ and database/ folders
4. Verify all prerequisites are correctly installed

