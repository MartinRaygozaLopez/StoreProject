import { Component, OnInit } from '@angular/core';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';
import { ProductsService } from 'src/app/Services/products.service';
import {ConfirmationService} from 'primeng/api';
import { CartService } from 'src/app/Services/cart.service';
import { ProductsCartInterface } from 'src/app/Interface/ProductsCartInterface';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  products: ProductsInterface[] = [];
  sidebarVisible: boolean = true;
  cart: ProductsCartInterface[] = [];

  constructor(
    private productsService: ProductsService,
    private cartService: CartService) { 

    }

  ngOnInit(): void {
    this.readProducts();
  }

  readProducts() {
    this.productsService.getProductsByAvailable().subscribe((response: any) => {
      if (response.data.length > 0) {
        this.products = response.data;
      }
    });
  }

  addCart(event) {
    this.cart.push(event);

    this.cartService.setListenerCart(this.cart);
  }
}
