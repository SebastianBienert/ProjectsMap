package project.projectsmap;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.design.widget.Snackbar;
import android.text.TextUtils;
import android.widget.TextView;
import android.widget.Toast;

import com.koushikdutta.async.future.FutureCallback;
import com.koushikdutta.ion.Ion;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 14.03.2018.
 */

public class FetchDataAboutDeveloper extends AsyncTask<Void,Void,Void> {
    TextView TextViewStatement;
    String data ="";
    String choice = "";
    String inputData = "";
    String errorText = "";
    ArrayList<Developer> dataList = new ArrayList<Developer>();
    //String webApiURL = "https://19484bc4.ngrok.io";
    //String webApiURL = "https://projectsmapwebapi.azurewebsites.net";
    String token = "";

    /*      dodane do zapisu do pliku       */
    boolean saveDataToFile = false;
    Context context;
    Context toSavecontext;
    String fileName;
    boolean append;
    /*--------------------------------------*/

    public void setTextViewStatement(TextView statement) {
        this.TextViewStatement = statement;
    }
    public void setChoice(String name){
        choice = name;
    }
    public void setInputData(String name){
        inputData = name;
    }

    /*      dodane do zapisu do pliku       */
    public void setSaveDataToFile(boolean choice){
        saveDataToFile = choice;
    }
    public void setcontext(Context con){
        context = con;
    }
    public void setFileName(String name){
        fileName = name;
    }
    public void setAppend(boolean ap){append = ap;}
    public void setToken(String token_){ token = token_; }
    /*--------------------------------------*/
    @Override
    protected Void doInBackground(Void... voids) {
        if (inputData.isEmpty() && !choice.equals("Wszyscy")) {
            errorText = "Wprowadź dane";
        } else {
            try {
                boolean digitsOnly = TextUtils.isDigitsOnly(inputData);
                if(choice.equals("Id") && !digitsOnly)
                    throw new NumberFormatException("incorrect input format!");
                URL url = setURLAdress();
                if (url == null) {
                    return null;
                }
                HttpsURLConnection httpsURLConnection = (HttpsURLConnection) url.openConnection();
                httpsURLConnection.addRequestProperty("Content-Type", "application/x-www-form-urlencoded");
                httpsURLConnection.addRequestProperty("Authorization", "Bearer " + token);
                InputStream inputStream = httpsURLConnection.getInputStream();
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
                String line = "";
                while (line != null) {
                    line = bufferedReader.readLine();
                    data = data + line;
                }
                dataList = new ArrayList<Developer>();
                Object json = new JSONTokener(data).nextValue();
                if (json instanceof JSONObject) {
                    dataList.add(new Developer(new JSONObject(data)));
                } else if (json instanceof JSONArray) {
                    JSONArray JA = new JSONArray(data);
                    for (int i = 0; i < JA.length(); i++) {
                        dataList.add(new Developer((JSONObject) JA.get(i)));
                    }
                }
            } catch (MalformedURLException e) {
                errorText = "MalformedURLException";
                e.printStackTrace();
            } catch (NumberFormatException e){
                errorText = "Id powinno zawierać jedynie liczby!";
                e.printStackTrace();
            } catch (FileNotFoundException e){
                errorText = "Brak wyników";
            } catch (IOException e) {
                errorText = "IOException " ;
                e.printStackTrace();
            } catch (JSONException e) {
                errorText = "JSONException";
                e.printStackTrace();
            }
        }
        return null;
    }
    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);
        showErrorStatement();
        if(saveDataToFile){ //dodane do zapisu do pliku
            /*String data = "";
            for(int i=0; i<dataList.size();i++) {
                data+=dataList.get(i).description();
            }*/
            ((SaveToFileActivity)context).DisableProgressBar();
            ((SaveToFileActivity)context).saveDataToFile(toSavecontext,fileName,this.data,append);
            //SaveToFileActivity.saveDataToFile(context,fileName,this.data,append);
            //SaveToFileActivity.DisableProgressBar();
        }else{
            for(int i=0; i<dataList.size();i++) {
                ((SearchDevelopersActivity)context).addDeveloper(dataList.get(i));
                //SearchDevelopersActivity.adapter.list.add(dataList.get(i).description());
            }
            //SearchDevelopersActivity.adapter.notifyDataSetChanged();
            ((SearchDevelopersActivity)context).notifyDataSetChanged();
            ((SearchDevelopersActivity)context).DisableProgressBar();
            //SearchDevelopersActivity.DisableProgressBar();
        }
    }

    private void showErrorStatement() {
        if(TextViewStatement!=null){
            if(isNetworkAvailable()){
                TextViewStatement.setText(errorText);
            }else{
                TextViewStatement.setText("Brak internetu");
            }
        }
    }

    private URL setURLAdress(){
        try{
            if(choice.equals("Technologia")){
                return new URL(GlobalVariable.webApiURL+"/api/developers/technology/"+inputData);
                //return new URL("http://localhost:58923/api/developers/technology/"+inputData);
            }else if(choice.equals("Id")){
                return new URL(GlobalVariable.webApiURL+"/api/developers/"+inputData);
                //return new URL("http://localhost:58923/api/developers/"+inputData);
            }else if(choice.equals("Wszyscy")){
                return new URL(GlobalVariable.webApiURL+"/api/developers");
                //return new URL("http://localhost:58923/api/developers");
            }else if(choice.equals("Imię lub nazwisko")){
                return new URL(GlobalVariable.webApiURL+"/api/developers/"+inputData);
                //return new URL("http://localhost:58923/api/developers/"+inputData);
            }
        }catch(MalformedURLException e){
            e.printStackTrace();
        }
        return null;
    }
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }

    public void setToSavecontext(Context toSavecontext) {
        this.toSavecontext = toSavecontext;
    }
}