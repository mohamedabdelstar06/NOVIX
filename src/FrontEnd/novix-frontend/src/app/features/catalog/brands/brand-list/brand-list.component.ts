import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { BrandService } from '../../../../core/services/brand.service';
import { CatalogService } from '../../../../core/services/catalog.service';
import { Brand } from '../../../../core/models/brand.model';

@Component({
  selector: 'app-brand-list',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, DialogModule, ButtonModule, InputTextModule, ToastModule, ConfirmDialogModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.scss']
})
export class BrandListComponent implements OnInit {
  brands: Brand[] = [];
  brand: Brand = { name: '', description: '', logoUrl: '' };
  brandDialog: boolean = false;
  submitted: boolean = false;
  selectedFile: File | undefined;

  constructor(
    private brandService: BrandService,
    private catalogService: CatalogService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadBrands();
  }

  loadBrands() {
    this.brandService.getBrands().subscribe(data => this.brands = data);
  }

  openNew() {
    this.brand = { name: '', description: '', logoUrl: '' };
    this.submitted = false;
    this.brandDialog = true;
  }

  editBrand(brand: Brand) {
    this.brand = { ...brand };
    this.brandDialog = true;
  }

  deleteBrand(brand: Brand) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + brand.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        if (brand.id) {
          this.brandService.deleteBrand(brand.id).subscribe(() => {
            this.brands = this.brands.filter(val => val.id !== brand.id);
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Brand Deleted', life: 3000 });
          });
        }
      }
    });
  }

  hideDialog() {
    this.brandDialog = false;
    this.submitted = false;
  }

  onLogoUpload(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      // Show local preview immediately
      const reader = new FileReader();
      reader.onload = (e: any) => this.brand.logoUrl = e.target.result;
      reader.readAsDataURL(file);
    }
  }

  saveBrand() {
    this.submitted = true;

    if (this.brand.name.trim()) {
      if (this.brand.id) {
        this.brandService.updateBrand(this.brand.id, this.brand, this.selectedFile).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Brand Updated', life: 3000 });
            this.loadBrands();
            this.brandDialog = false;
            this.brand = { name: '', description: '', logoUrl: '' };
            this.selectedFile = undefined;
          },
          error: (err) => {
            const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to update brand');
            this.messageService.add({ severity: 'error', summary: 'Error', detail: msg, life: 3000 });
          }
        });
      } else {
        this.brandService.createBrand(this.brand, this.selectedFile).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Brand Created', life: 3000 });
            this.loadBrands();
            this.brandDialog = false;
            this.brand = { name: '', description: '', logoUrl: '' };
            this.selectedFile = undefined;
          },
          error: (err) => {
            const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to create brand');
            this.messageService.add({ severity: 'error', summary: 'Error', detail: msg, life: 3000 });
          }
        });
      }
    }
  }
}
