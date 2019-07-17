import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  togglebtn: boolean;
  constructor() {
    this.togglebtn = false;
  }
  ngOnInit() {
  }
  toggleFun() {
    this.togglebtn = !this.togglebtn;
    return this.togglebtn;
  }

}
