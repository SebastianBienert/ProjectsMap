import { Wall } from './wall';
import { Room } from "./room";

export interface Floor{
    Id : number,
    Description : string,
    BuildingId : number,
    Rooms : Room[],
    Walls : Wall[],
}