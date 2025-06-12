import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product';
import { CartService } from '../../services/cart';
import { AuthService } from '../../services/auth';
import { Product, AddToCartRequest } from '../../models/interfaces';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.scss']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  isLoading = false;
  searchTerm = '';
  selectedCategory = '';

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Check for search query parameter
    this.route.queryParams.subscribe(params => {
      if (params['search']) {
        this.searchTerm = params['search'];
        this.onSearch();
      } else if (params['category']) {
        this.selectedCategory = params['category'];
        this.onCategoryChange();
      } else {
        this.loadAllProducts();
      }
    });
  }

  loadAllProducts(): void {
    this.isLoading = true;
    this.productService.getAllProducts().subscribe({
      next: (products) => {
        this.products = products;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading products:', error);
        this.isLoading = false;
      }
    });
  }

  onSearch(): void {
    if (this.searchTerm.trim()) {
      this.isLoading = true;
      this.productService.searchProducts(this.searchTerm).subscribe({
        next: (products) => {
          this.products = products;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error searching products:', error);
          this.isLoading = false;
        }
      });
    } else {
      this.loadAllProducts();
    }
  }

  onCategoryChange(): void {
    if (this.selectedCategory) {
      this.isLoading = true;
      this.productService.getProductsByCategory(this.selectedCategory).subscribe({
        next: (products) => {
          this.products = products;
          this.isLoading = false;
        },
        error: (error) => {
          console.error('Error loading products by category:', error);
          this.isLoading = false;
        }
      });
    } else {
      this.loadAllProducts();
    }
  }

  viewProduct(productId: number): void {
    this.router.navigate(['/products', productId]);
  }

  addToCart(product: Product): void {
    if (!this.authService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    const cartItem: AddToCartRequest = {
      productId: product.id,
      quantity: 1
    };

    this.cartService.addToCart(cartItem).subscribe({
      next: () => {
        // Show success message or update UI
        console.log('Product added to cart successfully');
      },
      error: (error) => {
        console.error('Error adding product to cart:', error);
      }
    });
  }

  onImageError(event: any): void {
    event.target.src = '/assets/placeholder-product.jpg';
  }
}

