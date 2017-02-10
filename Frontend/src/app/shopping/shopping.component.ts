import { Component, OnInit } from '@angular/core';
import {User} from "../User";
import {Item} from "../Item";
import {ItemService} from "../item.service";

@Component({
  selector: 'app-shopping',
  templateUrl: './shopping.component.html',
  styleUrls: ['./shopping.component.css']
})
export class ShoppingComponent implements OnInit {

    user: User;
    myToken: any;
    items: Item[];
    message: string;

    constructor(
        private itemService: ItemService,
    ) { }

    ngOnInit() {
        // get items from server
        this.itemService.getItemsRemote(this.user).then(response => {
            if (response.flag == 'success') {
                this.items = response.items;
                console.log('get items success');
            } else {
                console.log(response);
                this.message = 'get items fail';
                console.log('get items fail');
            }
        });

    }


    openCheckout(price:number, descr: string): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_XGmc8VOUVttNbHcEyQhodzwX',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // TODO: send to server
                console.log('pay end.');
            }
        });

        handler.open({
            name: 'Pay It!!!!!!!',
            description: descr,
            amount: Number(price) * 100,
        });

        console.log('pay start');
    }
}
