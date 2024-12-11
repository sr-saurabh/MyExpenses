import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonalExpenseComponent } from './components/personal-expense/personal-expense.component';
import { PersonalExpensesRoutingModule } from './personal-expenses-routing.module';



@NgModule({
  declarations: [
    PersonalExpenseComponent
  ],
  imports: [
    CommonModule,
    PersonalExpensesRoutingModule
  ]
})
export class PersonalExpensesModule { }
