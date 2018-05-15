import { Seat } from './../common-interfaces/seat';
import { Technology } from './../common-interfaces/technology';

export interface Employee{
    Id : number,
    PhotoUrl : string,
    Url : string,
    ManagerId : number,
    FirstName : string,
    Surname : string,
    Email : string,
    JobTitle : string,
    Technologies : string[],
    Seat : Seat,
    Projects : any[]
}