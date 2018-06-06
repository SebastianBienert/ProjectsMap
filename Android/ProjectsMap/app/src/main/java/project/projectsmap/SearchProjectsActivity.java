package project.projectsmap;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.support.v7.widget.Toolbar;

import com.miguelcatalan.materialsearchview.MaterialSearchView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class SearchProjectsActivity extends AppCompatActivity {
    Toolbar toolbar;
    MaterialSearchView searchView;
    Spinner spinner;
    TextView statement;
    ListView listProjects;
    ProjectAdapter adapter;
    ArrayAdapter<CharSequence> arrayAdapter;
    String choice="";
    ProgressBar waitForData;
    ArrayList<Project> arrayProjects = new ArrayList<Project>();
    Boolean isOnline;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search_projects);

        final String token = getIntent().getExtras().getString("token");
        isOnline = getIntent().getExtras().getBoolean("isOnline");
        //context = (MainActivity)getIntent().getExtras().get("context");

        statement = (TextView) findViewById(R.id.textViewStatement);
        listProjects = (ListView) findViewById(R.id.listProjects);
        spinner = (Spinner) findViewById(R.id.spinnerSelectionMethod);
        arrayAdapter = ArrayAdapter.createFromResource(this, R.array.selected_method_project, android.R.layout.simple_spinner_item);
        arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        waitForData = (ProgressBar) findViewById(R.id.progressBarWaitForData);
        waitForData.setVisibility(View.INVISIBLE);
        spinner.setAdapter(arrayAdapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int position, long l) {
                //Toast.makeText(getBaseContext(), adapterView.getItemAtPosition(position) + " selected", Toast.LENGTH_LONG);
                choice = (String) adapterView.getItemAtPosition(position);
                waitForData.setVisibility(View.VISIBLE);
                adapter.ProjectsList.clear();
                FetchDataAboutProject process = new FetchDataAboutProject();
                process.setToken(token);
                process.setInfoAboutConnectToInternet(isOnline);
                process.setSaveDataToFile(false);
                process.setChoice(choice);
                process.setcontext(SearchProjectsActivity.this);
                process.setTextViewStatement(statement);
                process.execute();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        adapter = new ProjectAdapter(this, isOnline);
        listProjects.setAdapter(adapter);

        toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setTitle("Szukaj:");
        toolbar.setTitleTextColor(Color.parseColor("#FFFFFF"));
        searchView = (MaterialSearchView) findViewById(R.id.search_view);

        searchView.setOnSearchViewListener(new MaterialSearchView.SearchViewListener() {
            @Override
            public void onSearchViewShown() {

            }

            @Override
            public void onSearchViewClosed() {
                //listDevelopers = (ListView) findViewById(R.id.listDevelopers);
                adapter.ProjectsList.clear();
                adapter = new ProjectAdapter(SearchProjectsActivity.this, isOnline);
                listProjects.setAdapter(adapter);

            }
        });

        searchView.setOnQueryTextListener(new MaterialSearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                if (newText != null && !newText.isEmpty() && choice!="Wszyscy") {
                    waitForData.setVisibility(View.VISIBLE);
                    adapter.ProjectsList.clear();
                    FetchDataAboutProject process = new FetchDataAboutProject();
                    process.setToken(token);
                    process.setInfoAboutConnectToInternet(isOnline);
                    process.setSaveDataToFile(false);
                    process.setChoice(choice);
                    process.setInputData(newText);
                    process.setcontext(SearchProjectsActivity.this);
                    process.setTextViewStatement(statement);
                    process.execute();
                }
                return true;
            }
        });
    }

    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void addProject(Project project) {
        arrayProjects.add(project);
        adapter.ProjectsList.add(project);
    }

    public void notifyDataSetChanged() {
        adapter.notifyDataSetChanged();
    }

    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.menu_item,menu);
        MenuItem item = menu.findItem(R.id.action_search);
        searchView.setMenuItem(item);
        return true;
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
}
