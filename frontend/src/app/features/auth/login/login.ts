import { CommonModule, NgIf } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterLink, NgIf],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  model = {
    email: '',
    password: '',
  };

  isLoading = signal(false);
  error = signal<string | null>(null);

  constructor(private authService: Auth, private router: Router) {}

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    this.isLoading.set(true);
    this.error.set(null);

    this.authService.login(this.model).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/todos']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.error.set(err.error?.message ?? 'Login failed');
      },
    });
  }
}
