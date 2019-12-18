import { Component, OnInit } from '@angular/core';

import { MenuItem } from 'primeng/api';

import { CarService, Car } from 'src/core/services/car.service';
import { DiaryService } from 'src/core/services/results/studnentResults.service';
import { StudentResult } from 'src/core/services/results/studentResult.model';
import { User } from 'src/core/models';
import { UserService } from 'src/core/services/auth/user.service';

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

  public results: StudentResult[] = [];
  public user: User;

  constructor(
    private carService: CarService,
    private diaryService: DiaryService,
    private userService: UserService) { }

  public ngOnInit(): void {
    this.user = this.userService.getUserFromLocalStorage();
    this.uploadResults();

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

  public uploadResults(): void {
    this.diaryService.getResults()
      .subscribe(result => {
        this.results = result;
        this.rebuildMenuItems();
      });
  }

  private rebuildMenuItems(): void {
    this.items = [];
    this.carItems = Object.keys(this.results[0]);
  }
}
