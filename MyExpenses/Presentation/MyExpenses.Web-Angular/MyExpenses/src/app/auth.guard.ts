import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router: Router = new Router();
  const isAuthenticated = !!localStorage.getItem('token'); // Example: check token in localStorage

    if (isAuthenticated) {
      return true;
    } else {
      // Redirect to login if not authenticated
      router.navigate(['auth/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
};
