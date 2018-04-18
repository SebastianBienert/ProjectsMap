import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditEmployeeDataComponent } from './edit-employee-data.component';

describe('EditEmployeeDataComponent', () => {
  let component: EditEmployeeDataComponent;
  let fixture: ComponentFixture<EditEmployeeDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditEmployeeDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditEmployeeDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
