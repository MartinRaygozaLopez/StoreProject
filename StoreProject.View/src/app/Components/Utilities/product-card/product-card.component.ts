import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ProductsCartInterface } from 'src/app/Interface/ProductsCartInterface';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';
import { CartService } from 'src/app/Services/cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
  providers: [MessageService]
})
export class ProductCardComponent implements OnInit {
  @Input() product: ProductsInterface;
  numberCart: number = 1;

  @Output() onAddCart = new EventEmitter<ProductsCartInterface>();
  
  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
  }

  addNumber() {
    this.numberCart += 1;
  }

  subtractNumber() {
    if(this.numberCart != 1)
      this.numberCart -= 1;
  }

  addCart() {
    let cart: ProductsCartInterface = {
      idProduct: this.product.idProduct,
      code: this.product.code,
      price: this.product.price,
      description: this.product.description,
      image: this.product.image,
      qty: this.numberCart
    };

    this.onAddCart.emit(cart);

    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Product Added' });
  }
}
