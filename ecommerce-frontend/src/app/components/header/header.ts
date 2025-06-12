import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';
import { CartService } from '../../services/cart';
import { User } from '../../models/interfaces';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './header.html',
  styleUrls: ['./header.scss']
})
export class HeaderComponent implements OnInit {
  currentUser: User | null = null;
  cartItemCount = 0;
  searchTerm = '';

  constructor(
    private authService: AuthService,
    private cartService: CartService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
      if (user) {
        this.cartService.getCartItems().subscribe();
      }
    });

    this.cartService.cartItems$.subscribe(items => {
      this.cartItemCount = this.cartService.getCartItemCount();
    });
  }

  onSearch(): void {
    if (this.searchTerm.trim()) {
      this.router.navigate(['/products'], { queryParams: { search: this.searchTerm } });
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}

