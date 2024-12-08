export class Doctor {

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
    specialization!: string;
    qualifications!: string;
    departmentId!: string;
    createdAt!: string;
    updatedAt!: string;

    constructor() {
        // Initialize dateOfBirth as null or any default value
        this.dateOfBirth = null;
      }


}