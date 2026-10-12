import { Component, OnInit } from '@angular/core';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { MenuModule } from 'primeng/menu';
import { MenuItem, ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { CatalogService } from '../../../../core/services/catalog.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [TableModule, ButtonModule, CommonModule, RouterModule, MenuModule, ConfirmDialogModule, ToastModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  selectedProduct: any = null;
  menuItems: MenuItem[] = [];
  totalRecords: number = 0;
  loading: boolean = true;

  constructor(
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private catalogService: CatalogService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();

    this.menuItems = [
      {
        label: 'Edit',
        icon: 'pi pi-pencil',
        command: () => {
          if (this.selectedProduct) {
            this.router.navigate(['/admin/products/edit', this.selectedProduct.id]);
          }
        }
      },
      {
        label: 'Delete',
        icon: 'pi pi-trash',
        command: () => {
          this.confirmDelete(this.selectedProduct);
        }
      }
    ];
  }

  loadProducts(page: number = 1, size: number = 10, search: string = '') {
    this.loading = true;
    this.catalogService.getProducts(page, size, search).subscribe({
      next: (res: any) => {
        this.products = res.items;
        this.totalRecords = res.totalCount;
        this.loading = false;
      },
      error: (err: any) => {
        const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to load products');
        this.messageService.add({ severity: 'error', summary: 'Error', detail: msg });
        this.products = [];
        this.totalRecords = 0;
        this.loading = false;
      }
    });
  }

  confirmDelete(product: any) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete this product?',
      header: 'Delete Confirmation',
      icon: 'pi pi-info-circle',
      acceptButtonStyleClass: 'p-button-danger p-button-text',
      rejectButtonStyleClass: 'p-button-text p-button-text',
      acceptIcon: 'none',
      rejectIcon: 'none',
      accept: () => {
        this.catalogService.deleteProduct(product.id).subscribe({
            next: () => {
                this.products = this.products.filter(p => p.id !== product.id);
                this.messageService.add({ severity: 'success', summary: 'Confirmed', detail: 'Product deleted successfully.' });
            },
            error: (err: any) => {
                const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to delete product');
                this.messageService.add({ severity: 'error', summary: 'Error', detail: msg });
            }
        });
      },
      reject: () => {
        this.messageService.add({ severity: 'error', summary: 'Rejected', detail: 'You have rejected' });
      }
    });
  }
}
