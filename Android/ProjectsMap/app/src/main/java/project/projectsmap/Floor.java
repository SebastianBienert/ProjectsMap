package project.projectsmap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class Floor implements Serializable {
    int FloorId;

    String Description;

    int FloorNumber;

    //public Building Building { get; set; }

    int BuildingId;

    ArrayList<Room> Rooms;

    ArrayList<Wall> Walls;

    public Floor(JSONObject object) throws JSONException {
        convertData(object);
    }

    public int getFloorId() {
        return FloorId;
    }

    public int getFloorNumber() {
        return FloorNumber;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public int getBuildingId() {
        return BuildingId;
    }

    public ArrayList<Room> getRooms() {
        return Rooms;
    }

    public void setRooms(ArrayList<Room> rooms) {
        Rooms = rooms;
    }

    public ArrayList<Wall> getWalls() {
        return Walls;
    }

    public void setWalls(ArrayList<Wall> walls) {
        Walls = walls;
    }

    public String allDescription(){
        String description;
        description = "Id: " + FloorId + "\n"+
                "Description: " + Description + "\n";
        if(FloorNumber != -1) {
            description += "FloorNumber: " + FloorNumber + "\n";
        }
        if(BuildingId != -1) {
            description += "BuildingId: " + BuildingId + "\n";
        }
        if(Walls!=null){
            description += "Walls: " + "\n";
            for(int i = 0; i < Walls.size(); i++){
                description += Walls.get(i).getBeginX() + ", " + Walls.get(i).getBeginY() + ", " + Walls.get(i).getEndX() + ", " + Walls.get(i).getEndY() + " \n";
                if(i!=Walls.size()){
                    description += ", ";
                }
            }
        }else{
            description += "Brak wprowadzonych ścian" + " \n";
        }
        if(Rooms!=null){
            description += "Rooms: " + "\n";
            for(int i = 0; i < Rooms.size(); i++){
                description += Rooms.get(i).getRoomId();// + " \n";
                if(i!=Rooms.size()){
                    description += ", ";
                }
            }
        }else{
            description += "Brak wprowadzonych pokoi" + "\n";
        }
        return description;
    }
    public String floorDescription(){
        String description;
        description = "Id: " + FloorId + "\n"+
                "Description: " + Description + "\n";
        if(FloorNumber != -1) {
            description += "FloorNumber: " + FloorNumber + "\n";
        }
        if(BuildingId != -1) {
            description += "BuildingId: " + BuildingId + "\n";
        }
        return description;
    }

    private void convertData(JSONObject object) throws JSONException {
        FloorId = (int) object.get("Id");
        if(object.has("Description")){
            if(!object.isNull("Description")){
                Description = object.getString("Description");
            }else{
                Description = "brak danych";
            }
        }else{
            Description = "brak danych";
        }
        if(object.has("FloorNumber")){
            if(!object.isNull("FloorNumber")){
                FloorNumber = object.getInt("FloorNumber");
            }else{
                FloorNumber = -1;
            }
        }else{
            FloorNumber = -1;
        }
        if(object.has("BuildingId")){
            if(!object.isNull("BuildingId")){
                BuildingId = object.getInt("BuildingId");
            }else{
                BuildingId = -1;
            }
        }else{
            BuildingId = -1;
        }
        if(object.has("Rooms")){
            if(!object.isNull("Rooms")){
                Rooms = convertJSONRoomsOnList(object);
            }else{
                Rooms = null;
            }
        }else{
            Rooms = null;
        }
        if(object.has("Walls")){
            if(!object.isNull("Walls")){

                Walls = convertJSONWallsOnList(object);
            }else{
                Walls = null;
            }
        }else{
            Walls = null;
        }
    }

    private ArrayList<Room> convertJSONRoomsOnList(JSONObject object) throws JSONException {
        ArrayList<Room> rooms = new ArrayList<Room>();
        JSONArray JObjects = object.getJSONArray("Rooms");
        for(int i = 0; i < JObjects.length(); i++){
            JSONObject JO = (JSONObject) JObjects.get(i);
            rooms.add(new Room(JO));
        }
        return rooms;
    }
    private ArrayList<Wall> convertJSONWallsOnList(JSONObject object) throws JSONException {
        ArrayList<Wall> walls = new ArrayList<Wall>();
        JSONArray JObjects = object.getJSONArray("Walls");
        for(int i = 0; i < JObjects.length(); i++){
            JSONObject JO = (JSONObject) JObjects.get(i);
            walls.add(new Wall(JO));
        }
        return walls;
    }

}
