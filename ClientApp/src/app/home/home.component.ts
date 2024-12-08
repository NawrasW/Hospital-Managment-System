import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor( private auth: AuthService,private router: Router) { }


  isPatient(): boolean {
    return this.auth.getUserRole() === 'Patient';
  }
}
