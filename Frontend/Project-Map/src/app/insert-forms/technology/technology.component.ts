import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  moduleId: module.id,
  selector: 'form-technology',
  templateUrl: './technology.component.html'
})

export class TechnologyComponent implements OnInit {
  formAddTechnology : FormGroup;
  
  formErrors = {
    TechnologyId: '',
    Name: ''
  }

  private validationMessages = {
    TechnologyId: {
      required: 'Technology ID cannot be empty'
    },
    Name: {
      required: 'Technology Name cannot be empty'
    }
  }
  
  constructor(private formBuilder : FormBuilder) {}
  
  ngOnInit() {
    this.formAddTechnology = this.formBuilder.group({
     TechnologyId: ['', Validators.required],
     Name: ['', Validators.required]
   });
   
   this.formAddTechnology.valueChanges.debounceTime(500).subscribe((value) => {
    this.onControlValueChanged();
   });
   this.onControlValueChanged();
  }

  onSubmit(form) {
    console.log(form);
  }
  
  onControlValueChanged() : void {
  const form = this.formAddTechnology;

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
