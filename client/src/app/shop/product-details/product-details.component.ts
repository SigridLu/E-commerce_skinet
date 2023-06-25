import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService) {
      // Clear up header breadcrumb before loading product.
      this.bcService.set('@productDetails', ' ')
    }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    // After successfully loading product detail, display the product name in header breadcrumb.
    // Cast the string to number by using '+'
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: product => {
          this.product = product,
          this.bcService.set('@productDetails', product.name)
        },
        error: error => console.log(error)
      });
  }

}
