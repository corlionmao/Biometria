import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Imagen } from 'src/app/core/interfaces/imagen.interface';

// export interface Imagen  {
//   numint: string;
//   tipo: number ;
//   imagenDataUrl: string;
//   imagenAsbase64: string;
// }

@Injectable({
  providedIn: 'root'
})
export class ImagenService {

  configUrl = 'http://localhost:6296/api/Imagen';

  constructor(private http: HttpClient) { }

  getImages() {
    return this.http.get(this.configUrl);
    // return this.http.get(this.configUrl).subscribe(data => {
    //   console.log(data);
    // });
  }
  getImagen(numint: string): Observable<any>{
    return this.http.get(this.configUrl + '/' + numint);
  }
  getImg_2() {
    // now returns an Observable of Config
    return this.http.get<Imagen>(this.configUrl);
  }
  getImages3() {
    this.http.get(this.configUrl).subscribe(data => {
      console.log(data);
    });
  }
  crearImagen(imagen: any): Observable<any>{
    console.log('Sending image Base64', imagen);
    return this.http.post(this.configUrl, imagen);
  }


}
