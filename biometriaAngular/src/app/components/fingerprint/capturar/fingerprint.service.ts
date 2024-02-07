import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FingerprintService {
  configUrl = '/CallMorphoAPI';
  configApiUrl = 'http://localhost:6296/api/fingerprint'

  constructor(private http: HttpClient) { }

  leerHuella(huella: any): Observable<any>{
     return this.http.post(this.configUrl, huella);
  }
  crearRegistroHuella(huella: any): Observable<any>{
    console.log('crearRegistroHuella  Base64', huella);
    return this.http.post(this.configApiUrl, huella);
  }
  obtenerRegistroHuella(numint: string): Observable<any>{
    return this.http.get(this.configApiUrl + '/' + numint);
  }

}
