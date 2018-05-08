package project.projectsmap;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.Toast;

import java.util.ArrayList;

/**
 * Created by Mateusz on 14.04.2018.
 */

public class ActivityShowProjectDevelopers extends AppCompatActivity {

    ListView listDevelopers;
    CustomAdapter adapter;
    ArrayList<Developer> arrayDevelopers = new ArrayList<Developer>();
    ProgressBar waitForData;
    String ProjectId;
    public void setArrayDevelopers(ArrayList<Developer> arrayDevelopers) {
        this.arrayDevelopers = arrayDevelopers;
        ShowEmployees();
        DisableProgressBar();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_project_developers);
        adapter = new CustomAdapter(this);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        listDevelopers.setAdapter(adapter);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        waitForData = (ProgressBar) findViewById(R.id.progressBarLoadData);
        waitForData.setVisibility(View.INVISIBLE);
        ProjectId = getIntent().getExtras().getString("Id", null);
        if(ProjectId!=null){
            Toast.makeText(getBaseContext(),"ID projektu: " + ProjectId, Toast.LENGTH_SHORT).show();
            LoadEmployees();
        }

        adapter.notifyDataSetChanged();
    }
    private void LoadEmployees(){
        waitForData.setVisibility(View.VISIBLE);
        FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
        process.setToken(GlobalVariable.token);
        process.setSaveDataToFile(false);
        process.setChoice("Project");
        process.setInputData(ProjectId);
        process.setcontext(ActivityShowProjectDevelopers.this);
        process.execute();
    }
    private void ShowEmployees(){
        for(int i = 0; i < arrayDevelopers.size(); i++){
            adapter.list.add(arrayDevelopers.get(i).description());  // potem zmienić na długi i krótki opis
        }
        adapter.notifyDataSetChanged();
    }
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }
}
