import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ScheduleService } from './schedule.auth.service';
import { AuthService } from 'app/auth.service'; // AuthService to retrieve doctor details
import { Schedule } from 'app/models/schedule';
import { Router } from '@angular/router';

@Component({
  selector: 'app-schedule-form',
  templateUrl: './schedule-form.component.html',
  styleUrls: ['./schedule-form.component.css']
})
export class ScheduleFormComponent implements OnInit {
  scheduleForm: FormGroup;
  isSubmitting = false;
  successMessage = '';
  errorMessage = '';
  doctorName = ''; // Variable to hold the doctor's name
  daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday']; // Days options

  constructor(
    private scheduleService: ScheduleService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    // Initialize form with fields and validation
    this.scheduleForm = this.formBuilder.group({
      doctorId: ['', Validators.required], // Hidden field for the doctor ID
      availableStartDay: ['', Validators.required],
      availableEndDay: ['', Validators.required],
      availableStartTime: ['', Validators.required],
      availableEndTime: ['', Validators.required],
      doctor:['', Validators.required],
      status:['', Validators.required]
    });
  }

  ngOnInit(): void {
    const doctorId = this.authService.getUserId();
    const doctorFullName = this.authService.getFullName();
  
    if (doctorId && doctorFullName) {
      this.scheduleForm.controls['doctorId'].setValue(doctorId);
      this.scheduleForm.controls['doctor'].setValue(doctorFullName); // Set doctor field
      this.scheduleForm.controls['status'].setValue('active'); // Default status
      this.doctorName = doctorFullName;
    } else {
      this.errorMessage = 'Failed to load doctor details.';
    }
  }
  

  // Submit schedule data to add or update the schedule
  submitSchedule() {
    if (this.scheduleForm.invalid) {
      return;
    }
  
    this.isSubmitting = true;
  
    // Format the time fields to ISO strings (or backend-compatible format)
    const today = new Date().toISOString().split('T')[0]; // Get today's date in YYYY-MM-DD format
  
    const scheduleData: Schedule = {
      id: 0, // If creating a new schedule, use 0 or an undefined value
      doctor: this.doctorName,
      doctorId: this.scheduleForm.value.doctorId,
      availableStartDay: this.scheduleForm.value.availableStartDay,
      availableEndDay: this.scheduleForm.value.availableEndDay,
      // Convert to UTC
      availableStartTime: new Date(`${today}T${this.scheduleForm.value.availableStartTime}:00Z`).toISOString(),
      availableEndTime: new Date(`${today}T${this.scheduleForm.value.availableEndTime}:00Z`).toISOString(),
      status: this.scheduleForm.value.status,
    };
  
    this.scheduleService.addOrUpdateSchedule(scheduleData).subscribe(
      (response) => {
        this.successMessage = 'Schedule saved successfully.';
        this.isSubmitting = false;
        this.scheduleForm.reset();
        this.router.navigate(['/']);
      },
      (error) => {
        this.errorMessage = 'Failed to save the schedule.';
        this.isSubmitting = false;
        console.error('Error saving schedule:', error);
      }
    );
  }
  
  
}
