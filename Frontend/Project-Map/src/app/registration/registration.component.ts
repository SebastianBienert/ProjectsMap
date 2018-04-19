import { Component, OnInit, OnChanges, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray, FormControl } from '@angular/forms';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../common-interfaces/employee';
import { Technology } from '../common-interfaces/technology';
import { TechnologyService } from '../services/technology.service';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { SecurityService } from '../security/security.service';

@Component({
  moduleId: module.id,
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  providers: [ EmployeeService ],
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  formRegistration : FormGroup;
  formErrors = {
    Email: '',
    Username: '',
    Password: '',
    PasswordConfirm: '',
    DeveloperId: '',
    FirstName: '',
    Surname: ''
  }

  private validationMessages = {
    Email: {
      required: 'E-mail cannot be empty'
    },
    Username: {
      required: 'Username cannot be empty'
    },
    Password: {
      required: 'Password cannot be empty'
    },
    PasswordConfirm: {
      required: 'Password confirmation cannot be empty'
    },
    DeveloperId: {
      required: 'Developer ID cannot be empty'
    },
    FirstName: {
      required: 'First Name cannot be empty'
    },
    Surname: {
      required: 'Surname cannot be empty'
    }
  }
  constructor(private formBuilder : FormBuilder,
    private employeeService : EmployeeService,
    private securityService: SecurityService,
   private cd: ChangeDetectorRef) { }

  ngOnInit() {
    this.formRegistration = this.formBuilder.group({
      Email: ['', Validators.required],
      Username: ['', Validators.required],
      Password: ['', Validators.required],
      PasswordConfirm: ['', Validators.required],
      DeveloperId: ['', Validators.required],
      FirstName: ['', Validators.required],
      Surname: ['', Validators.required],
    });
    this.formRegistration.valueChanges.debounceTime(500).subscribe((value) => {
      this.onControlValueChanged();
     });
     this.onControlValueChanged();
  }

  onControlValueChanged() : void {
    const form = this.formRegistration;
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

  onSubmit(form) {
    console.log(form);
    // const formModel = this.formRegistration.value;
    var user = {
      Email : this.formRegistration.get('Email').value,
      Username : this.formRegistration.get('Username').value,
      Password : this.formRegistration.get('Password').value,
      ConfirmPassword : this.formRegistration.get('PasswordConfirm').value,
      DeveloperId : this.formRegistration.get('DeveloperId').value,
      FirstName : this.formRegistration.get('FirstName').value,
      Lastname : this.formRegistration.get('Surname').value,
    };

    var registerAccount = {
      Email : this.formRegistration.get('Email').value,
      Username : this.formRegistration.get('Username').value,
      Lastname : this.formRegistration.get('Surname').value,
      Password : this.formRegistration.get('Password').value,
      ConfirmPassword : this.formRegistration.get('PasswordConfirm').value,
    }
    console.log(user);
    this.securityService.register(user).subscribe(response =>{
      console.log(response);
    })

  }



}
