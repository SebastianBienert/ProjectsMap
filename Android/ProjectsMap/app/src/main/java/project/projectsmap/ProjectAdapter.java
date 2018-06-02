package project.projectsmap;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Button;
import android.widget.TextView;

import java.util.ArrayList;

/**
 * Created by Mateusz on 25.03.2018.
 */

public class ProjectAdapter extends BaseAdapter {
    ArrayList<Project> ProjectsList;
    Context c;
    boolean isOnline;

    ProjectAdapter(Context context, boolean isOnline_){
        c = context;
        ProjectsList = new ArrayList<Project>();
        isOnline = isOnline_;
        ////////////// minuta 16:00 link: https://www.youtube.com/watch?v=vpfeDoIWT0U !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }

    @Override
    public int getCount() {
        return ProjectsList.size();
    }

    @Override
    public Object getItem(int position) {
        return ProjectsList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(final int position, View convertView, ViewGroup parent) {
        LayoutInflater layoutInflater = (LayoutInflater)c.getSystemService(Context.LAYOUT_INFLATER_SERVICE);

        View row = layoutInflater.inflate(R.layout.activity_single_row_project,parent,false);

        TextView ProjectName = (TextView)row.findViewById(R.id.ProjectName);
        TextView ProjectId = (TextView)row.findViewById(R.id.ProjectId);
        TextView RepositoryLink = (TextView)row.findViewById(R.id.RepositoryLink);
        TextView DocumentationLink = (TextView)row.findViewById(R.id.DocumentationLink);
        TextView ProjectTechnologies = (TextView)row.findViewById(R.id.ProjectTechnologies);

        Button btnShowDev = (Button)row.findViewById(R.id.buttonShowDevelopers);
        ProjectName.setText(ProjectsList.get(position).getDescription());
        ProjectId.setText("ID: "+ProjectsList.get(position).getProjectId());
        RepositoryLink.setText("Repozytorium: "+ProjectsList.get(position).getRepositoryLink());
        DocumentationLink.setText("Dokumentacja: "+ProjectsList.get(position).getDocumentationLink());
        if(ProjectsList.get(position).getTechnologies()!=null) {
            ProjectTechnologies.setText("Technologie: ");
            for (int i = 0; i < ProjectsList.get(position).getTechnologies().size(); i++) {
                if(i==ProjectsList.get(position).getTechnologies().size()-1){
                    ProjectTechnologies.setText(ProjectTechnologies.getText() +
                            ProjectsList.get(position).getTechnologies().get(i).replace('"', ' '));
                }
                else{
                    ProjectTechnologies.setText(ProjectTechnologies.getText() +
                            ProjectsList.get(position).getTechnologies().get(i).replace('"', ' ') + " | ");
                }
            }
        }


        final String projectId = Integer.toString(ProjectsList.get(position).getProjectId());
        btnShowDev.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Toast.makeText(c,"Ta funkcjonalność nie została jeszcze zaimplementowana", Toast.LENGTH_SHORT).show();
                Intent intent = new Intent(c, ActivityShowProjectDevelopers.class);
                intent.putExtra("Id", projectId);
                intent.putExtra("isOnline", isOnline);
                c.startActivity(intent);
            }
        });



        return row;
    }
}
