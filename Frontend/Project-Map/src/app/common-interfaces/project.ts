import { Employee } from "./employee";
import { Room } from "./room";
import { Technology } from "./technology";

export interface Project{
    Id : number,
    Url : string,

    Description : string,
    RepositoryLink : string,
    DocumentationLink : string,
    EmployeesNames : string[],
    Rooms : Room,
    Technologies : Technology
}