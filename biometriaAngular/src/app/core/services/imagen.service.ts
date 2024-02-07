import { Injectable } from '@angular/core';
import { Imagen } from 'src/app/core/interfaces/imagen.interface';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ImagenService  {

  configUrl = 'assets/config.json';

  constructor() {}



}
