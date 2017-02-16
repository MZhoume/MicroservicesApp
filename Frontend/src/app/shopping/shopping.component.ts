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

    async ngOnInit() : Promise<any> {
        // get items from server
        try {
            let itemResult = await this.itemService.getItemsRemote(this.user);
            this.items = itemResult;
            console.log('get items success');
        } catch (ex) {
            console.error('An error occurred', ex);
        }

    }


    openCheckout(price:number, descr: string, id: string): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_XGmc8VOUVttNbHcEyQhodzwX',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // TODO: send to server
                this.itemService.sendTokenToServer(this.myToken, this.user.JWT, id, price);
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
