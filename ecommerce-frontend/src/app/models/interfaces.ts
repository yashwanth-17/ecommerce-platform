export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
}

export interface AuthResponse {
  token: string;
  userId: string;
  email: string;
  firstName: string;
  lastName: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  category: string;
  imageUrl: string;
  isActive: boolean;
}

export interface CartItem {
  id: number;
  productId: number;
  productName: string;
  productPrice: number;
  productImageUrl: string;
  quantity: number;
  totalPrice: number;
}

export interface AddToCartRequest {
  productId: number;
  quantity: number;
}

export interface Order {
  id: number;
  totalAmount: number;
  status: string;
  shippingAddress: string;
  paymentMethod: string;
  createdAt: Date;
  orderItems: OrderItem[];
}

export interface OrderItem {
  productId: number;
  productName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

export interface CreateOrderRequest {
  shippingAddress: string;
  paymentMethod: string;
}

