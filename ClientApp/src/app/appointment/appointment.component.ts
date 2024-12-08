import { Component, OnInit } from '@angular/core';
import { Appointment } from 'app/models/appointment';
import { AppointmentService } from './appointment.service';
import { AuthService } from 'app/auth.service';
import { ScheduleService } from 'app/components/schedule-form/schedule.auth.service';


@Component({
  selector: 'app-appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  appointments: Appointment[] = [];
  filteredAppointments: Appointment[] = [];
  userRole: string | null = null;
  userId: number | null = null;

  constructor(
    private appointmentService: AppointmentService,
    private authService: AuthService,
    private scheduleService: ScheduleService
  ) {}

  ngOnInit(): void {
    this.userRole = this.authService.getUserRole();
    this.userId = parseInt(this.authService.getUserId() || '0', 10);

    //console.log("User ID:", this.userId);
   // console.log("User Role:", this.userRole);

    if (this.userRole && this.userId) {
      this.fetchAppointments();
    } else {
      console.warn("User role or ID is missing.");
    }
  }

  fetchAppointments() {
    if (this.userRole && this.userId !== null && this.userId !== undefined) {
      const apiUrl = `${this.appointmentService.apiUrl}/getAppointmentsByUser/${this.userRole}/${this.userId}`;
     // console.log("Calling API URL:", apiUrl);

      this.appointmentService.getAppointmentsForUser(this.userRole, this.userId)
        .subscribe(
          (appointments) => {
            this.appointments = appointments;
          //  console.log("Fetched appointments:", appointments);
            this.filterAppointments();
          },
          (error) => {
            console.error("Error fetching appointments:", error);
          }
        );
    } else {
      console.warn("User role or ID is missing, cannot fetch appointments.");
    }
  }

  filterAppointments() {
    if (this.userRole === 'Doctor') {
      this.filteredAppointments = this.appointments.filter(app => app.doctorId === this.userId);
    } else if (this.userRole === 'Patient') {
      this.filteredAppointments = this.appointments.filter(app => app.patientId === this.userId);
    }
  //  console.log("Filtered appointments:", this.filteredAppointments);
  }

 


}
