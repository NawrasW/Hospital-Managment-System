import { Component, OnInit, ViewChild } from '@angular/core';
import { Doctor } from 'app/models/doctor';
import { DoctorService } from '../doctor.service';
import { PageModeEnum } from 'app/constants/enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {

  constructor(private doctorservice: DoctorService, private router: Router) { }
  doctors: Doctor[] = new Array<Doctor>();

  isPopupParent: boolean = true;
  isValid: boolean = false;
  doctor: Doctor = new Doctor();
  pageMode: PageModeEnum = PageModeEnum.Add;
  @ViewChild('closeButton') closeButton: any;
  @ViewChild('openModalButton') openModalButton: any;
  ngOnInit(): void {

    this.getAllDoctors();
  }


  delete(doctor: Doctor) {
    if (doctor.id) {
      this.doctorservice.deleteDoctor(doctor.id).subscribe(result => {
        this.getAllDoctors();
        
      });
    }
  }

  getAllDoctors() {
    this.doctorservice.getAllDoctors().subscribe(result => {

      this.doctors = result;
      console.log(this.doctors)

    });

  }
  goToUpdateDoctor(Doctor: Doctor) {
    if (Doctor.id)
    this.router.navigateByUrl(`admin/doctor-update/${Doctor.id}`);
  }

  reset() {
    this.doctor = new Doctor();
    this.pageMode = PageModeEnum.Add;
  }

  save() {
    if (this.isValid) {
      if (!(this.doctor.password === this.doctor.confirmpassword))
         console.log('wrong password')

      else
        this.doctorservice.addUpdateDoctor(this.doctor).subscribe(result => {

       
          this.closeButton.nativeElement.click();
          this.getAllDoctors();

        });
    }
    else {
       console.log("In valid form")
    }
  }
  edit(doctor: Doctor) {
    
    
    this.openModalButton.nativeElement.click();
    this.doctor = doctor;
    this.doctor.confirmpassword = this.doctor.password;
    this.pageMode = PageModeEnum.Update;
    console.log("Page mode set to:", this.pageMode);
  }

  add() {
    this.doctor = new Doctor();
    
    this.pageMode = PageModeEnum.Add;
  }
  view(doctor: Doctor) {
   
    this.openModalButton.nativeElement.click();
    console.log("Viewing Doctor:", doctor);
    console.log("Page mode set to:", this.pageMode);
    this.doctor = doctor;
    this.doctor.confirmpassword = this.doctor.password;
    this.pageMode = PageModeEnum.View;
  }

  get PageModeEnum(): any {

    return PageModeEnum;
  }
}
