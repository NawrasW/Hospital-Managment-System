import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/auth.service';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  loggedinUser: string | undefined;
  loggedinUserId: number | undefined;
  dropdownOpen = false;

  constructor( private auth: AuthService,private router: Router) { }
  collapse() {
    this.isExpanded = false;

  }
  
  navigateToLogin() {
    console.log('cli')
    this.router.navigateByUrl('/signin');
    
  }

  loggedin() {
    const fullName = this.auth.getFullName();
    const userId = this.auth.getUserId();

    if (fullName && userId) {
      this.loggedinUser = fullName;
      this.loggedinUserId = +userId;
      return true;
    } else {
      return false;
    }
  }

  logout() {
    this.auth.logOut();
    this.router.navigateByUrl('/');
  }

  isAdmin(): boolean {
    return this.auth.getUserRole() === 'Admin';
  }

  isDoctor(): boolean {
    return this.auth.getUserRole() === 'Doctor';
  }

  isPatient(): boolean {
    return this.auth.getUserRole() === 'Patient';
  }

  // Navigate to doctor list page
  navigateToDoctorList() {
    this.router.navigate(['/admin/doctor-list']);
    console.log('doctor-list:');
  }

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
    console.log('Dropdown open:', this.dropdownOpen);
  }
  
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
