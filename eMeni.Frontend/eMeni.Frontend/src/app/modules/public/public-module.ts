import {NgModule} from '@angular/core';
import {SharedModule} from '../shared/shared-module';
import {PublicRoutingModule} from './public-routing-module';
import {PublicLayout} from './public-layout/public-layout';
import {CommonModule} from '@angular/common';
import {TranslatePipe} from '@ngx-translate/core';
import {RouterModule} from '@angular/router';




@NgModule({
  declarations: [
    PublicLayout,
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    RouterModule,
    SharedModule,
    TranslatePipe,
  ]
})
export class PublicModule { }
