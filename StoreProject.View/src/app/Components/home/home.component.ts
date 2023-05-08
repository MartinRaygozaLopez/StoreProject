import { Component, OnInit } from '@angular/core';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  articles: ProductsInterface[] = [];
  
  constructor(
    private productsService: ProductsService) { }

  ngOnInit(): void {
  }

  // readArticles() {
  //   this.productsService.getAllArticlesByStore(1).subscribe((response) => {
  //     if (response.length > 0) {
  //       this.articles = [];
  //       this.articles = response;
  //     }
  //   });
  // }
}
