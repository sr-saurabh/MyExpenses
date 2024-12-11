import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'personal-expense',
    loadChildren: () =>
      import('./modules/personal-expenses/personal-expenses.module').then(
        (m) => m.PersonalExpensesModule
      ),
  },
  {
    path: 'groups',
    loadChildren: () =>
      import('./modules/groups/groups.module').then((m) => m.GroupsModule),
  },
  {
    path:'',
    redirectTo:'login',
    pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
