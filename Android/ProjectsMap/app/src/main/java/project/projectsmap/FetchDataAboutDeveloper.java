package project.projectsmap;

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

    @Override
    protected Void doInBackground(Void... voids) {
        if(inputData.isEmpty()){
            errorText = "Wprowadź dane";
        }else{
            try {
                URL url = setURLAdress();
                HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
                InputStream inputStream = httpsURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                String line = "";
                while(line != null) {
                    line = bufferedReader.readLine();
                    data = data + line;
                }

                Object json = new JSONTokener(data).nextValue();
                if( json instanceof JSONObject){
                    dataList.add(new Developer(new JSONObject(data)));
                }else if(json instanceof JSONArray){
                    JSONArray JA = new JSONArray(data);
                    for(int i=0;i<JA.length(); i++){
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
    private URL setURLAdress(){
        try{
            if(choice.equals("Technology")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/technology/"+inputData);
            }else if(choice.equals("Id")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/"+inputData);
            }else if(choice.equals("All")){
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers");
            }else{
                return new URL("https://projectsmapwebapi.azurewebsites.net/api/developers");
            }
        }catch(MalformedURLException e){
            e.printStackTrace();
            return null;
        }
    }
    public void setChoice(String name){
        choice = name;
    }
    public void setInputData(String name){
        inputData = name;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        statement.setText(errorText);   // tutaj ustawiany tekst bo w metodzie doInBackground rzuca błędem
        for(int i=0; i<dataList.size();i++) {
            SearchDevelopers.adapter.list.add(new singleRow(dataList.get(i).developerDescription()));
        }
        SearchDevelopers.adapter.notifyDataSetChanged();
    }
    public void setStatement(TextView statement) { this.statement = statement;}
}