import { NgModule } from '@angular/core';

import { TestComponent } from './test.component';
import { CommonModule } from '@angular/common';
import { TestRoutingModule } from './test-routing.mudule';

@NgModule({
  declarations: [
    TestComponent
  ],
  imports: [
    CommonModule,
    TestRoutingModule
  ],
  providers: []
})
export class TestModule { }
