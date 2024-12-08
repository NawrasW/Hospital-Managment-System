import { Injectable } from '@angular/core';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Status } from '../models/status';


@Injectable({
    providedIn: 'root'
  })
  export class DoctorService {

    constructor(private _httpClient: HttpClient) { }
    url: string = "https://localhost:7187/api/Doctor";



    addUpdateDoctor(doctor: Doctor): Observable<Status> {
        return this._httpClient.post<Status>(`${this.url}/AddUpdateDoctor`, doctor)
      }


      getAllDoctors(): Observable<Doctor[]> {
        return this._httpClient.get<Doctor[]>(`${this.url}/getAllDoctors`);
    
      }
    
    
      deleteDoctor(id: number): Observable<Status> {
    
        return this._httpClient.delete<Status>(`${this.url}/deleteDoctor/${id}`);
      }


      getDoctorById(id: number): Observable<Doctor> {

        return this._httpClient.get<Doctor>(`${this.url}/getDoctorById/${id}`);
      }


  }
