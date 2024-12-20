import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-wrapper',
  templateUrl: './wrapper.component.html',
  styleUrls: ['./wrapper.component.scss']
})
export class WrapperComponent implements OnInit {
  sideNavItem: string[] = ['My Expense', 'Groups', 'Friends'];
  activeIndex = 0;
  searchText: string = '';
  isAuthorized:boolean=false;

  constructor(private sharedService: SharedService, private router:Router, private authService:AuthService) {}

  ngOnInit(): void {
      this.isAuthorized=this.authService.isAuthorized();
  }

  itemSelected(index: number) {
    this.activeIndex = index;
  }

  emitSearchValue() {
    if (!!this.searchText) {
      this.sharedService.updateData(this.searchText);
    }
  }

  logout()
  {
    localStorage.removeItem('token');
    // this.router.navigateByUrl('/auth/login');
    // this.router.navigate(['/auth/login']);
    this.router.navigate(['']).then(success => {
      console.log('Navigation Successful:', success);
    }).catch(err => {
      console.error('Navigation Error:', err);
    });
  }
}
