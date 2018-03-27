import { Seat } from './../common-interfaces/seat';
import { Technology } from './../common-interfaces/technology';

export interface Employee{
    Id : number,
    CompanyId : number,
    Url : string,
    ManagerId : number,
    ManagerCompanyId : number,
    FirstName : string,
    Surname : string,
    Email : string,
    JobTitle : string,
    CompanyName : string,
    Technologies : string[],
    Seat : Seat
}