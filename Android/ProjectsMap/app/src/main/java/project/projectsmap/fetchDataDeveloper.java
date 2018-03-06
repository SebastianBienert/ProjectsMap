package project.projectsmap;

import android.os.AsyncTask;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 2018-02-28.
 */

public class fetchDataDeveloper extends AsyncTask<Void,Void,Void> {
    String data ="";
    String numberId;
    Developer developer;
    @Override
    protected Void doInBackground(Void... voids) {

        try {
            URL url = new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/" + numberId);
            //URL url = new URL("http://localhost:58923/api/developers/" + numberId);
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
            }

            JSONObject jsonObject = new JSONObject(data);
            developer = new Developer(jsonObject);
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

        SerachDeveloper.data.setText(developer.developerDescription());
    }
}