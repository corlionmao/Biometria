import { Component, OnInit } from '@angular/core';
import { FingerprintService } from 'src/app/components/fingerprint/capturar/fingerprint.service';
import { Huella } from '../../../core/interfaces/huella.interface';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-capturar',
  templateUrl: './capturar.component.html',
  styleUrls: ['./capturar.component.css']
})
export class CapturarComponent implements OnInit {

  fingerprintData: Huella;
  dataHuella: any; // {};
  timeout = 8;
  constructor(private fingerprintService: FingerprintService, private http: HttpClient) { }

  ngOnInit(): void {


  }

  public registrarHuella(): void{
    this.http.post('http://127.0.0.1:8080/CallMorphoAPI?' + this.timeout, this.dataHuella).subscribe(data => this.dataHuella = data);
    // tampoco funciono
    // this.fingerprintService.leerHuella({
    // }).subscribe(data => {
    //   console.log('Resultado Huella', data);
    // });

    // no funciono
    // this.fingerprintService.leerHuella()
    //   .subscribe((data: Huella  ) => this.fingerprintData = {
    //     Base64ISOTemplate: data.isoTemplate,
    //       imagenDataUrl:  data.imagenDataUrl,
    //       numint:  data.numint,
    //       tipo:  data.tipo,
    //   });
  }

  public enviarHuella(): void{
    this.fingerprintService.crearRegistroHuella({
      numint: '1234',
      dedo: 1,
      mano: '1',
      isoTemplate:  this.dataHuella.Base64ISOTemplate,
      ImagenDedo: this.dataHuella.Base64BMPIMage,
      rawImage: '',
    }).subscribe(data => {
      console.log('Resultado POST', data);
    });
  }

}
