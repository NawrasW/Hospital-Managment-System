import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';

import { AdminAuthGuard } from './admin-auth.guard';
import { HomeAdminComponent } from './home/home.component';
import { DoctorListComponent } from './doctor-list/doctor-list.component';
import { RegistrationComponent } from './registration/registration.component';

const routes: Routes = [
  { path: '', component: AdminComponent, canActivate: [AdminAuthGuard], children: [
      { path: 'home', component: HomeAdminComponent },
      { path: 'doctor-list', component: DoctorListComponent },
      { path: 'doctor-add', component: RegistrationComponent },
      { path: 'doctor-update/:id', component: RegistrationComponent },
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
