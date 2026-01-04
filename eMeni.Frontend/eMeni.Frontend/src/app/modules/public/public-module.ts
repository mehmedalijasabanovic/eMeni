import {NgModule} from '@angular/core';

import {PublicRoutingModule} from './public-routing-module';
import {PublicLayout} from './public-layout/public-layout';
import {CommonModule} from '@angular/common';
import {MatTable} from '@angular/material/table';
import {MatIcon} from '@angular/material/icon';
import {MatMenu, MatMenuItem, MatMenuTrigger} from '@angular/material/menu';
import {MatIconButton, MatButton} from '@angular/material/button';
import {MatToolbar} from '@angular/material/toolbar';
import {MatCard, MatCardContent, MatCardActions} from '@angular/material/card';
import {MatDivider} from '@angular/material/divider';
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
    MatTable,
    MatIcon,
    MatMenu,
    MatMenuTrigger,
    MatIconButton,
    MatButton,
    MatToolbar,
    MatCard,
    MatCardContent,
    MatCardActions,
    MatDivider,
    MatMenuItem,
    TranslatePipe,
  ]
})
export class PublicModule { }
