import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { PageModeEnum } from 'app/constants/enum';
import { Patient } from 'app/models/patient';

import {PatientService} from 'app/Patient/patient.service'
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  constructor(private PatientService: PatientService, private router: Router, private activatedRoute: ActivatedRoute) {

  }
  ngOnInit(): void {

    this.activatedRoute.params.subscribe(params => {
      this.PatientId = params['id'];

      if (this.PatientId) {
        this.pageMode = PageModeEnum.Update;
        this.PatientService.getPatientById(this.PatientId).subscribe(result => {
          // console.log(result)
          this.patient = result;
          this.patient.confirmpassword = this.patient.password;

        });


      }

    });
  }
  @Input() isPopup: boolean = false;
  @Output() IsValid = new EventEmitter<boolean>();
  @ViewChild('PatientForm') PatientForm!: NgForm;

  pageModeTest: string = "Add";
  @Input() pageMode: PageModeEnum = PageModeEnum.Add;
  PatientId!: number;
  @Input() patient: Patient = new Patient();

  get PageModeEnum(): any {

    return PageModeEnum;

  }

  onSubmit(PatientForm: NgForm) {
console.log(PatientForm, this.patient)
    if (!(this.patient.password === this.patient.confirmpassword)) {
     
     console.log("invaild passowrd");



    }

    else {

      this.PatientService.addUpdatePatient(this.patient).subscribe(
        (result) => {
          if (result) {
            console.log("Patient saved:", result);
           
        
            console.log("Navigating to Patient list...");
            this.router.navigateByUrl('admin/Patient-list');
          } else {
            console.error("No result returned.");
          }
        },
        (error) => {
          console.error('Error adding Patient:', error);
        }
      );
    }


  }

  reset() {
    this.patient = new Patient();
  }

  ngAfterViewInit() {
    if (this.PatientForm.statusChanges)
      this.PatientForm.statusChanges.subscribe(result =>
        this.IsValid.emit(result != 'INVALID')

      );
  }
}
