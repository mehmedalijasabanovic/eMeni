import {NgModule} from '@angular/core';
import {SharedModule} from '../shared/shared-module';
import {PublicRoutingModule} from './public-routing-module';
import {PublicLayout} from './public-layout/public-layout';
import {BusinessesComponent} from './businesses/businesses.component';
import {CommonModule} from '@angular/common';
import {TranslatePipe} from '@ngx-translate/core';
import {RouterModule} from '@angular/router';
import { MenusItems } from './menus/menus-items/menus-items';
import { UserProfileComponent } from './user-profile/user-profile.component';




@NgModule({
  declarations: [
    PublicLayout,
    BusinessesComponent,
    MenusItems,
    UserProfileComponent,
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
