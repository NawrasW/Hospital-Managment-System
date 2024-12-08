import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorService } from 'app/admin/doctor.service';
import { PageModeEnum } from 'app/constants/enum';
import { Doctor } from 'app/models/doctor';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private doctorService: DoctorService, private router: Router, private activatedRoute: ActivatedRoute) {

  }
  ngOnInit(): void {

    this.activatedRoute.params.subscribe(params => {
      this.doctorId = params['id'];

      if (this.doctorId) {
        this.pageMode = PageModeEnum.Update;
        this.doctorService.getDoctorById(this.doctorId).subscribe(result => {
          // console.log(result)
          this.doctor = result;
          this.doctor.confirmpassword = this.doctor.password;

        });


      }

    });
  }
  @Input() isPopup: boolean = false;
  @Output() IsValid = new EventEmitter<boolean>();
  @ViewChild('doctorForm') doctorForm!: NgForm;

  pageModeTest: string = "Add";
  @Input() pageMode: PageModeEnum = PageModeEnum.Add;
  doctorId!: number;
  @Input() doctor: Doctor = new Doctor();

  get PageModeEnum(): any {

    return PageModeEnum;

  }

  onSubmit(doctorForm: NgForm) {
console.log(doctorForm, this.doctor)
    if (!(this.doctor.password === this.doctor.confirmpassword)) {
     
     console.log("invaild passowrd");



    }

    else {

      this.doctorService.addUpdateDoctor(this.doctor).subscribe(
        (result) => {
          if (result) {
            console.log("Doctor saved:", result);
           
        
            console.log("Navigating to doctor list...");
            this.router.navigateByUrl('admin/doctor-list');
          } else {
            console.error("No result returned.");
          }
        },
        (error) => {
          console.error('Error adding doctor:', error);
        }
      );
    }


  }

  reset() {
    this.doctor = new Doctor();
  }

  ngAfterViewInit() {
    if (this.doctorForm.statusChanges)
      this.doctorForm.statusChanges.subscribe(result =>
        this.IsValid.emit(result != 'INVALID')

      );
  }
}
