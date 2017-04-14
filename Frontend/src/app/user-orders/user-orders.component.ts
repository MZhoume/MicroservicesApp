import { Component, OnInit } from '@angular/core';
import {Item} from '../Item';
import {CartService} from '../cart.service';
import {UserService} from '../user.service';
import {Router} from '@angular/router';
import {ItemService} from "../item.service";

@Component({
  selector: 'app-user-orders',
  templateUrl: './user-orders.component.html',
  styleUrls: ['./user-orders.component.css']
})
export class UserOrdersComponent implements OnInit {

    ordersIds: number[];
    orders: any[];
    items: Item[];
    total: number;
    myParseFloat = parseFloat;
    myToken:string;

    constructor(
        private cartService: CartService,
        private userService: UserService,
        private itemService: ItemService,
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

    async getOrders() {
        let itemResult = await this.itemService.getItemsRemote();
        this.items = itemResult.Payload;

        this.ordersIds = [];
        this.orders = [];
        this.total = this.cartService.getCartTotalPrice();
        let res = await this.cartService.getOrdersFromServer(this.userService.getUser().JWT);
        console.log(res);
        for (let i = res.length-1; i >= 0 ; i--){
            this.ordersIds.push(res[i].OrderId);
            this.orders.push(res[i]);
        }
    }

    calc(order:any):number {

        let total:number = 0;
        for (let i = 0; i < order.Products.length ; i++){
            let product = order.Products[i];
            total += this.myParseFloat(this.items[product.ProductId].Price) * product.Count;
        }
        return total;
    }

    openCheckout(id: string): void{
        const handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_hPyQl7aPo9jabKR2WwAVYSWk',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // todo send to server

                this.cartService.checkoutOrder(
                    this.userService.getUser().JWT, this.userService.getUser().uid, this.myToken, this.calc(this.orders[id]) * 100, this.ordersIds[id]);

                console.log('pay end.');
                this.router.navigate(['/shopping']);
            }
        });
        handler.open({
            name: 'Pay',
            description: 'Your Order',
            amount: Number(this.calc(this.orders[id])) * 100,
        });
        console.log('pay start');
    }
}
