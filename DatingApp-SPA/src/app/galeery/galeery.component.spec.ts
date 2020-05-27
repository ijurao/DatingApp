import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GaleeryComponent } from './galeery.component';

describe('GaleeryComponent', () => {
  let component: GaleeryComponent;
  let fixture: ComponentFixture<GaleeryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GaleeryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GaleeryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
