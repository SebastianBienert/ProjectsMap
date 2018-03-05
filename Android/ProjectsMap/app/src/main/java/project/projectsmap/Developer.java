package project.projectsmap;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

/**
 * Created by Mateusz on 05.03.2018.
 */

public class Developer {
    public int DeveloperId ;

    public String FirstName;

    public String Surname;

    public String Email;

    public boolean WantToHelp;

    public byte[] Photo;

    public String JobTitle;

    public int CompanyId;

    public ArrayList<String> Technologies;    // docelowo później String zmienić na technology

    //public virtual User User { get; set; }
    //public virtual Company Company { get; set; }
    //Many to many relation (Project - Developer)
    //public virtual ICollection<Project> Projects { get; set; }
    //One to many relation(Seat-Developer)
    //public virtual ICollection<Seat> Seat { get; set; }

    Developer(JSONObject developerData) throws JSONException {
        DeveloperId = (int) developerData.get("Id");
        if(developerData.has("FirstName")){
            if(!developerData.isNull("FirstName")){
                FirstName = developerData.getString("FirstName");
            }else{
                FirstName = "brak danych";
            }
        }else{
            FirstName = "brak danych";
        }
        if(developerData.has("Surname")){
            if(!developerData.isNull("Surname")){
                Surname = developerData.getString("Surname");
            }else{
                Surname = "brak danych";
            }
        }else{
            Surname = "brak danych";
        }
        if(developerData.has("Email")){
            if(!developerData.isNull("Email")){
                Email = developerData.getString("Email");
            }else{
                Email = "brak danych";
            }
        }else{
            Email = "brak danych";
        }
        if(developerData.has("WantToHelp")){
            if(!developerData.isNull("WantToHelp")){
                WantToHelp = developerData.getBoolean("WantToHelp");
            }else{
                WantToHelp = false;
            }
        }else{
            WantToHelp = false;
        }
        if(developerData.has("Photo")){
            if(!developerData.isNull("Photo")){
                Photo = (byte[]) developerData.get("Photo");
            }else{
                Photo = null;
            }
        }else{
            Photo = null;
        }
        if(developerData.has("JobTitle")){
            if(!developerData.isNull("JobTitle")){
                JobTitle = developerData.getString("JobTitle");
            }else{
                JobTitle = "brak danych";
            }
        }else{
            JobTitle = "brak danych";
        }
        if(developerData.has("CompanyId")){
            if(!developerData.isNull("CompanyId")){
                CompanyId = developerData.getInt("CompanyId");
            }else{
                CompanyId = -1;
            }
        }else{
            CompanyId = -1;
        }
        if(developerData.has("Technologies")){
            if(!developerData.isNull("Technologies")){
                Technologies = divisinOfTechnologies(developerData.getString("Technologies"));
            }else{
                Technologies = null;;
            }
        }else{
            Technologies = null;;
        }
    }
    private ArrayList<String> divisinOfTechnologies(String text){
        String[] table;
        ArrayList<String> newArray = new ArrayList<String>();
        text = text.substring(1,text.length()-1);
        table = text.split(",");
        for(int i = 0; i < table.length; i++){
            newArray.add(table[i]);
        }
        return newArray;
    }
    public String developerDescription(){
        String description;
        description = "Id: " + DeveloperId + "\n"+
                    "FirstName: " + FirstName + "\n"+
                    "Surname: " + Surname + "\n"+
                    "Email: " + Email + "\n"+
                    "JobTitle: " + JobTitle + "\n"+
                    "WantToHelp: " + WantToHelp + "\n";
        if(CompanyId != -1) {
            description += "CompanyId: " + CompanyId + "\n";
        }
        if(Technologies!=null){
            description += "Technologies: " + "\n";
            for(int i = 0; i < Technologies.size(); i++){
                description += Technologies.get(i) + "\n";;
            }
        }else{
            description += "Technologies: brak danych" + "\n";
        }
        return description;
    }
}