import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

// NgZorro imports
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';

import { AuthService } from '../../services/auth.service';
import { LoginRequest } from '../../interfaces/login-request.interface';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzCardModule,
    NzFormModule,
    NzInputModule,
    NzButtonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  credentials: LoginRequest = { username: '', password: '' };
  isLoading = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private message: NzMessageService
  ) {}

  login(): void {
    this.isLoading = true;

    this.authService.login(this.credentials).subscribe({
      next: () => {
        this.message.success('Login exitoso');
        this.router.navigate(['/productos']);
      },
      error: (err) => {
        this.message.error('Credenciales invalidas');
        console.error('Login failed: ', err);
        this.isLoading = false;

      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
  
}
