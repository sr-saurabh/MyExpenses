import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() changeIsLogin: EventEmitter<boolean> = new EventEmitter<boolean>();

  ngOnInit(): void {

  }

  onRegister(){
    this.changeIsLogin.emit(true);
  }
}
