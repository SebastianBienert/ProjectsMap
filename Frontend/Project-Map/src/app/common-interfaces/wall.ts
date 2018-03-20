import { Vertex } from './../common-interfaces/vertex';
export interface Wall {
    WallId: number;
    StartVertex: Vertex;
    EndVertex: Vertex;
  }