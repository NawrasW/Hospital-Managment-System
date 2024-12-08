import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from 'app/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const isLoggedIn = this.authService.isLogged();
    const userRole = this.authService.getUserRole();
    
    console.log("User role:", userRole); // Debug log
  
    if (isLoggedIn && userRole === 'Admin') {
      return true;
     
    } else {
      this.router.navigate(['/']);
      console.log('unauthorized')
      return false;
    }
  }
}
