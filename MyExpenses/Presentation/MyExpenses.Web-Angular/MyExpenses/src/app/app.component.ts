import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'MyExpenses';
  isAuthenticated:boolean=false;
  returnUrl: string | null = null;

  constructor(private authService:AuthService, private route:ActivatedRoute, private router:Router) {
  }
  ngOnInit(): void {
    this.isAuthenticated = !!localStorage.getItem('token');
    this.navigate(this.isAuthenticated);
  }


  navigate(isAuthenticated: boolean){
    this.route.queryParams.subscribe((params) => {
      this.returnUrl = params['returnUrl'];
    });
    if(isAuthenticated)
    {
      this.router.navigate([`/${this.returnUrl??'personal-expense'}`])
    }
    else{
      this.router.navigate(['/auth'],{queryParams:{'returnUrl':this.returnUrl}})
    }
    console.log("isAuthenticated", isAuthenticated)
  }

}
