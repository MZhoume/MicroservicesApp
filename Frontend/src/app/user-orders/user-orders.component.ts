import { Component, OnInit } from '@angular/core';
import {Item} from '../Item';
import {CartService} from '../cart.service';
import {UserService} from '../user.service';
import {Router} from '@angular/router';

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
        private userService: UserService,
        private router: Router,
    ) { }

    ngOnInit() {
        // prevent unlog usr get in
        if (this.userService.getUser() === undefined){
            this.router.navigate(['/login']);
            console.log('you should not be here');
        }

        this.getOrders();
    }

    getOrders() {
        this.ordersIds = [1, 2, 3];
        this.items = [];
        this.nums = [];
        this.keys = [];
        this.cartService.getCartContent(this.items, this.nums, this.keys);
        this.total = this.cartService.getCartTotalPrice();
        this.cartService.getOrdersFromServer(this.userService.getUser().JWT);
    }
}
