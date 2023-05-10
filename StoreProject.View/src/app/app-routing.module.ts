import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { ProductCatalogComponent } from './Components/product-catalog/product-catalog.component';
import { StoreCatalogComponent } from './Components/store-catalog/store-catalog.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'Home', component: HomeComponent },
  { path: 'ProductCatalog', component: ProductCatalogComponent },
  { path: 'StoreCatalog', component: StoreCatalogComponent }
  
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
