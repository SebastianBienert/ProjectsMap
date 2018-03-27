package project.projectsmap;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.InputType;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class SearchProjectsActivity extends AppCompatActivity {
    Spinner spinner;
    Button clickSerach;
    Button clickBack;
    TextView inputDataField;
    TextView statement;
    TextView data;
    ListView listProjects;
    ProjectAdapter adapter;
    ArrayAdapter<CharSequence> arrayAdapter;
    String choice="";
    ProgressBar waitForData;
    ArrayList<Project> arrayProjects = new ArrayList<Project>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search_projects);

        clickSerach = (Button) findViewById(R.id.buttonSearch);
        clickBack = (Button) findViewById(R.id.buttonBack);
        inputDataField = (TextView) findViewById(R.id.editTextInputData);
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
                setInputDataField();
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        adapter = new ProjectAdapter(this);
        listProjects.setAdapter(adapter);

        clickSerach.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                waitForData.setVisibility(View.VISIBLE);
                adapter.list.clear();
                FetchDataAboutProject process = new FetchDataAboutProject();
                process.setSaveDataToFile(false);
                process.setChoice(choice);
                process.setInputData(inputDataField.getText().toString());
                process.setcontext(SearchProjectsActivity.this);
                process.setTextViewStatement(statement);
                process.execute();
            }
        });
        clickBack.setOnClickListener(new View.OnClickListener() {
            public void onClick(View view) {
                SearchProjectsActivity.super.finish();
            }
        });
    }
    private void setInputDataField(){
        inputDataField.setText("");
        if(choice.equals("Id")){
            inputDataField.setInputType(InputType.TYPE_CLASS_NUMBER);
        }else{
            inputDataField.setInputType(InputType.TYPE_CLASS_TEXT);
        }
        if(choice.equals("Wszystkie")){
            inputDataField.setEnabled(false);
            inputDataField.setFocusable(false);
            inputDataField.setCursorVisible(false);
            inputDataField.setHint("");
        }else{
            inputDataField.setEnabled(true);
            inputDataField.setFocusableInTouchMode(true);
            inputDataField.setCursorVisible(true);
            inputDataField.setHint("Podaj " + choice);
        }
    }
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void addProject(Project project) {
        arrayProjects.add(project);
        adapter.list.add(project.description());
    }

    public void notifyDataSetChanged() {
        adapter.notifyDataSetChanged();
    }
}
