import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {User} from "../User";
import {Router} from "@angular/router";
import {Item} from "../Item";
import {ItemService} from "../item.service";

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
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


    openCheckout(price:number, descr: string): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_hPyQl7aPo9jabKR2WwAVYSWk',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // TODO: send to server
                console.log('pay end.');
            }
        });

        handler.open({
            name: 'Please Pay',
            description: descr,
            amount: Number(price) * 100,
        });

        console.log('pay start');
    }
}
