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
    //String webApiURL = "https://19484bc4.ngrok.io";
    //String webApiURL = "http://projectsmapwebapi.azurewebsites.net";
    //Building building;
    ArrayList<Building> buildingsList = new ArrayList<Building>();
    Context context;

    public void setContext(Context context) {
        this.context = context;
    }

    @Override
    protected Void doInBackground(Void... voids) {

        try {
            //URL url = new URL(GlobalVariable.webApiURL+"/api/company/" + numberId + "/buildings");
            URL url = new URL(GlobalVariable.webApiURL+"/api/buildings");
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            httpsURLConnection.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            httpsURLConnection.addRequestProperty("Authorization", "Bearer "+"eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImIzMmJhNDdlLWYxMjUtNDYxMC04MDBmLWYzNWRjZGYwNWY1YyIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJTdXBlclBvd2VyVXNlciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vYWNjZXNzY29udHJvbHNlcnZpY2UvMjAxMC8wNy9jbGFpbXMvaWRlbnRpdHlwcm92aWRlciI6IkFTUC5ORVQgSWRlbnRpdHkiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6Ijc3OThjMTdkLWRiMjctNDlkNC1iZmRhLWRmMTI2OWIxMzE3NSIsImNhbldyaXRlUHJvamVjdHMiOiJ0cnVlIiwiY2FuUmVhZFVzZXJzIjoidHJ1ZSIsIm5iZiI6MTUyMzk3MDIxNywiZXhwIjoxNTI0MDU2NjE3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjU4OTIzIiwiYXVkIjoiMDk5MTUzYzI2MjUxNDliYzhlY2IzZTg1ZTAzZjAwMjIifQ.lT2NPI7YpDNRRebBepSSjmS2SRMmndJCDEMg403H9oY");
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
