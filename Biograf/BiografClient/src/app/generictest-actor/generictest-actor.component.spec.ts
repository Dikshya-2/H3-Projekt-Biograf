import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerictestActorComponent } from './generictest-actor.component';

describe('GenerictestActorComponent', () => {
  let component: GenerictestActorComponent;
  let fixture: ComponentFixture<GenerictestActorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GenerictestActorComponent]
    });
    fixture = TestBed.createComponent(GenerictestActorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
