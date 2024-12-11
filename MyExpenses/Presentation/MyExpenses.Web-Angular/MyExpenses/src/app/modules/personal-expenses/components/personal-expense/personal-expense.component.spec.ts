import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PersonalExpenseComponent } from './personal-expense.component';

describe('PersonalExpenseComponent', () => {
  let component: PersonalExpenseComponent;
  let fixture: ComponentFixture<PersonalExpenseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PersonalExpenseComponent]
    });
    fixture = TestBed.createComponent(PersonalExpenseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
