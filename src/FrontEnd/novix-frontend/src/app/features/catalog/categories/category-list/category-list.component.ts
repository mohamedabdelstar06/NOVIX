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
import { CategoryService } from '../../../../core/services/category.service';
import { Category } from '../../../../core/models/category.model';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, FormsModule, TableModule, DialogModule, ButtonModule, InputTextModule, ToastModule, ConfirmDialogModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];
  category: Category = { name: '', description: '' };
  categoryDialog: boolean = false;
  submitted: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  ngOnInit() {
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories().subscribe(data => this.categories = data);
  }

  openNew() {
    this.category = { name: '', description: '' };
    this.submitted = false;
    this.categoryDialog = true;
  }

  editCategory(category: Category) {
    this.category = { ...category };
    this.categoryDialog = true;
  }

  deleteCategory(category: Category) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to delete ' + category.name + '?',
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        if (category.id) {
          this.categoryService.deleteCategory(category.id).subscribe(() => {
            this.categories = this.categories.filter(val => val.id !== category.id);
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Category Deleted', life: 3000 });
          });
        }
      }
    });
  }

  hideDialog() {
    this.categoryDialog = false;
    this.submitted = false;
  }

  saveCategory() {
    this.submitted = true;

    if (this.category.name.trim()) {
      if (this.category.id) {
        this.categoryService.updateCategory(this.category.id, this.category).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Category Updated', life: 3000 });
            this.loadCategories();
            this.categoryDialog = false;
            this.category = { name: '', description: '' };
          },
          error: (err) => {
            const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to update category');
            this.messageService.add({ severity: 'error', summary: 'Error', detail: msg, life: 3000 });
          }
        });
      } else {
        this.categoryService.createCategory(this.category).subscribe({
          next: () => {
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Category Created', life: 3000 });
            this.loadCategories();
            this.categoryDialog = false;
            this.category = { name: '', description: '' };
          },
          error: (err) => {
            const msg = err.status === 0 ? 'Network Error: Cannot connect to server.' : (err.error?.message || 'Failed to create category');
            this.messageService.add({ severity: 'error', summary: 'Error', detail: msg, life: 3000 });
          }
        });
      }
    }
  }
}
