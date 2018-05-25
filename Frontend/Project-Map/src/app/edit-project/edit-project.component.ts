import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TechnologyService } from '../services/technology.service';
import { EmployeeService } from '../services/employee.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Project } from '../common-interfaces/project';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css'],
  providers: [EmployeeService, TechnologyService, ProjectService]
})
export class EditProjectComponent implements OnInit {
  formEditProject : FormGroup;
  modalReference : any;
  allTechnologies : string[];
  project : Project;
  selectedEmployee = null;
  formErrors = {
    Description: '',
    RepositoryLink: '',
    DocumentationLink: '',
    EmployeeRole: ''
  }
  private validationMessages = {
    Description: {
      required: 'Description cannot be empty'
    },
    RepositoryLink: {
      required: 'Repository Link cannot be empty'
    },
    DocumentationLink: {
      required: 'Documentation Link cannot be empty'
    },
  }

  constructor(private formBuilder : FormBuilder,
    private technologyService : TechnologyService,
    private employeeService : EmployeeService,
    private projectService : ProjectService,
    private modalService: NgbModal,
    private router: Router,
    private activatedRoute:ActivatedRoute) { }

  ngOnInit() {
    this.projectService.getProject(this.activatedRoute.snapshot.params['id'])
    .subscribe(result => {
      this.project = result;
      this.formEditProject.patchValue(
        {
          Description: result.Description,
          RepositoryLink: result.RepositoryLink,
          DocumentationLink: result.DocumentationLink,
          Technologies: result.Technologies,
          Employees: result.Employees.map(x => {
            return {
              value: x.Id,
              display: '#ID: ' + x.Id + ', ' + x.FirstName + ' ' + x.Surname,
              role: 'Developer'                                                               //TUTAJ TRZEBA KIEDYS ZWRACAC TEZ ROLE XD
            }
          })
        }
      )
      console.log("Technologies: " + result.Technologies);
      console.log("Employees : " + JSON.stringify(result));
    })

    this.formEditProject = this.formBuilder.group({
      Description: ['', Validators.required],
      RepositoryLink: ['', Validators.required],
      DocumentationLink: ['', Validators.required],
      Technologies: [''],
      Employees: [''],
      EmployeeRole: ['']
    });
    
     this.formEditProject.valueChanges.debounceTime(500).subscribe((value) => {
       this.onControlValueChanged();
     });
     this.onControlValueChanged();
     this.technologyService.getTechnologiesNames().subscribe(x => this.allTechnologies = x);
  }

  public requestAutocompleteItems = (text: string): Observable<any[]> => {
    return this.employeeService.getEmployeesByQuery(text).map(array => array.map(employee => {
      return {
        value: employee.Id,
        display: '#ID: ' + employee.Id + ', ' + employee.FirstName + ' ' + employee.Surname,
        role: 'Developer'
      }
    }));
  };


  public onSelect(item)
  {
    this.selectedEmployee = item;
  }

  onSubmit(form) {
    const employees = form.value.Employees.map(x =>{
      return {
        EmployeeId: x.value,
        Role: x.role,        
        ProjectId: this.project.Id,
      }
    });

    const project = {
      Id: this.project.Id,
      Description : form.value.Description,
      RepositoryLink : form.value.RepositoryLink,
      DocumentationLink : form.value.DocumentationLink,
      Technologies : form.value.Technologies,
      EmployeesRoles : employees
    }
    this.projectService.editProject(project).subscribe(response =>{
      console.log("Project edited");
      this.modalReference.close();
      this.router.navigate(['/main']);
    });
  }
  
  onControlValueChanged() : void {
  const form = this.formEditProject;

  for (let field in this.formErrors) {
    this.formErrors[field] = '';
    let control = form.get(field);

    if (control && control.dirty && !control.valid) {
      const validationMessages = this.validationMessages[field];
      for (const key in control.errors) {
        this.formErrors[field] += validationMessages[key] + ' ';
      }
    }
  }
}


open(content) {
  this.modalReference = this.modalService.open(content)
  this.modalReference.result.then((result) => {
    //this.closeResult = `Closed with: ${result}`;
  }, (reason) => {
   // this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
  });
}
}
