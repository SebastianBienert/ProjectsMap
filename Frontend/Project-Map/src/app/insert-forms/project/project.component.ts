import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  moduleId: module.id,
  selector: 'form-project',
  templateUrl: './project.component.html'
})

export class ProjectComponent implements OnInit {
  formAddProject : FormGroup;
  
  formErrors = {
    Description: '',
    RepositoryLink: '',
    DocumentationLink: '',
    ProductOwner: '',
    Company: '',
    CompanyId: ''
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
    ProductOwner: {
      required: 'Product Owner cannot be empty'
    },
    Company: {
      required: 'Company cannot be empty'
    },
    CompanyId: {
      required: 'Company ID cannot be empty'
    }
  }
  
  constructor(private formBuilder : FormBuilder) {}
  
  ngOnInit() {
    this.formAddProject = this.formBuilder.group({
     Description: ['', Validators.required],
     RepositoryLink: ['', Validators.required],
     DocumentationLink: ['', Validators.required],
     ProductOwner: ['', Validators.required],
     Company: ['', Validators.required],
     CompanyId: ['', Validators.required]
   });
   
   this.formAddProject.valueChanges.debounceTime(500).subscribe((value) => {
    this.onControlValueChanged();
   });
   this.onControlValueChanged();
  }

  onSubmit(form) {
    console.log(form);
  }
  
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
