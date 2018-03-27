package project.projectsmap;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 05.03.2018.
 */

public class Developer implements Serializable {
    int DeveloperId ;

    String FirstName;

    String Surname;

    String Email;

    boolean WantToHelp;

    byte[] Photo;

    String JobTitle;

    int CompanyId;

    ArrayList<String> Technologies;    // docelowo później String zmienić na technology

    Seat Place;
    //public virtual User User { get; set; }
    //public virtual Company Company { get; set; }
    //Many to many relation (Project - Developer)
    //public virtual ICollection<Project> Projects { get; set; }
    //One to many relation(Seat-Developer)
    //public virtual ICollection<Seat> Seat { get; set; }
    Developer(JSONObject developerData) throws JSONException {
        convertJSON(developerData);
    }

    public Seat getSeat() {
        return Place;
    }

    public void setSeat(Seat place) {
        this.Place = place;
    }

    public int getDeveloperId() {
        return DeveloperId;
    }

    public int getCompanyId() {
        return CompanyId;
    }

    public void setCompanyId(int companyId) {
        CompanyId = companyId;
    }

    public ArrayList<String> getTechnologies() {
        return Technologies;
    }

    public void setTechnologies(ArrayList<String> technologies) {
        Technologies = technologies;
    }

    public void setWantToHelp(boolean wantToHelp) {
        WantToHelp = wantToHelp;
    }

    public String getFirstName() {
        return FirstName;
    }

    public void setFirstName(String firstName) {
        FirstName = firstName;
    }

    public String getSurname() {
        return Surname;
    }

    public void setSurname(String surname) {
        Surname = surname;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public byte[] getPhoto() {
        return Photo;
    }

    public void setPhoto(byte[] photo) {
        Photo = photo;
    }

    public String getJobTitle() {
        return JobTitle;
    }

    public void setJobTitle(String jobTitle) {
        JobTitle = jobTitle;
    }

    public String description(){
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
        if(Place!=null){
            description += "Seat: \n Number seat" + Place.seatId + " \n Number room" + Place.roomId + "\n";
        }else{
            description += "Seat: brak danych" + "\n";
        }
        return description;
    }

    private void convertJSON(JSONObject developerData) throws JSONException {
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
        if(developerData.has("Seat")){
            if(!developerData.isNull("Seat")){
                Place = new Seat(developerData.getJSONObject("Seat"));
            }else{
                Place = null;
            }
        }else{
            Place = null;
        }
        if(developerData.has("CompanyId")){
            if(developerData.has("JobTitle")){
                if(!developerData.isNull("JobTitle")){
                    JobTitle = developerData.getString("JobTitle");
                }else{
                    JobTitle = "brak danych";
                }
            }else{
                JobTitle = "brak danych";
            }  if(!developerData.isNull("CompanyId")){
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
}