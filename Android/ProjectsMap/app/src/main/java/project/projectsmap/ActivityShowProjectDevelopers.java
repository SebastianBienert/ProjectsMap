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
    DeveloperAdapter adapter;
    ArrayList<Developer> arrayDevelopers = new ArrayList<Developer>();
    ProgressBar waitForData;
    String ProjectId;
    boolean isOnline;

    public void setArrayDevelopers(ArrayList<Developer> arrayDevelopers) {
        this.arrayDevelopers = arrayDevelopers;
        ShowEmployees();
        DisableProgressBar();
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_show_project_developers);

        isOnline = getIntent().getExtras().getBoolean("isOnline");

        adapter = new DeveloperAdapter(this, isOnline);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        listDevelopers.setAdapter(adapter);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        waitForData = (ProgressBar) findViewById(R.id.progressBarLoadData);
        waitForData.setVisibility(View.INVISIBLE);
        ProjectId = getIntent().getExtras().getString("Id", null);
        if(ProjectId!=null){
            LoadEmployees();
        }
        adapter.notifyDataSetChanged();
    }
    private void LoadEmployees(){
        waitForData.setVisibility(View.VISIBLE);
        FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
        process.setToken(GlobalVariable.token);
        process.setSaveDataToFile(false);
        process.setInfoAboutConnectToInternet(isOnline);
        process.setChoice("Project");
        process.setInputData(ProjectId);
        process.setcontext(ActivityShowProjectDevelopers.this);
        process.execute();
    }
    private void ShowEmployees(){
        for(int i = 0; i < arrayDevelopers.size(); i++){
            adapter.DevelopersList.add(arrayDevelopers.get(i));  // potem zmienić na długi i krótki opis
        }
        adapter.notifyDataSetChanged();
    }
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }
}
