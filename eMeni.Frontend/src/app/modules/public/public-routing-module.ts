import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicLayout } from './public-layout/public-layout';
import { BusinessesComponent } from './businesses/businesses.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { myAuthGuard } from '../../core/guards/my-auth-guard';


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
  },
  {
    path: 'profile',
    component: UserProfileComponent,
    canActivate: [myAuthGuard],
    data: { requireAuth: true }
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
