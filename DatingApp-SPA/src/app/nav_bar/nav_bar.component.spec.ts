/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { Nav_barComponent } from './nav_bar.component';

describe('Nav_barComponent', () => {
  let component: Nav_barComponent;
  let fixture: ComponentFixture<Nav_barComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Nav_barComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Nav_barComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
