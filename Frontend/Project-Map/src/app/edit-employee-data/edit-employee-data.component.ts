import { Component, OnInit } from '@angular/core';
import { OnChanges, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../common-interfaces/employee';
import { Technology } from '../common-interfaces/technology';
import { TechnologyService } from '../services/technology.service';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { ActivatedRoute, Router } from '@angular/router';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-edit-employee-data',
  templateUrl: './edit-employee-data.component.html',
  providers: [ EmployeeService, TechnologyService ],
  styleUrls: ['./edit-employee-data.component.css']
})
export class EditEmployeeDataComponent implements OnInit {
  employeeInfo: Employee = null;
  modalReference : any;
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
   private employeeService : EmployeeService,
   private cd: ChangeDetectorRef,
   private modalService: NgbModal,
   private router: Router) { }

   ngOnInit() {
    this.employeeService.getCurrentUserEmployeeData()
    .subscribe(employeeResult => {
      this.employeeInfo = employeeResult;
      this.formAddEmployee.patchValue(
        {
          DeveloperId: employeeResult.Id,
          FirstName: employeeResult.FirstName,
          Surname: employeeResult.Surname,
          Technologies: employeeResult.Technologies,
          Email: employeeResult.Email,
          JobTitle: employeeResult.JobTitle,
          ManagerId: employeeResult.ManagerId
        });
        if(this.employeeInfo.PhotoUrl != null)
        this.employeeInfo.PhotoUrl += "?q=" + new Date().getMilliseconds();
    })
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
    
    this.service.editEmployee(form.value.Photo, emp).subscribe(res => {
      this.modalReference.close();
      this.employeeInfo.PhotoUrl += "&p=" + new Date().getMilliseconds();
      this.cd.markForCheck();
      this.cd.detectChanges();
    })
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

open(content) {
  this.modalReference = this.modalService.open(content)
  this.modalReference.result.then((result) => {
    //this.closeResult = `Closed with: ${result}`;
  }, (reason) => {
   // this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
  });
}

}
