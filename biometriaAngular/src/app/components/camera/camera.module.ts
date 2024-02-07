import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SnapshotComponent } from './snapshot/snapshot.component';
import { VideoComponent } from './video/video.component';
import { ImagenComponent } from './imagen/imagen.component';
import {WebcamModule} from 'ngx-webcam';


@NgModule({
  declarations: [SnapshotComponent, VideoComponent, ImagenComponent],
  imports: [
    CommonModule,
    WebcamModule
  ],
  exports: [
    ImagenComponent,
    SnapshotComponent,
    VideoComponent
  ]
})
export class CameraModule { }
