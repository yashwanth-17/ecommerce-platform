import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../../services/cart';
import { AuthService } from '../../services/auth';
import { CartItem } from '../../models/interfaces';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart.html',
  styleUrls: ['./cart.scss']
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  isLoading = false;

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.loadCartItems();
  }

  loadCartItems(): void {
    this.isLoading = true;
    this.cartService.getCartItems().subscribe({
      next: (items) => {
        this.cartItems = items;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading cart items:', error);
        this.isLoading = false;
      }
    });
  }

  updateQuantity(item: CartItem, newQuantity: number): void {
    if (newQuantity < 1) return;

    this.cartService.updateCartItem(item.id, newQuantity).subscribe({
      next: () => {
        item.quantity = newQuantity;
        // Recalculate total price
        item.totalPrice = item.productPrice * newQuantity;
      },
      error: (error) => {
        console.error('Error updating cart item:', error);
      }
    });
  }

  removeItem(item: CartItem): void {
    this.cartService.removeFromCart(item.id).subscribe({
      next: () => {
        this.cartItems = this.cartItems.filter(ci => ci.id !== item.id);
      },
      error: (error) => {
        console.error('Error removing cart item:', error);
      }
    });
  }

  clearCart(): void {
    this.cartService.clearCart().subscribe({
      next: () => {
        this.cartItems = [];
      },
      error: (error) => {
        console.error('Error clearing cart:', error);
      }
    });
  }

  getCartTotal(): number {
    return this.cartItems.reduce((total, item) => total + item.totalPrice, 0);
  }

  proceedToCheckout(): void {
    // Navigate to checkout page (to be implemented)
    this.router.navigate(['/checkout']);
  }

  continueShopping(): void {
    this.router.navigate(['/products']);
  }

  onImageError(event: any): void {
    event.target.src = '/assets/placeholder-product.jpg';
  }
}

