import { Seat } from './seat';
import { Vertex } from './../common-interfaces/vertex';
import { Wall } from './wall';
export interface Room {
    Id: number;
    Walls: Wall[];
    Seats: Seat[];
    Projects: string[];
    StateChanged: string;
  }
  