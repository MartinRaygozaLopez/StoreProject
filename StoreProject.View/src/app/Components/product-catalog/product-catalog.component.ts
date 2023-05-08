import { Component, OnInit } from '@angular/core';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';
import { ProductsService } from 'src/app/Services/products.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-product-catalog',
  templateUrl: './product-catalog.component.html',
  styleUrls: ['./product-catalog.component.scss'],
  providers: [MessageService]
})
export class ProductCatalogComponent implements OnInit {
  products: ProductsInterface[] = [];
  cols: any[] = [];
  clonedProducts: { [s: string]: ProductsInterface; } = {};

  constructor(
    private productsService: ProductsService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.productsService.getAllProducts().subscribe((response: any) => {
      this.products = response.data;
      Object.keys(response.data[0]).filter(u => u.toLowerCase() !== 'idproduct' && u.toLowerCase() !== 'customerproducts' && u.toLowerCase() !== 'productstores').forEach(key => {
        this.cols.push({ header: key.trim(), field: key.trim() });
      });
    });
  }

  openNew() {
  }

  onRowEditInit(product: ProductsInterface) {
    this.clonedProducts[product.idProduct] = { ...product };
  }

  onRowEditSave(product: ProductsInterface) {
    if (product.price > 0) {
      this.productsService.updateProduct(product).subscribe(response => {
        delete this.clonedProducts[product.idProduct];
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Product is updated' });
      });

    } else {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Invalid Price' });
    }
  }

  onRowEditCancel(product: ProductsInterface, index: number) {
    this.products[index] = this.clonedProducts[product.idProduct];
    delete this.clonedProducts[product.idProduct];
  }
}
