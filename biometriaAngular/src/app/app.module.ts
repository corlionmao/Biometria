import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { ComponentsModule } from './components/components.module';
import { ImagenService } from './components/camera/imagen/imagen.service';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ImagenComponent } from './components/camera/imagen/imagen.component';
import { CapturarComponent } from './components/fingerprint/capturar/capturar.component';
import { CompareComponent } from './components/fingerprint/compare/compare.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent
  ],
  imports: [
    BrowserModule,
    ComponentsModule,
    HttpClientModule,
    AppRoutingModule,
    RouterModule.forRoot([
      {path: 'camara', component: ImagenComponent},
      {path: 'capturar', component: CapturarComponent},
      {path: 'compare', component: CompareComponent},
    ]),
  ],
  providers: [ImagenService],
  bootstrap: [AppComponent]
})
export class AppModule { }
