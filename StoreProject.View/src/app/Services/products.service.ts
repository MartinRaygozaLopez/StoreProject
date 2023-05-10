import { Injectable } from '@angular/core';
import { ProductsInterface } from '../Interface/ProductsInterface';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(
    private http: HttpClient
  ) { }

  getAllProducts(): Observable<ProductsInterface[]> {
    return this.http.get<ProductsInterface[]>(environment.API + 'product/GetAllProducts');
  }

  getProductsByAvailable(): Observable<ProductsInterface[]> {
    return this.http.get<ProductsInterface[]>(environment.API + 'product/GetProductsByAvailable/' + true);
  }

  createProduct(product: ProductsInterface): Observable<any> {
    return this.http.post(environment.API + 'product/CreateProduct', product);
  }

  updateProduct(product: ProductsInterface): Observable<any> {   
    return this.http.put(environment.API + 'product/UpdateProduct', product);
  }
}
