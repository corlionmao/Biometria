import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CameraModule } from './camera/camera.module';
import { FingerprintModule } from './fingerprint/fingerprint.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CameraModule,
    FingerprintModule
  ],
  exports: [CameraModule, FingerprintModule]
})
export class ComponentsModule { }
