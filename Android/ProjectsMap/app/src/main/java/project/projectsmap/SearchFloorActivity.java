package project.projectsmap;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.io.Serializable;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class SearchFloorActivity extends AppCompatActivity {

    Button clickSearch,showMap, clickBack;
    TextView floorNumber;
    TextView data;
    ProgressBar waitForSaveData;
    Floor floor;

    public void setFloor(Floor floor) {
        this.floor = floor;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_data_about_floor);

        final String token = getIntent().getExtras().getString("token");
        clickSearch = (Button) findViewById(R.id.buttonSearch);
        showMap = (Button) findViewById(R.id.buttonShowMap);
        clickBack = (Button) findViewById(R.id.buttonBack);
        floorNumber = (TextView) findViewById(R.id.editText);
        data = (TextView) findViewById(R.id.textViewInfoFloor);
        waitForSaveData = (ProgressBar) findViewById(R.id.progressBarWaitForData);
        waitForSaveData.setVisibility(View.INVISIBLE);
        clickBack.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                SearchFloorActivity.super.finish();
            }
        });
        clickSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                waitForSaveData.setVisibility(View.VISIBLE);
                FetchDataFloor process = new FetchDataFloor();
                process.setToken(token);
                process.setNumberId(floorNumber.getText().toString());
                process.setContext(SearchFloorActivity.this);
                process.execute();
            }
        });
        showMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(floor!=null){
                    Intent intent = new Intent(SearchFloorActivity.this, MapActivity.class);
                    intent.putExtra("choice", true);
                    intent.putExtra("fl", floor);
                    if(floor.getRooms().size()> 1){  // do testów wyświetlania miejsca siedzącego
                        intent.putExtra("st", floor.getRooms().get(1).getSeats().get(1));
                    }
                    //MapActivity.floor = floor;
                    startActivity(intent);
                }else{
                    data.setText("Najpierw pobierz piętro");
                }
            }
        });
    }
    public void DisableProgressBar(){
        waitForSaveData.setVisibility(View.INVISIBLE);
    }

    public void showDescription(String text) {
        data.setText(text);
    }
}
