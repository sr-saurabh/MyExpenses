import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonalExpenseComponent } from './components/personal-expense/personal-expense.component';
import { authGuard } from 'src/app/auth.guard';

const routes: Routes = [{ path: '', component: PersonalExpenseComponent, canActivate: [authGuard] }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonalExpensesRoutingModule { }
