import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Schedule } from 'app/models/schedule';



@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  public apiUrl = 'https://localhost:7187/api/Schedule'; // Adjust base URL as needed

  constructor(private http: HttpClient) {}

  getAllSchedules(): Observable<Schedule[]> {
    return this.http.get<Schedule[]>(`${this.apiUrl}/getAllSchedules`);
  }

  getScheduleById(id: number): Observable<Schedule> {
    return this.http.get<Schedule>(`${this.apiUrl}/getScheduleById/${id}`);
  }

  addOrUpdateSchedule(Schedule: Schedule): Observable<any> {
    return this.http.post(`${this.apiUrl}/AddUpdateSchedule`, Schedule);
  }

  deleteSchedule(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteSchedule/${id}`);
  }



  getSchedulesForUser(role: string, userId: number): Observable<Schedule[]> {
    console.log(`Fetching Schedules for role: ${role}, userId: ${userId}`);
    return this.http.get<Schedule[]>(`${this.apiUrl}/getSchedulesByUser/${role}/${userId}`);
  }
}
