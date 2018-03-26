package project.projectsmap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 20.03.2018.
 */

public class Building implements Serializable {
    int BuildingId;
    String Address;
    //String Company;     //docelowo obiekt Company
    int CompanyId;
    ArrayList<Integer> Floors;
    Building(JSONObject object) throws JSONException {
        convertJSON(object);
    }
    public String description(){
        String description;
        description = "Id: " + BuildingId + "\n"+
                "Address: " + Address + "\n";
        if(CompanyId != -1) {
            description += "CompanyId: " + CompanyId + "\n";
        }
        if(Floors!=null){
            description += "Floors: " + "\n";
            for(int i = 0; i < Floors.size(); i++){
                description += Floors.get(i);
                if(i!=Floors.size()){
                    description += ", ";
                }
            }
        }else{
            description += "Brak wprowadzonych pięter" + " \n";
        }
        return description;
    }

    private void convertJSON(JSONObject object) throws JSONException {
        BuildingId = (int) object.get("Id");
        if(object.has("Address")){
            if(!object.isNull("Address")){
                Address = object.getString("Address");
            }else{
                Address = "brak danych";
            }
        }else{
            Address = "brak danych";
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
        if(object.has("FloorsIds")){
            if(!object.isNull("FloorsIds")){
                Floors = convertJSONFloorsOnList(object);
            }else{
                Floors = null;
            }
        }else{
            Floors = null;
        }
    }

    private ArrayList<Integer> convertJSONFloorsOnList(JSONObject object) throws JSONException {
        ArrayList<Integer> floors = new ArrayList<Integer>();
        JSONArray JObjects = object.getJSONArray("FloorsIds");
        for(int i = 0; i < JObjects.length(); i++){
            floors.add(JObjects.getInt(i));
        }
        return floors;
    }
}
