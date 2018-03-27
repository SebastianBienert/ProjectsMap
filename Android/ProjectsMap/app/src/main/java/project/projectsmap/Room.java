package project.projectsmap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class Room implements Serializable {
    public Room() { }
    public Room(JSONObject object) throws JSONException {
        convertData(object);
    }
    int RoomId;
    int FloorId;
    ArrayList<Wall> Walls;
    ArrayList<Seat> Seats;

    public ArrayList<Seat> getSeats() {
        return Seats;
    }
    public void setSeats(ArrayList<Seat> seats) {
        Seats = seats;
    }
    public void setWalls(ArrayList<Wall> walls) {
        Walls = walls;
    }
    public ArrayList<Wall> getWalls() {
        return Walls;
    }
    public int getRoomId() {
        return RoomId;
    }
    public int getFloorId() {
        return FloorId;
    }

    public String description(){
        String description;
        description = "RoomId: " + RoomId + "\n";
        if(FloorId != -1) {
            description += "FloorId: " + FloorId + "\n";
        }else{
            description += "FloorId: brak danych \n";
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
        if(Seats!=null){
            description += "Seats: " + "\n";
            for(int i = 0; i < Seats.size(); i++){
                description += Seats.get(i).getX() + ", " + Seats.get(i).getY() + " \n";
                if(i!=Seats.size()){
                    description += ", ";
                }
            }
        }else{
            description += "Brak wprowadzonych miejsc siedzących" + " \n";
        }
        return description;
    }

    private void convertData(JSONObject object)throws JSONException {
        RoomId = (int) object.get("Id");
        if(object.has("FloorId")){
            if(!object.isNull("FloorId")){
                FloorId = object.getInt("FloorId");
            }else{
                FloorId = -1;
            }
        }else{
            FloorId = -1;
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
        if(object.has("Seats")){
            if(!object.isNull("Seats")){

                Seats = convertJSONSeatsOnList(object);
            }else{
                Seats = null;
            }
        }else{
            Seats = null;
        }
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

    private ArrayList<Seat> convertJSONSeatsOnList(JSONObject object) throws JSONException {
        ArrayList<Seat> seats = new ArrayList<Seat>();
        JSONArray JObjects = object.getJSONArray("Seats");
        for(int i = 0; i < JObjects.length(); i++){
            JSONObject JO = (JSONObject) JObjects.get(i);
            seats.add(new Seat(JO));
        }
        return seats;
    }
}
