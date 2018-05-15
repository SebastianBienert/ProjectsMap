package project.projectsmap;

import android.content.Context;
import android.os.Environment;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.RadioButton;
import android.widget.TextView;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.nio.file.Path;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class SaveToFileActivity extends AppCompatActivity {
    Button saveData, loadData;
    TextView dataFromFile;
    TextView dataToFile;
    RadioButton buttonYes, buttonNo;
    Button clickSaveToFile;
    Boolean append = true;
    ProgressBar waitForSaveData;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            setContentView(R.layout.activity_save_to_file);
            final String token = getIntent().getExtras().getString("token");
            loadData  = (Button) findViewById(R.id.buttonLoadData);
            saveData = (Button) findViewById(R.id.buttonSaveData);
            dataFromFile = findViewById(R.id.textViewTextFromFile);
            dataToFile = findViewById(R.id.editTextToSave);
            buttonYes = findViewById(R.id.radioAppendTrue);
            buttonNo = findViewById(R.id.radioAppendFalse);
            buttonYes.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    append = true;
                }
            });
            buttonNo.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    append = false;
                }
            });
            waitForSaveData = (ProgressBar) findViewById(R.id.progressBarProgressSave);
            waitForSaveData.setVisibility(View.INVISIBLE);
            clickSaveToFile = findViewById(R.id.buttonSaveToFile);
            saveData.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    saveDataToFile(getApplicationContext(), "plikTestowy", dataToFile.getText().toString(), append);
                }
            });
            loadData.setOnClickListener(new View.OnClickListener() {
                public void onClick(View view) {
                    dataFromFile.setText(loadDataFromFile(getApplicationContext(), "plikTestowy"));
                }
            });
            clickSaveToFile.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {    //zapis wszystkich pracowników do pliku plikTestowy.txt
                    waitForSaveData.setVisibility(View.VISIBLE);
                    FetchDataAboutDeveloper process = new FetchDataAboutDeveloper();
                    process.setToken(token);
                    process.setSaveDataToFile(true);
                    process.setToSavecontext(getApplicationContext());
                    process.setcontext(SaveToFileActivity.this);
                    process.setFileName("plikTestowy");
                    process.setChoice("Wszyscy");
                    process.setAppend(append);
                    //process.setInputData("1");
                    process.setTextViewStatement(null);
                    process.execute();
                }
            });
    }
    static public boolean saveDataToFile(Context context, String fileName, String text, boolean append){
        try {
            File path = Environment.getExternalStorageDirectory();
            File[] files = ContextCompat.getExternalFilesDirs(context, null);
            File file = new File(files[0], "/" + fileName+".json");
            FileOutputStream fos = new FileOutputStream(file, append);//context.openFileOutput(context.getFilesDir().getAbsolutePath() + "/" + fileName +".txt",Context.MODE_PRIVATE);
            //FileOutputStream fos = openFileOutput(files[1] + "/" + fileName+".txt", MODE_APPEND);
            Writer out = new OutputStreamWriter(fos);
            //out.append(files[1] + "/" + fileName+".txt");
            //out.append("\n\r");
            //out.write(text);
            out.append(text);
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
    static public String loadDataFromFile(Context context, String fileName){
        try {
            File path = Environment.getExternalStorageDirectory();
            File[] files = ContextCompat.getExternalFilesDirs(context, null);
            File file = new File(files[0], "/" + fileName+".json");
            //FileInputStream fis = context.openFileInput(context.getFilesDir().getAbsolutePath() + "/" + fileName + ".txt");
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
        waitForSaveData.setVisibility(View.INVISIBLE);
    }
}
