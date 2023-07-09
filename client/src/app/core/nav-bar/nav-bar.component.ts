import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  constructor(public basketService: BasketService, public accountService: AccountService) {}

  // Count the total number of items in basket.
  getCount(items: BasketItem[]) {
    // Adds up the quantity of each item in the items array. 
    return items.reduce((sum, item) => sum + item.quantity, 0); // initialize sum = 0
  }
}
