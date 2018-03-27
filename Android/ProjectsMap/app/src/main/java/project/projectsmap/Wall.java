package project.projectsmap;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.Serializable;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class Wall implements Serializable {
    int beginX;
    int beginY;
    int endX;
    int endY;
    Wall(int bx,int by,int ex,int ey) {
        beginX = bx;
        beginY = by;
        endX = ex;
        endY = ey;
    }
    Wall(JSONObject object) throws JSONException {
        convertJSON(object);
    }
    public void setEndX(int endX) {
        this.endX = endX;
    }
    public void setEndY(int endY) {
        this.endY = endY;
    }
    public void setBeginX(int x) {
        this.beginX = x;
    }
    public void setBeginY(int y) {
        this.beginY = y;
    }
    public int getEndX() {
        return endX;
    }
    public int getEndY() {
        return endY;
    }
    public int getBeginX() {
        return beginX;
    }
    public int getBeginY() {
        return beginY;
    }

    private void convertJSON(JSONObject object) throws JSONException {
        JSONObject JOBegin = (JSONObject) object.get("StartVertex");
        beginX = JOBegin.getInt("X");
        beginY = JOBegin.getInt("Y");
        JSONObject JOEnd = (JSONObject) object.get("EndVertex");
        endX = JOEnd.getInt("X");
        endY = JOEnd.getInt("Y");
    }
}
