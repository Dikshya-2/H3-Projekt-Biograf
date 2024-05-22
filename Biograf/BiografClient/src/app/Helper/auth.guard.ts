import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthorService } from '../Services/author.service';
import { inject } from '@angular/core';
import { LoginService } from '../Services/login.service';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const router = inject(Router);
  const authService = inject(LoginService);
  const currentUser = authService.currentUserValue;

  if (currentUser) {
    // Check if the route requires specific roles and if the user has one of those roles
    if (route.data['roles'] && route.data['roles'].indexOf(currentUser.role) === -1) {
      router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
        // User is logged in and has the required role
  return true;
  }
  // User is not logged in, redirect to the login page
  router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
  return false;

};
