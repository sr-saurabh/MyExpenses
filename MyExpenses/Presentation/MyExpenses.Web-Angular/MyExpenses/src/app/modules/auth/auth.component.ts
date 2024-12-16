import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {

  isLogin:boolean=true;


  changeInLogin(event: any){
    console.log(event)
    this.isLogin=event;
  }
}
