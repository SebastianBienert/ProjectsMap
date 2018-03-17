import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, ReactiveFormsModule } from '@angular/forms';
import { TechnologyService } from '../../services/technology.service';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';
import { Employee } from '../../common-interfaces/employee';
import { EmployeeService } from '../../services/employee.service';
import { Observable } from 'rxjs/Observable';
import { ProjectService } from '../../services/project.service';
@Component({
  moduleId: module.id,
  selector: 'form-project',
  templateUrl: './project.component.html',
  providers: [ EmployeeService, TechnologyService, ProjectService ]
})

export class ProjectComponent implements OnInit {
  formAddProject : FormGroup;
  companyId : number = 1;
  allTechnologies : string[];

  addedEmployees = [];
  itemsAsObjects = [{FirstName: 'Pracownik3 ', Id: 3, Surname: 'Nazwisko3'}];


  autocompleteItemsAsObjects = [
    {FirstName: 'Pracownik1 ', Id: 0, Surname: 'Nazwisko1'},
    {FirstName: 'Pracownik2', Id: 1, Surname: 'Nazwisko'},
    'item3'
  ];

  formErrors = {
    Description: '',
    RepositoryLink: '',
    DocumentationLink: '',
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
    private projectService : ProjectService) {}
  
  ngOnInit() {
    this.formAddProject = this.formBuilder.group({
     Description: ['', Validators.required],
     RepositoryLink: ['', Validators.required],
     DocumentationLink: ['', Validators.required],
     Technologies: [''],
     Employees: ['']
   });
   
   this.formAddProject.valueChanges.debounceTime(500).subscribe((value) => {
    this.onControlValueChanged();
   });
   this.onControlValueChanged();

   this.technologyService.getTechnologiesNames().subscribe(x => this.allTechnologies = x);
  }

  public requestAutocompleteItems = (text: string): Observable<any[]> => {
    return this.employeeService.getEmployeesByQuery(text).map(array => array.map(employee => {
      return {
        value: employee.Id,
        display: '#ID: ' + employee.Id + ', ' + employee.FirstName + ' ' + employee.Surname
      }
    }));
  };


  onSubmit(form) {
    var employees = form.value.Employees.map(x =>{
      return {
        EmployeeId: x.value,
        Role: 'Developer',         //TODO
        ProjectId: 0,
        CompanyId: this.companyId
      }
    });

    var project = {
      Description : form.value.Description,
      RepositoryLink : form.value.RepositoryLink,
      DocumentationLink : form.value.DocumentationLink,
      CompanyId : this.companyId,
      Technologies : form.value.Technologies,
      EmployeesRoles : employees
    }
    console.log(form);
    console.log(project);
    this.projectService.addProject(project);
  }
  
  get Technologies(): FormArray {
    return this.formAddProject.get('Technologies') as FormArray;
  };

  onControlValueChanged() : void {
  const form = this.formAddProject;

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
}
