import { Employee } from "./employee";
import { Room } from "./room";
import { Technology } from "./Technology";

export interface Project{
    Id : number,
    Description : string,
    RepositoryLink : string,
    DocumentationLink : string,
    ProductOwnerId : Employee,
    CompanyId : number,
    Developers : Employee[],
    Rooms : Room,
    Technologies : Technology
}