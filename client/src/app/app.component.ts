import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Product } from './models/product';
import { Pagination } from './models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Skinet';
  products: Product[] = [];

  // For DI
  constructor (private http: HttpClient) { }

  // Make Http request
  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('https://localhost:5001/api/products?pageSize=50').subscribe({
      next: (response: any) => this.products = response.data, // what to do next
      error: error => console.log(error), // what to do if there's an error
      complete: () => {
        console.log('request completed');
        console.log('extra statement');
      }
    });
  }
}
