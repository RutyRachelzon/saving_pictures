import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewPictureComponent } from './components/new-picture/new-picture.component';
import { SavingPicturesHomePageComponent } from './components/saving-pictures-home-page/saving-pictures-home-page.component';

const routes: Routes = [
  { path: '', component: SavingPicturesHomePageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
