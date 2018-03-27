package project.projectsmap;

import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.PorterDuff;
import android.graphics.drawable.BitmapDrawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class ShowMapActivity extends AppCompatActivity {
    Canvas canvas;
    Button clickLoadData, clickBack, clickShowMap;
    TextView buildingDescription, statement;
    ProgressBar waitForData;
    Floor floor;
    ArrayList<Floor> arrayFloors = new ArrayList<Floor>();
    //ArrayAdapter<CharSequence> arrayAdapterFloors;
    ArrayAdapter<String> arrayAdapterFloors;
    ArrayList<Building> arrayBulindings = new ArrayList<Building>();
    ArrayAdapter<String> arrayAdapterBuildings;
    //ArrayAdapter<CharSequence> arrayAdapterBuildings;
    Spinner spinnerBuildings, spinnerFloors;

    public void setArrayBulindings(ArrayList<Building> arrayBulindings) {
        this.arrayBulindings = arrayBulindings;
        setSpinnerBuildingsAdapter(nameBuildings(arrayBulindings));
    }

    public void setArrayFloors(ArrayList<Floor> arrayFloors) {
        this.arrayFloors = arrayFloors;
        setSpinnerFloorsAdapter(nameFloors(arrayFloors));
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_map);
        setCanvas();
        spinnerBuildings = (Spinner)  findViewById(R.id.spinnerBuildings);
        spinnerFloors = (Spinner)  findViewById(R.id.spinnerFloors);
        clickBack = (Button) findViewById(R.id.buttonBack);
        clickLoadData = (Button) findViewById(R.id.buttonLoadData);
        clickShowMap = (Button) findViewById(R.id.buttonShowMap);
        //buildingDescription = findViewById(R.id.textViewBuildingDescription);
        statement = findViewById(R.id.textViewStatement);
        waitForData = (ProgressBar) findViewById(R.id.progressBarWaitForData);
        waitForData.setVisibility(View.INVISIBLE);
        clickBack.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                ShowMapActivity.super.finish();
            }
        });
        spinnerBuildings.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int position, long l) {
                setSpinnerFloorsAdapter(nameFloors(floorsInBuilding(arrayBulindings.get(findPositionBulidingOnArray((String) adapterView.getItemAtPosition(position))).BuildingId)));
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        spinnerFloors.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int position, long l) {
                //Toast.makeText(getBaseContext(), adapterView.getItemAtPosition(position) + " selected", Toast.LENGTH_LONG);
                floor = findFloor((String) adapterView.getItemAtPosition(position));
                //buildingDescription.setText(floor.allDescription());
                waitForData.setVisibility(View.VISIBLE);
                canvas.drawColor(Color.TRANSPARENT, PorterDuff.Mode.CLEAR);
                DrawMap();
                DisableProgressBar();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        clickLoadData.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                clearStaement();
                waitForData.setVisibility(View.VISIBLE);
                FetchDataMap process = new FetchDataMap();
                process.setNumberId("1");
                process.setNumberCompanyId("1");    //na sztywno do testów potem ustawiane po zalogowaniu
                process.setContext(ShowMapActivity.this);
                process.execute();
            }
        });
        clickShowMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                clearStaement();
                if(floor!=null){
                    Intent intent = new Intent(ShowMapActivity.this, MapActivity.class);
                    intent.putExtra("choice", true);
                    intent.putExtra("fl", floor);
                    //MapActivity.floor = floor;
                    startActivity(intent);
                }else{
                    setStatement("Najpierw pobierz piętro");
                }
            }
        });
    }
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void setStatement(String text) {
        statement.setText(text);
    }
    public void clearStaement() {
        statement.setText("");
    }
    public void setDescription(String text) {
        buildingDescription.setText(text);
    }
    private void loadDataToSpinnerBuildings(){

    }
    private void loadDataToSpinnerFloors(int buildingId){

    }
    private int findPositionBulidingOnArray(String text){
        for(int i = 0; i < arrayBulindings.size(); i++){
            if(arrayBulindings.get(i).Address.equals(text)){
                return i;
            }
        }
        return -1;
    }
    private Floor findFloor(String text){
        for(int i = 0; i < arrayFloors.size(); i++){
            if(arrayFloors.get(i).Description.equals(text)){
                return arrayFloors.get(i);
            }
        }
        return null;
    }
    private void setSpinnerBuildingsAdapter(ArrayList<String> nameBuildings){
        arrayAdapterBuildings = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, nameBuildings);
        arrayAdapterBuildings.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerBuildings.setAdapter(arrayAdapterBuildings);
    }
    private void setSpinnerFloorsAdapter(ArrayList<String> nameFloors){
        arrayAdapterFloors = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_item, nameFloors);
        arrayAdapterFloors.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerFloors.setAdapter(arrayAdapterFloors);
    }
    private ArrayList<String> nameBuildings(ArrayList<Building> arrBuildings){
        ArrayList<String> names = new ArrayList<String>();
        for(int i = 0; i < arrBuildings.size(); i++){
            names.add(arrBuildings.get(i).Address);
        }
        return names;
    }
    private ArrayList<String> nameFloors(ArrayList<Floor> arrFloors){
        ArrayList<String> names = new ArrayList<String>();
        for(int i = 0; i < arrFloors.size(); i++){
            names.add(arrFloors.get(i).Description);
        }
        return names;
    }
    private ArrayList<Floor> floorsInBuilding(int buildingID){
        ArrayList<Floor> floors = new ArrayList<Floor>();
        for(int i = 0; i < arrayFloors.size(); i++){
            if(arrayFloors.get(i).getBuildingId()==buildingID){
                floors.add(arrayFloors.get(i));
            }
        }
        return floors;
    }
    private void setCanvas(){
        Bitmap bg;
        LinearLayout ll;
        bg = Bitmap.createBitmap(600,600,Bitmap.Config.ARGB_8888);
        canvas = new Canvas(bg);
        ll = (LinearLayout) findViewById(R.id.rect);
        ll.setBackground(new BitmapDrawable(bg));
    }
    private void DrawRectangle(int x, int y, int width, int length, boolean fulfillment, String colorString){
        Paint paint = new Paint();
        paint.setColor(Color.parseColor(colorString));
        if(fulfillment){
            paint.setStyle(Paint.Style.FILL);
        }else{
            paint.setStyle(Paint.Style.STROKE);
        }
        canvas.drawRect(x,y,x+length,y+width,paint);
    }
    private void DrawPolygon(ArrayList<Wall> walls, boolean fulfillment, String colorString){
        Paint paint = new Paint();
        paint.setColor(Color.parseColor(colorString));
        if(fulfillment){
            paint.setStyle(Paint.Style.FILL);
        }else{
            paint.setStyle(Paint.Style.STROKE);
        }
        Path wallPath = new Path();
        wallPath.reset();
        wallPath.moveTo(walls.get(0).beginX,walls.get(0).beginY);
        wallPath.lineTo(walls.get(0).endX,walls.get(0).endY);
        for(int i = 1; i < walls.size(); i++){
            wallPath.lineTo(walls.get(i).beginX,walls.get(i).beginY);
            wallPath.lineTo(walls.get(i).endX,walls.get(i).endY);
        }
        canvas.drawPath(wallPath,paint);
    }
    private void DrawMap(){
        ArrayList<Room> rooms = floor.Rooms;
        for(int i = 0; i < rooms.size(); i++){
            DrawPolygon(rooms.get(i).getWalls(),false, "#000000");
            ArrayList<Seat> seats = rooms.get(i).getSeats();
            for(int j = 0; j < seats.size(); j++){
                DrawRectangle(seats.get(j).getX(),seats.get(j).getY(),10,10, true, "#000000");
            }
        }
    }
}
