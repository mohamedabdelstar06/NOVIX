import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { MessageModule } from 'primeng/message';
import { MessageService } from 'primeng/api';
import { Router, ActivatedRoute } from '@angular/router';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CatalogService } from '../../../../core/services/catalog.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, InputNumberModule, DropdownModule, ButtonModule, MessageModule, InputSwitchModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.scss'
})
export class ProductFormComponent implements OnInit {
  product: any = {
    name: '',
    slug: '',
    shortDescription: 'Auto-generated',
    description: 'Auto-generated desc',
    categoryId: null,
    brandId: null,
    basePrice: null,
    imageUrl: '',
    isPublished: false
  };

  isSaving: boolean = false;

  categories: any[] = [];
  brands: any[] = [];

  isEditMode: boolean = false;
  productId: string | null = null;
  selectedFile: File | undefined;
  localImagePreview: SafeUrl | null = null;
  private objectUrl: string | null = null;

  constructor(
    private messageService: MessageService,
    private catalogService: CatalogService,
    private router: Router,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit() {
    this.catalogService.getCategories().subscribe(res => {
      this.categories = res.map((c: any) => ({ label: c.name, value: c.id }));
    });
    this.catalogService.getBrands().subscribe(res => {
      this.brands = res.map((b: any) => ({ label: b.name, value: b.id }));
    });

    this.productId = this.route.snapshot.paramMap.get('id');
    if (this.productId) {
      this.isEditMode = true;
      this.catalogService.getProductById(this.productId).subscribe(res => {
        this.product = res;
      });
    }
  }

  onImageUpload(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      // Use createObjectURL instead of FileReader to prevent browser memory issues and crashes
      if (this.objectUrl) {
        URL.revokeObjectURL(this.objectUrl); // clean up previous url
      }
      this.objectUrl = URL.createObjectURL(file);
      this.localImagePreview = this.sanitizer.bypassSecurityTrustUrl(this.objectUrl);
    }
  }

  save(form: any) {
    if (form.invalid) {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Please fill all required fields correctly.' });
      return;
    }

    this.isSaving = true;

    // Generate a simple unique slug
    this.product.slug = this.product.name.toLowerCase().replace(/ /g, '-') + '-' + Math.random().toString(36).substring(2, 8);

    if (this.isEditMode && this.productId) {
      this.product.id = this.productId;
      this.catalogService.updateProduct(this.productId, this.product, this.selectedFile).subscribe({
          next: () => {
              this.isSaving = false;
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Product updated successfully.' });
              this.router.navigate(['/admin/products']);
          },
          error: (err: any) => {
              this.isSaving = false;
              const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to update product');
              this.messageService.add({ severity: 'error', summary: 'Error', detail: msg });
          }
      });
    } else {
      this.catalogService.createProduct(this.product, this.selectedFile).subscribe({
          next: () => {
              this.isSaving = false;
              this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Product created successfully.' });
              this.router.navigate(['/admin/products']);
          },
          error: (err: any) => {
              this.isSaving = false;
              const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to create product');
              this.messageService.add({ severity: 'error', summary: 'Error', detail: msg });
          }
      });
    }
  }
}
