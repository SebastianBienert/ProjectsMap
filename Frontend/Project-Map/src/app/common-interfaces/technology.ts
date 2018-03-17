import { Project } from './../common-interfaces/project';
import { Employee } from './../common-interfaces/employee';

export interface Technology{
    Id : number,
    Name : string,
    DevelopersIds : Employee[],
    ProjectsIds : Project[]
}