import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestwithFlemmingComponent } from './testwith-flemming.component';

describe('TestwithFlemmingComponent', () => {
  let component: TestwithFlemmingComponent;
  let fixture: ComponentFixture<TestwithFlemmingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestwithFlemmingComponent]
    });
    fixture = TestBed.createComponent(TestwithFlemmingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
