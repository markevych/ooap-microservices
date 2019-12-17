import { NgModule } from '@angular/core';

import { TabMenuModule } from 'primeng/tabmenu';

import { ResultsComponent } from './results.component';

import { CommonModule } from '@angular/common';
import { TestRoutingModule } from './results-routing.mudule';
import { TableModule } from 'primeng/table';

@NgModule({
  declarations: [
    ResultsComponent
  ],
  imports: [
    CommonModule,
    TestRoutingModule,
    TabMenuModule,
    TableModule
  ],
  providers: []
})
export class ResultsModule { }
