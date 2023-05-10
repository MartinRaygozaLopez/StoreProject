import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';
import { CartService } from 'src/app/Services/cart.service';
import { ProductsCartInterface } from 'src/app/Interface/ProductsCartInterface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  productsCart: ProductsCartInterface[] = [];

  constructor(
    private router: Router,
    private cartService: CartService
  ) { }

  ngOnInit(): void {
    this.cartService.listenercart$.subscribe(response => {
      this.productsCart = response;
    });
  }
  
  fnDeleteProduct(idProduct: number) {
    this.productsCart = this.productsCart.filter(x => x.idProduct != idProduct);
    console.log(this.productsCart);
    this.cartService.setListenerCart(this.productsCart);
  }

  fnDeleteCart() {
    this.cartService.setListenerCart([]);
  }

  redirectView(url: string) {
    this.router.navigate([url]);
  }
}
