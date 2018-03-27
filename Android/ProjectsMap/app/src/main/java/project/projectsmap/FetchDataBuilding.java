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
 * Created by Mateusz on 20.03.2018.
 */

public class FetchDataBuilding extends AsyncTask<Void,Void,Void> {
    String data ="";
    String numberId;
    //Building building;
    ArrayList<Building> buildingsList = new ArrayList<Building>();
    Context context;

    public void setContext(Context context) {
        this.context = context;
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

        if(buildingsList!=null){ // to tylko do testów
            ((ShowMapActivity)context).setDescription(buildingsList.get(0).description());
            //ShowMapActivity.buildingDescription.setText(buildingsList.get(0).description());
        }else{
            ((ShowMapActivity)context).setDescription("Brak budynku o tym numerze");
            //ShowMapActivity.buildingDescription.setText("Brak budynku o tym numerze");
        }
        ((ShowMapActivity)context).DisableProgressBar();
        //ShowMapActivity.DisableProgressBar();
    }
    public void setNumberId(String number){
        numberId = number;
    }
}
