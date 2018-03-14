import { Component, OnInit, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../common-interfaces/employee';
import { Technology } from '../../common-interfaces/technology';
import { TechnologyService } from '../../services/technology.service';
@Component({
  moduleId: module.id,
  selector: 'form-employee',
  templateUrl: './employee.component.html',
  providers: [ EmployeeService, TechnologyService ]
})
export class EmployeeComponent implements OnInit {

  formAddEmployee : FormGroup;
  companyId : number = 1;
  allTechnologies : Technology[];
  formErrors = {
    DeveloperId: '',
    FirstName: '',
    Surname: '',
    ManagerId: '',
    Email: '',
    WantToHelp: '',
    JobTitle: ''
  }

  private validationMessages = {
    DeveloperId: {
      required: 'Developer ID cannot be empty'
    },
    FirstName: {
      required: 'First Name cannot be empty'
    },
    Surname: {
      required: 'Surname cannot be empty'
    },
    Email: {
      required: 'E-mail cannot be empty'
    },
    ManagerId:{
      
    },
    WantToHelp: {
      required: 'Wants To Help form cannot be empty'
    },
    JobTitle: {
      required: 'Job Title cannot be empty'
    }
  }
  
  constructor(private formBuilder : FormBuilder,
     private service : EmployeeService,
    private technologyService : TechnologyService) {}
  
    removeTechnology(i : number)
    {
      this.Technologies.removeAt(i);
    }

  ngOnInit() {
    this.formAddEmployee = this.formBuilder.group({
     DeveloperId: ['', Validators.required],
     FirstName: ['', Validators.required],
     Surname: ['', Validators.required],
     Technologies: this.formBuilder.array([]),
     Email: ['', Validators.required],
     WantToHelp: ['', Validators.required],
     JobTitle: ['', Validators.required],
     ManagerId: ['']
   });
   
   this.formAddEmployee.valueChanges.debounceTime(500).subscribe((value) => {
    this.onControlValueChanged();
   });
   this.onControlValueChanged();

   this.technologyService.getTechnologies().subscribe(x => this.allTechnologies = x);

  }

  onSubmit(form) {
    //console.log(form);
    const formModel = this.formAddEmployee.value;
   var developersTechnologies = form.value.Technologies.map(x => x.Name);
   console.log("Lista: " + JSON.stringify(developersTechnologies));

    var emp = {
      FirstName : form.value.FirstName,
      Id : form.value.DeveloperId,
      Surname : form.value.Surname,
      ManagerCompanyId : this.companyId,
      CompanyId : this.companyId,
      ManagerId : form.value.ManagerId,
      Email : form.value.Email,
      JobTitle : form.value.JobTitle,
      Technologies : developersTechnologies
    } as Employee;
    console.log(emp);
    this.service.addEmployee(emp);
  }

  get Technologies(): FormArray {
    return this.formAddEmployee.get('Technologies') as FormArray;
  };

  addTechnology() {
    this.Technologies.push(this.formBuilder.group(
      {
        Name : ['']
    }));
  }
  
  onControlValueChanged() : void {
  const form = this.formAddEmployee;

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
