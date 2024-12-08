import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Status } from '../models/status';
import { Patient } from 'app/models/patient';


@Injectable({
    providedIn: 'root'
  })
  export class PatientService {

    constructor(private _httpClient: HttpClient) { }
    url: string = "https://localhost:7187/api/Patient";



    addUpdatePatient(Patient: Patient): Observable<Status> {
        return this._httpClient.post<Status>(`${this.url}/AddUpdatePatient`, Patient)
      }


      getAllPatients(): Observable<Patient[]> {
        return this._httpClient.get<Patient[]>(`${this.url}/getAllPatients`);
    
      }
    
    
      deletePatient(id: number): Observable<Status> {
    
        return this._httpClient.delete<Status>(`${this.url}/deletePatient/${id}`);
      }


      getPatientById(id: number): Observable<Patient> {

        return this._httpClient.get<Patient>(`${this.url}/getPatientById/${id}`);
      }


  }
