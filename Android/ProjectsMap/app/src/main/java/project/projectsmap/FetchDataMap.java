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
 * Created by Mateusz on 26.03.2018.
 */

public class FetchDataMap extends AsyncTask<Void,Void,Void> {
    String data ="";
    String numberId;
    String numberCompanyId;
    //Building building;
    ArrayList<Building> buildingsList = new ArrayList<Building>();
    ArrayList<Floor> floorsList = new ArrayList<Floor>();
    Context context;

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
            URL url = new URL("https://projectsmapwebapi.azurewebsites.net/api/company/" + numberId + "/buildings");
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
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

            for(int i = 0; i < buildingsList.size(); i++){
                for(int j = 0; j < buildingsList.get(i).Floors.size(); j++){
                    data = "";
                    URL urlFloor = new URL("https://projectsmapwebapi.azurewebsites.net/api/floor/" + buildingsList.get(i).Floors.get(j));
                    HttpsURLConnection httpsURLConnectionFloor = (HttpsURLConnection) urlFloor.openConnection();
                    InputStream inputStreamFloor = httpsURLConnectionFloor.getInputStream();
                    BufferedReader bufferedReaderFloor = new BufferedReader(new InputStreamReader(inputStreamFloor));
                    String lineFloor = "";
                    while(lineFloor != null) {
                        lineFloor = bufferedReaderFloor.readLine();
                        data = data + lineFloor;
                    }
                    Object jsonFloor = new JSONTokener(data).nextValue();
                    if (jsonFloor instanceof JSONObject) {
                        floorsList.add(new Floor(new JSONObject(data)));
                    } else if (jsonFloor instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int n = 0; n < JA.length(); n++) {
                            floorsList.add(new Floor((JSONObject) JA.get(n)));
                        }
                    }
                }
            }

        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
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

}
