import { Vertex } from './../common-interfaces/vertex';
import { Wall } from './wall';
export interface Room {
    Walls: Wall[];
    Seats: Vertex[];
    Projects: string[];
  }
  