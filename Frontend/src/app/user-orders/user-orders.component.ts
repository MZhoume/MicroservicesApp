import { Component, OnInit } from '@angular/core';
import {Item} from "../Item";
import {CartService} from "../cart.service";

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.css']
})
export class UserOrdersComponent implements OnInit {

    ordersIds: number[];
    items: Item[];
    nums: number[];
    keys: string[];
    total: number;
    myParseFloat = parseFloat;

    constructor(
        private cartService: CartService,
    ) { }

    ngOnInit() {
        this.ordersIds = [1, 2, 3];
        this.getCartContent();
    }

    getCartContent() {
        this.items = [];
        this.nums = [];
        this.keys = [];
        this.cartService.getCartContent(this.items, this.nums, this.keys);
        this.total = this.cartService.getCartTotalPrice();
    }
}
