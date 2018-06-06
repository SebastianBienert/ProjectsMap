package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.PorterDuff;
import android.graphics.drawable.BitmapDrawable;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
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
    Boolean isOnline;
    Context context;

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
        final String token = getIntent().getExtras().getString("token");
        isOnline = getIntent().getExtras().getBoolean("isOnline");
        //context = (MainActivity)getIntent().getExtras().get("context");

        spinnerBuildings = (Spinner)  findViewById(R.id.spinnerBuildings);
        spinnerFloors = (Spinner)  findViewById(R.id.spinnerFloors);
        //buildingDescription = findViewById(R.id.textViewBuildingDescription);
        statement = findViewById(R.id.textViewStatement);
        waitForData = (ProgressBar) findViewById(R.id.progressBarWaitForData);
        waitForData.setVisibility(View.INVISIBLE);
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
        clearStatement();
        if(!isOnline || !isNetworkAvailable()){
            setStatement("Pracujesz offline");
        }
        waitForData.setVisibility(View.VISIBLE);
        FetchDataMap process = new FetchDataMap();
        process.setToken(token);
        process.setNumberId("1");
        process.setNumberCompanyId("1");    //na sztywno do testów potem ustawiane po zalogowaniu
        process.setContext(ShowMapActivity.this);
        process.setInfoAboutConnectToInternet(isOnline);
        process.execute();
    }
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void setStatement(String text) {
        if(text.equals("Pracujesz offline")){
            GlobalVariable.setOnlineWork(false);
            statement.setBackgroundColor(Color.BLUE);
            statement.setTextColor(Color.WHITE);
            //((MainActivity)context).setOfflineWork();
        }else{
            statement.setBackgroundColor(Color.parseColor("#33FFFFFF"));
            statement.setTextColor(Color.GRAY);
        }
        statement.setText(text);
    }
    public void clearStatement() {
        statement.setBackgroundColor(Color.parseColor("#33FFFFFF"));
        statement.setTextColor(Color.GRAY);
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
        bg = Bitmap.createBitmap(800,800,Bitmap.Config.ARGB_8888);
        canvas = new Canvas(bg);
        ll = (LinearLayout) findViewById(R.id.rect);
        ll.setBackground(new BitmapDrawable(bg));
    }
    private void DrawSeat(int x, int y, int width, int length, boolean fulfillment, String colorString){
        Paint paint = new Paint();
        paint.setColor(Color.parseColor(colorString));
        if(fulfillment){
            paint.setStyle(Paint.Style.FILL);
        }else{
            paint.setStyle(Paint.Style.STROKE);
        }
        canvas.drawRect(x,y,x+length,y+width,paint);
    }
    private void DrawRoom(ArrayList<Wall> walls, String colorStringWalls, String colorStringArea){
        Paint paintWalls = new Paint();
        Paint paintArea = new Paint();
        paintWalls.setColor(Color.parseColor(colorStringWalls));
        paintArea.setColor(Color.parseColor(colorStringArea));
        paintArea.setStyle(Paint.Style.FILL);
        paintWalls.setStyle(Paint.Style.STROKE);
        Path wallPath = new Path();
        wallPath.reset();
        wallPath.moveTo(walls.get(0).beginX,walls.get(0).beginY);
        wallPath.lineTo(walls.get(0).endX,walls.get(0).endY);
        for(int i = 1; i < walls.size(); i++){
            wallPath.lineTo(walls.get(i).beginX,walls.get(i).beginY);
            wallPath.lineTo(walls.get(i).endX,walls.get(i).endY);
        }
        canvas.drawPath(wallPath,paintArea);
        canvas.drawPath(wallPath,paintWalls);
    }
    private void DrawMap(){
        ArrayList<Room> rooms = floor.Rooms;
        for(int i = 0; i < rooms.size(); i++){
            DrawRoom(rooms.get(i).getWalls(), "#000000", "#D8D8D8");
            ArrayList<Seat> seats = rooms.get(i).getSeats();
            for(int j = 0; j < seats.size(); j++){
                DrawSeat(seats.get(j).getX(),seats.get(j).getY(),10,10, true, "#000000");
            }
        }
    }
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) getApplicationContext().getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }
}
