import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from 'app/models/department';



@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  public apiUrl = 'https://localhost:7187/api/Department'; // Adjust base URL as needed

  constructor(private http: HttpClient) {}

  getAllDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(`${this.apiUrl}/getAllDepartments`);
  }

  getDepartmentById(id: number): Observable<Department> {
    return this.http.get<Department>(`${this.apiUrl}/getDepartmentById/${id}`);
  }

  addOrUpdateDepartment(Department: Department): Observable<any> {
    return this.http.post(`${this.apiUrl}/AddUpdateDepartment`, Department);
  }

  deleteDepartment(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/deleteDepartment/${id}`);
  }



  getDepartmentsForUser(role: string, userId: number): Observable<Department[]> {
    console.log(`Fetching Departments for role: ${role}, userId: ${userId}`);
    return this.http.get<Department[]>(`${this.apiUrl}/getDepartmentsByUser/${role}/${userId}`);
  }
}
