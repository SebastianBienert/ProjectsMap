package project.projectsmap;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 20.03.2018.
 */

public class CustomAdapter extends BaseAdapter {

    ArrayList<SingleRow> list;
    Context c;

    CustomAdapter(Context context){
        c = context;
        list = new ArrayList<SingleRow>();
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

        View row = layoutInflater.inflate(R.layout.singlerow,parent,false);

        TextView rowData = (TextView)row.findViewById(R.id.rowData);

        SingleRow tmp = list.get(position);

        rowData.setText(tmp.RowData);

        return row;
    }

}
