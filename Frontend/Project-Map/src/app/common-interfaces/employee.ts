import { Seat } from './../common-interfaces/seat';
import { Technology } from './../common-interfaces/technology';

export interface Employee{
    Id : number,
    CompanyId: number,
    FirstName : string,
    Surname : string,
    Technologies : string[],
    Seat : Seat,
    ManagerId: number,
    ManagerCompanyId: number,
    Email: string,
    JobTitle: string
}