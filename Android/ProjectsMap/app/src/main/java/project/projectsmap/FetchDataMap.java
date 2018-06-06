package project.projectsmap;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Environment;
import android.support.v4.content.ContextCompat;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 26.03.2018.
 */

public class FetchDataMap extends AsyncTask<Void,Void,Void> {
    String data ="", dataFloor ="";
    String numberId;
    String numberCompanyId;
    String token = "";
    //Building building;
    ArrayList<Building> buildingsList = new ArrayList<Building>();
    ArrayList<Floor> floorsList = new ArrayList<Floor>();
    Context context;
    Boolean isOnline;

    public void setToken(String token_){ token = token_; }
    public void setInfoAboutConnectToInternet(Boolean on){ isOnline = on; }
    public void setContext(Context context) {
        this.context = context;
    }
    public void setNumberId(String number){
        numberId = number;
    }
    public void setNumberCompanyId(String numberCompanyId) {
        this.numberCompanyId = numberCompanyId;
    }

    @Override
    protected Void doInBackground(Void... voids) {

        try {
            if(!isNetworkAvailable()){
                isOnline = false;
            }
            if(isOnline){
                data = loadDataAboutBuildingsFromServer();
                dataFloor = loadDataAboutFloorsFromServer();
            }else{
                data = loadDataFromFile(context, "buildings");
                dataFloor = loadDataFromFile(context, "map");
            }

            Object json = new JSONTokener(data).nextValue();
            if (json instanceof JSONObject) {
                buildingsList.add(new Building(new JSONObject(data)));
            } else if (json instanceof JSONArray) {
                JSONArray JA = new JSONArray(data);
                for (int i = 0; i < JA.length(); i++) {
                    buildingsList.add(new Building((JSONObject) JA.get(i)));
                }
            }

            Object jsonFloors = new JSONTokener(dataFloor).nextValue();
            if (jsonFloors instanceof JSONObject) {
                floorsList.add(new Floor(new JSONObject(dataFloor)));
            } else if (jsonFloors instanceof JSONArray) {
                JSONArray JA = new JSONArray(dataFloor);
                for (int i = 0; i < JA.length(); i++) {
                    floorsList.add(new Floor((JSONObject) JA.get(i)));
                }
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }

    private String loadDataAboutFloorsFromServer() {
        String returnData="";
        try{
            URL urlFloors = new URL(GlobalVariable.webApiURL+"/api/floor/allInformation");
            HttpsURLConnection httpsURLConnectionFloor = (HttpsURLConnection) urlFloors.openConnection();
            httpsURLConnectionFloor.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            httpsURLConnectionFloor.addRequestProperty("Authorization", "Bearer "+token);
            InputStream inputStreamFloor = httpsURLConnectionFloor.getInputStream();
            BufferedReader bufferedReaderFloor = new BufferedReader(new InputStreamReader(inputStreamFloor));
            String lineFloor = "";
            while(lineFloor != null) {
                lineFloor = bufferedReaderFloor.readLine();
                returnData = returnData + lineFloor;
            }
            return returnData;
        }catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    private String loadDataAboutBuildingsFromServer() {
        String returnData="";
        try{
            URL url = new URL(GlobalVariable.webApiURL+"/api/buildings");
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            httpsURLConnection.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            httpsURLConnection.addRequestProperty("Authorization", "Bearer "+token);
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                returnData = returnData + line;
            }
            return returnData;
        }catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

        if(buildingsList!=null&&floorsList!=null){ // to tylko do testów
            //ShowMapActivity.buildingDescription.setText(buildingsList.get(0).description());
            //((ShowMapActivity)context).setDescription(buildingsList.get(0).description());
            ((ShowMapActivity)context).setArrayBulindings(buildingsList);
            ((ShowMapActivity)context).setArrayFloors(floorsList);
        }else{
            //ShowMapActivity.buildingDescription.setText("Brak budynku o tym numerze");
            ((ShowMapActivity)context).setStatement("Brak budynków dla wybranej firmy");
        }
        ((ShowMapActivity)context).DisableProgressBar();
        //ShowMapActivity.DisableProgressBar();
    }
    private String loadDataFromFile(Context context, String fileName){
        try {
            File path = Environment.getExternalStorageDirectory();
            File[] files = ContextCompat.getExternalFilesDirs(context, null);
            File file = new File(files[0], "/" + fileName+".txt");
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
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }
}
