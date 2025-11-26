import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="login-container">
      <h2>Login</h2>

      <form [formGroup]="form" (ngSubmit)="onSubmit()">

        <div class="form-group">
          <label for="userName">Username</label>
          <input
            id="userName"
            type="text"
            formControlName="userName"
            placeholder="Enter username" />
        </div>

        <div class="form-group">
          <label for="password">Password</label>
          <input
            id="password"
            type="password"
            formControlName="password"
            placeholder="Enter password" />
        </div>

        <button type="submit" [disabled]="form.invalid || loading">
          {{ loading ? 'Logging in...' : 'Login' }}
        </button>
      </form>

      <p *ngIf="error" class="error">{{ error }}</p>
    </div>
  `,
  styles: [ /* same styles as before */ ]
})
export class LoginComponent {
  form: FormGroup;
  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.error = '';

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.authService.login(this.form.value).subscribe({
      next: () => {
        this.loading = false;
        // token already stored by AuthService
        this.router.navigate(['/products']);
      },
      error: () => {
        this.loading = false;
        this.error = 'Login failed. Check username or password.';
      }
    });
  }
}
