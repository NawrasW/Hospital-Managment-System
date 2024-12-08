import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { PayLoad } from './models/payload';
import { UserForLogin, UserLogin } from './models/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url: string = "https://localhost:7187/api/auth";
  userPayLoad: PayLoad;

  constructor(private _httpClient: HttpClient, private router: Router, private jwtHelper: JwtHelperService) {
    this.userPayLoad = this.decodedToken();
  }

  // Login Doctors
  authDoctor(doctor: UserForLogin): Observable<UserLogin> {
    return this._httpClient.post<UserLogin>(`${this.url}/login/doctor`, doctor);
  }


  // Login Patients
  authPatient(patient: UserForLogin): Observable<UserLogin> {
    return this._httpClient.post<UserLogin>(`${this.url}/login/patient`, patient);
  }

  logOut() {
    localStorage.clear();
    this.router.navigateByUrl('/login');
  }

  storeId(idvalue: number) {
    localStorage.setItem('id', idvalue.toString());
  }

  getUserId() {
    return localStorage.getItem('id');
  }

// get user roles
  getUserRole(): string | null {
    const tokenPayload = this.decodedToken();
    const role = tokenPayload ? tokenPayload.role : null;
   
    return role;
  }

  storeToken(tokenValue: string) {
    if (tokenValue) {
    //  console.log("Storing token:", tokenValue);
      localStorage.setItem('token', tokenValue);
    } else {
      console.log("Token value is undefined.");
    }
  }

  getToken() {
    const token = localStorage.getItem('token');
//console.log("Retrieved token:", token);
    return token;
    
  }

  isLogged(): boolean {
    const token = localStorage.getItem('token');
    const userId = localStorage.getItem('id');
    return !!userId && !!token;
  }

  storeFullName(fullName: string) {
    localStorage.setItem('fullName', fullName);
  }

  getFullName() {
    const fullName = localStorage.getItem('fullName');
  //  console.log('Retrieved patient name:', fullName);
    return fullName;
  }

  storeEmail(email: string) {
    localStorage.setItem('email', email);
  }

  getEmail() {
    return localStorage.getItem('email');
  }

 
decodedToken() {
  const token = this.getToken();
  if (token) {
    //console.log("Decoding token:", token); // Log before decoding
    return this.jwtHelper.decodeToken(token);
  } else {
    //console.log("No token to decode.");
    return null;
  }
}
  
}
