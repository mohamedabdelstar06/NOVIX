import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CatalogService } from '../../../core/services/catalog.service';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ButtonModule, CardModule, RatingModule, FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  products: any[] = [];
  loading: boolean = true;
  
  categories = [
    { label: 'All Essentials', icon: 'pi pi-compass', value: '' },
    { label: 'Electronics', icon: 'pi pi-desktop', value: 'Electronics' },
    { label: 'Smartphones', icon: 'pi pi-mobile', value: 'Smartphones' },
    { label: 'Home & Kitchen', icon: 'pi pi-home', value: 'Home' },
    { label: 'Fashion', icon: 'pi pi-shopping-bag', value: 'Fashion' },
    { label: 'Food & Groceries', icon: 'pi pi-apple', value: 'Food' }
  ];

  selectedCategory: string = '';

  constructor(private catalogService: CatalogService) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts(categorySearch: string = '') {
    this.loading = true;
    this.catalogService.getProducts(1, 20, categorySearch).subscribe({
      next: (res: any) => {
        this.products = res.items;
        this.loading = false;
      },
      error: (err: any) => {
        // Fallback mock data with random ratings and images if backend is offline
        this.products = [
          { name: 'iPhone 15 Pro Max', categoryName: 'Smartphones', brandName: 'Apple', basePrice: 1199, rating: 5, reviews: 1245, imageUrl: 'https://images.unsplash.com/photo-1695048133142-1a20484d2569?auto=format&fit=crop&q=80&w=800' },
          { name: 'Samsung Galaxy S24 Ultra', categoryName: 'Smartphones', brandName: 'Samsung', basePrice: 1299, rating: 4, reviews: 892, imageUrl: 'https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?auto=format&fit=crop&q=80&w=800' },
          { name: 'MacBook Pro M3 Max', categoryName: 'Electronics', brandName: 'Apple', basePrice: 3499, rating: 5, reviews: 342, imageUrl: 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?auto=format&fit=crop&q=80&w=800' },
          { name: 'Nike Air Max 270', categoryName: 'Fashion', brandName: 'Nike', basePrice: 150, rating: 4, reviews: 2100, imageUrl: 'https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&q=80&w=800' },
          { name: 'LG OLED C3 65" TV', categoryName: 'Home & Kitchen', brandName: 'LG', basePrice: 1599, rating: 5, reviews: 541, imageUrl: 'https://images.unsplash.com/photo-1593784991095-a205069470b6?auto=format&fit=crop&q=80&w=800' },
          { name: 'Nescafe Gold Blend', categoryName: 'Food', brandName: 'Nestlé', basePrice: 12, rating: 4, reviews: 89, imageUrl: 'https://images.unsplash.com/photo-1559525839-b184a4d698c7?auto=format&fit=crop&q=80&w=800' }
        ];
        
        if (categorySearch) {
           this.products = this.products.filter(p => p.categoryName.includes(categorySearch) || p.name.includes(categorySearch));
        }
        
        this.loading = false;
      }
    });
  }

  filterByCategory(category: any) {
    this.selectedCategory = category.value;
    this.loadProducts(this.selectedCategory);
  }
}
