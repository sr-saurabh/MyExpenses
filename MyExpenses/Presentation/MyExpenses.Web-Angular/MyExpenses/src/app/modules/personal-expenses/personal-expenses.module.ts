import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonalExpenseComponent } from './components/personal-expense/personal-expense.component';
import { PersonalExpensesRoutingModule } from './personal-expenses-routing.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    PersonalExpenseComponent
  ],
  imports: [
    CommonModule,
    PersonalExpensesRoutingModule,
    FormsModule,
  ]
})
export class PersonalExpensesModule { }
