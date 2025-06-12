-- E-commerce Database Schema for SQL Server

-- Create database
CREATE DATABASE EcommerceDB;
GO

USE EcommerceDB;
GO

-- Create Users table (extends AspNetUsers from Identity)
-- This is handled by ASP.NET Identity, but here's the conceptual structure
/*
CREATE TABLE AspNetUsers (
    Id NVARCHAR(450) NOT NULL PRIMARY KEY,
    UserName NVARCHAR(256),
    NormalizedUserName NVARCHAR(256),
    Email NVARCHAR(256),
    NormalizedEmail NVARCHAR(256),
    EmailConfirmed BIT NOT NULL,
    PasswordHash NVARCHAR(MAX),
    SecurityStamp NVARCHAR(MAX),
    ConcurrencyStamp NVARCHAR(MAX),
    PhoneNumber NVARCHAR(MAX),
    PhoneNumberConfirmed BIT NOT NULL,
    TwoFactorEnabled BIT NOT NULL,
    LockoutEnd DATETIMEOFFSET,
    LockoutEnabled BIT NOT NULL,
    AccessFailedCount INT NOT NULL,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
*/

-- Create Products table
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    Price DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL,
    Category NVARCHAR(100),
    ImageUrl NVARCHAR(500),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);

-- Create Orders table
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status INT NOT NULL DEFAULT 0, -- 0=Pending, 1=Processing, 2=Shipped, 3=Delivered, 4=Cancelled
    ShippingAddress NVARCHAR(500),
    PaymentMethod NVARCHAR(100),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

-- Create OrderItems table
CREATE TABLE OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Create CartItems table
CREATE TABLE CartItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(450) NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(Id) ON DELETE CASCADE,
    UNIQUE(UserId, ProductId)
);

-- Create indexes for better performance
CREATE INDEX IX_Products_Name ON Products(Name);
CREATE INDEX IX_Products_Category ON Products(Category);
CREATE INDEX IX_Orders_UserId ON Orders(UserId);
CREATE INDEX IX_CartItems_UserId_ProductId ON CartItems(UserId, ProductId);

-- Insert sample data
INSERT INTO Products (Name, Description, Price, StockQuantity, Category, ImageUrl, IsActive, CreatedAt, UpdatedAt)
VALUES 
    ('Laptop Computer', 'High-performance laptop for work and gaming', 999.99, 50, 'Electronics', 'https://example.com/laptop.jpg', 1, GETUTCDATE(), GETUTCDATE()),
    ('Wireless Headphones', 'Premium noise-cancelling wireless headphones', 199.99, 100, 'Electronics', 'https://example.com/headphones.jpg', 1, GETUTCDATE(), GETUTCDATE()),
    ('Coffee Mug', 'Ceramic coffee mug with custom design', 15.99, 200, 'Home & Kitchen', 'https://example.com/mug.jpg', 1, GETUTCDATE(), GETUTCDATE()),
    ('Smartphone', 'Latest model smartphone with advanced features', 799.99, 75, 'Electronics', 'https://example.com/smartphone.jpg', 1, GETUTCDATE(), GETUTCDATE()),
    ('Running Shoes', 'Comfortable running shoes for daily exercise', 89.99, 150, 'Sports & Outdoors', 'https://example.com/shoes.jpg', 1, GETUTCDATE(), GETUTCDATE());

GO

