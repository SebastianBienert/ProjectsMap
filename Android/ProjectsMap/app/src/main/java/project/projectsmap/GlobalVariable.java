package project.projectsmap;

/**
 * Created by Mateusz on 24.04.2018.
 */

public class GlobalVariable {
    static public String webApiURL = "https://projectsmapwebapi.azurewebsites.net";
    static String token;
    static private boolean onlineWork = false;
    static public boolean getOnlineWork(){
        return onlineWork;
    }
    static public void setOnlineWork(boolean decision){
        onlineWork = decision;
    }
    //static public String webApiURL = "http://projectsmapwebapi.azurewebsites.net";
}