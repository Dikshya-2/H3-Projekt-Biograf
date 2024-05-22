import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RandomovieComponent } from './randomovie.component';

describe('RandomovieComponent', () => {
  let component: RandomovieComponent;
  let fixture: ComponentFixture<RandomovieComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RandomovieComponent]
    });
    fixture = TestBed.createComponent(RandomovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
