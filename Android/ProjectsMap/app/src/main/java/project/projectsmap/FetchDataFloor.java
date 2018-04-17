package project.projectsmap;

import android.content.Context;
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
 * Created by Mateusz on 19.03.2018.
 */

public class FetchDataFloor extends AsyncTask<Void,Void,Void> {
    String data ="";
    String numberId;
    String webApiURL = "https://67a04196.ngrok.io";
    Floor floor;
    Context context;
    String token = "";

    public void setContext(Context context) {
        this.context = context;
    }
    public void setToken(String token_){ token = token_; }

    @Override
    protected Void doInBackground(Void... voids) {

        try {
            URL url = new URL(webApiURL+"/api/floor/" + numberId);
            //URL url = new URL("http://localhost:58923/api/floor/" + numberId);
            HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
            httpsURLConnection.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
            httpsURLConnection.addRequestProperty("Authorization", "Bearer "+token);
            InputStream inputStream = httpsURLConnection.getInputStream();
            BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
            String line = "";
            while(line != null) {
                line = bufferedReader.readLine();
                data = data + line;
            }

            JSONObject jsonObject = new JSONObject(data);
            floor = new Floor(jsonObject);
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

        if(floor!=null){
            ((SearchFloorActivity)context).showDescription(floor.allDescription());
            //SearchFloorActivity.data.setText(floor.allDescription());
        }else{
            ((SearchFloorActivity)context).showDescription("Brak piętra o tym numerze");
            //SearchFloorActivity.data.setText("Brak piętra o tym numerze");
        }

        ((SearchFloorActivity)context).DisableProgressBar();
        ((SearchFloorActivity)context).setFloor(floor);
        //SearchFloorActivity.DisableProgressBar();
        //SearchFloorActivity.floor = floor;
        //ShowMapActivity.DisableProgressBar();
        //ShowMapActivity.floor = floor;
    }
    public void setNumberId(String number){
        numberId = number;
    }
}
