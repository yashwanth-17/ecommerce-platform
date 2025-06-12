# Full-Stack E-commerce Platform

A complete e-commerce platform built with ASP.NET Core Web API, Angular, Entity Framework, and containerized with Docker and Kubernetes.

## 🚀 Features

- **User Authentication & Authorization**: JWT-based authentication with user registration and login
- **Product Management**: Browse, search, and filter products by category
- **Shopping Cart**: Add, remove, and update items in the cart
- **Order Processing**: Create and track orders
- **Responsive Design**: Mobile-friendly user interface
- **RESTful API**: Well-structured backend API with Swagger documentation
- **Containerization**: Docker and Kubernetes support for easy deployment
- **Database**: Entity Framework with SQLite (development) and SQL Server (production) support

## 🏗️ Architecture

### Backend (ASP.NET Core Web API)
- **Framework**: .NET 6.0
- **Database**: Entity Framework Core with SQLite/SQL Server
- **Authentication**: JWT tokens with ASP.NET Identity
- **API Documentation**: Swagger/OpenAPI
- **CORS**: Configured for cross-origin requests

### Frontend (Angular)
- **Framework**: Angular 17+ with TypeScript
- **Styling**: SCSS with responsive design
- **HTTP Client**: Angular HttpClient with interceptors
- **Routing**: Angular Router with lazy loading
- **State Management**: Services with RxJS observables

### Database
- **Development**: SQLite for easy setup
- **Production**: SQL Server with proper schema and relationships
- **ORM**: Entity Framework Core with Code First migrations

### DevOps
- **Containerization**: Docker with multi-stage builds
- **Orchestration**: Kubernetes with deployments, services, and ingress
- **Reverse Proxy**: Nginx for frontend with API proxying

## 📁 Project Structure

```
ecommerce-platform/
├── backend/                    # ASP.NET Core Web API
│   ├── Controllers/           # API controllers
│   ├── Data/                  # Entity Framework DbContext
│   ├── DTOs/                  # Data Transfer Objects
│   ├── Models/                # Entity models
│   ├── Services/              # Business logic services
│   ├── Program.cs             # Application entry point
│   └── appsettings.json       # Configuration
├── ecommerce-frontend/        # Angular application
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/    # Angular components
│   │   │   ├── services/      # Angular services
│   │   │   ├── models/        # TypeScript interfaces
│   │   │   └── interceptors/  # HTTP interceptors
│   │   └── assets/            # Static assets
│   ├── angular.json           # Angular configuration
│   └── package.json           # NPM dependencies
├── database/                  # Database scripts and documentation
│   ├── schema.sql             # SQL Server schema
│   └── README.md              # Database setup guide
├── docker/                    # Docker configuration
│   ├── backend.Dockerfile     # Backend container
│   ├── frontend.Dockerfile    # Frontend container
│   ├── docker-compose.yml     # Multi-container setup
│   ├── nginx.conf             # Nginx configuration
│   └── README.md              # Deployment guide
├── kubernetes/                # Kubernetes manifests
│   ├── backend-deployment.yaml
│   ├── frontend-deployment.yaml
│   ├── database-deployment.yaml
│   └── configmap.yaml
└── docs/                      # Additional documentation
```

## 🚀 Quick Start

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/) (optional)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (for production)

### Local Development

#### 1. Backend Setup

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at `http://localhost:5000`

#### 2. Frontend Setup

```bash
cd ecommerce-frontend
npm install
ng serve
```

The application will be available at `http://localhost:4200`

### Docker Deployment

```bash
# Build and run with Docker Compose
docker-compose -f docker/docker-compose.yml up -d

# Access the application
# Frontend: http://localhost
# Backend: http://localhost:5000
```

### Kubernetes Deployment

```bash
# Deploy to Kubernetes
kubectl apply -f kubernetes/

# Port forward for local access
kubectl port-forward service/ecommerce-frontend-service 8080:80
```

## 📚 API Documentation

Once the backend is running, visit `http://localhost:5000/swagger` for interactive API documentation.

### Key Endpoints

- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/cart` - Add item to cart
- `GET /api/cart` - Get cart items
- `POST /api/orders` - Create order

## 🔧 Configuration

### Backend Configuration (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecommerce.db"
  },
  "JwtSettings": {
    "SecretKey": "YourSecretKey",
    "Issuer": "EcommerceAPI",
    "Audience": "EcommerceClient",
    "ExpiryInMinutes": 60
  }
}
```

### Frontend Configuration

Update the API URL in the services if needed:
- `src/app/services/auth.ts`
- `src/app/services/product.ts`
- `src/app/services/cart.ts`
- `src/app/services/order.ts`

## 🧪 Testing

### Backend Testing

```bash
cd backend
dotnet test
```

### Frontend Testing

```bash
cd ecommerce-frontend
npm test
```

## 🚀 Deployment

### Production Considerations

1. **Database**: Use SQL Server or PostgreSQL in production
2. **Security**: Update JWT secret keys and use HTTPS
3. **Environment Variables**: Use environment-specific configurations
4. **Monitoring**: Implement logging and monitoring
5. **Scaling**: Use load balancers and multiple instances

### Environment Variables

#### Backend
- `ASPNETCORE_ENVIRONMENT`
- `ConnectionStrings__DefaultConnection`
- `JwtSettings__SecretKey`

#### Frontend
- `API_URL` (configured in nginx.conf for Docker)

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🆘 Support

For support and questions:
- Check the documentation in the `docs/` folder
- Review the Docker and Kubernetes deployment guides
- Check the database setup instructions

## 🔄 Version History

- **v1.0.0** - Initial release with core e-commerce functionality
  - User authentication and authorization
  - Product catalog and search
  - Shopping cart functionality
  - Order management
  - Docker and Kubernetes support

## 🛠️ Built With

- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) - Backend framework
- [Angular](https://angular.io/) - Frontend framework
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM
- [JWT](https://jwt.io/) - Authentication
- [Docker](https://www.docker.com/) - Containerization
- [Kubernetes](https://kubernetes.io/) - Orchestration
- [Nginx](https://nginx.org/) - Reverse proxy

