import { Component, OnInit } from '@angular/core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'app-mainPage',
  templateUrl: './mainPage.component.html',
  styleUrls: ['./mainPage.component.css']
})
export class MainPageComponent implements OnInit {
  netAppMode = false;

  constructor() { }

  ngOnInit() {
  }

  netAppToggle() {
    this.netAppMode = true;
  }

}
