package project.projectsmap;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    Button click;
    public static TextView data;
    ListView listView;
    public static customAdapter custom;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        click = (Button) findViewById(R.id.button);
//        data = (TextView) findViewById(R.id.fetcheddata);

        listView = (ListView)findViewById(R.id.listview);

        custom = new customAdapter(this);

        listView.setAdapter(custom);

        click.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                fetchData process = new fetchData();
                process.execute();
            }
        });

    }
}

class singleRow{
    String RowData;

    singleRow(String rowData){
        this.RowData=rowData;
    }
}

class customAdapter extends BaseAdapter{

    ArrayList<singleRow> list;
    Context c;

    customAdapter(Context context){
        c = context;
        list = new ArrayList<singleRow>();
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

        singleRow tmp = list.get(position);

        rowData.setText(tmp.RowData);


        return row;
    }
}