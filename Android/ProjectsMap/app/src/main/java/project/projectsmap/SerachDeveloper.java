package project.projectsmap;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class SerachDeveloper extends AppCompatActivity {

    Button clickSerachDeveloper;
    Button clickBack;
    TextView numberIdDeveloper;
    static TextView data;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_serach_developer);

        clickSerachDeveloper = (Button) findViewById(R.id.buttonSearch);
        clickBack = (Button) findViewById(R.id.buttonBack);
        data = (TextView) findViewById(R.id.textViewInformationDeveloper);
        numberIdDeveloper = (TextView) findViewById(R.id.editText);
        clickSerachDeveloper.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                fetchDataDeveloper process = new fetchDataDeveloper();
                process.setNumberId(numberIdDeveloper.getText().toString());
                process.execute();
            }
        });
        clickBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                SerachDeveloper.super.finish();
                //Intent intent = new Intent(SerachDeveloper.this, MainActivity.class);
                //startActivity(intent);
            }
        });
    }

}
