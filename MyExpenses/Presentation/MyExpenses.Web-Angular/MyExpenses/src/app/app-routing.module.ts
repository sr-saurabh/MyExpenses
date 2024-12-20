import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './auth.guard';
import { LoginComponent } from './modules/auth/components/login/login.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('./modules/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'personal-expense',
    loadChildren: () =>
      import('./modules/personal-expenses/personal-expenses.module').then(
        (m) => m.PersonalExpensesModule
      ),
      canActivate:[authGuard]
  },
  {
    path: 'groups',
    loadChildren: () =>
      import('./modules/groups/groups.module').then((m) => m.GroupsModule),
    canActivate:[authGuard]
  },
  {
    path:'',
    redirectTo:'auth',
    pathMatch:'prefix'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
