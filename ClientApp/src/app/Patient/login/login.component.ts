import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';


import { Router } from '@angular/router';
import { AuthService } from 'app/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginPatientComponent {
  constructor(private _authservice: AuthService, private router: Router) {}

  onLogin(loginForm: NgForm) {
    // Check if form is valid
    if (loginForm.invalid) {
      console.log('Form is invalid. Please fill in all required fields.');
      return; // Prevent further execution
    }

    // Proceed with authentication
    this._authservice.authPatient(loginForm.value).subscribe(
      (result) => {
        if (result) {
          this._authservice.storeId(result.id);
          this._authservice.storeToken(result.token);
          this._authservice.storeFullName(`${result.firstName} ${result.lastName}`);
          this._authservice.storeEmail(`${result.email}`);

          this.router.navigateByUrl('/');
        } else {
          console.log('Invalid email or password.');
        }
      },
      (error) => {
        console.error('Error during login:', error);
      }
    );
  }
}