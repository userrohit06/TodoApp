import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenStorage } from '../services/token-storage';
import { Auth } from '../services/auth';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(Auth);
  const router = inject(Router);
  const tokenStorage = inject(TokenStorage);
  const token = tokenStorage.getToken();

  const isAuthCall = req.url.includes('/api/Auth/login') || req.url.includes('/api/Auth/register');

  const authReq =
    token && !isAuthCall ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }) : req;

  return next(authReq).pipe(
    catchError((err) => {
      if (err.status === 401) {
        auth.logout();
        router.navigate(['/auth/login']);
      }
      return throwError(() => err);
    })
  );
};
