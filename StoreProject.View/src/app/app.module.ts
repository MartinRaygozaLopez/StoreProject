import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
import {FormsModule} from '@angular/forms';
import {ToastModule} from 'primeng/toast';
import {InputSwitchModule} from 'primeng/inputswitch';
import {SidebarModule} from 'primeng/sidebar';
import {ConfirmPopupModule} from 'primeng/confirmpopup';
import {OverlayPanelModule} from 'primeng/overlaypanel';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Components/home/home.component';
import { HeaderComponent } from './Components/Utilities/header/header.component';
import { HttpClientModule } from '@angular/common/http';
import { ProductCatalogComponent } from './Components/product-catalog/product-catalog.component';
import { StoreCatalogComponent } from './Components/store-catalog/store-catalog.component';
import { CustomersComponent } from './Components/customers/customers.component';
import { ProductCardComponent } from './Components/Utilities/product-card/product-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    ProductCatalogComponent,
    StoreCatalogComponent,
    CustomersComponent,
    ProductCardComponent
  ],
  imports: [
    HttpClientModule,
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    TableModule,
    ConfirmPopupModule,
    ToolbarModule,
    ButtonModule,
    InputTextModule,
    ToastModule,
    OverlayPanelModule,
    SidebarModule,
    FormsModule,
    InputSwitchModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
