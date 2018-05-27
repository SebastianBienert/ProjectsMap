package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;

/**
 * Created by Mateusz on 20.03.2018.
 */

public class CustomAdapter extends BaseAdapter {

    ArrayList<String> list;
    Context c;

    CustomAdapter(Context context){
        c = context;
        list = new ArrayList<String>();
        ////////////// minuta 16:00 link: https://www.youtube.com/watch?v=vpfeDoIWT0U !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    @Override
    public int getCount() {
        return list.size();
    }

    @Override
    public Object getItem(int position) {
        return list.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater layoutInflater = (LayoutInflater)c.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        final View row = layoutInflater.inflate(R.layout.singlerow,parent,false);

        TextView rowData = (TextView)row.findViewById(R.id.rowData);
        Button btnShowOnMap = (Button)row.findViewById(R.id.buttonShowOnMap);
        rowData.setText(list.get(position));
        TextView DeveloperName = (TextView)row.findViewById(R.id.DeveloperName);

        int counter = 0;
        final String[] data = ((String) rowData.getText()).split(" ");
        final String employeeId = data[1];
        DeveloperName.setText(data[2] + " " + data[3]);

        /*do{
            if(data[counter].equals("Id:")){
                employeeId = data[counter + 1];
                break;
            }
        }while(counter++ < data.length);*/

        btnShowOnMap.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                    Intent i = new Intent(c, MapActivity.class);
                    i.putExtra("Id", employeeId);
                    c.startActivity(i);
            }
        });
        //String tmp = list.get(position);

        //rowData.setText(list.get(position));

        return row;
    }

}
