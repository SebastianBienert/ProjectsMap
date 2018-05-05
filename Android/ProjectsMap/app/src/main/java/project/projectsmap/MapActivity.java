package project.projectsmap;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Path;
import android.graphics.drawable.BitmapDrawable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
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
    Floor floor;
    Seat placeDeveloper;

    boolean choice;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_map);
        final String DeveloperId = getIntent().getExtras().getString("Id");
        Toast.makeText(getBaseContext(),DeveloperId, Toast.LENGTH_SHORT).show();

        choice = getIntent().getBooleanExtra("choice",false);
        floor = (Floor) getIntent().getSerializableExtra("fl");
        placeDeveloper = (Seat) getIntent().getSerializableExtra("st");

        bg = Bitmap.createBitmap(800,800,Bitmap.Config.ARGB_8888);
        canvas = new Canvas(bg);
        ll = (LinearLayout) findViewById(R.id.rect);
        ll.setBackground(new BitmapDrawable(bg));
        DrawMap();
    }

    private void DrawMap(){
        if(choice){
            ArrayList<Room> rooms = floor.Rooms;
            for(int i = 0; i < rooms.size(); i++){
                DrawRoom(rooms.get(i).getWalls(), "#000000", "#D8D8D8");
                ArrayList<Seat> seats = rooms.get(i).getSeats();
                for(int j = 0; j < seats.size(); j++){
                    DrawSeat(seats.get(j).getX(),seats.get(j).getY(),10,10, true, "#000000");
                }
            }
            if(placeDeveloper!=null){
                DrawSeat(placeDeveloper.getX(),placeDeveloper.getY(),10,10, true, "#FF4081");
            }
        }else{
            DrawSeat(50,50,200,200, false, "#000000");
            DrawSeat(250,50,200,200, false, "#000000");
            DrawSeat(100,100,20,20, true, "#000000");
            DrawSeat(150,100,20,20, true, "#000000");
            ArrayList<Wall> walls = new ArrayList<Wall>(Arrays.asList(new Wall(50,250,50,450),new Wall(50,450,450,450),new Wall(450,450,450,350),
                    new Wall(450,350,250,350), new Wall(250,350,250,250), new Wall(250,250,50,250)));
            DrawRoom(walls, "#000000", "#D8D8D8");
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
