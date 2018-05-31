package project.projectsmap;

import android.content.Context;
import android.os.Environment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.TimeZone;

public class Synchronization extends AppCompatActivity {

    Button buttonSynchronization;
    TextView dateLastSynchronization;
    String dateLastSyn="";
    ProgressBar waitForData;
    boolean isOnlineWork;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_synchronization);
        final String token = getIntent().getExtras().getString("token");
        isOnlineWork = getIntent().getExtras().getBoolean("work", false);
        waitForData = (ProgressBar) findViewById(R.id.progressBarSynchronization);
        waitForData.setVisibility(View.INVISIBLE);
        dateLastSynchronization = (TextView) findViewById(R.id.textViewDateLastSynchronization);
        buttonSynchronization = (Button) findViewById(R.id.buttonSynchronization);
        /*if(!isOnlineWork){
            buttonSynchronization.setEnabled(false);
        }*/
        dateLastSyn = loadDateFromFile();
        if(dateLastSyn.equals("")){
            dateLastSynchronization.setText("brak");
        }else{
            dateLastSynchronization.setText(dateLastSyn);
        }
        buttonSynchronization.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        if(isOnlineWork){
                            waitForData.setVisibility(View.VISIBLE);
                            EnableSynchronizationButton(false);
                            FetchDataSynchronization process = new FetchDataSynchronization();
                            process.setToken(token);
                            process.setContext(Synchronization.this);
                            process.execute();
                        }else{
                            Toast.makeText(Synchronization.this, "Musisz być zalogowany żeby móc synchronizować dane.", Toast.LENGTH_LONG).show();
                        }
                    }
                });
    }
    public void SaveActualDate(){
        saveDateToFile("LastSyn", actualTime());
        dateLastSyn = loadDateFromFile();
        dateLastSynchronization.setText(dateLastSyn);
        Toast.makeText(Synchronization.this, "Dane zsynchronizowane.", Toast.LENGTH_SHORT).show();
    }

    private boolean saveDateToFile(String fileName, String data){
        try {
            File path = Environment.getExternalStorageDirectory();
            File[] files = ContextCompat.getExternalFilesDirs(this, null);
            File file = new File(files[0], "/" + fileName + ".txt");
            FileOutputStream fos = new FileOutputStream(file, false);
            Writer out = new OutputStreamWriter(fos);
            out.write(data);
            out.close();
            return true;
        } catch (IOException e) {
            e.printStackTrace();
            return false;
        }catch (Exception e) {
            e.printStackTrace();
            return false;
        }
    }

    private String actualTime() {
        String date;
        Calendar c = Calendar.getInstance();
        c.setTimeZone(TimeZone.getTimeZone("CEST"));
        SimpleDateFormat dateformat = new SimpleDateFormat("HH:mm:ss  dd-MM-yyyy");
        date = dateformat.format(c.getTime());
        return date;
    }

    private String loadDateFromFile(){
        try {
            File path = Environment.getExternalStorageDirectory();
            File[] files = ContextCompat.getExternalFilesDirs(this, null);
            File file = new File(files[0], "/" + "LastSyn.txt");
            FileInputStream fis = new FileInputStream (file);
            BufferedReader r = new BufferedReader(new InputStreamReader(fis));
            String s = "";
            String txt = "";
            while ((s = r.readLine()) != null) {
                txt += s;
            }
            r.close();
            return txt;
        } catch (IOException e) {
            e.printStackTrace();
            return null;
        }catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    public void DisableProgressBar(){
        waitForData.setVisibility(View.INVISIBLE);
    }

    public void EnableSynchronizationButton(boolean choice){
        buttonSynchronization.setEnabled(choice);
    }

    public void ErrorMessage() {
        Toast.makeText(this, "Błąd podczas synchronizacji. Dane nie zostały pobrane.", Toast.LENGTH_SHORT).show();
    }

    public void SaveData(String data, String choice) {
        switch(choice){
            case "employees":
                saveDateToFile("employees", data); // pełna nazwa pliku to employees.txt
                break;
            case "projects":
                saveDateToFile("projects", data);
                break;
            case "map":
                saveDateToFile("map", data);
                break;
            case "buildings":
                saveDateToFile("buildings", data);
                break;
        }
    }
}
