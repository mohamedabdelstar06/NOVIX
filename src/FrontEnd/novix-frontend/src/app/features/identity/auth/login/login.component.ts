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
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, InputTextModule, PasswordModule, ButtonModule, RouterModule, ToastModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  credentials = {
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

  login() {
    this.isLoading = true;
    this.authService.login(this.credentials).subscribe({
      next: (res: any) => {
        this.isLoading = false;
        localStorage.setItem('token', res.token);
        this.router.navigate(['/products']);
      },
      error: (err: any) => {
        this.isLoading = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Invalid credentials. Because backend is offline, you can skip this by using mock setTimeout.' });
        
        // Mock fallback for current offline backend
        setTimeout(() => {
            this.router.navigate(['/products']);
        }, 1000);
      }
    });
  }

  googleLogin() {
    this.isGoogleLoading = true;
    // Mock Google Login since we need a real Google Client ID
    setTimeout(() => {
      this.isGoogleLoading = false;
      this.router.navigate(['/products']);
    }, 1500);
  }
}
