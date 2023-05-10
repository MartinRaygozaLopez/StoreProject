import { Component, OnInit, ViewChild } from '@angular/core';
import { StoreInterface } from 'src/app/Interface/StoreInterface';
import { StoreService } from 'src/app/Services/store.service';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-store-catalog',
  templateUrl: './store-catalog.component.html',
  styleUrls: ['./store-catalog.component.scss'],
  providers: [MessageService]
})
export class StoreCatalogComponent implements OnInit {
  stores: StoreInterface[] = [];
  cols: any[] = [];
  clonedstores: { [s: string]: StoreInterface; } = {};

  @ViewChild(Table, { read: Table }) pTable: Table;

  constructor(
    private storeService: StoreService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.storeService.getAllStore().subscribe((response: any) => {
      this.stores = response.data;
      Object.keys(response.data[0]).filter(u => u.toLowerCase() !== 'idstore' && u.toLowerCase() !== 'productstores').forEach(key => {
        this.cols.push({ header: key.trim(), field: key.trim() });
      });
    });
  }

  openNew() {
    const p: StoreInterface = { 
      address: "",
      idStore: 0,
      IsAvailable: true,
      subsidiary: ""
    };

    this.stores.unshift(p);

    this.pTable.editingRowKeys[p[this.pTable.dataKey]] = true;
    this.onRowEditInit(p);
  }

  onRowEditInit(store: StoreInterface) {
    this.clonedstores[store.idStore] = { ...store };
  }

  onRowEditSave(store: StoreInterface) {
      if(store.idStore == 0) {
        this.storeService.createStore(store).subscribe(response => {
          delete this.clonedstores[store.idStore];
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'store is created' });
        });
      } else {
        this.storeService.updateStore(store).subscribe(response => {
          delete this.clonedstores[store.idStore];
          this.messageService.add({ severity: 'success', summary: 'Success', detail: 'store is updated' });
        });
      }
  }

  onRowEditCancel(store: StoreInterface, index: number) {
    this.stores[index] = this.clonedstores[store.idStore];
    delete this.clonedstores[store.idStore];
  }
}
