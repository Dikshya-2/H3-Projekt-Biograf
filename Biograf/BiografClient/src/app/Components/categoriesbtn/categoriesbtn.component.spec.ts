import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoriesbtnComponent } from './categoriesbtn.component';

describe('CategoriesbtnComponent', () => {
  let component: CategoriesbtnComponent;
  let fixture: ComponentFixture<CategoriesbtnComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CategoriesbtnComponent]
    });
    fixture = TestBed.createComponent(CategoriesbtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
