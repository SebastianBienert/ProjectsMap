package project.projectsmap;

import android.content.Context;
import android.os.AsyncTask;

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
 * Created by lenovo on 31.05.2018.
 */

public class FetchDataSynchronization extends AsyncTask<Void,Void,Void> {
    String token = "";
    Context context;
    String dataEmployees = "", dataProjects = "", dataMap = "", dataBuildings = "";
    boolean result;
    public void setToken(String token_){ token = token_; }
    public void setContext(Context context) { this.context = context; }

    @Override
    protected Void doInBackground(Void... voids) {
        try {
            URL urlEmployees, urlProjects, urlMap, urlBuildings;
            urlEmployees = new URL(GlobalVariable.webApiURL+"/api/developers/");
            urlProjects = new URL(GlobalVariable.webApiURL+"/api/project/");
            urlMap = new URL(GlobalVariable.webApiURL+"/api/floor/allInformation"); // tego geta jeszcze nie ma
            urlBuildings = new URL(GlobalVariable.webApiURL+"/api/buildings");

            result = loadData(urlEmployees, "employees");
            if(result){
                result = loadData(urlProjects, "projects");
            }
            if(result){
                result = loadData(urlMap, "map");
            }
            if(result){
                result = loadData(urlBuildings, "buildings");
            }
        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    private boolean loadData(URL url, String choice) throws IOException {
        try{
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            httpsURLConnection.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            httpsURLConnection.addRequestProperty("Authorization", "Bearer "+token);
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            switch(choice){
                case "employees":
                    while(line != null) {
                        line = bufferedReader.readLine();
                        dataEmployees = dataEmployees + line;
                    }
                    break;
                case "projects":
                    while(line != null) {
                        line = bufferedReader.readLine();
                        dataProjects = dataProjects + line;
                    }
                    break;
                case "map":
                    while(line != null) {
                        line = bufferedReader.readLine();
                        dataMap = dataMap + line;
                    }
                    break;
                case "buildings":
                    while(line != null) {
                        line = bufferedReader.readLine();
                        dataBuildings = dataBuildings + line;
                    }
                    break;
            }
            return true;
        }catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return false;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        if(result){
            ((Synchronization)context).SaveActualDate();
            ((Synchronization)context).SaveData(dataEmployees, "employees");
            ((Synchronization)context).SaveData(dataProjects, "projects");
            ((Synchronization)context).SaveData(dataMap, "map");
            ((Synchronization)context).SaveData(dataBuildings, "buildings");
        }else{
            ((Synchronization)context).ErrorMessage();
        }
        ((Synchronization)context).DisableProgressBar();
        ((Synchronization)context).EnableSynchronizationButton(true);
    }
}
