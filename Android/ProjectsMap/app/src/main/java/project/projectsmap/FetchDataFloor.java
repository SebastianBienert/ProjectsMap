package project.projectsmap;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.AsyncTask;
import android.support.v4.content.ContextCompat;

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

import javax.net.ssl.HttpsURLConnection;

/**
 * Created by Mateusz on 19.03.2018.
 */

public class FetchDataFloor extends AsyncTask<Void,Void,Void> {
    String data ="";
    String numberId;
    String numberIdEmployee="-1";
    //String webApiURL = "https://19484bc4.ngrok.io";
    //String webApiURL = "http://projectsmapwebapi.azurewebsites.net";
    Floor floor;
    Context context;
    String token = "";
    Boolean isOnline;

    public void setContext(Context context) {
        this.context = context;
    }
    public void setToken(String token_){ token = token_; }
    public void setInfoAboutConnectToInternet(Boolean on){ isOnline = on; }


    @Override
    protected Void doInBackground(Void... voids) {
        if (isOnline && isNetworkAvailable()) {
            try {
                URL url;
                if (numberIdEmployee.equals("-1")) {
                    url = new URL(GlobalVariable.webApiURL + "/api/floor/" + numberId);
                } else {
                    url = new URL(GlobalVariable.webApiURL + "/api/developers/" + numberIdEmployee + "/floor");
                }
                //URL url = new URL("http://localhost:58923/api/floor/" + numberId);
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

                JSONObject jsonObject = new JSONObject(data);
                floor = new Floor(jsonObject);
            } catch (MalformedURLException e) {
                e.printStackTrace();
            } catch (IOException e) {
                e.printStackTrace();
            } catch (JSONException e) {
                e.printStackTrace();
            }
        } else {
            if (numberIdEmployee.equals("-1")) {
                try {
                    File[] files = ContextCompat.getExternalFilesDirs(context, null);
                    File file = new File(files[0], "/" + "map" + ".txt");
                    FileInputStream fis = null;

                    fis = new FileInputStream(file);

                    BufferedReader r = new BufferedReader(new InputStreamReader(fis));
                    String s = "";
                    while ((s = r.readLine()) != null) {
                        data += s;
                    }
                    r.close();

                    ArrayList<Floor> floorsList = new ArrayList<Floor>();
                    Object json = null;
                    json = new JSONTokener(data).nextValue();

                    if (json instanceof JSONObject) {
                        floorsList.add(new Floor(new JSONObject(data)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int i = 0; i < JA.length(); i++) {
                            floorsList.add(new Floor((JSONObject) JA.get(i)));
                        }
                    }

                    for(int i=0; i<floorsList.size(); i++){
                        if(Integer.toString(floorsList.get(i).getFloorId()).equals(numberId))
                            floor = floorsList.get(i);
                        break;
                    }

                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                } catch (IOException e) {
                    e.printStackTrace();
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            } else {
                try {
                    String seatId="",roomId="";
                    File[] files = ContextCompat.getExternalFilesDirs(context, null);
                    File file = new File(files[0], "/" + "map" + ".txt");
                    FileInputStream fis = null;

                    fis = new FileInputStream(file);

                    BufferedReader r = new BufferedReader(new InputStreamReader(fis));
                    String s = "";
                    while ((s = r.readLine()) != null) {
                        data += s;
                    }
                    r.close();

                    ArrayList<Floor> floorsList = new ArrayList<Floor>();
                    Object json = null;
                    json = new JSONTokener(data).nextValue();

                    if (json instanceof JSONObject) {
                        floorsList.add(new Floor(new JSONObject(data)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA = new JSONArray(data);
                        for (int i = 0; i < JA.length(); i++) {
                            floorsList.add(new Floor((JSONObject) JA.get(i)));
                        }
                    }

                    String data1="";
                    ArrayList<Developer> developersList = new ArrayList<Developer>();

                    File[] files1 = ContextCompat.getExternalFilesDirs(context, null);
                    File file1 = new File(files1[0], "/" + "employees" + ".txt");
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
                        developersList.add(new Developer(new JSONObject(data1)));
                    } else if (json instanceof JSONArray) {
                        JSONArray JA1 = new JSONArray(data1);
                        for (int i = 0; i < JA1.length(); i++) {
                            developersList.add(new Developer((JSONObject) JA1.get(i)));
                        }
                    }

                    for(int i=0; i<developersList.size();i++){
                        if(developersList.get(i).getDeveloperId()==Integer.parseInt(numberIdEmployee)) {
                            seatId = Integer.toString(developersList.get(i).getSeat().seatId);
                            roomId = Integer.toString(developersList.get(i).getSeat().roomId);
                        }
                    }

                    //floor = floorsList.get(1); // Zamiast tego trzeba coś jak zakomentowane poniżej, ale poniżej jest gdzieś błąd i nic nie znajduje nic

                    for(int i=0; i<floorsList.size(); i++){
                        for(int j=0; j<floorsList.get(i).getRooms().size();j++) {
                            if (Integer.toString(floorsList.get(i).getRooms().get(j).getRoomId()).equals(roomId)) {
                                floor = floorsList.get(i);
                                break;
                                /*for (int k = 0; k < floorsList.get(i).getRooms().get(j).getSeats().size(); k++) {
                                    if (Integer.toString(floorsList.get(i).getRooms().get(j).getSeats().get(k).seatId).equals(seatId)) {
                                        floor = floorsList.get(i);
                                        break;
                                    }
                                }
                                break;*/
                            }
                        }
                    }

                } catch (FileNotFoundException e) {
                    e.printStackTrace();
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                } catch (IOException e) {
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

        if(floor!=null){
            if(numberIdEmployee.equals("-1")){
                ((SearchFloorActivity)context).showDescription(floor.allDescription());
                ((SearchFloorActivity)context).DisableProgressBar();
                ((SearchFloorActivity)context).setFloor(floor);
            }else{
                ((MapActivity)context).SetFloor(floor);
                ((MapActivity)context).RefreshMap();
            }
            //SearchFloorActivity.data.setText(floor.allDescription());
        }else{
            if(numberIdEmployee.equals("-1")){
                ((SearchFloorActivity)context).showDescription("Brak piętra o tym numerze");
                ((SearchFloorActivity)context).DisableProgressBar();
            }else{

            }
            //SearchFloorActivity.data.setText("Brak piętra o tym numerze");
        }


        //SearchFloorActivity.DisableProgressBar();
        //SearchFloorActivity.floor = floor;
        //ShowMapActivity.DisableProgressBar();
        //ShowMapActivity.floor = floor;
    }
    public void setNumberId(String number){
        numberId = number;
    }
    public void setNumberEmployeeId(String number) { numberIdEmployee = number;}
    private boolean isNetworkAvailable() {
        ConnectivityManager connectivityManager
                = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }
}
