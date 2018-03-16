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
 * Created by mateusz on 14.03.2018.
 */

public class fetchDataDeveloperAfterSearchingThroughTechnologies extends AsyncTask<Void,Void,Void> {
    String data ="";
    String singleParsed = "";
    String technologyName = "";
    ArrayList<String> dataList = new ArrayList<String>();

    @Override
    protected Void doInBackground(Void... voids) {

        try {
            URL url;
            if(technologyName.isEmpty()){
                url = new URL("https://projectsmapwebapi.azurewebsites.net/api/developers");
            }else{
                url = new URL("https://projectsmapwebapi.azurewebsites.net/api/developers/technology/"+technologyName);
            }
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
    public void setTechnologyName(String name){
        technologyName = name;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

        //MainActivity.data.setText(this.dataParsed);
        for(int i=0; i<dataList.size();i++) {
            SearchDeveloperAfterTechnology.adapter.list.add(new singleRow(dataList.get(i)));
        }
        SearchDeveloperAfterTechnology.adapter.notifyDataSetChanged();
    }
}