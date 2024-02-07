import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ImagenComponent } from './components/camera/imagen/imagen.component';
import { CapturarComponent } from './components/fingerprint/capturar/capturar.component';
import { CompareComponent } from './components/fingerprint/compare/compare.component';

const APP_ROUTES: Routes = [
  {path: 'camara', component: ImagenComponent},
  {path: 'capturar', component: CapturarComponent},
  {path: 'compare', component: CompareComponent},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AppRoutingModule { }
