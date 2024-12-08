export class Appointment{

    id!: number;


   
    patientId!: number;
   
    doctorId!: number;
    appointmentDate: string | number | Date | null | undefined;
    problem!: string;
    status!: string;
    doctorName!: string;
    patientName!: string | null;
}