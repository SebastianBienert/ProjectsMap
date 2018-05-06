package project.projectsmap;

import android.graphics.Color;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.text.InputType;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.miguelcatalan.materialsearchview.MaterialSearchView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 14.03.2018.
 */

public class SearchDevelopersActivity extends AppCompatActivity {
    Toolbar toolbar;
    MaterialSearchView searchView;

    Spinner spinner;
    Button clickSerach;
    TextView inputDataField;
    TextView statement;
    TextView data;
    ListView listDevelopers;
    CustomAdapter adapter;
    ArrayAdapter<CharSequence> arrayAdapter;
    String choice="";
    ProgressBar waitForData;
    ArrayList<Developer> arrayDevelopers = new ArrayList<Developer>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_search_developers);

        toolbar = (Toolbar) findViewById(R.id.toolbar);
        listDevelopers = (ListView) findViewById(R.id.listDevelopers);
        final String token = getIntent().getExtras().getString("token");

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
                adapter.list.clear();
                adapter = new CustomAdapter(SearchDevelopersActivity.this);
                listDevelopers.setAdapter(adapter);

            }
        });

        searchView.setOnQueryTextListener(new MaterialSearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                if (newText != null && !newText.isEmpty()) {
                    waitForData.setVisibility(View.VISIBLE);
                    adapter.list.clear();
                    FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
                    process.setToken(token);
                    process.setSaveDataToFile(false);
                    process.setChoice(choice);
                    process.setInputData(newText);
                    process.setcontext(SearchDevelopersActivity.this);
                    process.setTextViewStatement(statement);
                    process.execute();
                }
                return true;
            }
        });




        // clickSerach = (Button) findViewById(R.id.buttonSearch);
            //inputDataField = (TextView) findViewById(R.id.editTextInputData);
            statement = (TextView) findViewById(R.id.textViewStatement);
            listDevelopers = (ListView) findViewById(R.id.listDevelopers);
            spinner = (Spinner) findViewById(R.id.spinnerSelectionMethod);
            arrayAdapter = ArrayAdapter.createFromResource(this, R.array.selected_method_developer, android.R.layout.simple_spinner_item);
            arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinner.setAdapter(arrayAdapter);
            waitForData = (ProgressBar) findViewById(R.id.progressBarWaitForData);
            waitForData.setVisibility(View.INVISIBLE);
            spinner.setAdapter(arrayAdapter);
            spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int position, long l) {
                    //Toast.makeText(getBaseContext(), adapterView.getItemAtPosition(position) + " selected", Toast.LENGTH_LONG);
                    choice = (String) adapterView.getItemAtPosition(position);
                    //setInputDataField();
                }

                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {

                }
            });
            adapter = new CustomAdapter(this);
            listDevelopers.setAdapter(adapter);

 /*           clickSerach.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    waitForData.setVisibility(View.VISIBLE);
                    adapter.list.clear();
                    FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
                    process.setSaveDataToFile(false);
                    process.setChoice(choice);
                    process.setInputData(inputDataField.getText().toString());
                    process.setcontext(SearchDevelopersActivity.this);
                    process.setTextViewStatement(statement);
                    process.execute();
                }
            });*/
    }

/*    private void setInputDataField(){
        inputDataField.setText("");
        if(choice.equals("Id")){
            inputDataField.setInputType(InputType.TYPE_CLASS_NUMBER);
        }else{
            inputDataField.setInputType(InputType.TYPE_CLASS_TEXT);
        }
        if(choice.equals("Wszyscy")){
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
    }*/
    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void notifyDataSetChanged() {
        adapter.notifyDataSetChanged();
    }

    public void addDeveloper(Developer developer) {
        adapter.list.add(developer.description());
        arrayDevelopers.add(developer);
    }

    public boolean onCreateOptionsMenu(Menu menu){
        getMenuInflater().inflate(R.menu.menu_item,menu);
        MenuItem item = menu.findItem(R.id.action_search);
        searchView.setMenuItem(item);
        return true;
    }

}
