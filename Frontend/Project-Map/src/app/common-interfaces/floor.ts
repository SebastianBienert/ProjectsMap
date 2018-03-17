import { Wall } from './wall';
import { Room } from "./room";

export interface Floor{
    Id : number,
    Description : string,
    FloorNumber : number,
    BuildingId : number,
    Rooms : Room[],
    Walls : Wall[],
}