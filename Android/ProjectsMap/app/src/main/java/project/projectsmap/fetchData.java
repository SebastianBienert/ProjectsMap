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
 * Created by Majkel on 2018-02-28.
 */

public class fetchData extends AsyncTask<Void,Void,Void> {
    String data ="";
    String singleParsed = "";
    ArrayList<String> dataList = new ArrayList<String>();

    @Override
    protected Void doInBackground(Void... voids) {

        try {
            URL url = new URL("https://projectsmapwebapi.azurewebsites.net/api/developers");
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
            }

            JSONArray JA = new JSONArray(data);
            for(int i=0;i<JA.length(); i++){
                JSONObject JO = (JSONObject) JA.get(i);
                singleParsed = "Id:" + JO.get("Id") + "\n"+
                        "FirstName:" + JO.get("FirstName") + "\n"+
                        "Surname:" + JO.get("Surname") + "\n"+
                        "Technologies:" + JO.get("Technologies") + "\n"+
                        "Seat:" + JO.get("Seat") + "\n";
                dataList.add(singleParsed);
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

        //MainActivity.data.setText(this.dataParsed);
        for(int i=0; i<dataList.size();i++) {
            MainActivity.custom.list.add(new singleRow(dataList.get(i)));
        }
        MainActivity.custom.notifyDataSetChanged();
    }
}
