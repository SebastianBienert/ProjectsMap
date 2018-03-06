import { Vertex } from './../common-interfaces/vertex';

export interface Seat{
    Id : number,
    Vertex : Vertex,
    RoomId : number,
    DeveloperId : number
}