import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { ProductsCartInterface } from 'src/app/Interface/ProductsCartInterface';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cart = new ReplaySubject<ProductsCartInterface[]>(1);
  listenercart$ = this.cart.asObservable();

  constructor() { }

  setListenerCart(cart: ProductsCartInterface[]) {
    
    this.cart.next(cart);
  }
}
