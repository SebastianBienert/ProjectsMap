package project.projectsmap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 26.03.2018.
 */

public class Project implements Serializable {
    int ProjectId;
    String Description;
    String RepositoryLink;
    String DocumentationLink;
    int CompanyId;
    ArrayList<String> technologies;
    ArrayList<String> employees;

    public Project(JSONObject object) throws JSONException {
        convertData(object);
    }

    public int getProjectId() {
        return ProjectId;
    }

    public String getDescription() {
        return Description;
    }

    public String getRepositoryLink() {
        return RepositoryLink;
    }

    public String getDocumentationLink() {
        return DocumentationLink;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public int getCompanyId() {
        return CompanyId;
    }

    public ArrayList<String> getTechnologies() {
        return technologies;
    }

    public void setTechnologies(ArrayList<String> technologies) {
        this.technologies = technologies;
    }

    public void setEmployees(ArrayList<String> employees) {
        this.employees = employees;
    }

    public ArrayList<String> getEmployees() {
        return employees;
    }

    public String description(){
        String description;
        description = "Id: " + ProjectId + " \n"+
                "OPIS: " + "\n" + Description + "\n";
        if(!RepositoryLink.isEmpty()) {
            description += "LINK DO REPOZYTORIUM: " + "\n" + RepositoryLink + "\n";
        }
        if(!DocumentationLink.isEmpty()) {
            description += "LINK DO DOKUMENTACJI: " + "\n" + DocumentationLink + "\n";
        }
        if(technologies!=null){
            description += "TECHNOLOGIE: " + "\n";
            for(int i = 0; i < technologies.size(); i++){
                description += technologies.get(i) + ", ";
            }
            description += " \n";
        }else{
            description += "Brak wprowadzonych technologii" + " \n";
        }
        if(employees!=null){
            description += "PRACOWNICY: " + "\n";
            for(int i = 0; i < employees.size(); i++){
                description += employees.get(i) + ", ";
            }
            description += " \n";
        }else{
            description += "Brak wprowadzonych pracowników" + " \n";
        }

        return description;
    }

    private void convertData(JSONObject object) throws JSONException {
        ProjectId = (int) object.get("Id");
        if(object.has("Description")){
            if(!object.isNull("Description")){
                Description = object.getString("Description");
            }else{
                Description = "brak danych";
            }
        }else{
            Description = "brak danych";
        }
        if(object.has("DocumentationLink")){
            if(!object.isNull("DocumentationLink")){
                DocumentationLink = object.getString("DocumentationLink");
            }else{
                DocumentationLink = "brak danych";
            }
        }else{
            DocumentationLink = "brak danych";
        }
        if(object.has("RepositoryLink")){
            if(!object.isNull("RepositoryLink")){
                RepositoryLink = object.getString("RepositoryLink");
            }else{
                RepositoryLink = "brak danych";
            }
        }else{
            Description = "brak danych";
        }
        if(object.has("CompanyId")){
            if(!object.isNull("CompanyId")){
                CompanyId = object.getInt("CompanyId");
            }else{
                CompanyId = -1;
            }
        }else{
            CompanyId = -1;
        }
        if(object.has("Technologies")){
            if(!object.isNull("Technologies")){
                technologies = convertJSONTechnologiesOnList(object);
            }else{
                technologies = null;
            }
        }else{
            technologies = null;
        }
        if(object.has("EmployeesNames")){
            if(!object.isNull("EmployeesNames")){

                employees = divisinOfEmployees(object.getString("EmployeesNames"));
            }else{
                employees = null;
            }
        }else{
            employees = null;
        }
    }
    private ArrayList<String> convertJSONTechnologiesOnList(JSONObject object) throws JSONException {
        String[] table;
        ArrayList<String> tech = new ArrayList<String>();
        JSONArray JObjects = object.getJSONArray("Technologies");
        String text = JObjects.toString().substring(1,JObjects.toString().length()-1);
        table = text.split(",");
        for(int i = 0; i < table.length; i++){
            tech.add(table[i].substring(1,table[i].length()-1));
        }
        /*for(int i = 0; i < JObjects.length(); i++){
            JSONObject JO = (JSONObject) JObjects.get(i);
            tech.add(JO.getString("Name"));
        }*/
        return tech;
    }
    private ArrayList<String> divisinOfEmployees(String text) throws JSONException {
        String[] table;
        ArrayList<String> emp = new ArrayList<String>();
        text = text.substring(1,text.length()-1);
        table = text.split(",");
        for(int i = 0; i < table.length; i++){
            emp.add(table[i]);
        }
        return emp;
    }
}
