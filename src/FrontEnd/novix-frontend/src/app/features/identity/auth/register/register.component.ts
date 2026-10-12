import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, PasswordModule, ButtonModule, RouterModule, ToastModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  user = {
    firstName: '',
    lastName: '',
    email: '',
    password: ''
  };

  isLoading: boolean = false;
  isGoogleLoading: boolean = false;

  constructor(
    private authService: AuthService,
    private messageService: MessageService,
    private router: Router
  ) {}

  register() {
    this.isLoading = true;
    this.authService.register(this.user).subscribe({
      next: (res: any) => {
        this.isLoading = false;
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Account created! Please login.' });
        setTimeout(() => this.router.navigate(['/auth/login']), 1000);
      },
      error: (err: any) => {
        this.isLoading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Registration failed. Fallback to mock.' });
        setTimeout(() => this.router.navigate(['/auth/login']), 1000);
      }
    });
  }

  googleRegister() {
    this.isGoogleLoading = true;
    setTimeout(() => {
      this.isGoogleLoading = false;
      this.router.navigate(['/products']);
    }, 1500);
  }
}
