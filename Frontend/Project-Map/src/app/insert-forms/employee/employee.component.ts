import { Component, OnInit, OnChanges, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../common-interfaces/employee';
import { Technology } from '../../common-interfaces/technology';
import { TechnologyService } from '../../services/technology.service';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
@Component({
  moduleId: module.id,
  selector: 'form-employee',
  templateUrl: './employee.component.html',
  providers: [ EmployeeService, TechnologyService ],
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  formAddEmployee : FormGroup;
  allTechnologies : string[];
  formErrors = {
    DeveloperId: '',
    FirstName: '',
    Surname: '',
    ManagerId: '',
    Email: '',
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
    JobTitle: {
      required: 'Job Title cannot be empty'
    }
  }
  
  constructor(private formBuilder : FormBuilder,
     private service : EmployeeService,
    private technologyService : TechnologyService,
    private cd: ChangeDetectorRef) {}
  
  ngOnInit() {
    this.formAddEmployee = this.formBuilder.group({
     Photo: [null, ],                                 //This is not actually <input file>
     PhotoName: [''],
     DeveloperId: ['', Validators.required],
     FirstName: ['', Validators.required],
     Surname: ['', Validators.required],
     Technologies: [''],
     Email: ['', Validators.required],
     JobTitle: ['', Validators.required],
     ManagerId: ['']
   });
   
 

   this.formAddEmployee.valueChanges.debounceTime(500).subscribe((value) => {
    this.onControlValueChanged();
   });
   this.onControlValueChanged();

   this.technologyService.getTechnologiesNames().subscribe(x => this.allTechnologies = x);

  }


  onFileChange(event){
     if(event.target.files.length > 0) {
      
      let file = event.target.files[0];
      this.formAddEmployee.patchValue(
        {
          Photo: file,
          PhotoName: file.name
        });
    }
   
  }

  onSubmit(form) {
    const formModel = this.formAddEmployee.value;
   var developersTechnologies = form.value.Technologies;

    var emp = {
      FirstName : form.value.FirstName,
      Id : form.value.DeveloperId,
      Surname : form.value.Surname,
      ManagerId : form.value.ManagerId,
      Email : form.value.Email,
      JobTitle : form.value.JobTitle,
      Technologies : form.value.Technologies
    } as Employee; 
    
    this.service.addEmployee(form.value.Photo, emp);
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
