package project.projectsmap;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;
import java.util.ArrayList;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class Seat implements Serializable {
    int seatId;
    int x;
    int y;
    int roomId;
    public Seat(int xx, int yy, int rid, int sid){
        seatId = sid;
        roomId = rid;
        x = xx;
        y = yy;
    }
    public Seat(JSONObject object) throws JSONException {
        convertJSON(object);
    }

    public int getX() {
        return x;
    }
    public int getY() {
        return y;
    }
    public void setX(int x) {
        this.x = x;
    }
    public void setY(int y) { this.y = y; }

    private void convertJSON(JSONObject object) throws JSONException {
        if(object.has("Vertex")){
            convertJSONVertex(object.getJSONObject("Vertex"));
        }else{
            x = object.getInt("X");
            y = object.getInt("Y");
        }
        if(object.has("Id")){
            if(!object.isNull("Id")){
                seatId = object.getInt("Id");
            }else{
                seatId = -1;
            }
        }else{
            seatId = -1;
        }
        if(object.has("RoomId")){
            if(!object.isNull("RoomId")){
                roomId = object.getInt("RoomId");
            }else{
                roomId = -1;
            }
        }else{
            roomId = -1;
        }
    }
    private void convertJSONVertex(JSONObject object) throws JSONException {
        x = object.getInt("X");
        y = object.getInt("Y");
    }
}
