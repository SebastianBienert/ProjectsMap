package project.projectsmap;

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
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class MapActivity extends AppCompatActivity {
    Bitmap bg;
    Canvas canvas;
    LinearLayout ll;
    //static Floor floor;
    Floor floor = null;
    Seat placeDeveloper;
    Room roomDeveloper;
    TextView description;

    boolean choice;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);
        description = (TextView) findViewById(R.id.textViewDescription);
        final String DeveloperId = getIntent().getExtras().getString("Id", null);
        if(DeveloperId!=null){
            String[] informaions = DeveloperId.split(" ");
        }
        Toast.makeText(getBaseContext(),"ID pracownika: " + DeveloperId, Toast.LENGTH_SHORT).show();

        if(DeveloperId != null){
            FetchDataFloor process = new FetchDataFloor();
            process.setToken(GlobalVariable.token);
            process.setNumberEmployeeId(DeveloperId);
            process.setContext(MapActivity.this);
            process.execute();
        }

        choice = getIntent().getBooleanExtra("choice",false);
        if(floor == null){
            floor = (Floor) getIntent().getSerializableExtra("fl");
        }
        placeDeveloper = (Seat) getIntent().getSerializableExtra("st");
        SetCanvas();

        //DrawMap();
    }
    public void SetFloor(Floor newFloor){
        floor = newFloor;
        description.setText(floor.Description);
        choice = true;
    }
    public void RefreshMap(){
        canvas.drawColor(Color.TRANSPARENT, PorterDuff.Mode.CLEAR);
        DrawMap();
    }
    private void SetCanvas(){
        bg = Bitmap.createBitmap(800,800,Bitmap.Config.ARGB_8888);
        canvas = new Canvas(bg);
        ll = (LinearLayout) findViewById(R.id.rect);
        ll.setBackground(new BitmapDrawable(bg));
    }
    private void DrawMap(){
        if(choice){
            SetCanvas();
            ArrayList<Room> rooms = floor.Rooms;
            ArrayList<Seat> seats;
            for(int i = 0; i < rooms.size(); i++){
                if( i != 0){ // do testow wartosc 0
                    DrawRoom(rooms.get(i).getWalls(), "#000000", "#D8D8D8");
                    seats = rooms.get(i).getSeats();
                    for(int j = 0; j < seats.size(); j++){
                        DrawSeat(seats.get(j).getX(),seats.get(j).getY(),10,10, true, "#000000");
                    }
                }else{
                    seats = rooms.get(i).getSeats();
                    placeDeveloper = rooms.get(0).Seats.get(0);//do testow
                    roomDeveloper = rooms.get(0);
                    if(roomDeveloper!=null){
                        DrawRoom(roomDeveloper.getWalls(),"#000000", "#ccff99");
                    }
                    if(placeDeveloper!=null){
                        DrawSeat(placeDeveloper.getX(),placeDeveloper.getY(),10,10, true, "#ff3300");
                    }
                    for(int j = 1; j < seats.size(); j++){
                        DrawSeat(seats.get(j).getX(),seats.get(j).getY(),10,10, true, "#000000");
                    }
                }
            }
        }else{
            SetCanvas();
            ArrayList<Wall> walls = new ArrayList<Wall>(Arrays.asList(new Wall(50,250,50,450),new Wall(50,450,450,450),new Wall(450,450,450,350),
                    new Wall(450,350,250,350), new Wall(250,350,250,250), new Wall(250,250,50,250)));
            ArrayList<Wall> wallsSecondRoom = new ArrayList<Wall>(Arrays.asList(new Wall(50,50,50,250),new Wall(50,250,250,250),new Wall(250,250,250,50),
                    new Wall(250,50,50,50)));
            ArrayList<Wall> wallsThirdRoom = new ArrayList<Wall>(Arrays.asList(new Wall(250,50,250,250),new Wall(250,250,450,250),new Wall(450,250,450,50),
                    new Wall(450,50,250,50)));
            DrawRoom(walls, "#000000", "#D8D8D8");
            DrawRoom(wallsSecondRoom, "#000000", "#D8D8D8");
            DrawRoom(wallsThirdRoom, "#000000", "#D8D8D8");
            DrawSeat(100,100,20,20, true, "#000000");
            DrawSeat(180,100,20,20, true, "#000000");
            DrawSeat(300,100,20,20, true, "#000000");
            DrawSeat(380,100,20,20, true, "#000000");
            DrawSeat(100,300,20,20, true, "#000000");
            DrawSeat(180,300,20,20, true, "#FF4081");
            DrawSeat(300,380,20,20, true, "#000000");
            DrawSeat(380,380,20,20, true, "#000000");
        }
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
}
