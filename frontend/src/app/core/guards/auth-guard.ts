import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { TokenStorage } from '../services/token-storage';
import { Auth } from '../services/auth';

export const authGuard: CanActivateFn = () => {
  const auth = inject(Auth);
  const router = inject(Router);

  if (auth.isAuthenticated()) {
    return true;
  }

  auth.logout();
  return router.createUrlTree(['/auth/login']);
};
