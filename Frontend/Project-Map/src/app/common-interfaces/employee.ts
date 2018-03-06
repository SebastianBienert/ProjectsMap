import { Seat } from './../common-interfaces/seat';
import { Technology } from './../common-interfaces/technology';

export interface Employee{
    Id : number,
    FirstName : string,
    Surname : string,
    Technologies : Technology[],
    Seat : Seat
}