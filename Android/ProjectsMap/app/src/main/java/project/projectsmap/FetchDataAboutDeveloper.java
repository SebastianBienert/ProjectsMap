package project.projectsmap;

import android.app.Application;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.os.Environment;
import android.support.design.widget.Snackbar;
import android.support.v4.content.ContextCompat;
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
 * Created by Mateusz on 14.03.2018.
 */

public class FetchDataAboutDeveloper extends AsyncTask<Void,Void,Void> {
    TextView TextViewStatement;
    String data ="";
    String choice = "";
    String inputData = "";
    String errorText = "";
    String typeContext = ""; // wykorzystane do pobierania danych o pracowniku w MapActivity
    ArrayList<Developer> dataList = new ArrayList<Developer>();
    Boolean isOnline;
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
    public void setTypeContext(String type){typeContext = type;}
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
            if (inputData.isEmpty() && !choice.equals("Wszyscy")) {
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
                    dataList = new ArrayList<Developer>();
                    Object json = null;
                    json = new JSONTokener(data).nextValue();

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
                } catch (NumberFormatException e) {
                    errorText = "Id powinno zawierać jedynie liczby!";
                    e.printStackTrace();
                } catch (FileNotFoundException e) {
                    errorText = "Brak wyników";
                } catch (IOException e) {
                    errorText = "IOException ";
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }
        else {
            if (inputData.isEmpty() && !choice.equals("Wszyscy")) {
                errorText = "Wprowadź dane";
            } else {
                try {
                    boolean digitsOnly = TextUtils.isDigitsOnly(inputData);
                    if (choice.equals("Id") && !digitsOnly)
                        throw new NumberFormatException("incorrect input format!");
                    File[] files = ContextCompat.getExternalFilesDirs(context, null);
                    File file = new File(files[0], "/" + "employees" + ".txt");
                    FileInputStream fis = new FileInputStream(file);
                    BufferedReader r = new BufferedReader(new InputStreamReader(fis));
                    String s = "";
                    while ((s = r.readLine()) != null) {
                        data += s;
                    }
                    r.close();
                    dataList = new ArrayList<Developer>();
                    ArrayList<Developer> developersList = new ArrayList<Developer>();
                    Object json = null;
                    json = new JSONTokener(data).nextValue();

                    if (json instanceof JSONObject) {
                        developersList.add(new Developer(new JSONObject(data)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int i = 0; i < JA.length(); i++) {
                            developersList.add(new Developer((JSONObject) JA.get(i)));
                        }
                    }

                    if(choice.equals("Wszyscy")) {
                        for(int i=0; i<developersList.size();i++){
                            dataList.add(developersList.get(i));
                        }
                    }
                    else if(choice.equals("Id") && digitsOnly){
                        for(int i=0; i<developersList.size();i++){
                            if(Integer.toString(developersList.get(i).getDeveloperId()).contains(inputData))
                            dataList.add(developersList.get(i));
                        }
                    }
                    else if(choice.equals("Imię lub nazwisko")){
                        for(int i=0; i<developersList.size();i++){
                            if(Pattern.compile(Pattern.quote(inputData), Pattern.CASE_INSENSITIVE).matcher(developersList.get(i).getFirstName()).find()
                                    || Pattern.compile(Pattern.quote(inputData), Pattern.CASE_INSENSITIVE).matcher(developersList.get(i).getSurname()).find())
                                dataList.add(developersList.get(i));
                        }
                    }
                    else if(choice.equals("Technologia")){
                        boolean found = false;
                        for(int i=0; i<developersList.size();i++) {
                            found = false;
                            for (int j = 0; j < developersList.get(i).getTechnologies().size(); j++) {
                                if(Pattern.compile(Pattern.quote(inputData), Pattern.CASE_INSENSITIVE).matcher(developersList.get(i).getTechnologies().get(j)).find()) {
                                    found = true;
                                    break;
                                }
                            }
                            if(found)
                            dataList.add(developersList.get(i));
                        }
                    }
                    else if(choice.equals("Project")){

                        String data1="";
                        ArrayList<Project> projectsList = new ArrayList<Project>();

                        File[] files1 = ContextCompat.getExternalFilesDirs(context, null);
                        File file1 = new File(files1[0], "/" + "projects" + ".txt");
                        FileInputStream fis1 = new FileInputStream(file1);
                        BufferedReader r1 = new BufferedReader(new InputStreamReader(fis1));
                        String s1 = "";
                        while ((s1 = r1.readLine()) != null) {
                            data1 += s1;
                        }
                        r1.close();
                        Object json1 = null;
                        json1 = new JSONTokener(data1).nextValue();

                        if (json1 instanceof JSONObject) {
                            projectsList.add(new Project(new JSONObject(data1)));
                        } else if (json instanceof JSONArray) {
                            JSONArray JA1 = new JSONArray(data1);
                            for (int i = 0; i < JA1.length(); i++) {
                                projectsList.add(new Project((JSONObject) JA1.get(i)));
                            }
                        }


                        for(int i=0; i<projectsList.size(); i++){
                            if(Integer.toString(projectsList.get(i).getProjectId()).equals(inputData)) {
                                    for (int j = 0; j < projectsList.get(i).getEmployeesId().size(); j++) {
                                        for (int k = 0; k < developersList.size(); k++) {
                                            if ( projectsList.get(i).getEmployeesId().get(j).equals(Integer.toString(developersList.get(k).getDeveloperId())))
                                                dataList.add(developersList.get(k));
                                        }
                                    }
                                break;
                            }
                        }
                    }
                    if(dataList.isEmpty())
                        throw new FileNotFoundException("no results!");

                } catch (FileNotFoundException e) {
                    errorText = "Brak wyników";
                } catch (IOException e) {
                    e.printStackTrace();
                    return null;
                } catch (NumberFormatException e) {
                    errorText = "Id powinno zawierać jedynie liczby!";
                    e.printStackTrace();
                } catch (JSONException e) {
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
            ((SaveToFileActivity)context).DisableProgressBar();
            ((SaveToFileActivity)context).saveDataToFile(toSavecontext,fileName,this.data,append);
            //SaveToFileActivity.saveDataToFile(context,fileName,this.data,append);
            //SaveToFileActivity.DisableProgressBar();
        }else{
            if(choice.equals("Project")){
                ((ActivityShowProjectDevelopers)context).setArrayDevelopers(dataList);
            }else if(typeContext.equals("MapActivity")){
                if(!dataList.isEmpty()){
                    ((MapActivity)context).SetDeveloper(dataList.get(0));
                    ((MapActivity)context).RefreshMap();
                }else{
                    ((MapActivity)context).SetDeveloper(null);
                    ((MapActivity)context).RefreshMap();
                }
            }else{
                for(int i=0; i<dataList.size();i++) {
                    ((SearchDevelopersActivity)context).addDeveloper(dataList.get(i));
                    //SearchDevelopersActivity.adapter.ProjectsList.add(dataList.get(i).description());
                }
                //SearchDevelopersActivity.adapter.notifyDataSetChanged();
                ((SearchDevelopersActivity)context).notifyDataSetChanged();
                ((SearchDevelopersActivity)context).DisableProgressBar();
                //SearchDevelopersActivity.DisableProgressBar();
            }
        }
    }

    private void showErrorStatement() {
        if(TextViewStatement!=null){
            if(!isNetworkAvailable()||!isOnline){
                ((SearchDevelopersActivity)context).setStatement("Pracujesz offline");
            }else{
                ((SearchDevelopersActivity)context).setStatement(errorText);
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
            }else if(choice.equals("Project")){
                return new URL(GlobalVariable.webApiURL+"/api/project/"+inputData+"/employees"); //tylko do testu trzeba dodac odpowiedni w backendzie
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