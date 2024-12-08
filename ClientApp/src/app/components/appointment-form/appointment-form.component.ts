import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppointmentService } from 'app/appointment/appointment.service';
import { AuthService } from 'app/auth.service';
import { Router } from '@angular/router';
import { Doctor } from 'app/models/doctor';
import { Department } from 'app/models/department';
import { ScheduleService } from 'app/components/schedule-form/schedule.auth.service';
import { DoctorService } from 'app/admin/doctor.service';
import { DepartmentService } from 'app/admin/department.service';


@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {
  appointmentForm: FormGroup;
  allDoctors: Doctor[] = [];
  filteredDoctors: Doctor[] = [];
  allDepartments: Department[] = [];

  constructor(
    private fb: FormBuilder,
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private scheduleService: ScheduleService,
    private doctorService: DoctorService,
    private departmentService: DepartmentService, // Added for fetching departments
    private router: Router
  ) {
    this.appointmentForm = this.fb.group({
      id: [0],
      departmentId: [null, Validators.required],
      doctorId: [null, Validators.required],
      appointmentDate: [null, Validators.required],
      problem: ['', Validators.required],
      status: [true]
    });
  }

  ngOnInit(): void {
    this.fetchAllDepartments();
    this.fetchAllDoctors();
  }

  fetchAllDepartments(): void {
    this.departmentService.getAllDepartments().subscribe(
      (departments) => {
        this.allDepartments = departments;
       // console.log('All departments fetched:', this.allDepartments);
      },
      (error) => {
        console.error('Error fetching departments:', error);
      }
    );
  }

  fetchAllDoctors(): void {
    this.doctorService.getAllDoctors().subscribe(
      (doctors) => {
        this.allDoctors = doctors;
        this.filteredDoctors = []; // Clear filtered doctors initially
        //console.log('All doctors fetched:', this.allDoctors);
      },
      (error) => {
        console.error('Error fetching doctors:', error);
      }
    );
  }

  onDepartmentChange(event: any): void {
    const selectedDepartmentId = +event.target.value || 0; // Safely coerce to number
    console.log('Selected Department ID:', selectedDepartmentId);
  
    if (!selectedDepartmentId) {
      // Reset if no valid selection
      this.filteredDoctors = [];
      this.appointmentForm.get('doctorId')?.reset();
      return;
    }
  
    // Filter doctors by department ID
    this.filteredDoctors = this.allDoctors.filter((doctor) => {
      return Number(doctor.departmentId) === selectedDepartmentId; // Ensure both are numbers
    });
  
    //console.log('Filtered Doctors:', this.filteredDoctors);
  
    // Reset the doctor selection if no match
    this.appointmentForm.get('doctorId')?.reset();
  }
  

  onSave(): void {
    if (!this.appointmentForm.valid) {
      alert('Form is invalid. Please fill out all required fields.');
      return;
    }

    const formValue = this.appointmentForm.value;
    const selectedDoctor = this.filteredDoctors.find(
      (doc) => doc.id === +formValue.doctorId
    );

    if (!selectedDoctor) {
      alert('Selected doctor not found.');
      return;
    }

    const patientId = parseInt(this.authService.getUserId() || '', 10);
    const patientName = this.authService.getFullName();

    const appointmentData = {
      ...formValue,
      doctorName: `${selectedDoctor.firstName} ${selectedDoctor.lastName}`,
      patientId,
      patientName
    };

    this.appointmentService.addOrUpdateAppointment(appointmentData).subscribe(
      () => {
        alert('Appointment saved successfully.');
        this.router.navigate(['/appointment']);
      },
      (error) => {
        console.error('Error saving appointment:', error);
      }
    );
  }
}
