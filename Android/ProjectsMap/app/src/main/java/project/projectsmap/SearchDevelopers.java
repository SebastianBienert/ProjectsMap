package project.projectsmap;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class SearchDevelopers extends AppCompatActivity {

    Spinner spinner;
    Button clickSerach;
    Button clickBack;
    TextView inputDataField;
    TextView statement;
    public static TextView data;
    ListView listDevelopers;
    public static customAdapter adapter;
    ArrayAdapter<CharSequence> arrayAdapter;
    String choice="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_search_developers);

            clickSerach = (Button) findViewById(R.id.buttonSearch);
            clickBack = (Button) findViewById(R.id.buttonBack);
            inputDataField = (TextView) findViewById(R.id.editTextInputData);
            statement = (TextView) findViewById(R.id.textViewStatement);
            listDevelopers = (ListView) findViewById(R.id.listDevelopers);
            spinner = (Spinner) findViewById(R.id.spinnerSelectionMethod);
            arrayAdapter = ArrayAdapter.createFromResource(this, R.array.selected_method, android.R.layout.simple_spinner_item);
            arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinner.setAdapter(arrayAdapter);
            spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int position, long l) {
                    Toast.makeText(getBaseContext(), adapterView.getItemAtPosition(position) + " selected", Toast.LENGTH_LONG);
                    choice = (String) adapterView.getItemAtPosition(position);
                    inputDataField.setHint("Podaj " + choice);
                }

                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {

                }
            });
            adapter = new customAdapter(this);

            clickSerach.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    listDevelopers.setAdapter(adapter);
                    FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
                    process.setChoice(choice);
                    process.setInputData(inputDataField.getText().toString());
                    process.setStatement(statement);
                    process.execute();
                }
            });
            clickBack.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    SearchDevelopers.super.finish();
                }
            });
    }
}
