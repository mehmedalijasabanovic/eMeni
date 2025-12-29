import {NgModule} from '@angular/core';

import {PublicRoutingModule} from './public-routing-module';
import {PublicLayoutComponent} from './public-layout/public-layout.component';



@NgModule({
  declarations: [
    PublicLayoutComponent,

  ],
  imports: [
    PublicRoutingModule,
  ]
})
export class PublicModule { }
