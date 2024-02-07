import { Component, OnInit } from '@angular/core';
import {Subject, Observable} from 'rxjs';
import {WebcamImage, WebcamInitError, WebcamUtil} from 'ngx-webcam';
import { ImagenService } from 'src/app/components/camera/imagen/imagen.service';
import { Imagen } from 'src/app/core/interfaces/imagen.interface';

@Component({
  selector: 'app-imagen',
  templateUrl: './imagen.component.html',
  styleUrls: ['./imagen.component.css']
})
export class ImagenComponent implements OnInit {
  // toggle webcam on/off
  public showWebcam = true;
  public allowCameraSwitch = true;
  public multipleWebcamsAvailable = false;
  public deviceId: string;
  public videoOptions: MediaTrackConstraints = {
    // width: {ideal: 1024},
    // height: {ideal: 576}
  };
  public errors: WebcamInitError[] = [];

  // latest snapshot
  public webcamImage: WebcamImage = null;

  // webcam snapshot trigger
  private trigger: Subject<void> = new Subject<void>();
  // switch to next / previous / specific webcam; true/false: forward/backwards, string: deviceId
  private nextWebcam: Subject<boolean|string> = new Subject<boolean|string>();
  ImagenData: Imagen;

  constructor(private imagenService: ImagenService) { }

  public ngOnInit(): void {
    WebcamUtil.getAvailableVideoInputs()
      .then((mediaDevices: MediaDeviceInfo[]) => {
        this.multipleWebcamsAvailable = mediaDevices && mediaDevices.length > 1;
      });
  }

  // public callServiceImage(): void{
  //   this.imagenService.get().subscribe(data => {
  //     console.log('OK Imagenes', data);
  //    // this.daysData = data;
  //    // Object.assign(this.ImagenesData, data);
  //     console.log(this.ImagenesData);
  //   });
  // }

  public triggerSnapshot(): void {
    this.trigger.next();
  }

  public toggleWebcam(): void {
    this.showWebcam = !this.showWebcam;
  }

  public handleInitError(error: WebcamInitError): void {
    this.errors.push(error);
  }

  public showNextWebcam(directionOrDeviceId: boolean|string): void {
    // true => move forward through devices
    // false => move backwards through devices
    // string => move to device with given deviceId
    this.nextWebcam.next(directionOrDeviceId);
  }

  public handleImage(webcamImage: WebcamImage): void {
    console.log('received webcam image', webcamImage);
    this.webcamImage = webcamImage;
    // console.log('Image Base64', this.webcamImage.imageAsBase64);
    // this.ImagenData.imagenAsbase64 = this.webcamImage.imageAsBase64;
    // this.ImagenData.imagenDataUrl = this.webcamImage.imageAsDataUrl;
    // this.ImagenData.numint = '1234';
    this.imagenService.crearImagen({
      imagenAsbase64: this.webcamImage.imageAsBase64,
      imagenDataUrl: this.webcamImage.imageAsDataUrl,
      numint: '104',
      tipo: '1'
    }).subscribe(data => {
      console.log('Resultado POST', data);
    });
    // this.imagenService.create(this.imagenData);
  }

  public cameraWasSwitched(deviceId: string): void {
    console.log('active device: ' + deviceId);
    this.deviceId = deviceId;
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  public get nextWebcamObservable(): Observable<boolean|string> {
    return this.nextWebcam.asObservable();
  }

  public showImages(): void{
    this.imagenService.getImages()
      .subscribe((data: Imagen  ) => this.ImagenData = {
          imagenAsbase64: data.imagenAsbase64,
          imagenDataUrl:  data.imagenDataUrl,
          numint:  data.numint,
          tipo:  data.tipo,
      });
  }
  public showImages2(): void{
    this.imagenService.getImagen('10201010').subscribe(data => {
      this.ImagenData = data;
      console.log('Datica', data);
    });
  }
}
