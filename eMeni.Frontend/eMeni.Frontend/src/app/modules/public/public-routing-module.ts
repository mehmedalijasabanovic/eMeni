import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicLayout } from './public-layout/public-layout';
import { MenusComponent } from './menus/menus.component';


const routes: Routes = [
  {
    path: '',
    component: PublicLayout,
  },
  {
    path: 'menus',
    component: MenusComponent,
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
