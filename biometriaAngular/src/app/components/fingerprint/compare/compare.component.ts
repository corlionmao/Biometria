import { Component, OnInit } from '@angular/core';
import { FingerprintService } from '../capturar/fingerprint.service';
import { HttpClient } from '@angular/common/http';
import { Huella } from '../../../core/interfaces/huella.interface';

import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-compare',
  templateUrl: './compare.component.html',
  styleUrls: ['./compare.component.css']
})
export class CompareComponent implements OnInit {

  fingerprintData: Huella;
  dataHuella: any; // {};
  templatesArray: string[];
  numeroTemplates = 1;
  timeout = 8;
  resultadoCompare: any;
  imagenHuella1: any;

  dataHuellaToTest: any;
  fingerprintDataToTest: Huella;

  constructor(private fingerprintService: FingerprintService,
              private http: HttpClient,
              private sanitizer: DomSanitizer) { }

  ngOnInit(): void {
    // leer de la base de datos
    this.consultarHuella();
    // Leer hueela del dispositivo
    this.leerHuella();
  }

  // var templatesArray = [1];
  // templatesArray[0] = template;
  // alert("templatearray1: "+ templatesArray[0]);
  // var numberOfTemplates = 1;
  // xmlhttp.open("POST",url+"?"+templatesArray+"$"+template+"$"+numberOfTemplates,true);

  public compareHuella(): void{
    // console.log('template ISOT', this.fingerprintData.isoTemplate);
    this.templatesArray = [this.fingerprintData.isoTemplate];
    // console.log('Templates array', this.templatesArray);
    // console.log('Template to test', this.dataHuellaToTest.Base64ISOTemplate);
    // this.templatesArray[1] = '';
    this.http.post('http://127.0.0.1:8080/CompareTemplates' + '?' + this.templatesArray + '$' +
    this.dataHuellaToTest.Base64ISOTemplate + '$' + this.numeroTemplates, this.dataHuella)
    .subscribe(data => {
      this.resultadoCompare = data;
      console.log('Resultado validar -->', data );
    });
  }

  public consultarHuella(): void{
    // console.log(this.dataHuella);
    this.fingerprintService.obtenerRegistroHuella('1234').subscribe(data => {
    //  console.log('Retorno huella', data);
      this.fingerprintData = data;
      console.log('Objeto base de datos -->', this.fingerprintData);
      // console.log('Imagen dedo -->', this.fingerprintData.imagenDedo);
    });
  }

  public leerHuella(): void{
    this.http.post('http://127.0.0.1:8080/CallMorphoAPI?' + this.timeout, this.dataHuellaToTest)
        .subscribe(data => {
          this.dataHuellaToTest = data;
          console.log('Huella para validar -->', this.dataHuellaToTest );
        });
  }

  public resultado(): void{
    console.log('Resultado Compare:', this.dataHuella);
  }

  // tslint:disable-next-line:typedef
  mostrarHuellaBD(){
    console.log('imagen dedo:', this.fingerprintData.imagenDedo);
    return this.sanitizer.bypassSecurityTrustResourceUrl('data:image/bmp;base64,' + this.fingerprintData.imagenDedo);
  }

}
