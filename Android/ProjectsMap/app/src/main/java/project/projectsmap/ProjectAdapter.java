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
 * Created by Mateusz on 25.03.2018.
 */

public class ProjectAdapter extends BaseAdapter {
    ArrayList<String> list;
    Context c;

    ProjectAdapter(Context context){
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
    public View getView(final int position, View convertView, ViewGroup parent) {
        LayoutInflater layoutInflater = (LayoutInflater)c.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        View row = layoutInflater.inflate(R.layout.activity_single_row_project,parent,false);

        TextView rowData = (TextView)row.findViewById(R.id.textViewRowData);

        Button btnShowDev = (Button)row.findViewById(R.id.buttonShowDevelopers);
        rowData.setText(list.get(position));
        final String[] data = ((String) rowData.getText()).split(" ");
        final String projectId = data[1];
        btnShowDev.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Toast.makeText(c,"Ta funkcjonalność nie została jeszcze zaimplementowana", Toast.LENGTH_SHORT).show();
                Intent intent = new Intent(c, ActivityShowProjectDevelopers.class);
                intent.putExtra("Id", projectId);
                //intent.putExtra("listDev", );
                c.startActivity(intent);
            }
        });



        return row;
    }
}
