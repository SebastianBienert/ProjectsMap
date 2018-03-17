import { Employee } from "./employee";
import { Room } from "./room";
import { Technology } from "./Technology";

export interface Project{
    Id : number,
    Url : string,

    Description : string,
    RepositoryLink : string,
    DocumentationLink : string,
    ProductOwnerId : Employee,
    CompanyId : number,
    EmployeesNames : string[],
    Rooms : Room,
    Technologies : Technology
}