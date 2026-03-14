import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicLayout } from './public-layout/public-layout';
import { BusinessesComponent } from './businesses/businesses.component';


const routes: Routes = [
  {
    path: '',
    component: PublicLayout,
  },
  {
    path: 'businesses',
    component: BusinessesComponent,
  },
  {
    path: 'businesses/:categorySlug',
    component: BusinessesComponent,
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
