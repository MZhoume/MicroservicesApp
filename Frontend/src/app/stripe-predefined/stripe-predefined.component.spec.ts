/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StripePredefinedComponent } from './stripe-predefined.component';

describe('StripePredefinedComponent', () => {
  let component: StripePredefinedComponent;
  let fixture: ComponentFixture<StripePredefinedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StripePredefinedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StripePredefinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
