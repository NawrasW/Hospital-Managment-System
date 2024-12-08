export class Schedule

{

    id!: number;

    doctor!:string;
    availableStartDay!: string;
    availableEndDay!: string;
    doctorId!: number;
    availableStartTime: string | number | Date | null | undefined;
    availableEndTime: string | number | Date | null | undefined;
 
    status!: string;
   


}