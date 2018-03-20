package project.projectsmap;

import android.app.Application;
import android.content.Context;
import android.os.AsyncTask;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by mateusz on 14.03.2018.
 */

public class FetchDataAboutDeveloper extends AsyncTask<Void,Void,Void> {
    TextView statement;
    String data ="";
    String choice = "";
    String inputData = "";
    String errorText = "";
    ArrayList<Developer> dataList = new ArrayList<Developer>();

    /*      dodane do zapisu do pliku       */
    boolean saveDataToFile = false;
    Context context;
    String fileName;
    boolean append;
    /*--------------------------------------*/

    public void setStatement(TextView statement) {
        this.statement = statement;
    }
    public void setChoice(String name){
        choice = name;
    }
    public void setInputData(String name){
        inputData = name;
    }

    /*      dodane do zapisu do pliku       */
    public void setSaveDataToFile(boolean choice){
        saveDataToFile = choice;
    }
    public void setcontext(Context con){
        context = con;
    }
    public void setFileName(String name){
        fileName = name;
    }
    public void setAppend(boolean ap){append = ap;
    }
    /*--------------------------------------*/
    @Override
    protected Void doInBackground(Void... voids) {
        if (inputData.isEmpty() && !choice.equals("Wszystko")) {
            errorText = "Wprowadź dane";
        } else {
            try {
                URL url = setURLAdress();
                if (url == null) {
                    return null;
                }
                HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
                InputStream inputStream = httpsURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                String line = "";
                while (line != null) {
                    line = bufferedReader.readLine();
                    data = data + line;
                }

                Object json = new JSONTokener(data).nextValue();
                if (json instanceof JSONObject) {
                    dataList.add(new Developer(new JSONObject(data)));
                } else if (json instanceof JSONArray) {
                    JSONArray JA = new JSONArray(data);
                    for (int i = 0; i < JA.length(); i++) {
                        dataList.add(new Developer((JSONObject) JA.get(i)));
                    }
                }
            } catch (MalformedURLException e) {
                errorText = "MalformedURLException";
                e.printStackTrace();
            } catch (IOException e) {
                errorText = "IOException";
                e.printStackTrace();
            } catch (JSONException e) {
                errorText = "JSONException";
                e.printStackTrace();
            }
        }
        return null;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        if(statement!=null){
            statement.setText(errorText);
        }
        if(saveDataToFile){ //dodane do zapisu do pliku
            /*String data = "";
            for(int i=0; i<dataList.size();i++) {
                data+=dataList.get(i).description();
            }*/
            SaveToFileActivity.saveDataToFile(context,fileName,this.data,append);
            SaveToFileActivity.DisableProgressBar();
        }else{
            for(int i=0; i<dataList.size();i++) {
                SearchDevelopersActivity.adapter.list.add(new SingleRow(dataList.get(i).description()));
            }
            SearchDevelopersActivity.adapter.notifyDataSetChanged();
            SearchDevelopersActivity.DisableProgressBar();
        }
    }
    private URL setURLAdress(){
        try{
            if(choice.equals("Technologia")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/technology/"+inputData);
                //return new URL("http://localhost:58923/api/developers/technology/"+inputData);
            }else if(choice.equals("Id")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/"+inputData);
                //return new URL("http://localhost:58923/api/developers/"+inputData);
            }else if(choice.equals("Wszystko")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers");
                //return new URL("http://localhost:58923/api/developers");
            }else if(choice.equals("Nazwisko")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/"+inputData);
                //return new URL("http://localhost:58923/api/developers/"+inputData);
            }
        }catch(MalformedURLException e){
            e.printStackTrace();
        }
        return null;
    }

}