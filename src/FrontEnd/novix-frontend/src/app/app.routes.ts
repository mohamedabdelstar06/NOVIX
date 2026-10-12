import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './core/layout/auth-layout/auth-layout.component';
import { AdminLayoutComponent } from './core/layout/admin-layout/admin-layout.component';
import { CustomerLayoutComponent } from './core/layout/customer-layout/customer-layout.component';
import { LoginComponent } from './features/identity/auth/login/login.component';
import { RegisterComponent } from './features/identity/auth/register/register.component';
import { ProductListComponent } from './features/catalog/products/product-list/product-list.component';
import { ProductFormComponent } from './features/catalog/products/product-form/product-form.component';
import { BrandListComponent } from './features/catalog/brands/brand-list/brand-list.component';
import { CategoryListComponent } from './features/catalog/categories/category-list/category-list.component';
import { HomeComponent } from './features/storefront/home/home.component';

export const routes: Routes = [
  {
    path: '',
    component: CustomerLayoutComponent,
    children: [
      { path: '', component: HomeComponent }
    ]
  },
  {
    path: 'auth',
    component: AuthLayoutComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent },
      { path: '', redirectTo: 'login', pathMatch: 'full' }
    ]
  },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: 'products', component: ProductListComponent },
      { path: 'products/new', component: ProductFormComponent },
      { path: 'products/edit/:id', component: ProductFormComponent },
      { path: 'brands', component: BrandListComponent },
      { path: 'categories', component: CategoryListComponent },
      { path: '', redirectTo: 'products', pathMatch: 'full' }
    ]
  },
 
  {
    path: 'products',
    component: AdminLayoutComponent,
    children: [
      { path: '', component: ProductListComponent },
      { path: 'new', component: ProductFormComponent }
    ]
  },
  { path: '**', redirectTo: '' }
];
