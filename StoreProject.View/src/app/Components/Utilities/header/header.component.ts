import { Component, OnInit } from '@angular/core';
import { ProductsInterface } from 'src/app/Interface/ProductsInterface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  articlesLocalStorage: any[] = [];

  constructor(
  ) { }

  ngOnInit(): void {
    //this.readArticles();
    this.articlesLocalStorage = JSON.parse(localStorage.getItem('cart') + '') || [];
  }



  fnloadLocalStorage() {

  }

  fnAddArticleinLocalStorage(model: any) {
    const exits = this.articlesLocalStorage.some(element => element.pkArticle === model.pkArticle);
    if (!model.count || !exits) model.count = 1;

    if (exits) {
      const article = this.articlesLocalStorage.map(element => {
        if (element.pkArticle === model.pkArticle) {
          element.count++;
          return element;
        } else {
          return element;
        }
      });
      this.articlesLocalStorage = [...article];
    } else {
      this.articlesLocalStorage = [...this.articlesLocalStorage, model];
    }
  }

  fnDeleteArticle(pkArticle: number) {
    this.articlesLocalStorage = this.articlesLocalStorage.filter(element => element.pkArticle !== pkArticle);
    // Mandar el this.articlesLocalStorage a LocalStorage
  }

  fnDeleteCart() {
    this.articlesLocalStorage = [];
    // Mandar el this.articlesLocalStorage a LocalStorage
  }

}
