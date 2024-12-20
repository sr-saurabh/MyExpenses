import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/services/shared.service';

@Component({
  selector: 'app-personal-expense',
  templateUrl: './personal-expense.component.html',
  styleUrls: ['./personal-expense.component.scss'],
})
export class PersonalExpenseComponent implements OnInit {

  searchText='';
  constructor(private sharedService: SharedService) {}


  ngOnInit(): void {
    this.sharedService.currentData.subscribe((searchText:string)=>{
      this.searchText=searchText;
    })
  }
}
