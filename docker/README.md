# Docker and Kubernetes Deployment Guide

## Prerequisites

- Docker and Docker Compose installed
- Kubernetes cluster (minikube, kind, or cloud provider)
- kubectl configured to access your cluster

## Docker Deployment

### Building Images

```bash
# Build backend image
docker build -f docker/backend.Dockerfile -t ecommerce-backend:latest ./backend

# Build frontend image
docker build -f docker/frontend.Dockerfile -t ecommerce-frontend:latest ./ecommerce-frontend
```

### Running with Docker Compose

```bash
# Start all services
docker-compose -f docker/docker-compose.yml up -d

# View logs
docker-compose -f docker/docker-compose.yml logs -f

# Stop all services
docker-compose -f docker/docker-compose.yml down
```

### Accessing the Application

- Frontend: http://localhost
- Backend API: http://localhost:5000
- Database: localhost:5432

## Kubernetes Deployment

### Deploy to Kubernetes

```bash
# Create namespace and configmap
kubectl apply -f kubernetes/configmap.yaml

# Deploy database
kubectl apply -f kubernetes/database-deployment.yaml

# Deploy backend
kubectl apply -f kubernetes/backend-deployment.yaml

# Deploy frontend
kubectl apply -f kubernetes/frontend-deployment.yaml
```

### Verify Deployment

```bash
# Check pod status
kubectl get pods -n ecommerce

# Check services
kubectl get services -n ecommerce

# Check ingress
kubectl get ingress -n ecommerce
```

### Scaling

```bash
# Scale backend
kubectl scale deployment ecommerce-backend --replicas=3 -n ecommerce

# Scale frontend
kubectl scale deployment ecommerce-frontend --replicas=3 -n ecommerce
```

### Monitoring

```bash
# View logs
kubectl logs -f deployment/ecommerce-backend -n ecommerce
kubectl logs -f deployment/ecommerce-frontend -n ecommerce

# Port forward for local access
kubectl port-forward service/ecommerce-frontend-service 8080:80 -n ecommerce
kubectl port-forward service/ecommerce-backend-service 5000:5000 -n ecommerce
```

## Environment Variables

### Backend Environment Variables

- `ASPNETCORE_ENVIRONMENT`: Application environment (Development/Production)
- `ConnectionStrings__DefaultConnection`: Database connection string
- `JwtSettings__SecretKey`: JWT signing key
- `JwtSettings__Issuer`: JWT issuer
- `JwtSettings__Audience`: JWT audience

### Frontend Environment Variables

- `API_URL`: Backend API URL (configured in nginx.conf)

## Health Checks

Both frontend and backend include health check endpoints:

- Backend: `GET /api/health`
- Frontend: `GET /` (nginx status)

## Security Considerations

1. **Secrets Management**: Use Kubernetes secrets for sensitive data
2. **Network Policies**: Restrict pod-to-pod communication
3. **Resource Limits**: Set CPU and memory limits
4. **Image Security**: Scan images for vulnerabilities
5. **HTTPS**: Use TLS certificates in production

## Troubleshooting

### Common Issues

1. **Database Connection**: Ensure database is running and accessible
2. **Image Pull**: Verify image names and registry access
3. **Port Conflicts**: Check for port conflicts on host machine
4. **Resource Limits**: Ensure sufficient cluster resources

### Debug Commands

```bash
# Describe pod for detailed information
kubectl describe pod <pod-name> -n ecommerce

# Execute commands in pod
kubectl exec -it <pod-name> -n ecommerce -- /bin/bash

# View events
kubectl get events -n ecommerce --sort-by='.lastTimestamp'
```

## Production Considerations

1. **Database**: Use managed database service (AWS RDS, Azure SQL, etc.)
2. **Load Balancer**: Configure external load balancer
3. **SSL/TLS**: Set up SSL certificates
4. **Monitoring**: Implement logging and monitoring (Prometheus, Grafana)
5. **Backup**: Set up automated backups
6. **CI/CD**: Implement automated deployment pipeline

