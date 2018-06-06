package project.projectsmap;

import android.content.Context;
import android.graphics.Color;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Environment;
import android.support.v4.content.ContextCompat;
import android.text.TextUtils;
import android.widget.TextView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.regex.Pattern;

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class FetchDataAboutProject extends AsyncTask<Void,Void,Void> {
    TextView TextViewStatement;
    String data ="";
    String choice = "";
    String inputData = "";
    String errorText = "";
    //String webApiURL = "https://19484bc4.ngrok.io";
    //String webApiURL = "http://projectsmapwebapi.azurewebsites.net";
    String token = "";
    Boolean isOnline;
    ArrayList<Project> dataList = new ArrayList<Project>();

    /*      dodane do zapisu do pliku       */
    boolean saveDataToFile = false;
    Context context;
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
    public void setInfoAboutConnectToInternet(Boolean on){ isOnline = on; }


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
        if (isOnline && isNetworkAvailable()) {
            if (inputData.isEmpty() && !choice.equals("Wszystkie")) {
                errorText = "Wprowadź dane";
            } else {
                try {
                    boolean digitsOnly = TextUtils.isDigitsOnly(inputData);
                    if (choice.equals("Id") && !digitsOnly)
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

                    Object json = new JSONTokener(data).nextValue();
                    if (json instanceof JSONObject) {
                        dataList.add(new Project(new JSONObject(data)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int i = 0; i < JA.length(); i++) {
                            dataList.add(new Project((JSONObject) JA.get(i)));
                        }
                    }
                } catch (MalformedURLException e) {
                    errorText = "MalformedURLException";
                    e.printStackTrace();
                } catch (NumberFormatException e) {
                    errorText = "Id powinno zawierać jedynie liczby!";
                    e.printStackTrace();
                } catch (FileNotFoundException e) {
                    errorText = "Brak wyników";
                } catch (IOException e) {
                    errorText = "IOException";
                    e.printStackTrace();
                } catch (JSONException e) {
                    errorText = "JSONException";
                    e.printStackTrace();
                }
            }
        }
        else {
            if (inputData.isEmpty() && !choice.equals("Wszystkie")) {
                errorText = "Wprowadź dane";
            } else {
                try {
                    boolean digitsOnly = TextUtils.isDigitsOnly(inputData);
                    if (choice.equals("Id") && !digitsOnly)
                        throw new NumberFormatException("incorrect input format!");
                    File[] files = ContextCompat.getExternalFilesDirs(context, null);
                    File file = new File(files[0], "/" + "projects" + ".txt");
                    FileInputStream fis = new FileInputStream(file);
                    BufferedReader r = new BufferedReader(new InputStreamReader(fis));
                    String s = "";
                    while ((s = r.readLine()) != null) {
                        data += s;
                    }
                    r.close();
                    ArrayList<Project> projectsList = new ArrayList<Project>();
                    Object json = null;
                    json = new JSONTokener(data).nextValue();

                    if (json instanceof JSONObject) {
                        projectsList.add(new Project(new JSONObject(data)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int i = 0; i < JA.length(); i++) {
                            projectsList.add(new Project((JSONObject) JA.get(i)));
                        }
                    }
                    if(choice.equals("Wszystkie")) {
                        for(int i=0; i<projectsList.size();i++){
                            dataList.add(projectsList.get(i));
                        }
                    }
                    else if(choice.equals("Id") && digitsOnly){
                        for(int i=0; i<projectsList.size();i++){
                            if(Integer.toString(projectsList.get(i).getProjectId()).contains(inputData))
                                dataList.add(projectsList.get(i));
                        }
                    }
                    else if(choice.equals("Nazwa")){
                        for(int i=0; i<projectsList.size();i++){
                            if(Pattern.compile(Pattern.quote(inputData), Pattern.CASE_INSENSITIVE).matcher(projectsList.get(i).getDescription()).find())
                                dataList.add(projectsList.get(i));
                        }
                    }
                    if(dataList.isEmpty())
                        throw new FileNotFoundException("no results!");

                } catch (NumberFormatException e) {
                    errorText = "Id powinno zawierać jedynie liczby!";
                    e.printStackTrace();
                } catch (FileNotFoundException e) {
                    errorText = "Brak wyników";
                } catch (IOException e) {
                    errorText = "IOException";
                    e.printStackTrace();
                } catch (JSONException e) {
                    errorText = "JSONException";
                    e.printStackTrace();
                }
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
            ((SaveToFileActivity)context).saveDataToFile(context,fileName,this.data,append);
            ((SaveToFileActivity)context).DisableProgressBar();
        }else{
            for(int i=0; i<dataList.size();i++) {
                //SearchProjectsActivity.adapter.ProjectsList.add(dataList.get(i).description());
                ((SearchProjectsActivity)context).addProject(dataList.get(i));
            }
            //SearchProjectsActivity.adapter.notifyDataSetChanged();
            ((SearchProjectsActivity)context).notifyDataSetChanged();
            ((SearchProjectsActivity)context).DisableProgressBar();
        }
    }
    private void showErrorStatement() {
        if(TextViewStatement!=null){
            if(!isNetworkAvailable()||!isOnline){
                ((SearchProjectsActivity)context).setStatement("Pracujesz offline");
            }else{
                ((SearchProjectsActivity)context).setStatement(errorText);
            }
        }
    }
    private URL setURLAdress(){
        try{
            if(choice.equals("Id")){
                return new URL(GlobalVariable.webApiURL+"/api/project/"+inputData);
                //return new URL("http://localhost:58923/api/developers/"+inputData);
            }else if(choice.equals("Wszystkie")){
                return new URL(GlobalVariable.webApiURL+"/api/project");
                //return new URL("http://localhost:58923/api/developers");
            }else if(choice.equals("Nazwa")){
                return new URL(GlobalVariable.webApiURL+"/api/project/name/"+inputData);
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
}
