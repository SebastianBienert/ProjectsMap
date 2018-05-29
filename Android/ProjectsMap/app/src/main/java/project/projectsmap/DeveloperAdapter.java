package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.util.DisplayMetrics;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 20.03.2018.
 */

public class DeveloperAdapter extends BaseAdapter {

    ArrayList<Developer> DevelopersList = new ArrayList<Developer>();
    Context c;

    DeveloperAdapter(Context context){
        c = context;
        DevelopersList = new ArrayList<Developer>();
        ////////////// minuta 16:00 link: https://www.youtube.com/watch?v=vpfeDoIWT0U !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    @Override
    public int getCount() {
        return DevelopersList.size();
    }

    @Override
    public Object getItem(int position) {
        return DevelopersList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater layoutInflater = (LayoutInflater)c.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        final View row = layoutInflater.inflate(R.layout.activity_single_row_developer,parent,false);

        Button btnShowOnMap = (Button)row.findViewById(R.id.buttonShowOnMap);
        ImageView UserImage = (ImageView)row.findViewById(R.id.UserImage);
        TextView DeveloperName = (TextView)row.findViewById(R.id.DeveloperName);
        TextView DeveloperId = (TextView)row.findViewById(R.id.DeveloperId);
        TextView DeveloperEmail = (TextView)row.findViewById(R.id.DeveloperEmail);
        TextView DeveloperJobTitle = (TextView)row.findViewById(R.id.DeveloperJobTitle);
        TextView DeveloperTechnologies = (TextView)row.findViewById(R.id.DeveloperTechnologies);
        TextView DeveloperWantToHelp = (TextView)row.findViewById(R.id.DeveloperWantToHelp);
        String WantToHelp = "";

        if(DevelopersList.get(position).getWantToHelp()) WantToHelp = "Chcę pomagać!";
        else{
            WantToHelp = "Jestem zajęty, nie będę pomagał.";
            DeveloperWantToHelp.setTextColor(Color.RED);
        }

        if(DevelopersList.get(position).getPhoto()!=null){
            Bitmap bm = BitmapFactory.decodeByteArray(DevelopersList.get(position).getPhoto(), 0, DevelopersList.get(position).getPhoto().length);
            DisplayMetrics dm = new DisplayMetrics();

            UserImage.setImageBitmap(bm);
        }

        final String employeeId = Integer.toString(DevelopersList.get(position).getDeveloperId());

        if(DevelopersList.get(position).getFirstName()!=null&&DevelopersList.get(position).getSurname()!=null)
            DeveloperName.setText(DevelopersList.get(position).getFirstName() + " " + DevelopersList.get(position).getSurname());
        else
            DeveloperName.setText("Name Surname: Brak danych");
        DeveloperId.setText("ID: "+DevelopersList.get(position).getDeveloperId());

        if(DevelopersList.get(position).getEmail()!=null)
            DeveloperEmail.setText("Email: "+DevelopersList.get(position).getEmail());
        else
            DeveloperEmail.setText("Email: Brak danych");

        if(DevelopersList.get(position).getJobTitle()!=null)
            DeveloperJobTitle.setText("Stanowisko: "+DevelopersList.get(position).getJobTitle());
        else
            DeveloperJobTitle.setText("Stanowisko: Brak danych");

        if(DevelopersList.get(position).getTechnologies()!=null) {
            DeveloperTechnologies.setText("Technologie: ");
            for (int i = 0; i < DevelopersList.get(position).getTechnologies().size(); i++) {
                DeveloperTechnologies.setText(DeveloperTechnologies.getText() +
                        DevelopersList.get(position).getTechnologies().get(i).replace('"',' ')+"|");
            }
        }
        else
            DeveloperTechnologies.setText("Technologie: Brak danych");

        DeveloperWantToHelp.setText(WantToHelp);

        btnShowOnMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                    Intent i = new Intent(c, MapActivity.class);
                    i.putExtra("Id", employeeId);
                    c.startActivity(i);
            }
        });


        return row;
    }

}
