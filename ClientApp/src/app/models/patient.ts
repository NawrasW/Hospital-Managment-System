export class Patient{

    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    password!: string;
    confirmpassword!: string;
    phoneNumber!: string;
    dateOfBirth: string | number | Date | null | undefined;
    address!: string;
    gender!: string;
   
    bloodType!: string;
    allergies!: string;
    insuranceDetails!: string;

    constructor() {
        // Initialize dateOfBirth as null or any default value
        this.dateOfBirth = null;
      }




}