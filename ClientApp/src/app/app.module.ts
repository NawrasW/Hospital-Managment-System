import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { JwtHelperService, JWT_OPTIONS } from '@auth0/angular-jwt';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FooterComponent } from './footer/footer.component';

import { TokenInterceptor } from './interceptors/token.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthService } from './auth.service';
import { LoginDoctorComponent } from './Doctor/login/login.component';
import { LoginPatientComponent } from './Patient/login/login.component';
import { AppointmentComponent } from './appointment/appointment.component';
import { AppointmentFormComponent } from './components/appointment-form/appointment-form.component';
import { ScheduleFormComponent } from './components/schedule-form/schedule-form.component';
import { RegistrationComponent } from './Patient/registration/registration.component';
import { PageModeEnum } from './constants/enum';
const routes: Routes = [
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
  { path: 'home', component: HomeComponent },
  { path: 'signindoctor', component: LoginDoctorComponent },
  { path: 'signinpatient', component: LoginPatientComponent },

  { path: 'appointment', component: AppointmentComponent },
  { path: 'appointment-form', component: AppointmentFormComponent },
  { path: 'schedule-form', component: ScheduleFormComponent },
  { path: 'signup', component: RegistrationComponent },
  { path: 'signup/:id', component: RegistrationComponent, data: { pageMode: PageModeEnum.View } },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home' }
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginDoctorComponent,
    LoginPatientComponent,
    NavMenuComponent,
    FooterComponent,
    AppointmentComponent,
    AppointmentFormComponent,
    ScheduleFormComponent,
    RegistrationComponent,
    

  ],
  imports: [
    BrowserModule,
    FormsModule,
     ReactiveFormsModule ,
    HttpClientModule,
    RouterModule.forRoot(routes)
  ],
  providers: [
    AuthService,
    JwtHelperService,
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
