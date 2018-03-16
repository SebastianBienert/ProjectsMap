package project.projectsmap;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

public class SearchDeveloperAfterTechnology extends AppCompatActivity {

    Button clickSerach;
    Button clickBack;
    TextView technologyName;
    public static TextView data;
    ListView listDevelopers;
    public static customAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search_developer_after_technology);

        clickSerach = (Button) findViewById(R.id.buttonSearch);
        clickBack = (Button) findViewById(R.id.buttonBack);
        technologyName = (TextView) findViewById(R.id.editTextTechnologyName);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);

        adapter = new customAdapter(this);

        listDevelopers.setAdapter(adapter);

        clickSerach.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                fetchDataDeveloperAfterSearchingThroughTechnologies process = new fetchDataDeveloperAfterSearchingThroughTechnologies();
                process.setTechnologyName(technologyName.getText().toString());
                process.execute();
            }
        });
        clickBack.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                SearchDeveloperAfterTechnology.super.finish();
            }
        });
    }
}
