import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IBrand } from '../models/brand';
import { IPagination } from '../models/pagination';
import { IType } from '../models/type';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7023/api/';

  constructor(private http: HttpClient) { }

  getProducts(brandId?: number, typeId?: number) {
    let params = new HttpParams();

    if(brandId) {
      params = params.append('brandId', brandId.toString());
    }

    if(typeId) {
      params = params.append('typeid', typeId.toString());
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {params: params, observe: "body"});
    // .pipe(delay(1000), map(response => {
    //   return response.body
    // }));
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
