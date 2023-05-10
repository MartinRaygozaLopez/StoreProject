import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { StoreInterface } from '../Interface/StoreInterface';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(
    private http: HttpClient) { }

  getAllStore(): Observable<StoreInterface[]> {
    return this.http.get<StoreInterface[]>(environment.API + 'stores/GetAllStores');
  }

  createStore(store: StoreInterface): Observable<any> {
    return this.http.post(environment.API + 'stores/CreateStore', store);
  }

  updateStore(store: StoreInterface): Observable<any> {   
    return this.http.put(environment.API + 'stores/UpdateStore', store);
  }
}
