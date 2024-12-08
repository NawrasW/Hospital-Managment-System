import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { HomeAdminComponent } from './home/home.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [AdminComponent, HomeAdminComponent, DoctorListComponent,RegistrationComponent],
  imports: [

    CommonModule,
    AdminRoutingModule, FormsModule // Make sure the routing module is imported here
  ]
})
export class AdminModule { }
