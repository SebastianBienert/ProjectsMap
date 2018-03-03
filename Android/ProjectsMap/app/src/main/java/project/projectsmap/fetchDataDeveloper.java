package project.projectsmap;

import android.os.AsyncTask;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 2018-02-28.
 */

public class fetchDataDeveloper extends AsyncTask<Void,Void,Void> {
    String data ="";
    String singleParsed = "";
    String numberId;
    @Override
    protected Void doInBackground(Void... voids) {

        try {
            URL url = new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/" + numberId);
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
            }

            JSONObject developer = new JSONObject(data);
                singleParsed = "Id:" + developer.get("Id") + "\n"+
                        "FirstName:" + developer.get("FirstName") + "\n"+
                        "Surname:" + developer.get("Surname") + "\n"+
                        "Technologies:" + developer.get("Technologies") + "\n"+
                        "Seat:" + developer.get("Seat") + "\n";

        } catch (MalformedURLException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }
    public void setNumberId(String number){
        numberId = number;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

        SerachDeveloper.data.setText(this.singleParsed);
    }
}