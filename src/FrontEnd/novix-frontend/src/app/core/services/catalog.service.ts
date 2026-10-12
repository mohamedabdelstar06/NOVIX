import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  private apiUrl = `${environment.apiUrl}/catalog/products`;

  constructor(private http: HttpClient) { }

  getProducts(pageNumber: number = 1, pageSize: number = 10, searchTerm: string = ''): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
      
    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get(this.apiUrl, { params });
  }

  getCategories(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/catalog/categories`);
  }

  getBrands(): Observable<any> {
    return this.http.get(`${environment.apiUrl}/catalog/brands`);
  }

  getProductById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  createProduct(product: any, imageFile?: File): Observable<any> {
    const formData = new FormData();
    Object.keys(product).forEach(key => {
      if (product[key] !== null && product[key] !== undefined) {
        formData.append(key, product[key]);
      }
    });
    if (imageFile) formData.append('imageFile', imageFile);

    return this.http.post(this.apiUrl, formData);
  }

  updateProduct(id: string, product: any, imageFile?: File): Observable<any> {
    const formData = new FormData();
    Object.keys(product).forEach(key => {
      if (product[key] !== null && product[key] !== undefined) {
        formData.append(key, product[key]);
      }
    });
    if (imageFile) formData.append('imageFile', imageFile);

    return this.http.put(`${this.apiUrl}/${id}`, formData);
  }

  deleteProduct(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }


}
