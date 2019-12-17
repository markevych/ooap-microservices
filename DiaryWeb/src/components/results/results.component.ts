import { Component, OnInit } from '@angular/core';

import { MenuItem } from 'primeng/api';

import { CarService, Car } from 'src/core/services/car.service';

@Component({
  selector: 'app-test',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.styl']
})
export class ResultsComponent implements OnInit {
  public items: MenuItem[];
  public cars: Car[];
  public carItems: string[];
  public activeItem: MenuItem;

  constructor(private carService: CarService) { }

  public ngOnInit(): void {
      this.items = [
          {label: 'Stats', icon: 'fa fa-fw fa-bar-chart'},
          {label: 'Calendar', icon: 'fa fa-fw fa-calendar'},
          {label: 'Documentation', icon: 'fa fa-fw fa-book'},
          {label: 'Support', icon: 'fa fa-fw fa-support'},
          {label: 'Social', icon: 'fa fa-fw fa-twitter'}
      ];

      this.activeItem = this.items[2];

      let date: Date = new Date(Date.now());
      this.cars = this.carService.buildTestData(date);
      this.carItems = Object.keys(this.cars[0]);
  }
}
