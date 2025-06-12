import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { CartItem, AddToCartRequest } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5000/api/cart';
  private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);
  public cartItems$ = this.cartItemsSubject.asObservable();

  constructor(private http: HttpClient) { }

  getCartItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(this.apiUrl)
      .pipe(
        tap(items => this.cartItemsSubject.next(items))
      );
  }

  addToCart(item: AddToCartRequest): Observable<CartItem> {
    return this.http.post<CartItem>(this.apiUrl, item)
      .pipe(
        tap(() => this.refreshCart())
      );
  }

  updateCartItem(cartItemId: number, quantity: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}/${cartItemId}`, { quantity })
      .pipe(
        tap(() => this.refreshCart())
      );
  }

  removeFromCart(cartItemId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/${cartItemId}`)
      .pipe(
        tap(() => this.refreshCart())
      );
  }

  clearCart(): Observable<boolean> {
    return this.http.delete<boolean>(`${this.apiUrl}/clear`)
      .pipe(
        tap(() => this.cartItemsSubject.next([]))
      );
  }

  private refreshCart(): void {
    this.getCartItems().subscribe();
  }

  getCartTotal(): number {
    return this.cartItemsSubject.value.reduce((total, item) => total + item.totalPrice, 0);
  }

  getCartItemCount(): number {
    return this.cartItemsSubject.value.reduce((count, item) => count + item.quantity, 0);
  }
}

