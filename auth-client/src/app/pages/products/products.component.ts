import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsService, Product } from '../../services/products.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="products-container">
      <h2>Products</h2>

      <button (click)="reload()">Reload</button>

      <p *ngIf="loading">Loading...</p>
      <p *ngIf="error" class="error">{{ error }}</p>

      <ul *ngIf="!loading && !error">
        <li *ngFor="let p of products">
          {{ p.id }} - {{ p.name }}
        </li>
      </ul>
    </div>
  `,
  styles: [`
    .products-container {
      max-width: 500px;
      margin: 40px auto;
      font-family: Arial, sans-serif;
    }

    h2 {
      text-align: center;
    }

    .error {
      color: #b00020;
      margin-top: 10px;
    }

    button {
      margin-bottom: 10px;
    }
  `]
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  loading = false;
  error = '';

  constructor(private productsService: ProductsService) {}

  ngOnInit(): void {
    this.reload();
  }

  reload(): void {
    this.loading = true;
    this.error = '';
    this.productsService.getProducts().subscribe({
      next: (data) => {
        this.loading = false;
        this.products = data;
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Failed to load products (maybe token invalid or API down).';
        console.error(err);
      }
    });
  }
}
