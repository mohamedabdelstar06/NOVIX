import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Brand } from '../models/brand.model';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private apiUrl = `${environment.apiUrl}/catalog/brands`;

  constructor(private http: HttpClient) { }

  getBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(this.apiUrl);
  }

  createBrand(brand: Brand, logoFile?: File): Observable<string> {
    const formData = new FormData();
    Object.keys(brand).forEach(key => {
      if ((brand as any)[key] !== null && (brand as any)[key] !== undefined) {
        formData.append(key, (brand as any)[key]);
      }
    });
    if (logoFile) formData.append('logoFile', logoFile);

    return this.http.post<string>(this.apiUrl, formData);
  }

  updateBrand(id: string, brand: Brand, logoFile?: File): Observable<void> {
    const formData = new FormData();
    Object.keys(brand).forEach(key => {
      if ((brand as any)[key] !== null && (brand as any)[key] !== undefined) {
        formData.append(key, (brand as any)[key]);
      }
    });
    formData.append('id', id);
    if (logoFile) formData.append('logoFile', logoFile);

    return this.http.put<void>(`${this.apiUrl}/${id}`, formData);
  }

  deleteBrand(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
