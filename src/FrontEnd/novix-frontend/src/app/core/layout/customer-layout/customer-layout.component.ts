import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer-layout',
  standalone: true,
  imports: [CommonModule, RouterModule, InputTextModule, ButtonModule, FormsModule],
  templateUrl: './customer-layout.component.html',
  styleUrl: './customer-layout.component.scss'
})
export class CustomerLayoutComponent {
  searchQuery: string = '';

  onSearch() {
    console.log('Searching for:', this.searchQuery);
    // In a real implementation, this would navigate to a search results page
    // this.router.navigate(['/search'], { queryParams: { q: this.searchQuery } });
  }
}
