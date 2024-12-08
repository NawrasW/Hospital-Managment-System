import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { catchError, Observable, of, tap } from 'rxjs';
import { Appointment } from 'app/models/appointment';
import { Doctor } from 'app/models/doctor';


@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  public apiUrl = 'https://localhost:7187/api/Appointment'; // Adjust base URL as needed

  constructor(private http: HttpClient) {}

  getAllAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/getAllAppointments`);
  }

  getAppointmentById(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.apiUrl}/getAppointmentById/${id}`);
  }

  addOrUpdateAppointment(appointment: Appointment): Observable<any> {
    return this.http.post(`${this.apiUrl}/AddUpdateAppointment`, appointment);
  }

  deleteAppointment(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteAppointment/${id}`);
  }


  getAvailableDoctors(date: Date) {
    const formattedDate = date.toISOString(); // Full ISO format
    console.log('Requesting available doctors with date:', formattedDate);
    
    return this.http.get<Doctor[]>(`/api/doctors/available?date=${formattedDate}`)
      .pipe(
        tap(doctors => console.log('Data from backend:', doctors)),
        catchError((error) => {
          console.error('Error in AppointmentService:', error);
          return of([]); // Return an empty array on error
        })
      );
  }
  

  getAppointmentsForUser(role: string, userId: number): Observable<Appointment[]> {
    console.log(`Fetching appointments for role: ${role}, userId: ${userId}`);
    return this.http.get<Appointment[]>(`${this.apiUrl}/getAppointmentsByUser/${role}/${userId}`);
  }
}
