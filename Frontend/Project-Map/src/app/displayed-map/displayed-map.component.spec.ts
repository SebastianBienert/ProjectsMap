import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DisplayedMapComponent } from './displayed-map.component';

describe('DisplayedMapComponent', () => {
  let component: DisplayedMapComponent;
  let fixture: ComponentFixture<DisplayedMapComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DisplayedMapComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DisplayedMapComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
