import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonalExpenseComponent } from './components/personal-expense/personal-expense.component';

const routes: Routes = [{ path: '', component: PersonalExpenseComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonalExpensesRoutingModule { }
