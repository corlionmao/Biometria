import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CapturarComponent } from './capturar/capturar.component';
import { CompareComponent } from './compare/compare.component';



@NgModule({
  declarations: [CapturarComponent, CompareComponent],
  imports: [
    CommonModule
  ],
  exports: [
    CapturarComponent
  ]
})
export class FingerprintModule { }
