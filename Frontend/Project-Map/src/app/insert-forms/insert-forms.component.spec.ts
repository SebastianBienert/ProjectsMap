import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertFormsComponent } from './insert-forms.component';

describe('InsertFormsComponent', () => {
  let component: InsertFormsComponent;
  let fixture: ComponentFixture<InsertFormsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InsertFormsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InsertFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
